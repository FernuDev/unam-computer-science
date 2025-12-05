# Física Computacional

[![Fortran](https://img.shields.io/badge/Fortran-90%2B-blue.svg)](https://fortran-lang.org/)
[![Python](https://img.shields.io/badge/Python-3.8%2B-green.svg)](https://www.python.org/)
[![C](https://img.shields.io/badge/C-99-orange.svg)](https://en.wikipedia.org/wiki/C99)

Repositorio académico para los trabajos realizados en la asignatura de **Física Computacional**, impartida en la Facultad de Ciencias de la Universidad Nacional Autónoma de México (UNAM).

## Descripción

Este repositorio contiene implementaciones de métodos numéricos y algoritmos computacionales aplicados a problemas de física, desarrollados en múltiples lenguajes de programación (Fortran 90+, Python 3.8+, C99). Los trabajos incluyen desde resolución de ecuaciones algebraicas hasta análisis de datos experimentales y simulaciones físicas.

## Estructura del Repositorio

```
FisicaComputacional/
├── tarea_1/          # Métodos numéricos en Fortran
├── tarea_2/          # Análisis de datos con Python/Jupyter
├── tarea_3/          # Cinemática y análisis numérico
├── examen_1/         # Primer examen parcial
└── README.md         # Este archivo
```

## Contenido por Módulo

### [Tarea 1: Métodos Numéricos Fundamentales](tarea_1/)

Implementación de algoritmos clásicos de álgebra numérica en Fortran 90:
- **Ecuaciones cúbicas**: Solución mediante la fórmula de Cardano
- **Álgebra vectorial**: Operaciones en espacios R^n (normas, productos, ángulos)

**Tecnologías**: Fortran 90, gfortran

### [Tarea 2: Análisis de Datos y Visualización](tarea_2/)

Ejercicios de física computacional con énfasis en análisis de datos:
- Algoritmos de búsqueda y procesamiento
- Conversión de unidades termodinámicas
- Cinemática: caída libre y análisis temporal
- Programación orientada a objetos: sistemas de partículas
- Álgebra lineal: operaciones matriz-vector

**Tecnologías**: Python 3.8+, Jupyter Notebook, pandas, matplotlib, numpy

### [Tarea 3: Análisis Cinemático de Corredores](tarea_3/)

Sistema de análisis de datos experimentales de carrera:
- Cálculo de velocidades mediante diferenciación numérica (método de 3 puntos de Lagrange)
- Integración numérica (regla del trapecio)
- Visualización comparativa de datos cinemáticos
- Procesamiento de archivos de datos experimentales

**Tecnologías**: Fortran 90 (procesamiento), Python 3.8+ (análisis y visualización)

### [Examen 1: Evaluación Integral](examen_1/)

Evaluación de competencias en múltiples lenguajes:
- **Álgebra lineal**: Cálculo de determinantes (Fortran y Python)
- **Visualización**: Gráficas de funciones polinomiales
- **Teoría de números**: Operaciones modulares y números primos
- **Algoritmos**: Generación de secuencias numéricas

**Tecnologías**: Fortran 90, Python 3.8+, C99

## Requisitos del Sistema

### Compiladores y Entornos

- **Fortran**: gfortran 9.0+ (GNU Fortran Compiler)
- **C**: gcc 9.0+ (GNU C Compiler)
- **Python**: 3.8 o superior

### Dependencias Python

```bash
pip install -r requirements.txt
```

Principales librerías:
- `numpy`: Computación numérica
- `pandas`: Análisis de datos
- `matplotlib`: Visualización
- `jupyter`: Notebooks interactivos

## Instalación y Uso

### Clonar el Repositorio

```bash
git clone https://github.com/usuario/FisicaComputacional.git
cd FisicaComputacional
```

### Compilación de Programas Fortran

```bash
# Ejemplo: Compilar con optimización
gfortran -O2 -std=f2008 -o ejecutable archivo.f90
```

### Compilación de Programas C

```bash
gcc -std=c99 -O2 -o ejecutable archivo.c
```

### Ejecución de Notebooks

```bash
jupyter notebook tarea_2/main.ipynb
```

## Documentación por Módulo

Cada directorio contiene su propio README con:
- Descripción detallada de los ejercicios
- Fundamentos teóricos
- Instrucciones de compilación y ejecución
- Ejemplos de uso
- Resultados esperados

## Metodología de Desarrollo

- **Control de versiones**: Git
- **Estándares de código**: 
  - Fortran 2008 estándar
  - PEP 8 para Python
  - C99 estándar
- **Documentación**: Comentarios inline y READMEs descriptivos
- **Testing**: Validación contra soluciones analíticas cuando es posible

## Estructura de Archivos

```
modulo/
├── src/              # Código fuente
├── bin/              # Ejecutables compilados
├── output/           # Resultados y archivos de salida
├── data/             # Datos de entrada/experimentales
├── img/              # Gráficas y visualizaciones
├── requirements.txt  # Dependencias Python
└── README.md         # Documentación del módulo
```

## Autor

**Luis Fernando Núñez Rangel**  
Facultad de Ciencias  
Universidad Nacional Autónoma de México (UNAM)

## Licencia

Este proyecto está bajo la licencia MIT. Ver archivo `LICENSE` para más detalles.

## Referencias

- Burden, R. L., & Faires, J. D. (2010). *Numerical Analysis* (9th ed.). Brooks/Cole.
- Press, W. H., et al. (2007). *Numerical Recipes: The Art of Scientific Computing* (3rd ed.). Cambridge University Press.
- Documentación oficial de Fortran: [https://fortran-lang.org/](https://fortran-lang.org/)
- Documentación de NumPy/SciPy: [https://numpy.org/](https://numpy.org/)
