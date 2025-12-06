# Análisis de Complejidad Algorítmica

## Introducción

El **análisis de complejidad** es el estudio de la cantidad de recursos (tiempo y espacio) que requiere un algoritmo en función del tamaño de la entrada.

## Notaciones Asintóticas

### 1. Big-O (O) - Cota Superior

**Definición:** f(n) = O(g(n)) si existen constantes c > 0 y n₀ ≥ 0 tales que:
```
f(n) ≤ c · g(n)  para todo n ≥ n₀
```

**Interpretación:** "f(n) crece a lo mucho tan rápido como g(n)"

**Uso:** Describe el **peor caso** del algoritmo

**Ejemplo:**
```
Si f(n) = 3n² + 5n + 2
Entonces f(n) = O(n²)

Porque: 3n² + 5n + 2 ≤ 4n² para n ≥ 6
```

### 2. Big-Omega (Ω) - Cota Inferior

**Definición:** f(n) = Ω(g(n)) si existen constantes c > 0 y n₀ ≥ 0 tales que:
```
f(n) ≥ c · g(n)  para todo n ≥ n₀
```

**Interpretación:** "f(n) crece al menos tan rápido como g(n)"

**Uso:** Describe el **mejor caso** del algoritmo

### 3. Big-Theta (Θ) - Cota Ajustada

**Definición:** f(n) = Θ(g(n)) si f(n) = O(g(n)) y f(n) = Ω(g(n))

**Interpretación:** "f(n) crece exactamente tan rápido como g(n)"

**Uso:** Describe el **caso promedio** cuando peor y mejor caso coinciden

### Relaciones Visuales

```
    f(n)
     |
     |        c₂·g(n)  ← Cota superior (O)
     |       /
     |      /  f(n)
     |     /  /
     |    /  /
     |   /  /
     |  /  /  c₁·g(n)  ← Cota inferior (Ω)
     | /  /
     |/  /
     +--+----------------→ n
        n₀

Si f(n) está entre ambas cotas: f(n) = Θ(g(n))
```

## Clases de Complejidad Comunes

### Ordenadas de Menor a Mayor

| Notación | Nombre | Ejemplo | Descripción |
|----------|--------|---------|-------------|
| O(1) | Constante | Acceso a arreglo | Tiempo fijo |
| O(log n) | Logarítmica | Búsqueda binaria | Divide el problema |
| O(n) | Lineal | Búsqueda lineal | Una pasada |
| O(n log n) | Lineal-logarítmica | MergeSort, HeapSort | Divide y combina |
| O(n²) | Cuadrática | BubbleSort, SelectionSort | Dos ciclos anidados |
| O(n³) | Cúbica | Multiplicación ingenua de matrices | Tres ciclos anidados |
| O(2ⁿ) | Exponencial | Subconjuntos, Fibonacci recursivo | Crece muy rápido |
| O(n!) | Factorial | Permutaciones, TSP brute force | Intratable |

### Comparación Numérica

```
n = 10:
O(1) = 1
O(log n) = 3
O(n) = 10
O(n log n) = 33
O(n²) = 100
O(2ⁿ) = 1,024
O(n!) = 3,628,800

n = 100:
O(1) = 1
O(log n) = 7
O(n) = 100
O(n log n) = 664
O(n²) = 10,000
O(2ⁿ) = 1.27 × 10³⁰
O(n!) ≈ 9.3 × 10¹⁵⁷
```

## Análisis de Código

### Reglas Básicas

1. **Operaciones Constantes:** O(1)
```
x = 5;
y = x + 3;
array[0] = 10;
```

2. **Ciclos Simples:** O(n)
```
for (int i = 0; i < n; i++) {
    // operación O(1)
}
```

3. **Ciclos Anidados:** Multiplicar complejidades
```
for (int i = 0; i < n; i++) {        // O(n)
    for (int j = 0; j < n; j++) {    // O(n)
        // operación O(1)
    }
}
// Total: O(n²)
```

4. **Ciclos Consecutivos:** Sumar complejidades
```
for (int i = 0; i < n; i++) {    // O(n)
    // ...
}
for (int j = 0; j < n; j++) {    // O(n)
    // ...
}
// Total: O(n) + O(n) = O(n)
```

5. **Statements Condicionales:** Tomar el mayor
```
if (condicion) {
    // O(n)
} else {
    // O(log n)
}
// Total: O(n)
```

### Ejemplos Detallados

**Ejemplo 1: Búsqueda Lineal**
```c#
int BusquedaLineal(int[] arr, int x) {
    for (int i = 0; i < arr.Length; i++) {  // O(n)
        if (arr[i] == x) {                   // O(1)
            return i;
        }
    }
    return -1;
}

Complejidad Temporal:
• Mejor caso: O(1) - elemento en primera posición
• Peor caso: O(n) - elemento no existe o está al final
• Caso promedio: O(n)

Complejidad Espacial: O(1) - solo variables locales
```

**Ejemplo 2: Búsqueda Binaria**
```c#
int BusquedaBinaria(int[] arr, int x) {
    int izq = 0, der = arr.Length - 1;
    
    while (izq <= der) {              // O(log n) iteraciones
        int mid = izq + (der - izq) / 2;
        
        if (arr[mid] == x)
            return mid;
        
        if (arr[mid] < x)
            izq = mid + 1;
        else
            der = mid - 1;
    }
    
    return -1;
}

Complejidad Temporal: O(log n)
• Cada iteración divide el espacio de búsqueda a la mitad
• log₂(n) iteraciones máximo

Complejidad Espacial: O(1)
```

