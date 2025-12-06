# Práctica 4: Pila con Referencias

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Data Structure](https://img.shields.io/badge/Estructura-Pila%20(Stack)-orange?style=for-the-badge)
![Status](https://img.shields.io/badge/Estado-Completo-success?style=for-the-badge)

## Descripción

Implementación de una **pila (stack)** utilizando nodos con referencias (lista enlazada). Esta estructura de datos sigue el principio **LIFO** (Last In, First Out - Último en Entrar, Primero en Salir).

## Objetivos de Aprendizaje

- Implementar estructura de datos pila con nodos enlazados
- Comprender el principio LIFO
- Manejar memoria dinámica con referencias
- Implementar la interfaz `IPila<T>`
- Aplicar genéricos en C#

## Documentación Conceptual

📚 **Para comprender los conceptos teóricos de pilas, consulta:**
[Documentación Conceptual: Pilas](../../docs/estructuras-datos/pilas.md)

## Estructura del Proyecto

```
practica-4-pila-con-referencias/
├── IColección/
│   ├── IColección.cs      # Interfaz base para colecciones
│   ├── IPila.cs           # Interfaz específica de pila
│   └── Nodo.cs            # Nodo genérico para estructura
├── Pila/
│   └── Pila.cs            # Implementación de la pila
├── NPruebasPila/
│   └── UnitTestPila.cs    # Pruebas unitarias
├── NPruebasColección/
│   └── ...                # Pruebas de la colección base
└── README.md
```

## Conceptos: ¿Qué es una Pila?

### Definición

Una **pila** es una estructura de datos lineal donde:
- Los elementos se agregan por un extremo (tope)
- Los elementos se eliminan por el mismo extremo (tope)
- Solo se puede acceder al elemento del tope

### Analogía del Mundo Real

Imagina una **pila de platos**:
- Solo puedes agregar platos en la parte superior
- Solo puedes quitar el plato superior
- No puedes acceder a los platos del fondo sin quitar los de arriba

```
     Tope →  [Plato 3]  ← Último agregado, primero en salir
             [Plato 2]
     Base →  [Plato 1]  ← Primero agregado, último en salir
```

## Operaciones Fundamentales

### 1. Push (Apilar)

Agrega un elemento al tope de la pila.

```csharp
public void Push(T elemento)
{
    Nodo<T> nuevoNodo = new Nodo<T>(elemento);
    nuevoNodo.Siguiente = tope;
    tope = nuevoNodo;
    tamaño++;
}
```

**Complejidad:** O(1)

**Visualización:**
```
Antes del Push(5):
tope → [3] → [1] → null

Después del Push(5):
tope → [5] → [3] → [1] → null
```

### 2. Pop (Desapilar)

Elimina y retorna el elemento del tope.

```csharp
public T Pop()
{
    if (IsEmpty())
        throw new InvalidOperationException("Pila vacía");
    
    T elemento = tope.Elemento;
    tope = tope.Siguiente;
    tamaño--;
    return elemento;
}
```

**Complejidad:** O(1)

**Visualización:**
```
Antes del Pop():
tope → [5] → [3] → [1] → null

Después del Pop() (retorna 5):
tope → [3] → [1] → null
```

### 3. Peek (Ver Tope)

Retorna el elemento del tope sin eliminarlo.

```csharp
public T Peek()
{
    if (IsEmpty())
        throw new InvalidOperationException("Pila vacía");
    
    return tope.Elemento;
}
```

**Complejidad:** O(1)

### 4. IsEmpty (Está Vacía)

Verifica si la pila está vacía.

```csharp
public bool IsEmpty()
{
    return tope == null;
}
```

**Complejidad:** O(1)

## Implementación con Referencias vs Arreglo

### Con Referencias (Esta Práctica)

**Ventajas:**
- ✅ Tamaño dinámico (sin límite predefinido)
- ✅ No desperdicia memoria
- ✅ No requiere redimensionamiento

**Desventajas:**
- ❌ Overhead de memoria por punteros
- ❌ Menor localidad de caché
- ❌ Fragmentación de memoria

**Complejidad Espacial:** O(n) donde n = número de elementos actuales

### Con Arreglo

**Ventajas:**
- ✅ Mejor localidad de caché
- ✅ Menos overhead de memoria
- ✅ Acceso más rápido

**Desventajas:**
- ❌ Tamaño fijo o requiere redimensionamiento
- ❌ Puede desperdiciar memoria

**Complejidad Espacial:** O(capacidad)

## Casos de Uso

### 1. Evaluación de Expresiones

**Conversión de notación infija a postfija:**
```
Infija: (3 + 4) * 5
Postfija: 3 4 + 5 *
```

La pila mantiene operadores pendientes.

### 2. Retroceso (Backtracking)

```
Laberinto: Guardar camino recorrido
Si llegamos a callejón sin salida → Pop para retroceder
```

### 3. Historial

```
Navegador web: Botón "Atrás"
Editor de texto: Función "Deshacer"
```

### 4. Llamadas a Funciones

```
El sistema usa una pila de llamadas (call stack):
main() llama a f1()
  f1() llama a f2()
    f2() llama a f3()
      f3() termina → pop
    f2() termina → pop
  f1() termina → pop
main() continúa
```

## Compilación y Ejecución

### Requisitos Previos

- .NET SDK 6.0 o superior
- NUnit para pruebas unitarias

### Construir el Proyecto

```bash
cd practica-4-pila-con-referencias
dotnet build ed-pila-ligada-cs.sln
```

### Ejecutar Pruebas

```bash
# Probar todas las pruebas
dotnet test

# Probar solo la pila
dotnet test NPruebasPila/NPruebasPila.csproj

# Con detalles
dotnet test --verbosity detailed
```

## Ejemplo de Uso

```csharp
using IColección;

// Crear una pila de enteros
IPila<int> pila = new Pila<int>();

// Apilar elementos
pila.Push(10);
pila.Push(20);
pila.Push(30);

Console.WriteLine($"Tamaño: {pila.Count}");  // 3

// Ver el tope sin eliminar
int tope = pila.Peek();
Console.WriteLine($"Tope: {tope}");  // 30

// Desapilar elementos (LIFO)
Console.WriteLine(pila.Pop());  // 30
Console.WriteLine(pila.Pop());  // 20
Console.WriteLine(pila.Pop());  // 10

Console.WriteLine($"¿Vacía?: {pila.IsEmpty()}");  // true
```

## Interfaz Implementada

```csharp
public interface IPila<T> : IColección<T>
{
    void Push(T elemento);     // Apilar
    T Pop();                   // Desapilar
    T Peek();                  // Ver tope
}

public interface IColección<T>
{
    int Count { get; }
    bool IsEmpty();
    void Clear();
    bool Contains(T elemento);
}
```

## Complejidad de Operaciones

| Operación | Complejidad Temporal | Complejidad Espacial |
|-----------|---------------------|----------------------|
| Push | O(1) | - |
| Pop | O(1) | - |
| Peek | O(1) | - |
| IsEmpty | O(1) | - |
| Count | O(1) | - |
| Clear | O(1) | O(1) |
| Contains | O(n) | O(1) |

**Nota:** Contains requiere recorrer toda la pila, por eso es O(n).

## Manejo de Errores

### Underflow

Ocurre cuando se intenta `Pop()` o `Peek()` en una pila vacía.

```csharp
IPila<int> pila = new Pila<int>();
// pila.Pop();  // ❌ InvalidOperationException: "Pila vacía"
```

**Solución:** Siempre verificar `IsEmpty()` antes de operar:

```csharp
if (!pila.IsEmpty())
{
    int elemento = pila.Pop();
}
```

## Algoritmo Ejemplo: Balanceo de Paréntesis

```csharp
public static bool VerificarBalanceo(string expresion)
{
    IPila<char> pila = new Pila<char>();
    
    foreach (char c in expresion)
    {
        // Si es apertura, apilar
        if (c == '(' || c == '[' || c == '{')
        {
            pila.Push(c);
        }
        // Si es cierre, verificar coincidencia
        else if (c == ')' || c == ']' || c == '}')
        {
            if (pila.IsEmpty())
                return false;  // Cierre sin apertura
            
            char apertura = pila.Pop();
            if (!Coinciden(apertura, c))
                return false;  // Tipos no coinciden
        }
    }
    
    return pila.IsEmpty();  // Debe estar vacía al final
}

private static bool Coinciden(char apertura, char cierre)
{
    return (apertura == '(' && cierre == ')') ||
           (apertura == '[' && cierre == ']') ||
           (apertura == '{' && cierre == '}');
}

// Ejemplos:
VerificarBalanceo("(a + b) * [c - d]");  // true
VerificarBalanceo("(a + b * [c - d)");   // false (no coinciden)
VerificarBalanceo("(a + b))");           // false (cierre extra)
```

**Complejidad:** O(n) tiempo, O(n) espacio

## Variantes de Pila

### Pila de Mínimos

Mantiene track del mínimo actual en O(1):

```csharp
public class PilaMinimos<T> where T : IComparable<T>
{
    private IPila<T> elementos;
    private IPila<T> minimos;
    
    public void Push(T elemento)
    {
        elementos.Push(elemento);
        
        if (minimos.IsEmpty() || elemento.CompareTo(minimos.Peek()) <= 0)
            minimos.Push(elemento);
    }
    
    public T Pop()
    {
        T elemento = elementos.Pop();
        
        if (elemento.Equals(minimos.Peek()))
            minimos.Pop();
        
        return elemento;
    }
    
    public T GetMin()
    {
        return minimos.Peek();
    }
}
```

## Pruebas Incluidas

Las pruebas unitarias verifican:

- ✅ Push agrega elementos correctamente
- ✅ Pop elimina en orden LIFO
- ✅ Peek no modifica la pila
- ✅ Count refleja el tamaño correcto
- ✅ IsEmpty funciona correctamente
- ✅ Excepciones en operaciones inválidas
- ✅ Clear vacía la pila

## Próximos Pasos

Después de dominar las pilas, continúa con:

- [Práctica 5: Cola en Arreglo](../practica-5-cola-en-arreglo) - Estructura FIFO
- [Proyecto I: Intérprete Matemático](../proyecto-i-interprete-matematico) - Aplica pilas para evaluar expresiones

## Recursos Adicionales

### Documentación
- [Pilas - Teoría Completa](../../docs/estructuras-datos/pilas.md)
- [Análisis de Complejidad](../../docs/analisis/complejidad.md)

### Referencias
- [Stack<T> en C#](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1)
- [Memoria dinámica en C#](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/)

---

<div align="center">

![LIFO](https://img.shields.io/badge/Principio-LIFO-orange?style=flat-square)
![O(1)](https://img.shields.io/badge/Push%2FPop-O(1)-success?style=flat-square)
![Dynamic](https://img.shields.io/badge/Tamaño-Dinámico-blue?style=flat-square)

**Facultad de Ciencias - UNAM**

*Estructuras de Datos y Algoritmos*

</div>
