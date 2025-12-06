# Práctica 0: Introducción a C#

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

## Descripción

Primera práctica introductoria al lenguaje C# y al framework .NET. Implementación de números complejos y números reales con sobrecarga de operadores y principios de programación orientada a objetos.

## Objetivos de Aprendizaje

- Familiarizarse con la sintaxis de C#
- Comprender conceptos de POO (Programación Orientada a Objetos)
- Implementar interfaces
- Sobrecarga de operadores
- Manejo de tipos de datos personalizados

## Estructura del Proyecto

```
practica-0-intro-cs/
├── Numeros/
│   ├── IComplejo.cs       # Interfaz para números complejos
│   ├── Complejo.cs        # Implementación de números complejos
│   ├── Real.cs            # Implementación de números reales
│   └── Programa.cs        # Programa principal
└── README.md
```

## Temas Abordados

### 1. Números Complejos

Un número complejo tiene la forma `a + bi` donde:
- `a` es la parte real
- `b` es la parte imaginaria
- `i` es la unidad imaginaria (i² = -1)

**Operaciones implementadas:**
- Suma
- Resta
- Multiplicación
- División
- Módulo (valor absoluto)
- Conjugado

### 2. Números Reales

Implementación de números reales con operaciones aritméticas básicas usando sobrecarga de operadores.

## Conceptos de C# Aplicados

### Interfaces
```csharp
public interface IComplejo
{
    double ParteReal { get; }
    double ParteImaginaria { get; }
    double Modulo();
    IComplejo Conjugado();
}
```

### Sobrecarga de Operadores
```csharp
public static Complejo operator +(Complejo c1, Complejo c2)
{
    return new Complejo(
        c1.ParteReal + c2.ParteReal,
        c1.ParteImaginaria + c2.ParteImaginaria
    );
}
```

### Propiedades
```csharp
public double ParteReal { get; private set; }
public double ParteImaginaria { get; private set; }
```

## Compilación y Ejecución

### Requisitos Previos

- .NET SDK 6.0 o superior
- Editor de código (Visual Studio Code, Visual Studio, Rider)

### Compilar el Proyecto

```bash
cd practica-0-intro-cs/Numeros
dotnet build
```

### Ejecutar el Programa

```bash
dotnet run
```

### Ejecutar desde la Solución

```bash
cd practica-0-intro-cs
dotnet build FernuSolutions.sln
dotnet run --project Numeros/Numeros.csproj
```

## Ejemplo de Uso

```csharp
// Crear números complejos
Complejo c1 = new Complejo(3, 4);  // 3 + 4i
Complejo c2 = new Complejo(1, 2);  // 1 + 2i

// Operaciones
Complejo suma = c1 + c2;           // 4 + 6i
Complejo resta = c1 - c2;          // 2 + 2i
Complejo producto = c1 * c2;       // -5 + 10i
Complejo cociente = c1 / c2;       // 2.2 - 0.4i

// Propiedades
double modulo = c1.Modulo();       // 5.0
Complejo conjugado = c1.Conjugado(); // 3 - 4i

Console.WriteLine($"c1 = {c1}");   // "3 + 4i"
```

## Matemática de Números Complejos

### Suma y Resta
```
(a + bi) ± (c + di) = (a ± c) + (b ± d)i
```

### Multiplicación
```
(a + bi) × (c + di) = (ac - bd) + (ad + bc)i
```

### División
```
(a + bi) / (c + di) = [(a + bi)(c - di)] / (c² + d²)
                    = [(ac + bd) + (bc - ad)i] / (c² + d²)
```

### Módulo
```
|a + bi| = √(a² + b²)
```

### Conjugado
```
conjugado(a + bi) = a - bi
```

## Recursos Adicionales

### Documentación

- [Documentación oficial de C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Sobrecarga de operadores en C#](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)
- [Interfaces en C#](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)

### Conceptos Relacionados

- **POO:** Encapsulamiento, abstracción, herencia, polimorfismo
- **Tipos de valor vs referencia:** struct vs class
- **Inmutabilidad:** Objetos que no cambian después de creación

## Notas de Implementación

### Buenas Prácticas Aplicadas

1. **Inmutabilidad:** Los números complejos son inmutables (valores no cambian)
2. **Validación:** Se validan divisiones por cero
3. **Override de ToString:** Para representación legible
4. **Override de Equals y GetHashCode:** Para comparaciones correctas

### Consideraciones de Diseño

- ¿Por qué `class` en lugar de `struct`?
- Manejo de casos especiales (división por cero, NaN, infinity)
- Precisión de punto flotante

## Siguientes Pasos

Después de completar esta práctica, estarás preparado para:
- [Práctica 1: Análisis de Complejidad](../practica-1-complejidad)
- Implementar estructuras de datos más complejas
- Aplicar conceptos de POO en problemas reales

---

<div align="center">

**Facultad de Ciencias - UNAM**

*Estructuras de Datos y Algoritmos*

</div>
