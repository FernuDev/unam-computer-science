# Algoritmos de Ordenamiento

## Introducción

Los **algoritmos de ordenamiento** reorganizan elementos de una colección en un orden específico (ascendente o descendente). Son fundamentales en ciencias de la computación.

## Clasificación

### Por Estabilidad
- **Estable:** Mantiene el orden relativo de elementos iguales
- **Inestable:** No garantiza mantener el orden relativo

### Por Lugar
- **In-place:** Requiere O(1) espacio adicional
- **Not in-place:** Requiere O(n) o más espacio adicional

### Por Método
- **Comparación:** Usa comparaciones entre elementos
- **No comparación:** Usa propiedades específicas de los datos

## Algoritmos Principales

### 1. Bubble Sort (Ordenamiento Burbuja)

**Algoritmo:**
```
BubbleSort(arr):
  n = arr.length
  para i desde 0 hasta n-1:
    para j desde 0 hasta n-i-2:
      si arr[j] > arr[j+1]:
        intercambiar(arr[j], arr[j+1])
```

**Características:**
- **Complejidad temporal:** O(n²) peor y promedio, O(n) mejor
- **Complejidad espacial:** O(1)
- **Estable:** Sí
- **In-place:** Sí

**Ventajas:** Simple, detecta si ya está ordenado
**Desventajas:** Muy lento, poco práctico

---

### 2. Selection Sort (Ordenamiento por Selección)

**Algoritmo:**
```
SelectionSort(arr):
  n = arr.length
  para i desde 0 hasta n-1:
    minIdx = i
    para j desde i+1 hasta n-1:
      si arr[j] < arr[minIdx]:
        minIdx = j
    intercambiar(arr[i], arr[minIdx])
```

**Características:**
- **Complejidad temporal:** O(n²) todos los casos
- **Complejidad espacial:** O(1)
- **Estable:** No
- **In-place:** Sí

**Ventajas:** Pocos intercambios (O(n))
**Desventajas:** Siempre O(n²), no detecta orden

---

### 3. Insertion Sort (Ordenamiento por Inserción)

**Algoritmo:**
```
InsertionSort(arr):
  para i desde 1 hasta n-1:
    key = arr[i]
    j = i - 1
    mientras j >= 0 y arr[j] > key:
      arr[j+1] = arr[j]
      j = j - 1
    arr[j+1] = key
```

**Características:**
- **Complejidad temporal:** O(n²) peor, O(n) mejor
- **Complejidad espacial:** O(1)
- **Estable:** Sí
- **In-place:** Sí

**Ventajas:** Eficiente para arreglos pequeños o casi ordenados
**Desventajas:** O(n²) en promedio

**Uso:** Híbridos como TimSort usan insertion sort para subarreglos pequeños

---

### 4. Merge Sort (Ordenamiento por Mezcla)

**Algoritmo:**
```
MergeSort(arr, izq, der):
  si izq < der:
    mid = (izq + der) / 2
    MergeSort(arr, izq, mid)
    MergeSort(arr, mid+1, der)
    Merge(arr, izq, mid, der)

Merge(arr, izq, mid, der):
  // Crear arreglos temporales
  // Mezclar en orden
```

**Características:**
- **Complejidad temporal:** O(n log n) todos los casos
- **Complejidad espacial:** O(n)
- **Estable:** Sí
- **In-place:** No

**Ventajas:** Consistente O(n log n), estable
**Desventajas:** Requiere memoria adicional

**Aplicación:** Ordenamiento de listas enlazadas (mejor que quicksort)

---

### 5. Quick Sort (Ordenamiento Rápido)

**Algoritmo:**
```
QuickSort(arr, bajo, alto):
  si bajo < alto:
    pivote = Partition(arr, bajo, alto)
    QuickSort(arr, bajo, pivote-1)
    QuickSort(arr, pivote+1, alto)

Partition(arr, bajo, alto):
  pivote = arr[alto]
  i = bajo - 1
  para j desde bajo hasta alto-1:
    si arr[j] <= pivote:
      i++
      intercambiar(arr[i], arr[j])
  intercambiar(arr[i+1], arr[alto])
  retornar i+1
```

**Características:**
- **Complejidad temporal:** O(n²) peor, O(n log n) promedio
- **Complejidad espacial:** O(log n) por recursión
- **Estable:** No (implementación estándar)
- **In-place:** Sí

**Ventajas:** Muy rápido en promedio, cache-efficient
**Desventajas:** Peor caso O(n²), no estable

**Optimizaciones:**
- Randomized quicksort: pivote aleatorio
- Mediana de tres: pivote como mediana
- Híbrido: Insertion sort para subarreglos pequeños

---

### 6. Heap Sort

**Algoritmo:**
```
HeapSort(arr):
  BuildMaxHeap(arr)
  para i desde n-1 hasta 1:
    intercambiar(arr[0], arr[i])
    heapSize--
    MaxHeapify(arr, 0)
```

**Características:**
- **Complejidad temporal:** O(n log n) todos los casos
- **Complejidad espacial:** O(1)
- **Estable:** No
- **In-place:** Sí

**Ventajas:** Consistente O(n log n), in-place
**Desventajas:** No estable, constantes mayores que quicksort

---

### 7. Counting Sort (Ordenamiento por Conteo)

