# Examen 1: Evaluación Integral de Física Computacional

**Autor**: Luis Fernando Núñez Rangel  
**Institución**: Facultad de Ciencias, UNAM  
**Asignatura**: Física Computacional

## Descripción General

Evaluación práctica que demuestra competencias en programación científica utilizando tres lenguajes: **Fortran 90**, **Python 3** y **C99**. Los ejercicios cubren álgebra lineal, visualización de datos, teoría de números y algoritmos computacionales.

## Contenido del Examen

### Ejercicio 1: Cálculo de Determinantes (Fortran y Python)

#### 1.1 - Implementación en Fortran (`ejercicio_1_1.f90`)

**Objetivo**: Calcular el determinante de una matriz 3×3 mediante la regla de Sarrus.

**Fundamento Matemático**:

Para matriz M 3×3:
```
M = | a₁₁  a₁₂  a₁₃ |
    | a₂₁  a₂₂  a₂₃ |
    | a₃₁  a₃₂  a₃₃ |
```

**Regla de Sarrus**:
```
det(M) = (a₁₁a₂₂a₃₃ + a₂₁a₃₂a₁₃ + a₃₁a₁₂a₂₃) - (a₁₃a₂₂a₃₁ + a₂₃a₃₂a₁₁ + a₃₃a₁₂a₂₁)
         └──────────── diagonal positiva ────────────┘   └──────────── diagonal negativa ────────────┘
```

**Características**:
- Entrada interactiva fila por fila
- Asignación dinámica de memoria
- Cálculo explícito de diagonales positivas y negativas

**Compilación y Ejecución**:
```bash
gfortran src/ejercicio_1_1.f90 -o output/ejercicio_1
./output/ejercicio_1
```

**Ejemplo**:
```
Entrada:
  Fila 1: 1  2  3
  Fila 2: 0  1  4
  Fila 3: 5  6  0

Salida:
  El valor del determinante es: 1.000000
```

#### 1.2 - Implementación en Python (`ejercicio_1_2.py`)

**Objetivo**: Implementar el mismo algoritmo en Python y validar con NumPy.

**Características**:
- Función modular `determinante(matrix: list) -> float`
- Validación cruzada con `numpy.linalg.det()`
- Entrada interactiva matricial
- Extensión de matriz para aplicar regla de Sarrus

**Ejecución**:
```bash
python src/ejercicio_1_2.py
```

**Validación**:
```python
Determinante obtenido por nuestro método: 1.0
Determinante de numpy: 1.0000000000000002
```

**Ventajas de la implementación**:
- Verificación automática con librería estándar
- Manejo de tipos genéricos (int/float)
- Código reutilizable como módulo

---

### Ejercicio 2: Visualización de Funciones Polinomiales (Python/Jupyter)

**Archivo**: `ejercicio_2.ipynb`

#### Objetivo
Generar y visualizar funciones cuadráticas y cúbicas en un rango definido.

#### Especificaciones

**Dominio**:
```python
x = [-10, -9.5, -9, ..., 9.5, 10]  # Paso de 0.5
```

**Funciones**:
1. f₁(x) = x²   (parábola)
2. f₂(x) = x³   (cúbica)

**Estructura de Datos**:
```python
df = pd.DataFrame({
    "x": x,
    "f1(x)": [x**2 for x in x],
    "f2(x)": [x**3 for x in x]
})
```

**Visualización**:
- Gráfica combinada con ambas funciones
- Etiquetas y leyendas descriptivas
- Grid para mejor lectura
- Ejes con unidades claras

**Características Implementadas**:
- Generación de vectores con list comprehensions
- DataFrame de Pandas para organización de datos
- Matplotlib para visualización profesional

**Interpretación Gráfica**:
- **f₁(x)**: Simetría par, mínimo en (0,0), crecimiento cuadrático
- **f₂(x)**: Simetría impar, punto de inflexión en (0,0), crecimiento cúbico

---

### Ejercicio 3: Teoría de Números y Primalidad

#### 3.1 - Operación Módulo en Fortran (`ejercicio_3_1.f90`)

**Objetivo**: Demostrar el uso de la función intrínseca `mod()` en Fortran.

**Fundamento**:
```
mod(a, b) = a - b * floor(a/b)
```

**Compilación**:
```bash
gfortran src/ejercicio_3_1.f90 -o output/ejercicio_3_1
./output/ejercicio_3_1
```

**Ejemplo**:
```
Entrada: 17, 5
Salida: 2.000000
```

**Aplicaciones**:
- Test de divisibilidad
- Aritmética modular
- Algoritmos criptográficos

