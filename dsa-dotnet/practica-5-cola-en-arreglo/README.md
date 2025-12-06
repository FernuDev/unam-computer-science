# Práctica 5: Cola en Arreglo

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Data Structure](https://img.shields.io/badge/Estructura-Cola%20(Queue)-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Estado-Completo-success?style=for-the-badge)

## Descripción

Implementación de una **cola (queue)** utilizando un arreglo circular. Esta estructura de datos sigue el principio **FIFO** (First In, First Out - Primero en Entrar, Primero en Salir).

## Objetivos de Aprendizaje

- Implementar estructura de datos cola con arreglo circular
- Comprender el principio FIFO
- Manejar índices circulares con operador módulo
- Optimizar uso de espacio con cola circular
- Implementar la interfaz `ICola<T>`

## Documentación Conceptual

📚 **Para comprender los conceptos teóricos de colas, consulta:**
[Documentación Conceptual: Colas](../../docs/estructuras-datos/colas.md)

## Estructura del Proyecto

```
practica-5-cola-en-arreglo/
├── IColección/
│   ├── IColección.cs      # Interfaz base para colecciones
│   ├── ICola.cs           # Interfaz específica de cola
│   └── ...
├── Cola/
│   └── Cola.cs            # Implementación de cola circular
├── NPruebasCola/
│   └── UnitTestCola.cs    # Pruebas unitarias
└── README.md
```

## Conceptos: ¿Qué es una Cola?

### Definición

Una **cola** es una estructura de datos lineal donde:
- Los elementos se agregan por un extremo (rear/final)
- Los elementos se eliminan por el otro extremo (front/frente)
- Solo se puede acceder al elemento del frente

### Analogía del Mundo Real

Imagina una **fila de personas** en un banco:
- Las personas se forman al final de la fila
- Se atiende a la persona del frente
- Se respeta el orden de llegada

```
Front (atender)  →  [👤] → [👤] → [👤] → [👤]  ← Rear (llegar)
                     ↓
                  Sale primero
```

## ¿Por qué Cola Circular?

### Problema con Cola Lineal

```
Arreglo de capacidad 5:
[_][_][_][_][_]
 ^front  ^rear

Enqueue(1), Enqueue(2), Enqueue(3):
[1][2][3][_][_]
 ^front    ^rear

Dequeue(), Dequeue():
[_][_][3][_][_]
       ^front ^rear

¡Espacio desperdiciado al inicio!
```

### Solución: Cola Circular

```
Arreglo circular:
[_][_][_][_][_]
 ^front/rear

Enqueue(1,2,3), Dequeue(), Dequeue(), Enqueue(4,5,6):
[5][6][3][4][_]
       ^front
 ^rear (wrap around)

¡Reutiliza el espacio del inicio!
```

## Operaciones Fundamentales

### 1. Enqueue (Encolar)

Agrega un elemento al final de la cola.

```csharp
public void Enqueue(T elemento)
{
    if (IsFull())
        Resize();
    
    arreglo[rear] = elemento;
    rear = (rear + 1) % capacidad;  // Circular
    count++;
}
```

**Complejidad:** O(1) amortizado

**Cálculo Circular:**
```
rear = (rear + 1) % capacidad

Ejemplo con capacidad=5:
rear=0 → (0+1)%5 = 1
rear=1 → (1+1)%5 = 2
rear=4 → (4+1)%5 = 0  ← Wrap around
```

### 2. Dequeue (Desencolar)

Elimina y retorna el elemento del frente.

```csharp
public T Dequeue()
{
    if (IsEmpty())
        throw new InvalidOperationException("Cola vacía");
    
    T elemento = arreglo[front];
    arreglo[front] = default(T);  // Limpiar referencia
    front = (front + 1) % capacidad;
    count--;
    return elemento;
}
```

**Complejidad:** O(1)

### 3. Front/Peek (Ver Frente)

Retorna el elemento del frente sin eliminarlo.

```csharp
public T Front()
{
    if (IsEmpty())
        throw new InvalidOperationException("Cola vacía");
    
    return arreglo[front];
}
```

**Complejidad:** O(1)

### 4. IsEmpty / IsFull

```csharp
public bool IsEmpty()
{
    return count == 0;
}

public bool IsFull()
{
    return count == capacidad;
}
```

**Complejidad:** O(1)

## Gestión de Índices Circulares

### Índices Front y Rear

```
Estado: [1][2][3][4][5]
         ^front    ^rear

Significado:
• front: índice del próximo elemento a dequeue
• rear: índice donde se hará el próximo enqueue
• count: número de elementos actuales
```

### Condiciones

```
Cola vacía:   count == 0
Cola llena:   count == capacidad

Nota: NO usar front == rear para determinar vacía/llena
      porque puede ser ambiguo en cola circular
```

### Ejemplo Completo

```
Capacidad = 5, inicialmente vacía:

Estado inicial:
[_][_][_][_][_]
 ^front/rear, count=0

Enqueue(1):
[1][_][_][_][_]
 ^front ^rear, count=1

Enqueue(2), Enqueue(3):
[1][2][3][_][_]
 ^front    ^rear, count=3

Dequeue() → retorna 1:
[_][2][3][_][_]
   ^front ^rear, count=2

Enqueue(4), Enqueue(5), Enqueue(6):
[6][2][3][4][5]
   ^front
 ^rear (wrapped), count=5, llena!

Dequeue() → retorna 2:
[6][_][3][4][5]
      ^front
 ^rear, count=4
```

## Redimensionamiento

Cuando la cola se llena, se duplica la capacidad:

```csharp
private void Resize()
{
    int nuevaCapacidad = capacidad * 2;
    T[] nuevoArreglo = new T[nuevaCapacidad];
    
    // Copiar elementos en orden
    for (int i = 0; i < count; i++)
    {
        nuevoArreglo[i] = arreglo[(front + i) % capacidad];
    }
    
    arreglo = nuevoArreglo;
    front = 0;
    rear = count;
    capacidad = nuevaCapacidad;
}
```

**Complejidad:** O(n) pero amortizada O(1)

## Casos de Uso

### 1. Sistemas de Atención

```csharp
// Simulación de banco
ICola<Cliente> fila = new Cola<Cliente>();

fila.Enqueue(new Cliente("Ana"));
fila.Enqueue(new Cliente("Bob"));
fila.Enqueue(new Cliente("Carlos"));

while (!fila.IsEmpty())
{
    Cliente actual = fila.Dequeue();
    AtenderCliente(actual);
}
```

### 2. BFS en Grafos

```csharp
public void BFS(Grafo grafo, int inicio)
{
    ICola<int> cola = new Cola<int>();
    HashSet<int> visitados = new HashSet<int>();
    
    cola.Enqueue(inicio);
    visitados.Add(inicio);
    
    while (!cola.IsEmpty())
    {
        int actual = cola.Dequeue();
        Procesar(actual);
        
        foreach (int vecino in grafo.Vecinos(actual))
        {
            if (!visitados.Contains(vecino))
            {
                cola.Enqueue(vecino);
                visitados.Add(vecino);
            }
        }
    }
}
```

### 3. Buffer de Datos

```csharp
// Streaming de datos
ICola<byte[]> buffer = new Cola<byte[]>();

// Productor agrega datos
buffer.Enqueue(LeerBloque());

// Consumidor procesa datos
byte[] datos = buffer.Dequeue();
ProcesarDatos(datos);
```

## Compilación y Ejecución

### Construir el Proyecto

```bash
cd practica-5-cola-en-arreglo
dotnet build ed-cola-en-arreglo-cs.sln
```

### Ejecutar Pruebas

```bash
# Todas las pruebas
dotnet test

# Solo pruebas de cola
dotnet test NPruebasCola/NPruebasCola.csproj

# Con detalles
dotnet test --verbosity detailed
```

## Ejemplo de Uso

```csharp
using IColección;

// Crear cola de strings con capacidad inicial 4
ICola<string> cola = new Cola<string>(4);

// Encolar elementos
cola.Enqueue("Primero");
cola.Enqueue("Segundo");
cola.Enqueue("Tercero");

Console.WriteLine($"Tamaño: {cola.Count}");  // 3

// Ver el frente sin eliminar
string frente = cola.Front();
Console.WriteLine($"Frente: {frente}");  // "Primero"

// Desencolar (FIFO)
Console.WriteLine(cola.Dequeue());  // "Primero"
Console.WriteLine(cola.Dequeue());  // "Segundo"
Console.WriteLine(cola.Dequeue());  // "Tercero"

Console.WriteLine($"¿Vacía?: {cola.IsEmpty()}");  // true
```

## Interfaz Implementada

```csharp
public interface ICola<T> : IColección<T>
{
    void Enqueue(T elemento);  // Encolar
    T Dequeue();               // Desencolar
    T Front();                 // Ver frente
}
```

## Complejidad de Operaciones

| Operación | Complejidad Temporal | Notas |
|-----------|---------------------|-------|
| Enqueue | O(1) amortizado | Puede requerir resize |
| Dequeue | O(1) | Siempre constante |
| Front | O(1) | Siempre constante |
| IsEmpty | O(1) | Siempre constante |
| IsFull | O(1) | Siempre constante |
| Count | O(1) | Mantenido como variable |

**Espacio:** O(n) donde n es la capacidad actual

## Cola vs Pila

| Aspecto | Cola (Queue) | Pila (Stack) |
|---------|-------------|--------------|
| Principio | FIFO | LIFO |
| Inserción | Rear (final) | Top (tope) |
| Eliminación | Front (frente) | Top (tope) |
| Analogía | Fila de personas | Pila de platos |
| Uso típico | Orden de llegada | Orden inverso |
| BFS | ✓ Usa cola | ✗ |
| DFS | ✗ | ✓ Usa pila |

## Manejo de Errores

### Underflow (Cola Vacía)

```csharp
ICola<int> cola = new Cola<int>();
// cola.Dequeue();  // ❌ InvalidOperationException: "Cola vacía"
```

**Solución:**
```csharp
if (!cola.IsEmpty())
{
    int elemento = cola.Dequeue();
}
```

### Overflow (Cola Llena)

En esta implementación, se maneja automáticamente con resize.

## Optimizaciones

### 1. Lazy Shrinking

No reducir capacidad inmediatamente cuando count baja:

```csharp
// Solo reducir si count < capacidad/4
if (count > 0 && count == capacidad / 4)
{
    Resize(capacidad / 2);
}
```

### 2. Capacidad Inicial

Elegir capacidad inicial apropiada al caso de uso:

```csharp
// Para muchos elementos
ICola<T> cola = new Cola<T>(1000);

// Para pocos elementos
ICola<T> cola = new Cola<T>(4);
```

## Variantes de Cola

### Cola de Prioridad

Elementos con prioridad, no FIFO estricto:

```csharp
// Generalmente implementada con heap
PriorityQueue<int> pq = new PriorityQueue<int>();
```

### Deque (Double-Ended Queue)

Permite inserción/eliminación en ambos extremos:

```csharp
// Operaciones adicionales
EnqueueFront(T elemento);
DequeueRear();
```

## Próximos Pasos

Después de dominar las colas, continúa con:

- [Práctica 6: Lista Doblemente Ligada](../practica-6-lista-doblemente-ligada)
- [Algoritmos de Grafos](../../docs/algoritmos/grafos-recorridos.md) - BFS usa colas

## Recursos Adicionales

### Documentación
- [Colas - Teoría Completa](../../docs/estructuras-datos/colas.md)
- [Análisis de Complejidad](../../docs/analisis/complejidad.md)

### Referencias
- [Queue<T> en C#](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)
- [Arreglos circulares](https://en.wikipedia.org/wiki/Circular_buffer)

---

<div align="center">

![FIFO](https://img.shields.io/badge/Principio-FIFO-blue?style=flat-square)
![O(1)](https://img.shields.io/badge/Enqueue%2FDequeue-O(1)-success?style=flat-square)
![Circular](https://img.shields.io/badge/Implementación-Circular-orange?style=flat-square)

**Facultad de Ciencias - UNAM**

*Estructuras de Datos y Algoritmos*

</div>
