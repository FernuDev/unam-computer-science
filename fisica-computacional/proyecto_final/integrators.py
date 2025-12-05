"""Time integration utilities for the interception dynamics."""
from __future__ import annotations

from dataclasses import dataclass
from typing import Callable, Dict

import numpy as np

from dynamics import Aircraft, Missile, pack_state, relative_distance, unpack_state

StateVector = np.ndarray
DerivativeFn = Callable[[float, StateVector], StateVector]


def euler_step(func: DerivativeFn, t: float, state: StateVector, dt: float) -> StateVector:
    return state + dt * func(t, state)


def rk4_step(func: DerivativeFn, t: float, state: StateVector, dt: float) -> StateVector:
    k1 = func(t, state)
    k2 = func(t + 0.5 * dt, state + 0.5 * dt * k1)
    k3 = func(t + 0.5 * dt, state + 0.5 * dt * k2)
    k4 = func(t + dt, state + dt * k3)
    return state + (dt / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4)


@dataclass
class SimulationResult:
    times: np.ndarray
    states: np.ndarray
    aircraft_acc: np.ndarray
    missile_acc: np.ndarray
    distances: np.ndarray
    intercept_time: float | None


def simulate(
    aircraft: Aircraft,
    missile: Missile,
    initial_state: StateVector,
    dt: float,
    duration: float,
    intercept_tolerance: float,
    method: str = "rk4",
) -> SimulationResult:
    method = method.lower()
    stepper = rk4_step if method == "rk4" else euler_step

    def derivatives(time: float, state_vec: StateVector) -> StateVector:
        a_pos, a_vel, m_pos, m_vel = unpack_state(state_vec)
        a_acc = aircraft.acceleration(time, a_pos, a_vel)
        m_acc = missile.guidance_acceleration(time, m_pos, m_vel, a_pos, a_vel)
        return pack_state(a_vel, a_acc, m_vel, m_acc)

    times = [0.0]
    states = [initial_state.copy()]
    aircraft_acc_history = []
    missile_acc_history = []
    distance_history = []
    intercept_time: float | None = None

    current_time = 0.0
    current_state = initial_state.copy()

    while True:
        a_pos, a_vel, m_pos, m_vel = unpack_state(current_state)
        a_acc = aircraft.acceleration(current_time, a_pos, a_vel)
        m_acc = missile.guidance_acceleration(current_time, m_pos, m_vel, a_pos, a_vel)
        aircraft_acc_history.append(a_acc)
        missile_acc_history.append(m_acc)
        distance = relative_distance(a_pos, m_pos)
        distance_history.append(distance)

        if distance <= intercept_tolerance:
            intercept_time = current_time
            break
        if current_time >= duration:
            break

        next_state = stepper(derivatives, current_time, current_state, dt)
        next_state = _enforce_constraints(next_state, missile)

        current_time = min(current_time + dt, duration)
        current_state = next_state
        times.append(current_time)
        states.append(current_state.copy())

    return SimulationResult(
        times=np.array(times),
        states=np.vstack(states),
        aircraft_acc=np.vstack(aircraft_acc_history),
        missile_acc=np.vstack(missile_acc_history),
        distances=np.array(distance_history),
        intercept_time=intercept_time,
    )


def _enforce_constraints(state: StateVector, missile: Missile) -> StateVector:
    a_pos, a_vel, m_pos, m_vel = unpack_state(state)
    speed = np.linalg.norm(m_vel)
    if speed > missile.max_speed:
        m_vel = m_vel / speed * missile.max_speed
    return pack_state(a_pos, a_vel, m_pos, m_vel)
