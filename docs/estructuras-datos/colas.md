# Colas (Queue)

## Definición

Una **cola** es una estructura de datos lineal que sigue el principio **FIFO** (First In, First Out - Primero en Entrar, Primero en Salir). Es análoga a una fila de personas: quien llega primero es atendido primero.

## Características Principales

### Operaciones Fundamentales

| Operación | Descripción | Complejidad |
|-----------|-------------|-------------|
| `Enqueue(elemento)` | Agrega un elemento al final de la cola | O(1) |
| `Dequeue()` | Elimina y retorna el elemento del frente | O(1) |
| `Front()` / `Peek()` | Retorna el elemento del frente sin eliminarlo | O(1) |
| `IsEmpty()` | Verifica si la cola está vacía | O(1) |
| `Size()` | Retorna el número de elementos | O(1) |

### Propiedades

- **Acceso Restringido**: Solo se puede acceder al elemento del frente
- **Dos Extremos**: Inserción por el final (rear), eliminación por el frente (front)
- **Orden**: Los elementos se procesan en el mismo orden de inserción

## Implementaciones

### 1. Cola Circular con Arreglo

**Ventajas:**
- Reutiliza el espacio del arreglo eficientemente
- Operaciones O(1) sin movimiento de elementos
- Localidad de memoria

**Desventajas:**
- Tamaño fijo (requiere conocer capacidad máxima)
- Posible desperdicio de memoria

**Implementación:**
```
Estado Inicial (capacidad = 5):
[_][_][_][_][_]
 ^front
 ^rear

Enqueue(3):
[3][_][_][_][_]
 ^front
   ^rear

Enqueue(5), Enqueue(7):
[3][5][7][_][_]
 ^front
       ^rear

Dequeue():  retorna 3
[_][5][7][_][_]
   ^front
       ^rear

Enqueue(9), Enqueue(1):
[_][5][7][9][1]
   ^front
 ^rear  (wrap around)

Enqueue(4):  (circular)
[4][5][7][9][1]
   ^front
   ^rear  (cola llena)
```

**Cálculo de Índices:**
```
rear = (rear + 1) % capacidad
front = (front + 1) % capacidad
llena = (rear + 1) % capacidad == front
vacía = front == rear (con contador o flag)
```

### 2. Cola con Lista Enlazada

**Ventajas:**
- Tamaño dinámico ilimitado
- No desperdicia memoria
- Fácil implementación

**Desventajas:**
- Overhead de memoria por punteros
- Menor localidad de cache

```
Estructura:
front -> [dato|next] -> [dato|next] -> [dato|next] -> null
                                           ^
                                          rear
```

## Tipos de Colas

### 1. Cola Simple (FIFO)
La implementación básica descrita arriba.

### 2. Cola de Prioridad (Priority Queue)
- Elementos tienen prioridad asociada
- Dequeue retorna el elemento de mayor/menor prioridad
- Implementación típica: Heap binario
- **Complejidad:** Enqueue O(log n), Dequeue O(log n)

### 3. Cola Doble (Deque - Double-Ended Queue)
- Permite inserción y eliminación en ambos extremos
- Operaciones: `EnqueueFront`, `EnqueueRear`, `DequeueFront`, `DequeueRear`
- Todas las operaciones en O(1)

### 4. Cola Circular
- La implementación con arreglo circular descrita arriba
- Optimiza el uso del espacio

## Aplicaciones Comunes

### 1. Sistemas de Procesamiento

**Colas de Impresión:**
- Documentos se imprimen en orden de llegada
- FIFO garantiza justicia

**Colas de Tareas (Job Scheduling):**
- Procesos esperan su turno de ejecución
- Round-robin scheduling

### 2. Algoritmos de Grafos

**BFS (Breadth-First Search):**
```
Algoritmo BFS(grafo, inicio):
  cola = nueva Cola()
  visitados = nuevo Conjunto()
  
  cola.Enqueue(inicio)
  visitados.Add(inicio)
  
  mientras no cola.IsEmpty():
    actual = cola.Dequeue()
    procesar(actual)
    
    para cada vecino de actual:
      si vecino no en visitados:
        cola.Enqueue(vecino)
        visitados.Add(vecino)
```

### 3. Simulaciones

**Sistemas de Atención:**
- Bancos, supermercados, call centers
- Modelar tiempo de espera y atención

**Buffers:**
- Streaming de video/audio
- Comunicación entre procesos

### 4. Estructuras de Datos

**Cache LRU (Least Recently Used):**
- Combinación de cola y hash map
- Elementos usados recientemente al final

## Ejemplo Conceptual

### Operaciones Paso a Paso

