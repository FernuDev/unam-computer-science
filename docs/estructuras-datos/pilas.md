# Pilas (Stack)

## Definición

Una **pila** es una estructura de datos lineal que sigue el principio **LIFO** (Last In, First Out - Último en Entrar, Primero en Salir). Es análoga a una pila de platos: solo podemos agregar o quitar elementos desde la parte superior.

## Características Principales

### Operaciones Fundamentales

| Operación | Descripción | Complejidad |
|-----------|-------------|-------------|
| `Push(elemento)` | Agrega un elemento al tope de la pila | O(1) |
| `Pop()` | Elimina y retorna el elemento del tope | O(1) |
| `Peek()` / `Top()` | Retorna el elemento del tope sin eliminarlo | O(1) |
| `IsEmpty()` | Verifica si la pila está vacía | O(1) |
| `Size()` | Retorna el número de elementos | O(1) |

### Propiedades

- **Acceso Restringido**: Solo se puede acceder al elemento del tope
- **Inserción/Eliminación**: Ambas operaciones ocurren en el mismo extremo (tope)
- **Orden**: Los elementos se procesan en orden inverso a su inserción

## Implementaciones

### 1. Pila con Arreglo

**Ventajas:**
- Acceso rápido por índice
- Localidad de memoria (cache-friendly)
- Tamaño compacto en memoria

**Desventajas:**
- Tamaño fijo (o requiere redimensionamiento costoso)
- Desperdicio de memoria si no se usa toda la capacidad

**Complejidad Espacial:** O(n) donde n es la capacidad máxima

```
Estructura:
[elemento_0][elemento_1][elemento_2][...][elemento_n-1]
                                           ^
                                         tope
```

### 2. Pila con Referencias (Lista Enlazada)

**Ventajas:**
- Tamaño dinámico (sin límite predefinido)
- No desperdicia memoria
- No requiere redimensionamiento

**Desventajas:**
- Overhead de memoria por los punteros
- Menor localidad de memoria
- Requiere gestión de memoria dinámica

**Complejidad Espacial:** O(n) donde n es el número de elementos actuales

```
Estructura:
tope -> [dato|next] -> [dato|next] -> [dato|next] -> null
```

## Aplicaciones Comunes

### 1. Evaluación de Expresiones

**Notación Infija a Postfija:**
```
Infija: (3 + 4) * 5
Postfija: 3 4 + 5 *
```

La pila se usa para almacenar operadores y mantener la precedencia.

### 2. Retroceso (Backtracking)

Algoritmos que exploran caminos y necesitan "deshacer" decisiones:
- Resolución de laberintos
- Problema de las N reinas
- Sudoku

### 3. Análisis de Sintaxis

- **Balanceo de paréntesis:** Verificar que `()`, `[]`, `{}` estén balanceados
- **Compiladores:** Análisis sintáctico y evaluación de expresiones

### 4. Gestión de Memoria

- **Call Stack:** Pila de llamadas a funciones en programas
- **Undo/Redo:** Historial de acciones en editores

### 5. Navegación

- **Historial del navegador:** Botón "Atrás"
- **Navegación en directorios:** Volver a carpeta anterior

## Ejemplo Conceptual

### Operaciones Paso a Paso

```
Estado Inicial: []

Push(5):  [5]
Push(3):  [5, 3]
Push(8):  [5, 3, 8]
Peek():   8 (la pila sigue [5, 3, 8])
Pop():    8 (la pila queda [5, 3])
Pop():    3 (la pila queda [5])
Push(1):  [5, 1]
Pop():    1 (la pila queda [5])
Pop():    5 (la pila queda [])
IsEmpty(): true
```

## Casos de Uso vs Otras Estructuras

| Escenario | Usar Pila | Alternativa |
|-----------|-----------|-------------|
| Procesar elementos en orden inverso | ✓ | - |
| Necesitas acceso al más reciente | ✓ | - |
| Necesitas acceso al más antiguo | ✗ | Cola |
| Necesitas acceso aleatorio | ✗ | Arreglo/Lista |
| LIFO es importante | ✓ | - |
| FIFO es importante | ✗ | Cola |

## Variantes Especiales

### 1. Pila de Mínimos

Mantiene track del mínimo actual en O(1):
```
Push: O(1)
Pop: O(1)
GetMin: O(1)
```

### 2. Pila con Máximo

Similar pero mantiene el máximo actual.

### 3. Dos Pilas en un Arreglo

Optimización de espacio usando un solo arreglo para dos pilas:
```
Pila 1 crece de izquierda a derecha: [-> ]
Pila 2 crece de derecha a izquierda: [ <-]
```

## Complejidad Resumida

### Temporal
- **Todas las operaciones:** O(1) - Tiempo constante

### Espacial
- **Arreglo:** O(capacidad)
- **Lista enlazada:** O(n) donde n = número de elementos

## Consideraciones de Implementación

### Condiciones de Error

1. **Underflow:** Intentar `Pop()` o `Peek()` en pila vacía
2. **Overflow:** Intentar `Push()` en pila llena (solo en implementación con arreglo)

### Buenas Prácticas

1. **Validar operaciones:** Verificar `IsEmpty()` antes de `Pop()` o `Peek()`
2. **Manejo de excepciones:** Lanzar excepciones apropiadas en condiciones de error
3. **Documentar comportamiento:** Especificar qué retorna en caso de pila vacía
4. **Thread-safety:** Considerar sincronización en ambientes concurrentes

## Ejemplo de Algoritmo: Balanceo de Paréntesis

```
Algoritmo: VerificarBalanceo(expresión)
  pila = nueva Pila()
  
  para cada carácter en expresión:
    si carácter es apertura ('(', '[', '{'):
      pila.Push(carácter)
    
    si carácter es cierre (')', ']', '}'):
      si pila.IsEmpty():
        retornar False  // Cierre sin apertura
      
      apertura = pila.Pop()
      si no coinciden (apertura, carácter):
        retornar False  // Tipo no coincide
  
  retornar pila.IsEmpty()  // True si balanceado

Complejidad: O(n) tiempo, O(n) espacio
```

## Relación con Otras Estructuras

### Pila vs Cola
- **Pila:** LIFO - Último en entrar, primero en salir
- **Cola:** FIFO - Primero en entrar, primero en salir

### Pila vs Lista
- **Pila:** Acceso restringido solo al tope
- **Lista:** Acceso a cualquier posición

### Implementación en Diferentes Lenguajes

- **C#:** `Stack<T>` en `System.Collections.Generic`
- **Java:** `Stack<E>` en `java.util` (aunque `Deque` es preferible)
- **Python:** Lista con métodos `append()` y `pop()`
- **C++:** `std::stack<T>` en `<stack>`

## Referencias y Recursos Adicionales

- **Práctica relacionada:** [practica-4-pila-con-referencias](../../dsa-dotnet/practica-4-pila-con-referencias)
- **Proyecto relacionado:** [proyecto-i-interprete-matematico](../../dsa-dotnet/proyecto-i-interprete-matematico)

---

<div align="center">

![Stack Visualization](https://img.shields.io/badge/Estructura-LIFO-orange?style=for-the-badge)
![Time Complexity](https://img.shields.io/badge/Complejidad-O(1)-success?style=for-the-badge)

</div>