---

#### 3.2 - Verificación de Números Primos

##### Fortran (`ejercicio_3_2.f90`)

**Algoritmo**: Prueba de divisibilidad por fuerza bruta.

```fortran
subroutine is_p(a, resultado)
    ! Casos base: 1,2,3 son primos
    ! Verificar divisibilidad desde 2 hasta a-1
    ! resultado = 1 (primo), 0 (compuesto)
end subroutine
```

**Compilación**:
```bash
gfortran src/ejercicio_3_2.f90 -o output/ejercicio_3_2
./output/ejercicio_3_2
```

**Complejidad**: O(n) - No optimizado, educativo

##### C (`ejercicio_3_2.c`)

**Implementación equivalente en C99**:

```c
int is_p(int a) {
    if (a >= 1 && a <= 3) return 1;
    
    for (int i = 2; i < a; i++) {
        if (a % i == 0) return 0;
    }
    return 1;
}
```

**Compilación**:
```bash
gcc -std=c99 src/ejercicio_3_2.c -o output/ejercicio_3_c
./output/ejercicio_3_c
```

**Salida**:
```
Digita un numero: 17
Es numero primo: true
```

**Comparación Fortran vs C**:
- **Sintaxis**: C más concisa, Fortran más verboso
- **Tipos lógicos**: C usa int (0/1), Fortran permite boolean
- **Performance**: Equivalente para este algoritmo

---

#### 3.3 - Generación de Primeros 100 Primos (`ejercicio_3_3.f90`)

**Objetivo**: Generar secuencia de los primeros 100 números primos.

**Algoritmo**:
```fortran
program primeros_100_primos
    integer :: i=1, resultado, count=0
    
    do
        call is_p(i, resultado)
        if (resultado == 1) then
            print *, i
            count = count + 1
        end if
        
        if (count == 100) return
        i = i + 1
    end do
end program
```

**Compilación**:
```bash
gfortran src/ejercicio_3_3.f90 -o output/ejercicio_3_3
./output/ejercicio_3_3
```

**Salida** (primeros 10):
```
2
3
5
7
11
13
17
19
23
29
...
```

**Optimizaciones Posibles**:
- Criba de Eratóstenes: O(n log log n)
- Test hasta √n en lugar de n
- Memoización de primos encontrados

---

## Estructura del Directorio

```
examen_1/
├── src/
│   ├── ejercicio_1_1.f90        # Determinante en Fortran
│   ├── ejercicio_1_2.py         # Determinante en Python
│   ├── ejercicio_2.ipynb        # Visualización de funciones
│   ├── ejercicio_3_1.f90        # Operación módulo
│   ├── ejercicio_3_2.f90        # Primalidad en Fortran
│   ├── ejercicio_3_2.c          # Primalidad en C
│   └── ejercicio_3_3.f90        # 100 primos
├── output/
│   ├── ejercicio_1              # Ejecutables compilados
│   ├── ejercicio_3_1
│   ├── ejercicio_3_2
│   ├── ejercicio_3_3
│   └── ejercicio_3_c
├── requirements.txt             # Dependencias Python
└── README.md                    # Esta documentación
```

## Requisitos del Sistema

### Compiladores

- **gfortran** 9.0+ (GNU Fortran)
- **gcc** 9.0+ (GNU C Compiler)
- **Python** 3.8+

### Dependencias Python

```bash
pip install -r requirements.txt
```

```
numpy>=1.19.0
pandas>=1.1.0
matplotlib>=3.3.0
jupyter>=1.0.0
```

## Compilación Completa

Script para compilar todos los ejercicios:

```bash
#!/bin/bash

# Fortran
gfortran src/ejercicio_1_1.f90 -o output/ejercicio_1
gfortran src/ejercicio_3_1.f90 -o output/ejercicio_3_1
gfortran src/ejercicio_3_2.f90 -o output/ejercicio_3_2
gfortran src/ejercicio_3_3.f90 -o output/ejercicio_3_3

# C
gcc -std=c99 src/ejercicio_3_2.c -o output/ejercicio_3_c

echo "Compilación completa"
```

## Ejecución de Pruebas

### Test del Ejercicio 1 (Determinantes)

**Matrices de prueba**:

1. **Matriz identidad** (det = 1):
```
1 0 0
0 1 0
0 0 1
```

2. **Matriz singular** (det = 0):
```
1 2 3
2 4 6
3 6 9
```

3. **Matriz general**:
```
2 -1 0
-1 2 -1
0 -1 2
```
det = 2(4-1) - (-1)(-2-0) + 0 = 6 - 2 = 4

