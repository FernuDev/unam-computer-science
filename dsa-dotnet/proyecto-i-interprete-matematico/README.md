[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-2e0aaae1b6195c2367325f4f02e2d04e9abb55f0b24a779b69b11b9e10269abc.svg)](https://classroom.github.com/online_ide?assignment_repo_id=15918548&assignment_repo_type=AssignmentRepo)

# Intérprete Matemático

> Evaluador de expresiones matemáticas con soporte para notaciones prefija, infija y postfija

## Tabla de Contenidos

- [Descripción](#descripción)
- [Características](#características)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Uso](#uso)
  - [Ejecutar el Intérprete](#ejecutar-el-intérprete)
  - [Ejecutar Pruebas](#ejecutar-pruebas)
- [Notaciones Soportadas](#notaciones-soportadas)
- [Operadores Soportados](#operadores-soportados)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Ejemplos de Uso](#ejemplos-de-uso)
- [Implementación](#implementación)
- [Construcción del Proyecto](#construcción-del-proyecto)

## Descripción

Este proyecto es un intérprete de expresiones matemáticas desarrollado en C# que permite evaluar operaciones aritméticas en tres diferentes notaciones: **prefija**, **infija** y **postfija**. La implementación utiliza estructuras de datos fundamentales como pilas (Stack) y colas (Queue) para el procesamiento eficiente de las expresiones.

El proyecto fue desarrollado como parte del curso de Estructuras de Datos en la UNAM, aplicando conceptos teóricos de estructuras de datos lineales en un contexto práctico.

## Características

- **Múltiples notaciones**: Soporte para notaciones prefija (polaca), infija (estándar) y postfija (polaca inversa)
- **Cambio dinámico**: Posibilidad de cambiar entre notaciones durante la ejecución
- **Operaciones aritméticas**: Suma, resta, multiplicación, división y módulo
- **Manejo de paréntesis**: Soporte completo para expresiones con paréntesis en notación infija
- **Validación de expresiones**: Detección de errores en expresiones malformadas
- **Interfaz de consola interactiva**: Interfaz de usuario intuitiva basada en texto
- **Suite de pruebas**: Conjunto completo de pruebas unitarias con NUnit

## Requisitos Previos

- **.NET SDK 8.0** o superior
- Sistema operativo compatible: Windows, macOS o Linux
- Editor de código recomendado: Visual Studio Code o Visual Studio

## Instalación

1. Clonar el repositorio:

```bash
git clone <url-del-repositorio>
cd proyecto-i-interprete-matematico
```

2. Restaurar las dependencias:

```bash
dotnet restore
```

3. Compilar la solución:

```bash
dotnet build
```

## Uso

### Ejecutar el Intérprete

Para iniciar el intérprete matemático interactivo:

```bash
dotnet run --project Calculadora/Calculadora.csproj
```

Una vez iniciado, el programa mostrará un menú con instrucciones. Las operaciones deben ingresarse con operadores y operandos separados por espacios.

### Ejecutar Pruebas

Para ejecutar la suite completa de pruebas unitarias:

```bash
dotnet test PruebasNotaciones/PruebasNotaciones.csproj
```

Para ejecutar las pruebas con información detallada:

```bash
dotnet test PruebasNotaciones/PruebasNotaciones.csproj --verbosity normal
```

## Notaciones Soportadas

### Notación Prefija (Polaca)

El operador precede a los operandos.

**Formato**: `operador operando1 operando2`

Para cambiar a esta notación, escriba: `:Prefija`

### Notación Infija (Estándar)

El operador se ubica entre los operandos. Requiere paréntesis para definir precedencia.

**Formato**: `operando1 operador operando2`

Para cambiar a esta notación, escriba: `:Infija`

### Notación Postfija (Polaca Inversa)

El operador sigue a los operandos.

**Formato**: `operando1 operando2 operador`

Para cambiar a esta notación, escriba: `:Postfija`

## Operadores Soportados

| Operador | Operación       | Precedencia |
|----------|-----------------|-------------|
| `+`      | Suma            | 1           |
| `-`      | Resta           | 1           |
| `*`      | Multiplicación  | 2           |
| `/`      | División        | 2           |
| `%`      | Módulo          | 2           |

Los paréntesis `()` son soportados únicamente en notación infija para controlar el orden de evaluación.

## Estructura del Proyecto

```
proyecto-i-interprete-matematico/
├── Calculadora/
│   ├── Calculadora.csproj       # Proyecto de consola principal
│   └── Programa.cs              # Interfaz de usuario y lógica de entrada
├── Notaciones/
│   ├── Notaciones.csproj        # Biblioteca de clases
│   ├── Evaluación.cs            # Lógica de evaluación de expresiones
│   └── Opciones.cs              # Enumeración de tipos de notación
├── PruebasNotaciones/
│   ├── PruebasNotaciones.csproj # Proyecto de pruebas unitarias
│   ├── PruebasCalculadora.cs    # Pruebas de la calculadora
│   ├── PruebasNotaciones.cs     # Pruebas de notaciones
│   ├── PruebasUnitarias.cs      # Pruebas unitarias adicionales
│   └── Usings.cs                # Referencias globales
├── ed-interprete-matematico-cs-demo.sln  # Solución de Visual Studio
├── Respuestas_Preguntas.pdf     # Documentación del proyecto
└── README.md                     # Este archivo
```

## Ejemplos de Uso

### Ejemplo 1: Notación Prefija

```
Digite su operacion: + 3 5
        + 3 5 = 8

Digite su operacion: * + 2 3 4
        * + 2 3 4 = 20
```

### Ejemplo 2: Notación Infija

```
Digite su operacion: :Infija
Digite su operacion: ( 3 + 5 ) * 2
        ( 3 + 5 ) * 2 = 16

Digite su operacion: 10 / ( 2 + 3 )
        10 / ( 2 + 3 ) = 2
```

### Ejemplo 3: Notación Postfija

```
Digite su operacion: :Postfija
Digite su operacion: 3 5 +
        3 5 + = 8

Digite su operacion: 2 3 + 4 *
        2 3 + 4 * = 20
```

## Implementación

### Algoritmos Principales

**Evaluación Postfija**
- Utiliza una pila para almacenar operandos
- Procesa los símbolos de izquierda a derecha
- Al encontrar un operador, extrae dos operandos de la pila, realiza la operación y apila el resultado

**Evaluación Prefija**
- Convierte la expresión prefija a postfija invirtiendo el arreglo de símbolos
- Utiliza una pila para reconstruir la expresión en notación postfija
- Evalúa la expresión resultante usando el algoritmo postfijo

**Evaluación Infija**
- Convierte la expresión infija a postfija usando el algoritmo de Shunting Yard
- Utiliza una pila para operadores y una cola para la salida
- Respeta la precedencia de operadores y maneja paréntesis
- Evalúa la expresión postfija resultante

### Estructuras de Datos

- **Stack\<T\>**: Utilizada para almacenar operadores y operandos durante la evaluación
- **Queue\<T\>**: Utilizada para generar la salida en el algoritmo de conversión infija a postfija
- **LinkedList\<T\>**: Utilizada para procesar tokens durante el análisis léxico

## Construcción del Proyecto

Si necesita recrear la estructura del proyecto desde cero, puede utilizar los siguientes comandos:

```bash
# Crear la solución
dotnet new sln -n ed-interprete-matematico-cs-demo

# Crear la biblioteca de clases
dotnet new classlib -o Notaciones
dotnet sln add Notaciones/Notaciones.csproj

# Crear la aplicación de consola
dotnet new console -o Calculadora
dotnet sln add Calculadora/Calculadora.csproj
dotnet add Calculadora/Calculadora.csproj reference Notaciones/Notaciones.csproj

# Crear el proyecto de pruebas
dotnet new nunit -o PruebasNotaciones
dotnet sln add PruebasNotaciones/PruebasNotaciones.csproj
dotnet add PruebasNotaciones/PruebasNotaciones.csproj reference Notaciones/Notaciones.csproj
dotnet add PruebasNotaciones/PruebasNotaciones.csproj reference Calculadora/Calculadora.csproj
```

## Tecnologías Utilizadas

- **Lenguaje**: C# 12
- **Framework**: .NET 8.0
- **Testing**: NUnit 4.2.2
- **Paradigma**: Programación Orientada a Objetos

## Autor

Proyecto desarrollado para el curso de Estructuras de Datos - UNAM

---

**Nota**: Este proyecto es con fines educativos. Para salir del intérprete, escriba `Salir` en cualquier momento.
