# Proyecto I: Intérprete Matemático

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Project](https://img.shields.io/badge/Tipo-Proyecto-red?style=for-the-badge)
![Status](https://img.shields.io/badge/Estado-Completo-success?style=for-the-badge)

## Descripción

Proyecto integrador que implementa un **intérprete matemático** capaz de evaluar expresiones aritméticas en tres notaciones diferentes:
- **Notación Infija** (la que usamos normalmente)
- **Notación Prefija** (notación polaca)
- **Notación Postfija** (notación polaca inversa)

Este proyecto aplica el concepto de **pilas** para evaluar expresiones de manera eficiente.

## Objetivos del Proyecto

- Implementar conversores entre notaciones
- Evaluar expresiones usando pilas
- Aplicar algoritmo Shunting Yard de Dijkstra
- Manejar precedencia de operadores
- Validar expresiones matemáticas
- Construir una calculadora completa

## Estructura del Proyecto

```
proyecto-i-interprete-matematico/
├── Notaciones/
│   ├── Infija.cs          # Evaluador de notación infija
│   ├── Prefija.cs         # Evaluador de notación prefija
│   └── Postfija.cs        # Evaluador de notación postfija
├── Calculadora/
│   └── Calculadora.cs     # Interfaz de usuario
├── PruebasNotaciones/
│   └── Tests...           # Pruebas unitarias
└── README.md
```

## Notaciones Matemáticas

### 1. Notación Infija (Infix)

Es la notación **que usamos normalmente**, donde el operador está **entre** los operandos.

```
Ejemplos:
3 + 4
5 * (2 + 3)
(1 + 2) * (3 + 4)
```

**Características:**
- ✓ Natural para humanos
- ✓ Fácil de leer
- ✗ Requiere paréntesis para precedencia
- ✗ Más compleja de evaluar

### 2. Notación Prefija (Prefix / Polish Notation)

El operador está **antes** de los operandos.

```
Conversión desde infija:
3 + 4         →  + 3 4
5 * (2 + 3)   →  * 5 + 2 3
(1 + 2) * (3 + 4)  →  * + 1 2 + 3 4
```

**Características:**
- ✓ No requiere paréntesis
- ✓ Precedencia implícita en el orden
- ✗ Menos intuitiva para humanos

### 3. Notación Postfija (Postfix / Reverse Polish Notation - RPN)

El operador está **después** de los operandos.

```
Conversión desde infija:
3 + 4         →  3 4 +
5 * (2 + 3)   →  5 2 3 + *
(1 + 2) * (3 + 4)  →  1 2 + 3 4 + *
```

**Características:**
- ✓ No requiere paréntesis
- ✓ Muy fácil de evaluar con pila
- ✓ Usada en calculadoras HP
- ✗ Menos intuitiva para humanos

## Evaluación de Expresiones

### Algoritmo: Evaluar Notación Postfija

**Más simple de evaluar**, por eso muchos compiladores convierten a postfija primero.

```
Algoritmo:
1. Crear pila vacía
2. Para cada token de izquierda a derecha:
   a. Si es número: apilar
   b. Si es operador:
      - Desapilar dos operandos (segundo, primero)
      - Calcular: primero operador segundo
      - Apilar resultado
3. El resultado final está en el tope de la pila
```

**Ejemplo paso a paso: 5 2 3 + \***

```
Token | Pila       | Acción
------|------------|------------------
5     | [5]        | Número: apilar 5
2     | [5, 2]     | Número: apilar 2
3     | [5, 2, 3]  | Número: apilar 3
+     | [5, 5]     | Operador: pop(3), pop(2), push(2+3=5)
*     | [25]       | Operador: pop(5), pop(5), push(5*5=25)

Resultado: 25
```

**Implementación:**

```csharp
public static double EvaluarPostfija(string expresion)
{
    Pila<double> pila = new Pila<double>();
    string[] tokens = expresion.Split(' ');
    
    foreach (string token in tokens)
    {
        if (EsNumero(token))
        {
            pila.Push(double.Parse(token));
        }
        else if (EsOperador(token))
        {
            double segundo = pila.Pop();  // ¡Orden importa!
            double primero = pila.Pop();
            double resultado = Operar(primero, segundo, token);
            pila.Push(resultado);
        }
    }
    
    return pila.Pop();
}
```

**Complejidad:** O(n) donde n = número de tokens

### Algoritmo: Evaluar Notación Prefija

Similar a postfija, pero recorriendo de **derecha a izquierda**.

```
Algoritmo:
1. Crear pila vacía
2. Para cada token de DERECHA a IZQUIERDA:
   a. Si es número: apilar
   b. Si es operador:
      - Desapilar dos operandos (primero, segundo)
      - Calcular: primero operador segundo
      - Apilar resultado
3. El resultado final está en el tope
```

**Ejemplo paso a paso: \* 5 + 2 3**

```
Token | Pila       | Acción (de derecha a izquierda)
------|------------|--------------------------------
3     | [3]        | Número: apilar 3
2     | [3, 2]     | Número: apilar 2
+     | [5]        | Operador: pop(2), pop(3), push(2+3=5)
5     | [5, 5]     | Número: apilar 5
*     | [25]       | Operador: pop(5), pop(5), push(5*5=25)

Resultado: 25
```

### Algoritmo: Evaluar Notación Infija

**Más complejo** porque debe manejar precedencia y paréntesis.

**Enfoque 1: Convertir a postfija y evaluar**
1. Usar algoritmo Shunting Yard para convertir infija → postfija
2. Evaluar la expresión postfija

**Enfoque 2: Evaluación directa con dos pilas**
- Pila de operandos
- Pila de operadores

## Algoritmo Shunting Yard (Dijkstra)

Convierte notación infija a postfija respetando precedencia.

```
Algoritmo:
1. Crear pila de operadores (vacía)
2. Crear cola de salida (vacía)
3. Para cada token:
   
   a. Si es número:
      → Agregar a salida
   
   b. Si es operador O:
      → Mientras haya operador en tope de pila con precedencia ≥ O:
          ├─ Pop del tope → Agregar a salida
      → Push O a pila
   
   c. Si es paréntesis izquierdo '(':
      → Push a pila
   
   d. Si es paréntesis derecho ')':
      → Mientras tope != '(':
          ├─ Pop del tope → Agregar a salida
      → Pop '(' (descartar)

4. Mientras pila no vacía:
   → Pop → Agregar a salida

5. Salida contiene expresión postfija
```

### Ejemplo: (1 + 2) \* 3

```
Token | Pila Operadores | Salida          | Acción
------|----------------|-----------------|------------------
(     | [(]            | []              | Push '('
1     | [(]            | [1]             | Número → salida
+     | [(, +]         | [1]             | Push '+'
2     | [(, +]         | [1, 2]          | Número → salida
)     | []             | [1, 2, +]       | Pop hasta '('
*     | [*]            | [1, 2, +]       | Push '*'
3     | [*]            | [1, 2, +, 3]    | Número → salida
FIN   | []             | [1, 2, +, 3, *] | Pop todo

Postfija: 1 2 + 3 *
```

**Implementación:**

```csharp
public static string InfijaAPostfija(string infija)
{
    Pila<string> operadores = new Pila<string>();
    List<string> salida = new List<string>();
    string[] tokens = Tokenizar(infija);
    
    foreach (string token in tokens)
    {
        if (EsNumero(token))
        {
            salida.Add(token);
        }
        else if (token == "(")
        {
            operadores.Push(token);
        }
        else if (token == ")")
        {
            while (operadores.Peek() != "(")
            {
                salida.Add(operadores.Pop());
            }
            operadores.Pop();  // Descartar '('
        }
        else if (EsOperador(token))
        {
            while (!operadores.IsEmpty() && 
                   Precedencia(operadores.Peek()) >= Precedencia(token))
            {
                salida.Add(operadores.Pop());
            }
            operadores.Push(token);
        }
    }
    
    while (!operadores.IsEmpty())
    {
        salida.Add(operadores.Pop());
    }
    
    return string.Join(" ", salida);
}
```

## Precedencia de Operadores

```
Precedencia (menor a mayor):
1. + -           (suma, resta)
2. * /           (multiplicación, división)
3. ^             (potencia)

Asociatividad:
• +, -, *, /  → Izquierda a derecha
• ^           → Derecha a izquierda
```

**Ejemplo de precedencia:**

```
Infija:    2 + 3 * 4
Postfija:  2 3 4 * +   (NO: 2 3 + 4 *)
Resultado: 14          (NO: 20)

Porque * tiene mayor precedencia que +
```

## Casos de Uso Reales

### Calculadoras

- **Calculadoras HP:** Usan RPN (postfija)
- **Compiladores:** Convierten a postfija para generar código
- **Intérpretes:** Python, JavaScript evalúan expresiones

### Análisis de Expresiones

```csharp
// Validar sintaxis
bool esValida = ValidarExpresion("(1 + 2) * 3");

// Simplificar expresiones
string simplificada = Simplificar("(x + 0) * 1");  // → "x"

// Evaluar con variables
double resultado = Evaluar("x + y", { x=5, y=3 });  // → 8
```

## Compilación y Ejecución

### Construir el Proyecto

```bash
cd proyecto-i-interprete-matematico
dotnet build ed-interprete-matematico-cs-demo.sln
```

### Ejecutar la Calculadora

```bash
dotnet run --project Calculadora/Calculadora.csproj
```

### Ejecutar Pruebas

```bash
dotnet test PruebasNotaciones/PruebasNotaciones.csproj
```

## Ejemplo de Uso

```csharp
using Notaciones;

// Evaluar postfija
double resultado1 = Postfija.Evaluar("3 4 +");
Console.WriteLine(resultado1);  // 7

// Evaluar prefija
double resultado2 = Prefija.Evaluar("+ 3 4");
Console.WriteLine(resultado2);  // 7

// Evaluar infija
double resultado3 = Infija.Evaluar("3 + 4");
Console.WriteLine(resultado3);  // 7

// Convertir infija a postfija
string postfija = Infija.APostfija("(1 + 2) * 3");
Console.WriteLine(postfija);  // "1 2 + 3 *"

// Evaluar expresión compleja
double resultado4 = Infija.Evaluar("(2 + 3) * (4 - 1)");
Console.WriteLine(resultado4);  // 15
```

## Operadores Soportados

| Operador | Nombre | Ejemplo | Resultado |
|----------|--------|---------|-----------|
| + | Suma | 3 + 4 | 7 |
| - | Resta | 5 - 2 | 3 |
| * | Multiplicación | 3 * 4 | 12 |
| / | División | 8 / 2 | 4 |
| ^ | Potencia | 2 ^ 3 | 8 |
| ( ) | Paréntesis | (1 + 2) * 3 | 9 |

## Validación de Expresiones

### Errores Comunes

```csharp
// División por cero
Infija.Evaluar("5 / 0");  // ❌ DivideByZeroException

// Paréntesis desbalanceados
Infija.Evaluar("(1 + 2");  // ❌ InvalidExpressionException

// Operador sin operandos
Postfija.Evaluar("3 +");  // ❌ InvalidExpressionException

// Operandos sin operador
Infija.Evaluar("3 4");  // ❌ InvalidExpressionException
```

## Complejidad del Proyecto

| Algoritmo | Complejidad Temporal | Complejidad Espacial |
|-----------|---------------------|----------------------|
| Evaluar Postfija | O(n) | O(n) |
| Evaluar Prefija | O(n) | O(n) |
| Infija → Postfija | O(n) | O(n) |
| Evaluar Infija | O(n) | O(n) |

Donde n = número de tokens en la expresión.

## Extensiones Posibles

### 1. Funciones Matemáticas

```csharp
// Soportar funciones
Evaluar("sin(3.14159)");
Evaluar("sqrt(16)");
Evaluar("log(100)");
```

### 2. Variables

```csharp
// Soportar variables
Dictionary<string, double> vars = new() { ["x"] = 5, ["y"] = 3 };
Evaluar("x + y * 2", vars);  // 11
```

### 3. Evaluación Simbólica

```csharp
// Simplificación algebraica
Simplificar("x + x");  // "2*x"
Simplificar("x * 0");  // "0"
```

## Aplicaciones en el Mundo Real

### Compiladores

```
Código fuente → Parser → Árbol sintáctico → Postfija → Código máquina
```

### Hojas de Cálculo

Excel, Google Sheets evalúan fórmulas usando técnicas similares.

### Lenguajes de Programación

Todos los lenguajes deben evaluar expresiones aritméticas correctamente.

## Recursos y Referencias

### Documentación
- [Pilas - Teoría](../../docs/estructuras-datos/pilas.md)
- [Algoritmo Shunting Yard](https://en.wikipedia.org/wiki/Shunting-yard_algorithm)

### Práctica Relacionada
- [Práctica 4: Pila con Referencias](../practica-4-pila-con-referencias)

### Lecturas Adicionales
- "Compilers: Principles, Techniques, and Tools" (Dragon Book)
- "Introduction to Algorithms" (CLRS) - Capítulo sobre pilas

---

<div align="center">

![Notations](https://img.shields.io/badge/Notaciones-Infija%20|%20Prefija%20|%20Postfija-blue?style=flat-square)
![Stack](https://img.shields.io/badge/Estructura-Pila-orange?style=flat-square)
![O(n)](https://img.shields.io/badge/Complejidad-O(n)-success?style=flat-square)

**Facultad de Ciencias - UNAM**

*Estructuras de Datos y Algoritmos - Proyecto Integrador*

</div>
