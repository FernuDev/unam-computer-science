# Tarea 2: Análisis de Datos y Visualización con Python

**Autor**: Luis Fernando Núñez Rangel  
**Institución**: Facultad de Ciencias, UNAM  
**Asignatura**: Física Computacional  
**Instructor**: Dr. José Eduardo González Mireles, Dra. Gabriela Berenice Díaz Cortés

## Descripción General

Esta tarea explora el uso de Python como herramienta de cómputo científico, implementando algoritmos fundamentales, análisis de datos físicos y visualización de resultados. Se utilizan librerías estándar de computación científica (NumPy, Pandas, Matplotlib) junto con programación orientada a objetos.

## Contenido del Notebook

El archivo `main.ipynb` contiene cinco ejercicios progresivos que cubren aspectos fundamentales de la física computacional.

### Ejercicio 1: Algoritmo de Búsqueda del Máximo

#### Objetivo
Implementar un algoritmo de búsqueda del elemento máximo en una lista sin utilizar funciones incorporadas de Python.

#### Fundamento Técnico
Algoritmo de búsqueda lineal con complejidad temporal O(n):

```python
def my_max(numbers: list) -> float
```

#### Características
- Validación de entrada (lista no vacía)
- Manejo de excepciones con `ValueError`
- Documentación con docstrings tipo PEP 257
- Retorno en punto flotante para compatibilidad con ejercicios posteriores

#### Aplicación
Lista de prueba: `[10, 56, 258, 16, 24, 18, 265, 893, 52, 39, 82]`  
Resultado esperado: `893`

---

### Ejercicio 2: Conversor de Temperaturas

#### Objetivo
Conversión entre escalas termométricas Celsius y Fahrenheit.

#### Fundamento Teórico
Fórmula de conversión termodinámica:

```
F = C × (9/5) + 32
```

#### Implementación

```python
def celcius_to_farenheit(celcius: float) -> float
```

#### Características Técnicas
- Redondeo a 2 decimales para precisión práctica
- Validación de entrada no nula
- Casos de prueba que incluyen puntos críticos:
  - Punto de congelación del agua: 0°C → 32°F
  - Punto de ebullición del agua: 100°C → 212°F
  - Temperaturas negativas y positivas

---

### Ejercicio 3: Cinemática - Caída Libre

#### Objetivo
Modelar y analizar el movimiento de caída libre con diferentes condiciones iniciales.

#### Fundamento Físico

Ecuación de posición en movimiento uniformemente acelerado:

```
y(t) = y₀ + v₀t - (1/2)gt²
```

Donde:
- `y₀`: Altura inicial (m)
- `v₀`: Velocidad inicial (m/s)
- `g`: Aceleración gravitatoria (9.81 m/s²)
- `t`: Tiempo (s)

#### Implementación

```python
def posicion(y0: float, v0: float, t: float, g: float = 9.81) -> float
```

#### Análisis Computacional

**Parámetros de simulación**:
- Altura inicial: 100 m
- Velocidades iniciales: [5, 7, 10, 15, 24] m/s
- Intervalo temporal: 0 a 10 s (Δt = 0.5 s)

**Estructura de datos**:
- DataFrame de Pandas con columnas:
  - `t (s)`: Tiempo
  - `v_i (X m/s)`: Posición para velocidad inicial X

**Visualización**:
- Gráfica multipanel con matplotlib
- Curvas diferenciadas por color y marcador
- Leyendas identificando cada velocidad inicial
- Ejes etiquetados con unidades físicas

#### Interpretación Física
- Velocidades iniciales mayores retrasan el momento de impacto
- Todas las trayectorias convergen a aceleración negativa constante
- El desplazamiento negativo indica caída por debajo del nivel de referencia

---

### Ejercicio 4: Programación Orientada a Objetos - Sistema de Partículas

#### Objetivo
Modelar partículas físicas con propiedades dinámicas y calcular energía cinética.

#### Fundamento Teórico

Energía cinética newtoniana:

```
Eₖ = (1/2)mv²
```

#### Implementación de la Clase

```python
class Particula:
    def __init__(self, masa, posicion, velocidad)
    def get_properties(self) -> dict
    def get_kinetic_energy(self) -> float
```

#### Características del Diseño
- **Encapsulación**: Atributos privados con doble guion bajo
- **Métodos de acceso**: Getters para propiedades y energía
- **Cálculo dinámico**: Energía cinética calculada on-demand

#### Función de Análisis

```python
def max_energy(particulas: list) -> None
```

Identifica la partícula con mayor energía cinética en un sistema de N partículas.

#### Ejemplo de Sistema
```
Partícula 1: m=0.1 kg, v=30 m/s  → Eₖ = 45 J
Partícula 2: m=5 kg, v=20 m/s    → Eₖ = 1000 J
Partícula 3: m=0.7 kg, v=300 m/s → Eₖ = 31500 J
Partícula 4: m=10 kg, v=88 m/s   → Eₖ = 38720 J (máxima)
```

---

