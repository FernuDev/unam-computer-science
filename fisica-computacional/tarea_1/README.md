# Tarea 1: Método de Cardano y Vectores

![Fortran](https://img.shields.io/badge/Fortran-90-734F96?style=for-the-badge&logo=fortran&logoColor=white)
![Physics](https://img.shields.io/badge/Física-Computacional-blueviolet?style=for-the-badge)

## Descripción

Primera tarea del curso de Física Computacional que aborda dos temas fundamentales:
1. **Método de Cardano**: Solución de ecuaciones cúbicas
2. **Operaciones con vectores**: Álgebra vectorial básica en 3D

## Objetivos de Aprendizaje

- Implementar algoritmos matemáticos en Fortran
- Resolver ecuaciones cúbicas usando el Método de Cardano
- Realizar operaciones con vectores en 3D
- Aplicar conceptos de álgebra lineal
- Familiarizarse con la programación científica

## Estructura del Proyecto

```
tarea_1/
├── src/
│   ├── Cardano.f90        # Implementación del Método de Cardano
│   └── vectores.f90       # Operaciones con vectores 3D
├── output/
│   ├── Cardano            # Ejecutable del método de Cardano
│   └── vectores           # Ejecutable de vectores
├── Tarea 1 Física Computacional.pdf  # Especificación
└── README.md
```

## 1. Método de Cardano

### ¿Qué es el Método de Cardano?

El **Método de Cardano** (o fórmula de Cardano) es un algoritmo para resolver ecuaciones cúbicas de la forma:

```
ax³ + bx² + cx + d = 0
```

Desarrollado por **Gerolamo Cardano** en el siglo XVI, fue uno de los primeros métodos generales para resolver ecuaciones de grado superior a 2.

### Forma Reducida

Primero se transforma la ecuación a su **forma reducida** (sin término cuadrático):

```
t³ + pt + q = 0
```

Mediante la sustitución:
```
x = t - b/(3a)
```

Donde:
```
p = (3ac - b²) / (3a²)
q = (2b³ - 9abc + 27a²d) / (27a³)
```

### Discriminante

El **discriminante** determina la naturaleza de las raíces:

```
Δ = -(4p³ + 27q²)
```

- Si **Δ > 0**: Tres raíces reales distintas
- Si **Δ = 0**: Raíces reales con multiplicidad
- Si **Δ < 0**: Una raíz real y dos complejas conjugadas

### Fórmula de Cardano

Para la ecuación reducida `t³ + pt + q = 0`:

```
t = ∛(-q/2 + √(q²/4 + p³/27)) + ∛(-q/2 - √(q²/4 + p³/27))
```

### Ejemplo

Resolver: `x³ - 6x² + 11x - 6 = 0`

**Paso 1:** Forma reducida (a=1, b=-6, c=11, d=-6)
```
p = (3·1·11 - (-6)²)/(3·1²) = (33 - 36)/3 = -1
q = (2·(-6)³ - 9·1·(-6)·11 + 27·1²·(-6))/(27·1³) = 0
```

Ecuación reducida: `t³ - t = 0`

**Paso 2:** Resolver
```
t(t² - 1) = 0
t(t-1)(t+1) = 0
```

Soluciones: `t = -1, 0, 1`

**Paso 3:** Regresar a x
```
x = t - b/(3a) = t - (-6)/3 = t + 2
```

Raíces: `x = 1, 2, 3`

### Implementación en Fortran

```fortran
program cardano
    implicit none
    real :: a, b, c, d
    real :: p, q, discriminante
    real :: raiz1, raiz2, raiz3
    
    ! Leer coeficientes
    print *, 'Ingrese coeficientes (a, b, c, d):'
    read *, a, b, c, d
    
    ! Calcular p y q
    p = (3*a*c - b**2) / (3*a**2)
    q = (2*b**3 - 9*a*b*c + 27*a**2*d) / (27*a**3)
    
    ! Calcular discriminante
    discriminante = -(4*p**3 + 27*q**2)
    
    ! Aplicar fórmula de Cardano
    call resolver_cubica(p, q, discriminante, raiz1, raiz2, raiz3)
    
    ! Mostrar resultados
    print *, 'Raíces:', raiz1, raiz2, raiz3
    
end program cardano
```

## 2. Operaciones con Vectores

### Vectores en 3D

Un vector en el espacio tridimensional se representa como:

```
v = (vₓ, vᵧ, vᵤ)
```

### Operaciones Implementadas

#### 1. Suma de Vectores

```
u + v = (uₓ + vₓ, uᵧ + vᵧ, uᵤ + vᵤ)
```

**Ejemplo:**
```
u = (1, 2, 3)
v = (4, 5, 6)
u + v = (5, 7, 9)
```

#### 2. Resta de Vectores

```
u - v = (uₓ - vₓ, uᵧ - vᵧ, uᵤ - vᵤ)
```

#### 3. Producto Escalar (Dot Product)

```
u · v = uₓvₓ + uᵧvᵧ + uᵤvᵤ
```

**Propiedades:**
- Resultado es un **escalar** (número)
- `u · v = |u||v|cos(θ)` donde θ es el ángulo entre vectores
- Si `u · v = 0`, los vectores son **perpendiculares**

**Ejemplo:**
```
u = (1, 2, 3)
v = (4, 5, 6)
u · v = 1·4 + 2·5 + 3·6 = 4 + 10 + 18 = 32
```

#### 4. Producto Vectorial (Cross Product)

```
u × v = |  i    j    k  |
        | uₓ   uᵧ   uᵤ |
        | vₓ   vᵧ   vᵤ |

u × v = (uᵧvᵤ - uᵤvᵧ, uᵤvₓ - uₓvᵤ, uₓvᵧ - uᵧvₓ)
```

**Propiedades:**
- Resultado es un **vector**
- Perpendicular a ambos vectores originales
- `|u × v| = |u||v|sin(θ)`
- **Regla de la mano derecha** determina dirección
- **Anticonmutativo:** `u × v = -(v × u)`

**Ejemplo:**
```
u = (1, 2, 3)
v = (4, 5, 6)

u × v = (2·6 - 3·5, 3·4 - 1·6, 1·5 - 2·4)
      = (12 - 15, 12 - 6, 5 - 8)
      = (-3, 6, -3)
```

#### 5. Magnitud (Norma)

```
|v| = √(vₓ² + vᵧ² + vᵤ²)
```

**Ejemplo:**
```
v = (3, 4, 0)
|v| = √(9 + 16 + 0) = √25 = 5
```

#### 6. Normalización

Vector unitario (magnitud = 1):

```
v̂ = v / |v|
```

**Ejemplo:**
```
v = (3, 4, 0)
|v| = 5
v̂ = (3/5, 4/5, 0) = (0.6, 0.8, 0)
```

#### 7. Ángulo entre Vectores

```
cos(θ) = (u · v) / (|u||v|)
θ = arccos((u · v) / (|u||v|))
```

**Ejemplo:**
```
u = (1, 0, 0)
v = (1, 1, 0)

cos(θ) = (1·1 + 0·1 + 0·0) / (1 · √2) = 1/√2
θ = arccos(1/√2) = 45° = π/4 rad
```

### Implementación en Fortran

```fortran
program vectores
    implicit none
    real, dimension(3) :: u, v, suma, producto_cruz
    real :: producto_punto, magnitud_u, magnitud_v
    
    ! Definir vectores
    u = [1.0, 2.0, 3.0]
    v = [4.0, 5.0, 6.0]
    
    ! Suma
    suma = u + v
    print *, 'Suma:', suma
    
    ! Producto escalar
    producto_punto = dot_product(u, v)
    print *, 'Producto escalar:', producto_punto
    
    ! Producto vectorial
    call cross_product(u, v, producto_cruz)
    print *, 'Producto vectorial:', producto_cruz
    
    ! Magnitudes
    magnitud_u = norm2(u)
    magnitud_v = norm2(v)
    print *, 'Magnitudes:', magnitud_u, magnitud_v
    
end program vectores

subroutine cross_product(a, b, result)
    real, dimension(3), intent(in) :: a, b
    real, dimension(3), intent(out) :: result
    
    result(1) = a(2)*b(3) - a(3)*b(2)
    result(2) = a(3)*b(1) - a(1)*b(3)
    result(3) = a(1)*b(2) - a(2)*b(1)
end subroutine cross_product
```

## Compilación y Ejecución

### Requisitos

- Compilador de Fortran (gfortran)
  ```bash
  # macOS
  brew install gcc
  
  # Linux
  sudo apt-get install gfortran
  ```

### Compilar

```bash
cd tarea_1/src

# Compilar Cardano
gfortran -o ../output/Cardano Cardano.f90

# Compilar Vectores
gfortran -o ../output/vectores vectores.f90
```

### Ejecutar

```bash
cd tarea_1/output

# Ejecutar Cardano
./Cardano

# Ejecutar Vectores
./vectores
```

## Aplicaciones

### Método de Cardano

1. **Física:** Ecuaciones de movimiento cúbicas
2. **Ingeniería:** Análisis de estructuras
3. **Computación gráfica:** Intersecciones ray-superficie
4. **Economía:** Modelos de equilibrio

### Vectores

1. **Física:**
   - Fuerzas, velocidades, aceleraciones
   - Campos electromagnéticos
   - Momento angular: **L** = **r** × **p**

2. **Computación Gráfica:**
   - Iluminación (ángulo entre normal y luz)
   - Transformaciones 3D
   - Detección de colisiones

3. **Robótica:**
   - Cinemática de robots
   - Planificación de trayectorias

4. **Simulaciones:**
   - Dinámica de partículas
   - Fluidos computacionales

## Conceptos de Fortran Aplicados

### Arrays

```fortran
real, dimension(3) :: vector
vector = [1.0, 2.0, 3.0]
```

### Funciones Intrínsecas

```fortran
dot_product(u, v)    ! Producto escalar
norm2(v)             ! Norma euclidiana (Fortran 2008+)
sqrt(x)              ! Raíz cuadrada
```

### Subrutinas

```fortran
subroutine nombre(arg_in, arg_out)
    intent(in)  :: arg_in
    intent(out) :: arg_out
end subroutine
```

## Desafíos y Consideraciones

### Método de Cardano

1. **Estabilidad numérica:** Cuidado con raíces muy cercanas
2. **Casos especiales:** Discriminante cero
3. **Raíces complejas:** Requiere aritmética compleja

### Vectores

1. **Precisión:** Usar `real*8` (double precision) para mayor exactitud
2. **Casos degenerados:** Vectores cero, paralelos
3. **Overflow/Underflow:** Vectores muy grandes o pequeños

## Verificación de Resultados

### Método de Cardano

Verificar que las raíces satisfacen la ecuación original:

```fortran
residuo = a*x**3 + b*x**2 + c*x + d
if (abs(residuo) < 1e-6) then
    print *, 'Raíz verificada'
end if
```

### Vectores

Propiedades para verificar:

```fortran
! Producto cruz es perpendicular
verificar = dot_product(u, cross_prod)
! Debe ser ≈ 0

! Magnitud de vector unitario
verificar = norm2(normalizado)
! Debe ser ≈ 1.0
```

## Próximos Pasos

Esta tarea sienta las bases para:

- [Tarea 2: Análisis Numérico](../tarea_2)
- [Tarea 3: Simulaciones](../tarea_3)
- Métodos numéricos avanzados
- Ecuaciones diferenciales

## Referencias y Recursos

### Matemáticas
- "Álgebra Lineal y sus Aplicaciones" - Gilbert Strang
- "Numerical Recipes in Fortran" - Press et al.

### Fortran
- [Modern Fortran Tutorial](https://fortran-lang.org/learn/)
- [Fortran Wiki](http://fortranwiki.org)

---

<div align="center">

![Cardano](https://img.shields.io/badge/Algoritmo-Cardano-blue?style=flat-square)
![Vectors](https://img.shields.io/badge/Álgebra-Vectores%203D-green?style=flat-square)
![Fortran90](https://img.shields.io/badge/Fortran-90-734F96?style=flat-square)

**Facultad de Ciencias - UNAM**

*Física Computacional*

</div>
