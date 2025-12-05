"""Dynamics primitives for the 3D interception simulation."""
from __future__ import annotations

from dataclasses import dataclass
from typing import Callable, Dict

import numpy as np

EPS = 1e-9


def norm(vec: np.ndarray) -> float:
    """Euclidean norm with guard for very small vectors."""
    value = float(np.linalg.norm(vec))
    return max(value, EPS)


def normalize(vec: np.ndarray) -> np.ndarray:
    """Return the normalized version of vec (defaults to x-axis if null)."""
    magnitude = np.linalg.norm(vec)
    if magnitude < EPS:
        return np.array([1.0, 0.0, 0.0], dtype=float)
    return vec / magnitude


def clamp(value: float, min_value: float, max_value: float) -> float:
    return max(min_value, min(value, max_value))


def relative_distance(a: np.ndarray, b: np.ndarray) -> float:
    return float(np.linalg.norm(a - b))


def steering_acceleration(
    velocity: np.ndarray,
    desired_direction: np.ndarray,
    target_speed: float,
    response_time: float,
) -> np.ndarray:
    """Compute an acceleration that steers velocity toward desired_direction."""
    desired_dir = normalize(desired_direction)
    desired_velocity = target_speed * desired_dir
    tau = max(response_time, 0.1)
    return (desired_velocity - velocity) / tau


@dataclass
class Aircraft:
    base_speed: float
    maneuver: str = "spiral"
    maneuver_params: Dict[str, float] | None = None
    response_time: float = 5.0

    def __post_init__(self) -> None:
        self.maneuver_params = self.maneuver_params or {}
        self._maneuver_table: Dict[str, Callable[[float, np.ndarray, np.ndarray], np.ndarray]] = {
            "spiral": self._spiral_direction,
            "jinking": self._jinking_direction,
            "descend_turn": self._descending_turn_direction,
            "sinusoidal": self._sinusoidal_direction,
        }

    def acceleration(self, t: float, position: np.ndarray, velocity: np.ndarray) -> np.ndarray:
        generator = self._maneuver_table.get(self.maneuver, self._spiral_direction)
        desired_direction = generator(t, position, velocity)
        return steering_acceleration(velocity, desired_direction, self.base_speed, self.response_time)

    # --- Maneuver generators -------------------------------------------------
    def _spiral_direction(self, t: float, _pos: np.ndarray, _vel: np.ndarray) -> np.ndarray:
        turn_rate = self.maneuver_params.get("turn_rate", 0.008)
        climb_ratio = self.maneuver_params.get("climb_rate", 2.0) / max(self.base_speed, 10.0)
        angle = turn_rate * t
        direction = np.array([
            np.cos(angle),
            np.sin(angle),
            clamp(climb_ratio, 0.0, 0.4),
        ])
        return normalize(direction)

    def _jinking_direction(self, t: float, _pos: np.ndarray, _vel: np.ndarray) -> np.ndarray:
        freq = self.maneuver_params.get("jink_frequency", 0.35)
        magnitude = self.maneuver_params.get("jink_magnitude", 0.7)
        vertical = 0.15 * np.sin(0.5 * freq * t)
        direction = np.array([
            1.0,
            magnitude * np.sin(freq * t),
            vertical,
        ])
        return normalize(direction)

    def _descending_turn_direction(self, t: float, _pos: np.ndarray, _vel: np.ndarray) -> np.ndarray:
        turn_rate = self.maneuver_params.get("turn_rate", 0.01)
        angle = turn_rate * t
        descent_ratio = -0.3
        direction = np.array([
            np.cos(angle),
            np.sin(angle) * 0.3,
            descent_ratio,
        ])
        return normalize(direction)

    def _sinusoidal_direction(self, t: float, _pos: np.ndarray, _vel: np.ndarray) -> np.ndarray:
        amplitude = self.maneuver_params.get("sinusoidal_amplitude", 1_000.0)
        frequency = self.maneuver_params.get("sinusoidal_frequency", 0.02)
        lateral = amplitude * np.sin(frequency * t)
        vertical = 0.2 * np.sin(0.5 * frequency * t)
        direction = np.array([
            1.0,
            lateral / max(self.base_speed, 10.0),
            vertical,
        ])
        return normalize(direction)


