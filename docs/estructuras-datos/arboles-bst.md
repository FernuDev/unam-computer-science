# Árboles Binarios de Búsqueda (BST)

## Definición

Un **Árbol Binario de Búsqueda** (Binary Search Tree - BST) es un árbol binario que cumple con la **propiedad de orden BST**: para cada nodo, todos los valores en su subárbol izquierdo son menores que el valor del nodo, y todos los valores en su subárbol derecho son mayores.

## Propiedad Fundamental

```
Para cada nodo n:
  • Todos los valores en subárbol izquierdo < valor(n)
  • Todos los valores en subárbol derecho > valor(n)
  • Ambos subárboles son BST recursivamente
```

**Ejemplo visual:**
```
        8
       / \
      3   10
     / \    \
    1   6    14
       / \   /
      4   7 13

Propiedad verificada:
• Izquierda de 8: {1,3,4,6,7} < 8
• Derecha de 8: {10,13,14} > 8
• Se cumple recursivamente en cada nodo
```

## Operaciones Fundamentales

### 1. Búsqueda (Search)

**Algoritmo:**
```
Buscar(nodo, valor):
  si nodo es null:
    retornar null  // No encontrado
  
  si valor == nodo.valor:
    retornar nodo  // Encontrado
  
  si valor < nodo.valor:
    retornar Buscar(nodo.izquierdo, valor)
  else:
    retornar Buscar(nodo.derecho, valor)
```

**Complejidad:**
- Mejor caso: O(log n) - árbol balanceado
- Peor caso: O(n) - árbol degenerado (lista enlazada)
- Caso promedio: O(log n)

### 2. Inserción (Insert)

**Algoritmo:**
```
Insertar(nodo, valor):
  si nodo es null:
    retornar nuevo Nodo(valor)
  
  si valor < nodo.valor:
    nodo.izquierdo = Insertar(nodo.izquierdo, valor)
  else si valor > nodo.valor:
    nodo.derecho = Insertar(nodo.derecho, valor)
  // Si valor == nodo.valor, no insertar duplicados
  
  retornar nodo
```

**Ejemplo paso a paso:**
```
Insertar: 5, 3, 7, 1, 9

Paso 1: Insertar 5
    5

Paso 2: Insertar 3 (< 5, va a izquierda)
    5
   /
  3

Paso 3: Insertar 7 (> 5, va a derecha)
    5
   / \
  3   7

Paso 4: Insertar 1 (< 5, < 3)
    5
   / \
  3   7
 /
1

Paso 5: Insertar 9 (> 5, > 7)
    5
   / \
  3   7
 /     \
1       9
```

**Complejidad:** Igual que búsqueda

### 3. Eliminación (Delete)

**Casos:**

**Caso 1: Nodo hoja (sin hijos)**
```
Eliminar 1:
    5               5
   / \             / \
  3   7    =>     3   7
 /     \               \
1       9               9

Solución: Simplemente eliminarlo
```

**Caso 2: Nodo con un hijo**
```
Eliminar 7:
    5               5
   / \             / \
  3   7    =>     3   9
 /     \         /
1       9       1

Solución: Reemplazar con su único hijo
```

**Caso 3: Nodo con dos hijos**
```
Eliminar 5:
    5               6
   / \             / \
  3   7    =>     3   7
 /   / \         /     \
1   6   9       1       9

Solución: Reemplazar con sucesor (mínimo del derecho)
o predecesor (máximo del izquierdo)
```

**Algoritmo:**
```
Eliminar(nodo, valor):
  si nodo es null:
    retornar null
  
  si valor < nodo.valor:
    nodo.izquierdo = Eliminar(nodo.izquierdo, valor)
  else si valor > nodo.valor:
    nodo.derecho = Eliminar(nodo.derecho, valor)
  else:
    // Nodo encontrado
    
    // Caso 1 y 2: Cero o un hijo
    si nodo.izquierdo es null:
      retornar nodo.derecho
    si nodo.derecho es null:
      retornar nodo.izquierdo
    
    // Caso 3: Dos hijos
    sucesor = EncontrarMinimo(nodo.derecho)
    nodo.valor = sucesor.valor
    nodo.derecho = Eliminar(nodo.derecho, sucesor.valor)
  
  retornar nodo

EncontrarMinimo(nodo):
  mientras nodo.izquierdo no es null:
    nodo = nodo.izquierdo
  retornar nodo
```

**Complejidad:** O(h) donde h es la altura

## Recorridos

### 1. In-Order (Izquierda-Raíz-Derecha)

**Propiedad importante:** In-order en BST produce elementos ordenados

```
        5
       / \
      3   7
     /   / \
    1   6   9

In-order: 1, 3, 5, 6, 7, 9  (¡Ordenado!)
```

**Algoritmo:**
```
InOrder(nodo):
  si nodo no es null:
    InOrder(nodo.izquierdo)
    Visitar(nodo)
    InOrder(nodo.derecho)
```

### 2. Pre-Order (Raíz-Izquierda-Derecha)

```
Pre-order: 5, 3, 1, 7, 6, 9
Útil para copiar el árbol
```

### 3. Post-Order (Izquierda-Derecha-Raíz)

```
Post-order: 1, 3, 6, 9, 7, 5
Útil para eliminar el árbol
```