### Test del Ejercicio 3 (Primos)

**Números de prueba**:
- Primos: 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31
- Compuestos: 4, 6, 8, 9, 10, 12, 14, 15, 16, 18, 20
- Casos borde: 1 (definido como primo en implementación), 0 (indefinido)

## Conceptos Evaluados

### Álgebra Lineal
- Determinantes de matrices 3×3
- Regla de Sarrus
- Matrices singulares vs no singulares

### Análisis Numérico
- Evaluación de funciones polinomiales
- Discretización de dominios
- Visualización de datos

### Teoría de Números
- Aritmética modular
- Test de primalidad
- Generación de secuencias numéricas

### Programación
- Fortran 90: Tipos, subrutinas, E/S
- Python: Funciones, listas, comprehensions
- C99: Funciones, loops, tipos

## Comparativa de Lenguajes

| Aspecto | Fortran 90 | Python 3 | C99 |
|---------|-----------|----------|-----|
| **Sintaxis** | Verboso, explícito | Conciso, legible | Medio, estructurado |
| **Tipos** | Estático, fuerte | Dinámico, duck typing | Estático, débil |
| **Arrays** | Nativos, 1-indexados | Listas dinámicas | Manual (punteros) |
| **Matemáticas** | Funciones intrínsecas | NumPy/SciPy | math.h |
| **I/O** | `read`, `print` | `input()`, `print()` | `scanf()`, `printf()` |
| **Uso científico** | Computación HPC | Análisis de datos | Sistemas, drivers |

## Aplicaciones Prácticas

### Determinantes
- Resolución de sistemas lineales (regla de Cramer)
- Cálculo de volúmenes (paralelepípedo)
- Jacobiano en transformaciones de coordenadas
- Independencia lineal de vectores

### Números Primos
- Criptografía (RSA)
- Generadores de números pseudoaleatorios
- Teoría de códigos
- Factorización de enteros

### Visualización de Funciones
- Análisis de comportamiento asintótico
- Identificación de puntos críticos
- Comparación de tasas de crecimiento
- Enseñanza de cálculo diferencial

## Limitaciones y Mejoras

### Ejercicio 1
- Limitado a matrices 3×3
- **Mejora**: Generalizar a n×n con expansión por cofactores

### Ejercicio 3.2
- Algoritmo no optimizado (O(n))
- **Mejoras**:
  - Test solo hasta √n
  - Criba de Eratóstenes
  - Test de Miller-Rabin (probabilístico)

### Ejercicio 2
- Funciones hardcodeadas
- **Mejora**: Parser de expresiones matemáticas genérico

## Referencias

### Álgebra Lineal
- Strang, G. (2016). *Introduction to Linear Algebra* (5th ed.)
- Lay, D. C. (2015). *Linear Algebra and Its Applications* (5th ed.)

### Teoría de Números
- Rosen, K. H. (2019). *Elementary Number Theory* (7th ed.)
- Hardy, G. H., & Wright, E. M. (2008). *An Introduction to the Theory of Numbers*

### Programación Científica
- Metcalf, M., Reid, J., & Cohen, M. (2018). *Modern Fortran Explained*
- Kernighan, B. W., & Ritchie, D. M. (1988). *The C Programming Language* (2nd ed.)

## Criterios de Evaluación

1. **Corrección algorítmica** (40%)
   - Implementación correcta de métodos matemáticos
   - Validación con casos de prueba

2. **Calidad del código** (30%)
   - Legibilidad y organización
   - Documentación inline
   - Manejo de casos borde

3. **Eficiencia** (15%)
   - Complejidad computacional razonable
   - Uso apropiado de estructuras de datos

4. **Diversidad de herramientas** (15%)
   - Dominio de múltiples lenguajes
   - Uso apropiado de cada herramienta

## Tiempo de Desarrollo

- **Ejercicio 1**: ~30 minutos (ambas versiones)
- **Ejercicio 2**: ~20 minutos
- **Ejercicio 3**: ~40 minutos (todas las versiones)
- **Total**: ~90 minutos

## Conclusiones

Este examen demuestra:
- Versatilidad en múltiples paradigmas de programación
- Comprensión de fundamentos matemáticos
- Capacidad de traducir algoritmos entre lenguajes
- Habilidades de visualización y análisis de datos
- Conocimientos de teoría de números aplicada

La combinación de Fortran, Python y C ilustra la importancia de seleccionar la herramienta adecuada para cada tarea en física computacional.