**Algoritmo:**
```
CountingSort(arr, k):  // k = rango de valores
  count = nuevo arreglo de tamaño k+1 inicializado en 0
  output = nuevo arreglo de tamaño n
  
  para cada elemento x en arr:
    count[x]++
  
  para i desde 1 hasta k:
    count[i] += count[i-1]  // Posiciones acumuladas
  
  para i desde n-1 hasta 0:
    output[count[arr[i]]-1] = arr[i]
    count[arr[i]]--
  
  copiar output a arr
```

**Características:**
- **Complejidad temporal:** O(n + k) donde k = rango
- **Complejidad espacial:** O(n + k)
- **Estable:** Sí
- **In-place:** No

**Ventajas:** Lineal cuando k = O(n)
**Desventajas:** Requiere conocer rango, mucha memoria si k es grande

**Uso:** Cuando valores están en rango pequeño conocido

---

### 8. Radix Sort

**Algoritmo:**
```
RadixSort(arr):
  max = encontrar máximo en arr
  para cada dígito desde menos significativo hasta más significativo:
    CountingSort(arr, basado en dígito actual)
```

**Características:**
- **Complejidad temporal:** O(d·(n + k)) donde d = dígitos
- **Complejidad espacial:** O(n + k)
- **Estable:** Sí (requiere sorting estable por dígito)
- **In-place:** No

**Ventajas:** Lineal para enteros con dígitos fijos
**Desventajas:** Requiere memoria, solo para tipos específicos

---

## Tabla Comparativa

| Algoritmo | Mejor | Promedio | Peor | Espacio | Estable | In-place |
|-----------|-------|----------|------|---------|---------|----------|
| Bubble Sort | O(n) | O(n²) | O(n²) | O(1) | Sí | Sí |
| Selection Sort | O(n²) | O(n²) | O(n²) | O(1) | No | Sí |
| Insertion Sort | O(n) | O(n²) | O(n²) | O(1) | Sí | Sí |
| Merge Sort | O(n log n) | O(n log n) | O(n log n) | O(n) | Sí | No |
| Quick Sort | O(n log n) | O(n log n) | O(n²) | O(log n) | No | Sí |
| Heap Sort | O(n log n) | O(n log n) | O(n log n) | O(1) | No | Sí |
| Counting Sort | O(n+k) | O(n+k) | O(n+k) | O(n+k) | Sí | No |
| Radix Sort | O(d(n+k)) | O(d(n+k)) | O(d(n+k)) | O(n+k) | Sí | No |

## Límite Teórico

### Ordenamiento por Comparación

**Teorema:** Cualquier algoritmo de ordenamiento basado en comparaciones requiere Ω(n log n) comparaciones en el peor caso.

**Prueba:** Árbol de decisión tiene n! hojas, altura ≥ log(n!) = Ω(n log n)

### Algoritmos Especiales

Counting Sort y Radix Sort superan este límite porque **no usan comparaciones**, sino propiedades específicas de los datos.

## Cuándo Usar Cada Algoritmo

| Escenario | Algoritmo Recomendado |
|-----------|----------------------|
| Uso general | Quick Sort (randomizado) |
| Peor caso garantizado | Merge Sort o Heap Sort |
| Estabilidad requerida | Merge Sort o Tim Sort |
| Memoria limitada | Heap Sort o Quick Sort |
| Casi ordenado | Insertion Sort |
| Arreglo pequeño (n < 10-20) | Insertion Sort |
| Valores en rango pequeño | Counting Sort |
| Enteros con k dígitos | Radix Sort |
| Lista enlazada | Merge Sort |

## Algoritmos Híbridos Modernos

### Tim Sort
- Usado en Python y Java
- Combina Merge Sort e Insertion Sort
- O(n log n) peor caso, O(n) mejor caso
- Estable

### Intro Sort
- Usado en C++ STL
- Combina Quick Sort, Heap Sort e Insertion Sort
- Empieza con quick sort
- Cambia a heap sort si la recursión es muy profunda
- Usa insertion sort para subarreglos pequeños
- O(n log n) peor caso garantizado

## Visualización de Comportamiento

```
Arreglo: [5, 2, 8, 1, 9]

Bubble Sort (comparaciones adyacentes):
[5,2,8,1,9] → [2,5,8,1,9] → [2,5,1,8,9] → [2,1,5,8,9] → [1,2,5,8,9]

Quick Sort (partición por pivote=9):
[5,2,8,1,|9|] → [5,2,8,1] [9] []
[5,2,8,|1|] → [1] [2,8,5]
[2,8,|5|] → [2] [5] [8]
Resultado: [1,2,5,8,9]

Merge Sort (divide y conquista):
[5,2,8,1,9]
[5,2,8] [1,9]
[5,2] [8] [1] [9]
[5] [2] → [2,5]
[2,5] [8] → [2,5,8]
[1] [9] → [1,9]
[2,5,8] [1,9] → [1,2,5,8,9]
```

## Referencias y Recursos

- **Práctica relacionada:** [practica-10-ordenamientos](../../dsa-dotnet/practica-10-ordenamientos)
- **Visualizaciones:** VisuAlgo, Sorting Visualizer

---

<div align="center">

![Sorting](https://img.shields.io/badge/Algoritmos-Ordenamiento-brightgreen?style=for-the-badge)
![Complexity](https://img.shields.io/badge/Mejor-O(n%20log%20n)-success?style=for-the-badge)

</div>

