"""Visualization utilities for the interception MVP."""
from __future__ import annotations

from pathlib import Path
from typing import Iterable, Tuple

import matplotlib.animation as animation
import matplotlib.pyplot as plt
import numpy as np

from integrators import SimulationResult

EPS = 1e-3


def _extract_trajectories(states: np.ndarray) -> Tuple[np.ndarray, np.ndarray]:
    aircraft_path = states[:, 0:3]
    missile_path = states[:, 6:9]
    return aircraft_path, missile_path


def plot_3d_trajectories(
    result: SimulationResult,
    show: bool = True,
    save_path: Path | None = None,
    elev: float = 25.0,
    azim: float = 35.0,
) -> None:
    aircraft_path, missile_path = _extract_trajectories(result.states)
    fig = plt.figure(figsize=(8, 6))
    ax = fig.add_subplot(111, projection="3d")

    ax.plot(
        aircraft_path[:, 0],
        aircraft_path[:, 1],
        aircraft_path[:, 2],
        label="Aeronave",
        color="tab:blue",
        linewidth=2,
    )
    ax.plot(
        missile_path[:, 0],
        missile_path[:, 1],
        missile_path[:, 2],
        label="Misil",
        color="tab:red",
        linewidth=2,
    )

    ax.set_xlabel("X [m]")
    ax.set_ylabel("Y [m]")
    ax.set_zlabel("Z [m]")
    ax.set_title("Trayectorias 3D de Intercepción")
    ax.view_init(elev=elev, azim=azim)
    _set_equal_aspect(ax, np.vstack([aircraft_path, missile_path]))
    ax.legend()

    if save_path:
        save_path.parent.mkdir(parents=True, exist_ok=True)
        fig.savefig(save_path, bbox_inches="tight", dpi=200)
    if show:
        plt.show()
    else:
        plt.close(fig)


def plot_distance_vs_time(result: SimulationResult, show: bool = True, save_path: Path | None = None) -> None:
    fig, ax = plt.subplots(figsize=(7, 4))
    ax.plot(result.times[: len(result.distances)], result.distances, color="tab:green")
    ax.set_xlabel("Tiempo [s]")
    ax.set_ylabel("Distancia relativa [m]")
    ax.set_title("Evolución de la distancia Misil-Aeronave")
    ax.grid(True, alpha=0.3)

    if save_path:
        save_path.parent.mkdir(parents=True, exist_ok=True)
        fig.savefig(save_path, bbox_inches="tight", dpi=200)
    if show:
        plt.show()
    else:
        plt.close(fig)


def plot_acceleration_profiles(result: SimulationResult, show: bool = True, save_path: Path | None = None) -> None:
    fig, ax = plt.subplots(figsize=(7, 4))
    missile_norm = np.linalg.norm(result.missile_acc, axis=1)
    aircraft_norm = np.linalg.norm(result.aircraft_acc, axis=1)
    ax.plot(result.times[: len(missile_norm)], missile_norm, label="Misil", color="tab:red")
    ax.plot(result.times[: len(aircraft_norm)], aircraft_norm, label="Aeronave", color="tab:blue")
    ax.set_xlabel("Tiempo [s]")
    ax.set_ylabel("|a| [m/s²]")
    ax.set_title("Perfiles de aceleración")
    ax.grid(True, alpha=0.3)
    ax.legend()

    if save_path:
        save_path.parent.mkdir(parents=True, exist_ok=True)
        fig.savefig(save_path, bbox_inches="tight", dpi=200)
    if show:
        plt.show()
    else:
        plt.close(fig)


