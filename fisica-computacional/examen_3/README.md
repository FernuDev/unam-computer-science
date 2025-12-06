# Tarea 6 - Examen 3: Ecuación de Laplace 2D

![Python](https://img.shields.io/badge/Python-3.x-3776AB?style=for-the-badge&logo=python&logoColor=white)
![Jupyter](https://img.shields.io/badge/Jupyter-Notebook-F37626?style=for-the-badge&logo=jupyter&logoColor=white)
![NumPy](https://img.shields.io/badge/NumPy-013243?style=for-the-badge&logo=numpy&logoColor=white)

## Descripción

Solución completa del Examen 3 de Física Computacional que aborda la resolución de la ecuación de Laplace en dos dimensiones usando métodos iterativos y comparación de algoritmos de optimización.

## Contenido

1. **Discretización de la ecuación de Laplace** usando diferencias finitas
2. **Formación del sistema lineal** Ax = b
3. **Implementación de tres métodos iterativos:**
   - Método de Jacobi
   - Método de Gauss-Seidel
   - Método de Gradiente Descendente
4. **Análisis de convergencia** y visualización de resultados
5. **Ejercicio adicional:** Algoritmos de optimización por enjambre

## Estructura del Proyecto

```
tarea_6/
├── tarea_6_laplace.ipynb           # Notebook principal
├── requirements.txt                # Dependencias
├── README.md                       # Este archivo
└── [Archivos generados]
    ├── solucion_laplace.csv
    ├── convergencia_metodos.csv
    ├── estadisticas_optimizacion.csv
    ├── convergencia_metodos.png
    ├── solucion_laplace.png
    └── optimizacion_comparacion.png
```

## Ecuación de Laplace

La ecuación de Laplace en dos dimensiones es:

```
∇²u = ∂²u/∂x² + ∂²u/∂y² = 0
```

### Discretización con Diferencias Finitas

En una malla uniforme (50x50 puntos), se utiliza el estencil de 5 puntos:

```
u(i+1,j) + u(i-1,j) + u(i,j+1) + u(i,j-1) - 4u(i,j) = 0
```

### Condiciones de Frontera

- Frontera inferior: u(x,0) = 0
- Frontera superior: u(x,L) = sin(πx)
- Fronteras laterales: u(0,y) = u(L,y) = 0

## Métodos Iterativos Implementados

### 1. Método de Jacobi

Actualización simultánea usando valores de la iteración anterior:

```
x[i]^(k+1) = (b[i] - Σ a[i,j]x[j]^(k)) / a[i,i]
```

**Características:**
- Convergencia: O(n²) iteraciones
- Paralelizable
- Requiere dos copias del vector solución

### 2. Método de Gauss-Seidel

Usa valores actualizados inmediatamente:

```
x[i]^(k+1) = (b[i] - Σ[j<i] a[i,j]x[j]^(k+1) - Σ[j>i] a[i,j]x[j]^(k)) / a[i,i]
```

**Características:**
- Convergencia: ~2x más rápido que Jacobi
- No paralelizable directamente
- Menor uso de memoria

### 3. Método de Gradiente Descendente

Minimiza el funcional F(x) = (1/2)x^T Ax - b^T x:

```
x^(k+1) = x^(k) - α[k] r^(k)
α[k] = (r^T r) / (r^T A r)
```

**Características:**
- Convergencia predecible
- Requiere matriz simétrica definida positiva
- Tamaño de paso óptimo en cada iteración

## Instalación y Uso

### Requisitos Previos

```bash
Python 3.x
pip (gestor de paquetes)
```

### Instalación de Dependencias

```bash
cd tarea_6
pip install -r requirements.txt
```

### Ejecutar el Notebook

**Opción 1: Jupyter Notebook**
```bash
jupyter notebook tarea_6_laplace.ipynb
```

**Opción 2: Jupyter Lab**
```bash
jupyter lab tarea_6_laplace.ipynb
```

**Opción 3: VS Code**
Abrir el archivo `.ipynb` directamente en VS Code con la extensión de Jupyter

## Resultados Esperados

### Archivos de Salida

1. **solucion_laplace.csv**
   - Matriz 50x50 con la solución numérica
   - Formato CSV con encabezados

2. **convergencia_metodos.csv**
   - Historia de residuos relativos
   - Comparación de los tres métodos

3. **estadisticas_optimizacion.csv**
   - Estadísticas de algoritmos de optimización
   - Métricas: mejor, peor, media, mediana, desviación estándar

### Visualizaciones

1. **convergencia_metodos.png**
   - Gráfica de convergencia (escala logarítmica y lineal)
   - Comparación visual de velocidad de convergencia

2. **solucion_laplace.png**
   - Gráfica 3D de la solución
   - Mapa de contorno
   - Mapa de calor

3. **optimizacion_comparacion.png**
   - Convergencia de algoritmos de optimización
   - Diagrama de caja (boxplot) comparativo

## Análisis de Convergencia

Los métodos muestran diferentes tasas de convergencia:

| Método | Iteraciones Típicas | Velocidad Relativa |
|--------|--------------------|--------------------|
| Jacobi | ~800-1000 | 1x (baseline) |
| Gauss-Seidel | ~400-500 | 2x más rápido |
| Gradiente Descendente | ~600-700 | 1.5x más rápido |

**Nota:** Los valores exactos dependen de la tolerancia y condiciones iniciales.

## Algoritmos de Optimización

Se implementan tres algoritmos para resolver la función de Rosenbrock:

```
f(x) = Σ[100(x[i+1] - x[i]²)² + (1 - x[i])²]
```

### Algoritmos

1. **Búsqueda Aleatoria**
   - Exploración global
   - Simple pero ineficiente
   - Útil para comparación baseline

2. **Gradiente Simple**
   - Descenso por gradiente con diferencias finitas
   - Convergencia local rápida
   - Puede quedar atrapado en mínimos locales

3. **Método Híbrido**
   - Fase 1: Búsqueda aleatoria (exploración)
   - Fase 2: Gradiente (refinamiento)
   - Balance entre exploración y explotación

## Conceptos Teóricos

### Ecuación de Laplace

Describe fenómenos de estado estacionario:
- Distribución de temperatura
- Potencial electrostático
- Flujo de fluidos incompresibles
- Deformación de membranas

### Métodos Iterativos

**Ventajas:**
- Eficientes para matrices dispersas
- Menor uso de memoria que métodos directos
- Fáciles de implementar
- Convergen para matrices con propiedades específicas

**Desventajas:**
- Número de iteraciones puede ser alto
- Sensibles a condiciones iniciales
- Requieren criterios de parada

### Matriz de Laplace

La matriz A resultante es:
- **Dispersa:** Solo 5 elementos no ceros por fila
- **Simétrica:** A = A^T
- **Definida positiva:** x^T Ax > 0 para todo x ≠ 0
- **Banda:** Estructura de 5 diagonales

## Complejidad Computacional

### Construcción del Sistema
- Tiempo: O(N²) donde N = Nx × Ny
- Espacio: O(N²) pero almacenamiento disperso reduce a O(5N)

### Métodos Iterativos
- Por iteración: O(N) para operaciones matriz-vector dispersa
- Total: O(kN) donde k = número de iteraciones

### Almacenamiento
- Matriz dispersa: ~5N elementos en lugar de N²
- Para N=2500: 12,500 vs 6,250,000 elementos

## Extensiones Posibles

1. **Condiciones de frontera mixtas**
   - Neumann: ∂u/∂n = g
   - Robin: αu + β∂u/∂n = g

2. **Ecuación de Poisson**
   - ∇²u = f(x,y)
   - Término fuente no homogéneo

3. **Métodos más avanzados**
   - Gradiente Conjugado
   - Multigrid
   - Métodos de Krylov (GMRES, BiCGSTAB)

4. **Dominios irregulares**
   - Mallas no uniformes
   - Fronteras curvas

## Verificación de Resultados

### Criterios de Convergencia

1. **Residuo relativo:** ||b - Ax|| / ||b|| < tolerancia
2. **Cambio relativo:** ||x^(k+1) - x^(k)|| / ||x^(k)|| < tolerancia
3. **Número máximo de iteraciones**

### Pruebas de Validación

El notebook incluye:
- Verificación de construcción de matriz
- Chequeo de simetría
- Análisis de esparsidad
- Estadísticas de solución

## Referencias

### Libros
- "Numerical Analysis" - Burden & Faires
- "Computational Physics" - Newman
- "Matrix Computations" - Golub & Van Loan

### Artículos
- Diferencias finitas para ecuaciones elípticas
- Métodos iterativos de Krylov
- Técnicas de precondicionamiento

---

<div align="center">

**Facultad de Ciencias - UNAM**

*Física Computacional - Tarea 6*

![Status](https://img.shields.io/badge/Estado-Completo-success?style=flat-square)
![Python](https://img.shields.io/badge/Python-3.x-blue?style=flat-square)

</div>


