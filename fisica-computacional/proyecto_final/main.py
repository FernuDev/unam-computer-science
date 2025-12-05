"""Run the 3D interception MVP simulation."""
from __future__ import annotations

import argparse
import csv
from dataclasses import replace
from pathlib import Path
from typing import Tuple

import numpy as np

from config import (
    AircraftSettings,
    DEFAULT_AIRCRAFT,
    DEFAULT_MISSILE,
    DEFAULT_SIMULATION,
    DEFAULT_VISUALIZATION,
    MissileSettings,
    SimulationSettings,
    VisualizationSettings,
)
from dynamics import Aircraft, Missile, pack_state
from integrators import SimulationResult, simulate
from visualization import (
    animate_interception,
    plot_3d_trajectories,
    plot_acceleration_profiles,
    plot_distance_vs_time,
)

MANEUVER_CHOICES = ["spiral", "jinking", "descend_turn", "sinusoidal"]
INTEGRATOR_CHOICES = ["rk4", "euler"]


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Simulación 3D de intercepción con guiado proporcional")
    parser.add_argument("--maneuver", choices=MANEUVER_CHOICES, default=None, help="Selecciona el patrón evasivo del objetivo")
    parser.add_argument("--method", choices=INTEGRATOR_CHOICES, default=None, help="Integrador numérico")
    parser.add_argument("--dt", type=float, default=None, help="Paso de integración en segundos")
    parser.add_argument("--duration", type=float, default=None, help="Duración máxima de la simulación")
    parser.add_argument("--no-animate", action="store_true", help="Desactiva la animación interactiva")
    parser.add_argument("--no-static", action="store_true", help="Evita mostrar las gráficas estáticas")
    parser.add_argument("--save-animation", action="store_true", help="Guarda la animación en video")
    parser.add_argument("--save-plots", action="store_true", help="Guarda las gráficas estáticas")
    parser.add_argument("--export-csv", action="store_true", help="Exporta los datos de trayectoria a CSV")
    parser.add_argument("--output", type=Path, default=None, help="Directorio donde guardar resultados")
    return parser.parse_args()


def configure_settings(args: argparse.Namespace) -> tuple[SimulationSettings, AircraftSettings, MissileSettings, VisualizationSettings]:
    output_dir = args.output or DEFAULT_SIMULATION.output_dir
    sim_cfg = replace(
        DEFAULT_SIMULATION,
        dt=args.dt or DEFAULT_SIMULATION.dt,
        duration=args.duration or DEFAULT_SIMULATION.duration,
        integrator=args.method or DEFAULT_SIMULATION.integrator,
        export_csv=args.export_csv or DEFAULT_SIMULATION.export_csv,
        output_dir=output_dir,
    )

    aircraft_cfg = replace(
        DEFAULT_AIRCRAFT,
        maneuver=args.maneuver or DEFAULT_AIRCRAFT.maneuver,
    )

    missile_cfg = DEFAULT_MISSILE

    animation_path = output_dir / DEFAULT_VISUALIZATION.animation_path.name
    static_dir = output_dir / "figuras"
    vis_cfg = replace(
        DEFAULT_VISUALIZATION,
        animate=not args.no_animate,
        show_static_plots=not args.no_static,
        save_animation=args.save_animation,
        save_static_plots=args.save_plots,
        animation_path=animation_path,
        static_dir=static_dir,
    )
    return sim_cfg, aircraft_cfg, missile_cfg, vis_cfg


def build_initial_state(aircraft_cfg: AircraftSettings, missile_cfg: MissileSettings) -> np.ndarray:
    aircraft_pos = np.array(aircraft_cfg.initial_position, dtype=float)
    aircraft_vel = np.array(aircraft_cfg.initial_velocity, dtype=float)
    missile_pos = np.array(missile_cfg.initial_position, dtype=float)
    missile_vel = np.array(missile_cfg.initial_velocity, dtype=float)

    if missile_cfg.align_initial_heading:
        los = aircraft_pos - missile_pos
        los_norm = np.linalg.norm(los)
        if los_norm > 1e-6:
            heading = los / los_norm
            speed = np.linalg.norm(missile_vel)
            if speed < 1e-3:
                speed = missile_cfg.cruise_speed
            missile_vel = heading * speed

    return pack_state(aircraft_pos, aircraft_vel, missile_pos, missile_vel)


