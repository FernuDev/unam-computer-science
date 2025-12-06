# Maze Solver - Backtracking Algorithm

[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=15687186&assignment_repo_type=AssignmentRepo)
![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?logo=.net)
![Avalonia](https://img.shields.io/badge/Avalonia-UI-8b44ac)
![License](https://img.shields.io/badge/license-MIT-green)

A cross-platform maze solver application that demonstrates the **backtracking algorithm** (recursive problem-solving technique) using a visual interface built with Avalonia UI framework.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Algorithm Explanation](#algorithm-explanation)
- [Project Structure](#project-structure)
- [Development](#development)
- [License](#license)

## Overview

This project is a practical implementation of the **backtracking algorithm** for solving mazes. The application generates random mazes and uses recursive backtracking to find a path from the starting position to the goal. The entire process is visualized through an interactive graphical interface.

## Features

- **Interactive Maze Generation**: Create custom-sized mazes with adjustable rows and columns
- **Visual Backtracking**: Watch the algorithm solve the maze step-by-step
- **Animated Agent**: Visual representation of the solving process with animations
- **Cross-Platform**: Runs on Windows, macOS, and Linux thanks to Avalonia UI
- **Reset Functionality**: Easily restart and try different maze configurations
- **Sprite-Based Graphics**: Clean visual representation using sprite sheets

## Technology Stack

- **Language**: C# (.NET 7.0)
- **UI Framework**: [Avalonia UI](https://avaloniaui.net/) - Cross-platform XAML-based UI framework
- **Algorithm**: Recursive Backtracking (Depth-First Search variant)
- **Graphics**: Bitmap-based sprite rendering

## Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or higher
- A compatible IDE (optional):
  - [Visual Studio 2022](https://visualstudio.microsoft.com/)
  - [Visual Studio Code](https://code.visualstudio.com/)
  - [JetBrains Rider](https://www.jetbrains.com/rider/)

## Installation

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd practica-2-retroceso
   ```

2. **Navigate to the project directory**:
   ```bash
   cd Laberinto
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

## Usage

### Running the Application

From the `Laberinto` directory, execute:

```bash
dotnet run
```

### Using the Interface

1. **Set Maze Dimensions**: 
   - Enter the desired number of rows and columns
   - Click "Generar" to create a new maze

2. **Solve the Maze**:
   - Click "Resolver" to start the backtracking algorithm
   - Watch as the agent explores paths and backtracks when necessary

3. **Reset**:
   - Click "Reiniciar" to reset the agent to the starting position

4. **View Sprites**:
   - Access the sprite test window to see available visual elements

## Algorithm Explanation

### Backtracking Algorithm

The maze solver implements a **recursive backtracking algorithm**, which works as follows:

```
ResuelveLaberinto():
  1. BASE CASE: If agent is at the goal, return true
  2. Mark current cell as visited
  3. Get all valid directions (corridors)
  4. For each direction:
     a. If cell in that direction is unvisited:
        - Move to that cell
        - Recursively call ResuelveLaberinto()
        - If solution found, return true
        - Otherwise, unmark cell (backtrack) and move back
  5. Return false (no solution from this cell)
```

### Key Characteristics

- **Depth-First Search**: Explores as far as possible along each branch
- **Systematic Exploration**: Tries all possible paths
- **Memory Efficient**: Uses the call stack for tracking paths
- **Guaranteed Solution**: Will find a solution if one exists

## Project Structure

```
practica-2-retroceso/
├── Laberinto/
│   ├── App.axaml              # Application definition
│   ├── App.axaml.cs           # Application logic
│   ├── Assets/                # Image assets and sprites
│   │   ├── icon.png
│   │   ├── sprites_laberinto.png
│   │   └── sprites_laberinto.svg
│   ├── Celda.cs               # Cell class definition
│   ├── Dirección.cs           # Direction enumeration and utilities
│   ├── Laberinto.cs           # Maze generation and management
│   ├── Laberinto.csproj       # Project configuration
│   ├── MainWindow.axaml       # Main window UI definition
│   ├── MainWindow.axaml.cs    # Main window logic and algorithm
│   ├── Program.cs             # Application entry point
│   ├── Sprites.cs             # Sprite management
│   ├── SpriteTestWindow.axaml # Sprite test window UI
│   └── SpriteTestWindow.axaml.cs
├── LICENSE
└── README.md
```

## Development

### Building the Project

```bash
cd Laberinto
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Creating a Release Build

```bash
dotnet publish -c Release -r <runtime-identifier>
```

Available runtime identifiers:
- `win-x64` - Windows 64-bit
- `linux-x64` - Linux 64-bit
- `osx-x64` - macOS Intel
- `osx-arm64` - macOS Apple Silicon

### Project Creation Reference

This project was created using:

```bash
dotnet new avalonia.app -o Laberinto -f net7.0
```

## License

This project is part of an academic assignment for Data Structures and Algorithms course at UNAM.

---

<div align="center">
  <p>Built with C# and Avalonia UI</p>
  <p>UNAM - Computer Science</p>
</div>