### 4. Level-Order (Por niveles)

```
Level-order: 5, 3, 7, 1, 6, 9
Usa una cola para BFS
```

## Operaciones Adicionales

### Mínimo y Máximo

```
Mínimo(nodo):
  mientras nodo.izquierdo no es null:
    nodo = nodo.izquierdo
  retornar nodo
  
Complejidad: O(h)

Máximo(nodo):
  mientras nodo.derecho no es null:
    nodo = nodo.derecho
  retornar nodo
  
Complejidad: O(h)
```

### Sucesor y Predecesor

**Sucesor (siguiente mayor):**
```
Sucesor(nodo):
  si nodo.derecho no es null:
    retornar Mínimo(nodo.derecho)
  
  // Buscar ancestro donde nodo está en subárbol izquierdo
  ancestro = nodo.padre
  mientras ancestro no es null y nodo == ancestro.derecho:
    nodo = ancestro
    ancestro = ancestro.padre
  
  retornar ancestro
```

### Validación

```
EsBST(nodo, min, max):
  si nodo es null:
    retornar true
  
  si nodo.valor <= min o nodo.valor >= max:
    retornar false
  
  retornar EsBST(nodo.izquierdo, min, nodo.valor) y
         EsBST(nodo.derecho, nodo.valor, max)

Uso inicial: EsBST(raíz, -∞, +∞)
```

## Complejidad Resumida

| Operación | Mejor Caso | Caso Promedio | Peor Caso |
|-----------|-----------|---------------|-----------|
| Búsqueda | O(log n) | O(log n) | O(n) |
| Inserción | O(log n) | O(log n) | O(n) |
| Eliminación | O(log n) | O(log n) | O(n) |
| Mínimo/Máximo | O(log n) | O(log n) | O(n) |

**Nota:** El peor caso ocurre con árboles degenerados.

## Ventajas del BST

1. **Búsqueda eficiente:** O(log n) en promedio
2. **Inserción/eliminación dinámica:** Sin reorganización masiva
3. **Orden natural:** In-order produce secuencia ordenada
4. **Rango de búsqueda:** Fácil buscar valores en un rango
5. **Operaciones de conjunto:** Intersección, unión eficientes

## Desventajas del BST

1. **Degeneración:** Puede convertirse en lista (O(n))
2. **Balance no garantizado:** Depende del orden de inserción
3. **Rendimiento variable:** Peor caso puede ser malo

**Ejemplo de degeneración:**
```
Insertar en orden: 1, 2, 3, 4, 5

Resultado (lista enlazada):
1
 \
  2
   \
    3
     \
      4
       \
        5

Altura = n (muy ineficiente)
```

## Solución: Árboles Auto-balanceados

Para evitar degeneración, usar variantes balanceadas:
- **AVL:** Balance estricto mediante rotaciones
- **Red-Black:** Balance menos estricto, mejor para inserciones
- **B-Trees:** Para bases de datos y sistemas de archivos

## Aplicaciones

### 1. Estructuras de Datos de Conjuntos

```c#
// C# usa Red-Black Tree internamente
SortedSet<int> conjunto = new SortedSet<int>();
conjunto.Add(5);
conjunto.Contains(5);  // O(log n)
```

### 2. Bases de Datos

- Índices de bases de datos (variante: B-Tree)
- Búsqueda eficiente de registros

### 3. Sistemas de Archivos

- Organización de directorios
- Búsqueda de archivos

### 4. Autocompletado

- Diccionarios y búsqueda de palabras
- Sugerencias en tiempo real

## Comparación con Otras Estructuras

| Aspecto | BST | Arreglo Ordenado | Hash Table |
|---------|-----|------------------|------------|
| Búsqueda | O(log n) | O(log n) | O(1)* |
| Inserción | O(log n) | O(n) | O(1)* |
| Eliminación | O(log n) | O(n) | O(1)* |
| Orden | Sí | Sí | No |
| Rango | O(k + log n) | O(k + log n) | No |
| Memoria | O(n) | O(n) | O(n) |

\* Promedio, colisiones pueden degradar

## Ejercicios Conceptuales

1. **¿Es BST válido?**
```
    5
   / \
  3   7
 / \
1   6

Respuesta: NO (6 > 5, no puede estar en subárbol izquierdo)
```

2. **Altura de BST perfecto con n nodos:**
```
Altura = log₂(n+1) - 1
Ejemplo: n=7 → altura=2
```

3. **Número de BST únicos con n nodos:**
```
Número de Catalan = C(n) = (2n)! / ((n+1)! * n!)
Ejemplo: n=3 → 5 BST diferentes
```

## Referencias y Recursos

- **Práctica relacionada:** [practica-7-arbol-binario-ordenado](../../dsa-dotnet/practica-7-arbol-binario-ordenado)
- **Extensiones:** [Árboles AVL](arboles-avl.md), [Árboles Rojinegro](arboles-rojinegro.md)

---

<div align="center">

![BST](https://img.shields.io/badge/Estructura-BST-purple?style=for-the-badge)
![Time Complexity](https://img.shields.io/badge/Búsqueda-O(log%20n)-success?style=for-the-badge)
![Balance](https://img.shields.io/badge/Balance-No%20garantizado-orange?style=for-the-badge)

</div>