@dataclass
class Missile:  # type: ignore[misc]
    cruise_speed: float
    max_speed: float
    max_accel: float
    nav_gain: float = 4.0
    speed_control_gain: float = 1.0
    warmup_duration: float = 3.5
    weave_duration: float = 10.0
    weave_frequency: float = 0.8
    loft_angle_deg: float = 20.0
    pn_blend_duration: float = 6.0
    pn_warmup_bias: float = 0.25
    terminal_distance: float = 12_000.0
    boost_duration: float = 4.0
    boost_accel: float = 25.0
    bank_accel: float = 18.0
    weave_accel: float = 14.0
    min_closing_speed: float = 120.0
    lead_time_constant: float = 5.0
    lead_response: float = 1.3
    max_lead_time: float = 35.0

    def guidance_acceleration(
        self,
        _t: float,
        missile_pos: np.ndarray,
        missile_vel: np.ndarray,
        target_pos: np.ndarray,
        target_vel: np.ndarray,
    ) -> np.ndarray:
        los = target_pos - missile_pos
        distance = norm(los)
        los_unit = los / distance
        relative_velocity = target_vel - missile_vel
        closing_speed = -np.dot(relative_velocity, los_unit)
        effective_closing = max(closing_speed, self.min_closing_speed)

        omega = np.cross(los, relative_velocity) / max(distance ** 2, EPS)
        pn_cmd = self.nav_gain * effective_closing * np.cross(omega, los_unit)

        heading = normalize(missile_vel)
        speed_error = self.cruise_speed - np.dot(missile_vel, heading)
        speed_hold = self.speed_control_gain * speed_error * heading

        lead_cmd = self._lead_acceleration(
            distance=distance,
            missile_pos=missile_pos,
            missile_vel=missile_vel,
            target_pos=target_pos,
            target_vel=target_vel,
        )

        guided_cmd = pn_cmd + speed_hold + lead_cmd

        maneuver_cmd = self._maneuver_command(_t, missile_pos, missile_vel)
        pn_weight = self._pn_weight(_t, distance)
        acceleration_cmd = pn_weight * guided_cmd + (1.0 - pn_weight) * maneuver_cmd

        magnitude = np.linalg.norm(acceleration_cmd)
        if magnitude > self.max_accel:
            acceleration_cmd = acceleration_cmd * (self.max_accel / magnitude)
        return acceleration_cmd

    def _pn_weight(self, t: float, distance: float) -> float:
        if distance <= self.terminal_distance:
            return 1.0
        warmup_bias = clamp(self.pn_warmup_bias, 0.05, 0.9)
        if t <= self.warmup_duration:
            return warmup_bias
        blend_window = max(self.pn_blend_duration, EPS)
        if t <= self.warmup_duration + blend_window:
            alpha = (t - self.warmup_duration) / blend_window
            return warmup_bias + (1.0 - warmup_bias) * alpha
        return 1.0

    def _maneuver_command(self, t: float, _pos: np.ndarray, missile_vel: np.ndarray) -> np.ndarray:
        """Generate realistic loft + weaving accelerations for a SAM."""
        up = np.array([0.0, 0.0, 1.0])
        heading = normalize(missile_vel)
        right = np.cross(heading, up)
        if np.linalg.norm(right) < EPS:
            right = np.array([1.0, 0.0, 0.0])
        right = normalize(right)

        if t < self.boost_duration:
            loft_angle = np.deg2rad(self.loft_angle_deg)
            loft_axis = normalize(np.cos(loft_angle) * heading + np.sin(loft_angle) * up)
            bank_axis = np.cross(loft_axis, heading)
            if np.linalg.norm(bank_axis) < EPS:
                bank_axis = right
            bank_axis = normalize(bank_axis)
            return self.boost_accel * loft_axis + self.bank_accel * bank_axis

        stage_time = t - self.boost_duration
        if stage_time < self.weave_duration:
            weave_phase = 2.0 * np.pi * self.weave_frequency * stage_time
            weave_axis = np.sin(weave_phase) * right + np.cos(weave_phase) * up
            weave_axis = normalize(weave_axis)
            return self.weave_accel * weave_axis

        return np.zeros(3)

    def _lead_acceleration(
        self,
        distance: float,
        missile_pos: np.ndarray,
        missile_vel: np.ndarray,
        target_pos: np.ndarray,
        target_vel: np.ndarray,
    ) -> np.ndarray:
        target_speed = np.linalg.norm(target_vel)
        missile_speed = max(np.linalg.norm(missile_vel), EPS)
        relative_speed = max(missile_speed - target_speed, 50.0)
        time_to_go = distance / relative_speed
        horizon = min(time_to_go + self.lead_time_constant, self.max_lead_time)
        lead_point = target_pos + target_vel * horizon
        desired = lead_point - missile_pos
        if np.linalg.norm(desired) < EPS:
            return np.zeros(3)
        return steering_acceleration(
            missile_vel,
            desired,
            target_speed=self.cruise_speed,
            response_time=max(self.lead_response, 0.3),
        )


def unpack_state(state: np.ndarray) -> tuple[np.ndarray, np.ndarray, np.ndarray, np.ndarray]:
    """Split the 12-element state vector into component vectors."""
    aircraft_pos = state[0:3]
    aircraft_vel = state[3:6]
    missile_pos = state[6:9]
    missile_vel = state[9:12]
    return aircraft_pos, aircraft_vel, missile_pos, missile_vel


def pack_state(
    aircraft_pos: np.ndarray,
    aircraft_vel: np.ndarray,
    missile_pos: np.ndarray,
    missile_vel: np.ndarray,
) -> np.ndarray:
    return np.concatenate([aircraft_pos, aircraft_vel, missile_pos, missile_vel])