**Ejemplo 3: Ciclos con Incremento Variable**
```c#
void Ejemplo(int n) {
    for (int i = 1; i < n; i *= 2) {  // ¿Cuántas iteraciones?
        Console.WriteLine(i);
    }
}

Análisis:
i toma valores: 1, 2, 4, 8, 16, ..., 2^k < n
2^k = n  →  k = log₂(n)

Complejidad: O(log n)
```

**Ejemplo 4: Dos Variables**
```c#
void Ejemplo(int n) {
    for (int i = 0; i < n; i++) {
        for (int j = i; j < n; j++) {
            // O(1)
        }
    }
}

Análisis:
i=0: j hace n iteraciones
i=1: j hace n-1 iteraciones
i=2: j hace n-2 iteraciones
...
i=n-1: j hace 1 iteración

Total: n + (n-1) + (n-2) + ... + 1 = n(n+1)/2

Complejidad: O(n²)
```

## Recursión y Recurrencias

### Método Maestro

Para recurrencias de la forma:
```
T(n) = a·T(n/b) + f(n)
```

Donde:
- a = número de subproblemas
- n/b = tamaño de cada subproblema
- f(n) = costo de dividir y combinar

**Tres casos:**

1. Si f(n) = O(n^(log_b(a) - ε)) para algún ε > 0:
   ```
   T(n) = Θ(n^(log_b(a)))
   ```

2. Si f(n) = Θ(n^(log_b(a))):
   ```
   T(n) = Θ(n^(log_b(a)) · log n)
   ```

3. Si f(n) = Ω(n^(log_b(a) + ε)) para algún ε > 0, y
   a·f(n/b) ≤ c·f(n) para c < 1:
   ```
   T(n) = Θ(f(n))
   ```

### Ejemplos de Aplicación

**MergeSort:**
```
T(n) = 2·T(n/2) + O(n)

a = 2, b = 2, f(n) = n
log_b(a) = log₂(2) = 1
f(n) = n = Θ(n¹)

Caso 2: T(n) = Θ(n log n)
```

**Búsqueda Binaria:**
```
T(n) = 1·T(n/2) + O(1)

a = 1, b = 2, f(n) = 1
log_b(a) = log₂(1) = 0
f(n) = 1 = Θ(n⁰)

Caso 2: T(n) = Θ(log n)
```

## Complejidad Espacial

### Definición

Cantidad de memoria adicional requerida por el algoritmo en función de n.

### Componentes

1. **Espacio de entrada:** No se cuenta (es dado)
2. **Espacio auxiliar:** Variables adicionales, stack de recursión
3. **Espacio de salida:** Depende del problema

### Ejemplos

**In-Place (O(1)):**
```c#
void BubbleSort(int[] arr) {
    // Solo usa variables: i, j, temp
    // No crea estructuras adicionales
}
Espacio: O(1)
```

**Con Arreglo Auxiliar (O(n)):**
```c#
int[] MergeSort(int[] arr) {
    // Crea arreglos auxiliares del tamaño de entrada
    int[] temp = new int[arr.Length];
    // ...
}
Espacio: O(n)
```

**Recursión (O(n)):**
```c#
int Factorial(int n) {
    if (n <= 1) return 1;
    return n * Factorial(n - 1);
}

Stack de llamadas: n niveles
Espacio: O(n)
```

## Complejidad Amortizada

### Concepto

Costo promedio por operación en una secuencia de operaciones.

**Ejemplo: ArrayList dinámica**
```
Operaciones de Add():
- Mayoría: O(1) - hay espacio
- Ocasional: O(n) - necesita redimensionar

Análisis amortizado: O(1) por operación
```

## Trade-offs Comunes

### Tiempo vs Espacio

| Estrategia | Tiempo | Espacio | Ejemplo |
|------------|--------|---------|---------|
| Memoización | Más rápido | Más memoria | Fibonacci con cache |
| Sin cache | Más lento | Menos memoria | Fibonacci recursivo |
| Tabulation | Rápido | Memoria fija | DP iterativo |

## Consejos Prácticos

1. **Constantes importan en la práctica:**
   - O(100n) puede ser peor que O(n²) para n pequeño
   - Considerar factores constantes en implementaciones reales

2. **Casos promedio vs peor caso:**
   - QuickSort: O(n²) peor, O(n log n) promedio
   - En práctica, promedio es más relevante

3. **Optimización prematura:**
   - Primero corrección, luego eficiencia
   - Medir antes de optimizar

4. **Escalabilidad:**
   - O(n) puede ser inaceptable para n muy grande
   - Considerar límites del problema

## Referencias y Recursos

- **Práctica relacionada:** [practica-1-complejidad](../../dsa-dotnet/practica-1-complejidad)
- **Libro recomendado:** Introduction to Algorithms (CLRS)

---

<div align="center">

![Complexity](https://img.shields.io/badge/Análisis-Complejidad-red?style=for-the-badge)
![Big-O](https://img.shields.io/badge/Notación-Big--O-blue?style=for-the-badge)

</div>