def animate_interception(
    result: SimulationResult,
    interval_ms: int = 60,
    save_path: Path | None = None,
    show: bool = True,
) -> animation.FuncAnimation:
    aircraft_path, missile_path = _extract_trajectories(result.states)
    distances = result.distances

    fig = plt.figure(figsize=(8, 6))
    ax = fig.add_subplot(111, projection="3d")
    _set_equal_aspect(ax, np.vstack([aircraft_path, missile_path]))
    ax.set_xlabel("X [m]")
    ax.set_ylabel("Y [m]")
    ax.set_zlabel("Z [m]")
    ax.set_title("Animación de Intercepción 3D")

    aircraft_line, = ax.plot([], [], [], color="tab:blue", label="Aeronave")
    missile_line, = ax.plot([], [], [], color="tab:red", label="Misil")
    los_line, = ax.plot([], [], [], color="gray", linestyle="--", linewidth=1, label="LOS")
    aircraft_marker, = ax.plot([], [], [], marker="o", color="tab:blue")
    missile_marker, = ax.plot([], [], [], marker="o", color="tab:red")
    text_box = ax.text2D(0.02, 0.95, "", transform=ax.transAxes)
    ax.legend(loc="upper right")

    def update(frame: int):
        aircraft_line.set_data(aircraft_path[: frame + 1, 0], aircraft_path[: frame + 1, 1])
        aircraft_line.set_3d_properties(aircraft_path[: frame + 1, 2])
        missile_line.set_data(missile_path[: frame + 1, 0], missile_path[: frame + 1, 1])
        missile_line.set_3d_properties(missile_path[: frame + 1, 2])

        aircraft_marker.set_data(
            [aircraft_path[frame, 0]],
            [aircraft_path[frame, 1]],
        )
        aircraft_marker.set_3d_properties([aircraft_path[frame, 2]])
        missile_marker.set_data(
            [missile_path[frame, 0]],
            [missile_path[frame, 1]],
        )
        missile_marker.set_3d_properties([missile_path[frame, 2]])

        los_line.set_data([aircraft_path[frame, 0], missile_path[frame, 0]], [aircraft_path[frame, 1], missile_path[frame, 1]])
        los_line.set_3d_properties([aircraft_path[frame, 2], missile_path[frame, 2]])

        time_stamp = result.times[min(frame, len(result.times) - 1)]
        distance = distances[min(frame, len(distances) - 1)]
        text_box.set_text(f"t = {time_stamp:5.1f} s\nLOS = {distance:7.1f} m")
        return (
            aircraft_line,
            missile_line,
            aircraft_marker,
            missile_marker,
            los_line,
            text_box,
        )

    anim = animation.FuncAnimation(
        fig,
        update,
        frames=aircraft_path.shape[0],
        interval=interval_ms,
        blit=False,
    )

    if save_path:
        save_path.parent.mkdir(parents=True, exist_ok=True)
        writer = None
        output_path = save_path
        suffix = save_path.suffix.lower()
        ffmpeg_ok = animation.writers.is_available("ffmpeg")
        if suffix == ".mp4" and not ffmpeg_ok:
            output_path = save_path.with_suffix(".gif")
            writer = "pillow"
            print(
                f"[WARN] ffmpeg no disponible, guardando animación como GIF en {output_path}"
            )
        try:
            if writer:
                anim.save(output_path, writer=writer)
            else:
                anim.save(output_path)
        except Exception as exc:  # pragma: no cover - depends on writer availability
            print(f"[WARN] No se pudo guardar la animación: {exc}")
    if show:
        plt.show()
    else:
        plt.close(fig)
    return anim


def _set_equal_aspect(ax, points: np.ndarray) -> None:
    mins = points.min(axis=0)
    maxs = points.max(axis=0)
    centers = (maxs + mins) / 2.0
    radii = (maxs - mins) / 2.0
    max_radius = float(radii.max())
    if max_radius < EPS:  # pragma: no cover - degenerate case
        max_radius = 1.0
    ax.set_xlim(centers[0] - max_radius, centers[0] + max_radius)
    ax.set_ylim(centers[1] - max_radius, centers[1] + max_radius)
    ax.set_zlim(centers[2] - max_radius, centers[2] + max_radius)
