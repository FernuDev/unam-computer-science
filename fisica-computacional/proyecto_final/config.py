"""Configuration objects holding default parameters for the interception MVP."""
from __future__ import annotations

from dataclasses import dataclass, field
from pathlib import Path
from typing import Dict, Tuple

Vec3 = Tuple[float, float, float]


@dataclass(frozen=True)
class SimulationSettings:
    dt: float = 0.05
    duration: float = 120.0
    intercept_tolerance: float = 50.0
    integrator: str = "rk4"
    export_csv: bool = False
    output_dir: Path = Path("outputs")


@dataclass(frozen=True)
class AircraftSettings:
    initial_position: Vec3 = (20_000.0, 0.0, 7_000.0)
    initial_velocity: Vec3 = (250.0, 0.0, 0.0)
    maneuver: str = "spiral"
    base_speed: float = 250.0
    maneuver_params: Dict[str, float] = field(
        default_factory=lambda: {
            "turn_rate": 0.004,
            "climb_rate": 2.0,
            "jink_frequency": 0.35,
            "jink_magnitude": 0.6,
            "sinusoidal_amplitude": 1200.0,
            "sinusoidal_frequency": 0.015,
        }
    )


@dataclass(frozen=True)
class MissileSettings:
    initial_position: Vec3 = (0.0, -5_000.0, 2_000.0)
    initial_velocity: Vec3 = (350.0, 80.0, 90.0)
    cruise_speed: float = 520.0
    max_speed: float = 650.0
    max_accel: float = 70.0
    nav_gain: float = 4.5
    speed_control_gain: float = 1.5
    warmup_duration: float = 3.5
    weave_duration: float = 12.0
    weave_frequency: float = 0.85
    loft_angle_deg: float = 22.0
    pn_blend_duration: float = 6.0
    pn_warmup_bias: float = 0.3
    terminal_distance: float = 12_000.0
    boost_duration: float = 4.5
    boost_accel: float = 28.0
    bank_accel: float = 20.0
    weave_accel: float = 16.0
    align_initial_heading: bool = True
    min_closing_speed: float = 120.0
    lead_time_constant: float = 5.0
    lead_response: float = 1.3
    max_lead_time: float = 35.0


@dataclass(frozen=True)
class VisualizationSettings:
    animate: bool = True
    animation_interval_ms: int = 60
    save_animation: bool = False
    animation_path: Path = Path("outputs/interception.mp4")
    show_static_plots: bool = True
    save_static_plots: bool = False
    static_dir: Path = Path("outputs/figures")


DEFAULT_SIMULATION = SimulationSettings()
DEFAULT_AIRCRAFT = AircraftSettings()
DEFAULT_MISSILE = MissileSettings()
DEFAULT_VISUALIZATION = VisualizationSettings()