def run_visualizations(result: SimulationResult, vis_cfg: VisualizationSettings) -> None:
    tuple_paths = (
        (plot_3d_trajectories, vis_cfg.static_dir / "trayectorias.png"),
        (plot_distance_vs_time, vis_cfg.static_dir / "distancia.png"),
        (plot_acceleration_profiles, vis_cfg.static_dir / "aceleraciones.png"),
    )
    for plot_fn, path in tuple_paths:
        save_path = path if vis_cfg.save_static_plots else None
        plot_fn(result, show=vis_cfg.show_static_plots, save_path=save_path)

    needs_animation = vis_cfg.animate or vis_cfg.save_animation
    if needs_animation:
        animate_interception(
            result,
            interval_ms=vis_cfg.animation_interval_ms,
            save_path=vis_cfg.animation_path if vis_cfg.save_animation else None,
            show=vis_cfg.animate,
        )


def export_csv(result: SimulationResult, path: Path) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", newline="") as csv_file:
        writer = csv.writer(csv_file)
        header = [
            "time_s",
            "aircraft_x",
            "aircraft_y",
            "aircraft_z",
            "missile_x",
            "missile_y",
            "missile_z",
            "distance",
        ]
        writer.writerow(header)
        times = result.times[: len(result.states)]
        aircraft_path = result.states[:, 0:3]
        missile_path = result.states[:, 6:9]
        for t, aircraft_pos, missile_pos, distance in zip(times, aircraft_path, missile_path, result.distances):
            writer.writerow([
                f"{t:.3f}",
                *map(lambda v: f"{v:.3f}", aircraft_pos),
                *map(lambda v: f"{v:.3f}", missile_pos),
                f"{distance:.3f}",
            ])


def summarize(result: SimulationResult, tolerance: float) -> None:
    if result.intercept_time is not None:
        print(f"Intercepción lograda en t = {result.intercept_time:.2f} s")
    else:
        final_distance = result.distances[-1]
        print("No hubo intercepción. Distancia mínima: {:.2f} m (tolerancia {:.2f} m)".format(final_distance, tolerance))


def main() -> None:
    args = parse_args()
    sim_cfg, aircraft_cfg, missile_cfg, vis_cfg = configure_settings(args)

    initial_state = build_initial_state(aircraft_cfg, missile_cfg)
    aircraft = Aircraft(
        base_speed=aircraft_cfg.base_speed,
        maneuver=aircraft_cfg.maneuver,
        maneuver_params=dict(aircraft_cfg.maneuver_params),
    )
    missile = Missile(
        cruise_speed=missile_cfg.cruise_speed,
        max_speed=missile_cfg.max_speed,
        max_accel=missile_cfg.max_accel,
        nav_gain=missile_cfg.nav_gain,
        speed_control_gain=missile_cfg.speed_control_gain,
        warmup_duration=missile_cfg.warmup_duration,
        weave_duration=missile_cfg.weave_duration,
        weave_frequency=missile_cfg.weave_frequency,
        loft_angle_deg=missile_cfg.loft_angle_deg,
        pn_blend_duration=missile_cfg.pn_blend_duration,
        pn_warmup_bias=missile_cfg.pn_warmup_bias,
        terminal_distance=missile_cfg.terminal_distance,
        boost_duration=missile_cfg.boost_duration,
        boost_accel=missile_cfg.boost_accel,
        bank_accel=missile_cfg.bank_accel,
        weave_accel=missile_cfg.weave_accel,
        min_closing_speed=missile_cfg.min_closing_speed,
        lead_time_constant=missile_cfg.lead_time_constant,
        lead_response=missile_cfg.lead_response,
        max_lead_time=missile_cfg.max_lead_time,
    )

    result = simulate(
        aircraft=aircraft,
        missile=missile,
        initial_state=initial_state,
        dt=sim_cfg.dt,
        duration=sim_cfg.duration,
        intercept_tolerance=sim_cfg.intercept_tolerance,
        method=sim_cfg.integrator,
    )

    summarize(result, sim_cfg.intercept_tolerance)

    if sim_cfg.export_csv:
        export_path = sim_cfg.output_dir / "trayectoria.csv"
        export_csv(result, export_path)
        print(f"Datos exportados a {export_path}")

    run_visualizations(result, vis_cfg)


if __name__ == "__main__":
    main()
