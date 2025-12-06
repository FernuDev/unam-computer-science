[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=15748193&assignment_repo_type=AssignmentRepo)

# Polinomio de Direccionamiento

## Descripción

Implementación de un arreglo multidimensional utilizando la técnica de **polinomio de direccionamiento** en C#. Este proyecto demuestra cómo almacenar y acceder eficientemente a elementos de arreglos n-dimensionales mediante su representación lineal en memoria.

## Tabla de Contenidos

- [Descripción](#descripción)
- [Conceptos Fundamentales](#conceptos-fundamentales)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Requisitos](#requisitos)
- [Instalación](#instalación)
- [Uso](#uso)
- [Ejecución de Pruebas](#ejecución-de-pruebas)
- [Detalles Técnicos](#detalles-técnicos)
- [Licencia](#licencia)

## Conceptos Fundamentales

### Polinomio de Direccionamiento

El polinomio de direccionamiento es una técnica que permite mapear las coordenadas de un arreglo multidimensional a una posición única en un arreglo unidimensional. Esta técnica es fundamental en estructuras de datos y se utiliza para:

- Optimizar el uso de memoria
- Facilitar el acceso secuencial a elementos
- Implementar arreglos de n-dimensiones de manera eficiente

### Fórmula General

Para un arreglo de n dimensiones con tamaños `[d₀, d₁, d₂, ..., dₙ₋₁]` y coordenadas `[i₀, i₁, i₂, ..., iₙ₋₁]`, el índice lineal se calcula como:

```
índice = i₀ + d₀(i₁ + d₁(i₂ + d₂(...)))
```

## Estructura del Proyecto

```
practica-3-polinomio-de-direccionamiento/
│
├── Arreglos/
│   ├── ArregloPolinomio.cs      # Implementación principal del arreglo multidimensional
│   ├── IArreglo.cs               # Interfaz genérica para arreglos n-dimensionales
│   ├── IllegalSizeException.cs   # Excepción personalizada para tamaños inválidos
│   └── Arreglos.csproj           # Configuración del proyecto de biblioteca
│
├── ArreglosNPruebas/
│   ├── UnitTestArregloPolinomio.cs  # Pruebas unitarias
│   ├── Calificador.cs               # Sistema de calificación automática
│   └── ArreglosNPruebas.csproj      # Configuración del proyecto de pruebas
│
├── UsoArreglos/
│   ├── Program.cs                # Programa de ejemplo y demostración
│   └── UsoArreglos.csproj        # Configuración del proyecto ejecutable
│
└── ed-polinomio-de-direccionamiento-cs-demo.sln  # Solución de Visual Studio
```

## Requisitos

- **.NET SDK** 6.0 o superior
- **Sistema Operativo**: Windows, macOS o Linux
- **IDE recomendado**: Visual Studio Code, Visual Studio o Rider

## Instalación

1. Clone el repositorio:

```bash
git clone <url-del-repositorio>
cd practica-3-polinomio-de-direccionamiento
```

2. Restaure las dependencias:

```bash
dotnet restore
```

3. Compile el proyecto:

```bash
dotnet build
```

## Uso

### Ejecución del Programa de Ejemplo

Para ejecutar el programa de demostración que muestra el funcionamiento del arreglo multidimensional:

```bash
dotnet run --project UsoArreglos/UsoArreglos.csproj
```

### Uso en Código

```csharp
using ED.Estructuras.Lineales.Arreglos;

// Crear un arreglo 3x4x5 (3 dimensiones)
int[] dimensiones = { 3, 4, 5 };
ArregloPolinomio<int> arreglo = new ArregloPolinomio<int>(dimensiones);

// Almacenar un elemento en la posición [1, 2, 3]
int[] indices = { 1, 2, 3 };
arreglo.AlmacenarElemento(indices, 42);

// Obtener un elemento
int valor = arreglo.ObtenerElemento(indices);

// Obtener el índice lineal correspondiente
int indiceLineal = arreglo.ObtenerÍndice(indices);
```

## Ejecución de Pruebas

El proyecto incluye un conjunto completo de pruebas unitarias para validar la implementación:

```bash
dotnet test ArreglosNPruebas/ArreglosNPruebas.csproj
```

Para obtener un reporte detallado:

```bash
dotnet test ArreglosNPruebas/ArreglosNPruebas.csproj --verbosity detailed
```

## Detalles Técnicos

### Características Implementadas

- **Genérico**: Soporta cualquier tipo de dato mediante el uso de generics (`T`)
- **Dimensiones arbitrarias**: Funciona con arreglos de n dimensiones
- **Validación robusta**: Incluye validación de índices y dimensiones
- **Manejo de excepciones**: 
  - `IllegalSizeException` para dimensiones inválidas
  - `IndexOutOfRangeException` para índices fuera de rango

### Complejidad Computacional

- **Acceso a elementos**: O(n) donde n es el número de dimensiones
- **Almacenamiento**: O(n) donde n es el número de dimensiones
- **Espacio**: O(∏dᵢ) donde dᵢ son las dimensiones del arreglo

### Interfaz Pública

La clase `ArregloPolinomio<T>` implementa la interfaz `IArreglo<T>` que proporciona:

- `T ObtenerElemento(int[] índices)`: Recupera un elemento
- `void AlmacenarElemento(int[] índices, T elem)`: Almacena un elemento
- `int ObtenerÍndice(int[] índices)`: Calcula el índice lineal

## Licencia

Este proyecto está bajo los términos especificados en el archivo [LICENSE](LICENSE).

---

**Desarrollado como parte del curso de Estructuras de Datos - UNAM**