```
Estado Inicial: []

Enqueue(10): [10]
             front ^ ^ rear

Enqueue(20): [10, 20]
             front ^    ^ rear

Enqueue(30): [10, 20, 30]
             front ^       ^ rear

Front():     10 (cola sigue igual)

Dequeue():   10 (cola: [20, 30])
                 front ^    ^ rear

Enqueue(40): [20, 30, 40]
             front ^       ^ rear

Dequeue():   20 (cola: [30, 40])
                 front ^    ^ rear

Dequeue():   30 (cola: [40])
                 front/rear ^

Dequeue():   40 (cola: [])

IsEmpty():   true
```

## Comparación: Cola vs Pila

| Aspecto | Cola (Queue) | Pila (Stack) |
|---------|-------------|--------------|
| Principio | FIFO | LIFO |
| Inserción | Rear (final) | Top (tope) |
| Eliminación | Front (frente) | Top (tope) |
| Uso típico | Orden de llegada | Orden inverso |
| Ejemplo real | Fila de banco | Pila de platos |

## Complejidad Resumida

### Implementación con Arreglo Circular
- **Temporal:** O(1) para todas las operaciones
- **Espacial:** O(capacidad)

### Implementación con Lista Enlazada
- **Temporal:** O(1) para todas las operaciones
- **Espacial:** O(n) donde n = número de elementos

## Cola de Prioridad

### Concepto
Cada elemento tiene una prioridad. El elemento con mayor prioridad se dequeue primero, sin importar el orden de inserción.

### Implementación con Heap
```
         [1]          (prioridad más alta)
        /   \
      [3]   [2]
     /  \
   [7]  [5]
```

### Complejidad
- **Enqueue:** O(log n)
- **Dequeue:** O(log n)
- **Peek:** O(1)

### Aplicaciones
- **Algoritmo de Dijkstra:** Caminos mínimos en grafos
- **Huffman Coding:** Compresión de datos
- **Sistemas operativos:** Scheduling de procesos
- **Simulación de eventos:** Event-driven simulation

## Consideraciones de Implementación

### Condiciones de Error

1. **Underflow:** Dequeue o Front en cola vacía
2. **Overflow:** Enqueue en cola llena (solo con arreglo fijo)

### Estrategias con Arreglo

**Problema del desperdicio:**
```
Solución ingenua (NO usar):
[_][_][5][7][9]
      ^front

Dequeue mueve todos los elementos (O(n)) ❌

Solución correcta:
Cola circular con índices móviles (O(1)) ✓
```

### Buenas Prácticas

1. **Validación:** Verificar `IsEmpty()` antes de `Dequeue()`
2. **Capacidad dinámica:** Redimensionar cuando sea necesario
3. **Invariantes:** Mantener front y rear válidos
4. **Documentación:** Especificar comportamiento en cola vacía/llena

## Ejemplo de Algoritmo: Simulación de Banco

```
Algoritmo SimularBanco(clientes, ventanillas):
  cola = nueva Cola()
  tiempoTotal = 0
  
  para cada cliente en clientes:
    cola.Enqueue(cliente)
  
  mientras no cola.IsEmpty():
    cliente = cola.Dequeue()
    ventanilla = ObtenerVentanillaLibre(ventanillas)
    
    tiempoEspera = ventanilla.HoraActual - cliente.HoraLlegada
    tiempoTotal += tiempoEspera
    
    ventanilla.Atender(cliente)
  
  retornar tiempoTotal / clientes.Count

Características:
- FIFO garantiza orden justo
- Simula tiempo de espera real
- Puede extenderse con prioridades
```

## Implementación en Diferentes Lenguajes

- **C#:** `Queue<T>` en `System.Collections.Generic`
- **Java:** `Queue<E>` interfaz, `LinkedList<E>` implementación
- **Python:** `collections.deque` (doble cola optimizada)
- **C++:** `std::queue<T>` en `<queue>`

## Variantes Avanzadas

### 1. Cola Bloqueante (Blocking Queue)
- Thread-safe para programación concurrent
- Bloquea si está vacía (en Dequeue) o llena (en Enqueue)

### 2. Cola Concurrente (Concurrent Queue)
- Lock-free para alto rendimiento
- Múltiples productores y consumidores

### 3. Deque (Double-Ended Queue)
- Generalización: pila + cola
- Operaciones en ambos extremos

## Referencias y Recursos Adicionales

- **Práctica relacionada:** [practica-5-cola-en-arreglo](../../dsa-dotnet/practica-5-cola-en-arreglo)
- **Algoritmos relacionados:** BFS en grafos, Scheduling

---

<div align="center">

![Queue Visualization](https://img.shields.io/badge/Estructura-FIFO-blue?style=for-the-badge)
![Time Complexity](https://img.shields.io/badge/Complejidad-O(1)-success?style=for-the-badge)

</div>