### Ejercicio 5: Álgebra Lineal - Producto Matriz-Vector

#### Objetivo
Implementar la multiplicación matriz-vector sin librerías externas.

#### Fundamento Matemático

Producto matriz-vector:

```
b = Ax

bᵢ = Σⱼ aᵢⱼ xⱼ  (i = 1...n)
```

#### Implementación

```python
def mat_vec_product(A: list[list[float]], x: list[float]) -> list[float]
```

#### Algoritmo
1. Inicializar vector resultado b con ceros
2. Para cada fila i de A:
   - Calcular suma de productos aᵢⱼ × xⱼ
   - Almacenar en bᵢ
3. Retornar vector resultado

#### Validación con Matriz Identidad

```python
A = [[1, 0, 0],
     [0, 1, 0],
     [0, 0, 1]]

x = [1, 2, 3]

A·x = [1.0, 2.0, 3.0]  # Verificado: I·x = x
```

#### Complejidad Computacional
- Tiempo: O(n²) para matriz n×n
- Espacio: O(n) para vector resultado

---

## Estructura del Directorio

```
tarea_2/
├── main.ipynb           # Notebook principal con todos los ejercicios
├── requirements.txt     # Dependencias Python
└── README.md           # Esta documentación
```

## Requisitos del Sistema

### Entorno Python

- Python 3.8 o superior
- Jupyter Notebook / JupyterLab

### Dependencias

```bash
pip install -r requirements.txt
```

**Librerías requeridas**:
```
numpy>=1.19.0          # Computación numérica
pandas>=1.1.0          # Análisis de datos
matplotlib>=3.3.0      # Visualización
jupyter>=1.0.0         # Entorno de notebooks
```

## Ejecución

### Opción 1: Jupyter Notebook
```bash
jupyter notebook main.ipynb
```

### Opción 2: JupyterLab
```bash
jupyter lab main.ipynb
```

### Opción 3: VS Code
Abrir `main.ipynb` con la extensión de Jupyter

## Resultados y Visualizaciones

### Ejercicio 3: Gráfica de Caída Libre
El notebook genera una visualización comparativa de trayectorias para diferentes velocidades iniciales, mostrando:
- Fase de ascenso (si v₀ > 0)
- Punto de máxima altura
- Fase de caída libre
- Momento de impacto

### Ejercicio 4: Análisis Energético
Salida en consola identificando la partícula con mayor energía cinética, incluyendo sus propiedades completas y el valor de energía.

## Conceptos Físicos Aplicados

1. **Termodinámica**: Escalas de temperatura y conversiones
2. **Cinemática**: Movimiento uniformemente acelerado, caída libre
3. **Mecánica Clásica**: Energía cinética, sistemas de partículas
4. **Álgebra Lineal**: Transformaciones lineales, espacios vectoriales

## Conceptos Computacionales

1. **Algoritmos**: Búsqueda, iteración, procesamiento de datos
2. **Estructuras de Datos**: Listas, diccionarios, DataFrames
3. **POO**: Clases, encapsulación, métodos
4. **Visualización**: Matplotlib, gráficas multipanel
5. **Análisis Numérico**: Discretización temporal, evaluación de funciones

## Validación y Pruebas

Cada ejercicio incluye:
- Casos de prueba con resultados conocidos
- Validación contra soluciones analíticas
- Comparación con librerías estándar (NumPy) cuando aplica

## Buenas Prácticas Implementadas

- Type hints en todas las funciones
- Docstrings descriptivos (Google/NumPy style)
- Manejo de excepciones
- Código modular y reutilizable
- Nombres de variables descriptivos
- Comentarios explicativos en código complejo

## Aplicaciones Prácticas

- **Análisis de datos experimentales**: Procesamiento de mediciones de laboratorio
- **Simulaciones físicas**: Modelado de sistemas mecánicos
- **Visualización científica**: Presentación de resultados
- **Educación**: Herramienta didáctica para física computacional

## Referencias

### Física
- Serway, R. A., & Jewett, J. W. (2018). *Physics for Scientists and Engineers* (10th ed.)
- Halliday, D., Resnick, R., & Walker, J. (2013). *Fundamentals of Physics* (10th ed.)

### Computación Científica
- McKinney, W. (2017). *Python for Data Analysis* (2nd ed.). O'Reilly Media
- VanderPlas, J. (2016). *Python Data Science Handbook*. O'Reilly Media
- NumPy Documentation: [https://numpy.org/doc/](https://numpy.org/doc/)
- Pandas Documentation: [https://pandas.pydata.org/docs/](https://pandas.pydata.org/docs/)

## Extensiones Sugeridas

- [ ] Implementar análisis de incertidumbre en mediciones
- [ ] Agregar ajuste de curvas (curve fitting) a datos experimentales
- [ ] Extender a 3D la visualización de trayectorias
- [ ] Implementar métodos numéricos de integración (Simpson, Runge-Kutta)
- [ ] Crear sistema de partículas con interacciones (fuerzas)

