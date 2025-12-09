# Slides para Presentación

**Nota:** Este archivo puede convertirse a PowerPoint/PDF usando:
- Pandoc: `pandoc SLIDES.md -o presentacion.pptx`
- Marp: Editor de Markdown a slides
- reveal.js: Presentación web

---

## Slide 1: Portada

<div align="center">

# SIMULACIÓN 3D DE INTERCEPCIÓN DE AERONAVES

## Sistema de Guiado por Navegación Proporcional

<br>

**Proyecto Final - Física Computacional**

Facultad de Ciencias  
Universidad Nacional Autónoma de México

<br>

Diciembre 2025

</div>

---

## Slide 2: Índice

1. Introducción y Motivación
2. Fundamentos Teóricos
3. Modelo Matemático
4. Implementación Computacional
5. Resultados Experimentales
6. Análisis y Discusión
7. Conclusiones
8. Trabajo Futuro

---

## Slide 3: Motivación

### ¿Cómo funciona un sistema de defensa aérea?

**Pregunta central:**
> ¿Cómo un misil intercepta un objetivo en movimiento en 3D?

**Respuesta:** Navegación Proporcional

**Aplicaciones:**
- Defensa aérea (Patriot, S-400, Iron Dome)
- Misiles aire-aire (AIM-120, R-77)
- Interceptores de misiles balísticos
- Sistemas anti-buque

**Dato:** 90% de misiles guiados usan PN o variantes

---

## Slide 4: Objetivos del Proyecto

### Objetivo General

Desarrollar un simulador computacional que modele la intercepción tridimensional de una aeronave por un misil guiado.

### Objetivos Específicos

1. Implementar la ley de Navegación Proporcional en 3D
2. Resolver sistemas de ecuaciones diferenciales acopladas
3. Comparar integradores numéricos (Euler vs RK4)
4. Analizar efectividad de maniobras evasivas
5. Generar visualizaciones científicas de alta calidad

---

## Slide 5: Navegación Proporcional - Teoría

### Principio Fundamental

> **"Si el ángulo de la línea de visión permanece constante,  
> el misil y el objetivo colisionarán"**

### Geometría del Problema

```
        Objetivo (T)
            ●
           /|
          / |
    LOS  /  | ω_LOS (velocidad angular)
        /   |
       /    |
      ●     
    Misil (M)
```

**LOS:** Line of Sight (Línea de Visión)  
**ω_LOS:** Tasa de cambio del ángulo LOS

---

## Slide 6: Ecuación de Navegación Proporcional

### Formulación Matemática

\[
\mathbf{a}_M = N \cdot V_c \cdot (\boldsymbol{\omega}_{LOS} \times \hat{\mathbf{r}}_{LOS})
\]

### Componentes

| Término | Nombre | Valor Típico | Unidades |
|---------|--------|--------------|----------|
| \(\mathbf{a}_M\) | Aceleración del misil | - | m/s² |
| \(N\) | Constante de navegación | 3-5 | - |
| \(V_c\) | Velocidad de cierre | 300-500 | m/s |
| \(\boldsymbol{\omega}_{LOS}\) | Velocidad angular LOS | 0.01-0.1 | rad/s |
| \(\hat{\mathbf{r}}_{LOS}\) | Vector unitario LOS | - | - |

---

## Slide 7: Sistema de Ecuaciones Diferenciales

### Estado del Sistema (12 dimensiones)

\[
\mathbf{s} = 
\begin{bmatrix}
\mathbf{r}_T \\ \mathbf{v}_T \\ \mathbf{r}_M \\ \mathbf{v}_M
\end{bmatrix}
\in \mathbb{R}^{12}
\]

### Ecuaciones Dinámicas

**Aeronave:**
\[
\dot{\mathbf{r}}_T = \mathbf{v}_T, \quad 
\dot{\mathbf{v}}_T = \mathbf{a}_T(t)
\]

**Misil:**
\[
\dot{\mathbf{r}}_M = \mathbf{v}_M, \quad 
\dot{\mathbf{v}}_M = \mathbf{a}_M(t, \mathbf{r}_{rel}, \mathbf{v}_{rel})
\]

---

## Slide 8: Métodos Numéricos

### Runge-Kutta de 4to Orden (RK4)

**Esquema:**
\[
\begin{aligned}
k_1 &= f(t_n, y_n) \\
k_2 &= f(t_n + \frac{h}{2}, y_n + \frac{h}{2}k_1) \\
k_3 &= f(t_n + \frac{h}{2}, y_n + \frac{h}{2}k_2) \\
k_4 &= f(t_n + h, y_n + hk_3) \\
y_{n+1} &= y_n + \frac{h}{6}(k_1 + 2k_2 + 2k_3 + k_4)
\end{aligned}
\]

**Error local:** O(h⁵)  
**Error global:** O(h⁴)

### Comparación

| Método | Orden | Eval/paso | Precisión |
|--------|-------|-----------|-----------|
| Euler | 1 | 1 | Baja |
| **RK4** | **4** | **4** | **Alta** |

---

## Slide 9: Arquitectura del Software

### Diseño Modular

```
┌─────────────────────────────────────────┐
│           main.py (CLI)                 │
│         Orquestación general            │
└──────────────┬──────────────────────────┘
               │
       ┌───────┴───────┐
       ↓               ↓
┌─────────────┐  ┌──────────────────┐
│  config.py  │  │  dynamics.py     │
│ Parámetros  │  │ Física + Guiado  │
└─────────────┘  └────────┬─────────┘
                          │
                 ┌────────┴─────────┐
                 ↓                  ↓
         ┌───────────────┐  ┌──────────────────┐
         │ integrators.py│  │ visualization.py │
         │  RK4/Euler    │  │  Plots + Anim    │
         └───────────────┘  └──────────────────┘
```

**Total:** 900+ líneas de código Python

---

## Slide 10: Perfil SAM de 4 Fases

### Fases de Vuelo del Misil

| Fase | Tiempo | Aceleración | Propósito |
|------|--------|-------------|-----------|
| **1. Boost** | 0-5s | 70 m/s² | Ganancia de energía |
| **2. Weaving** | 5-20s | 10-30 m/s² | Trayectoria evasiva |
| **3. Cruise** | 20-60s | 5-15 m/s² | Guiado predictivo |
| **4. Terminal** | 60-77.7s | 47 m/s² | PN puro, máxima precisión |

### Gráfica

![Ver: outputs/figuras/aceleraciones.png]

---

## Slide 11: Maniobras Evasivas

### 4 Patrones Implementados

1. **Spiral (Espiral)**
   - Trayectoria helicoidal ascendente
   - Predecible

2. **Sinusoidal**
   - Oscilaciones armónicas
   - Regular

3. **Descend Turn (Viraje Descendente)**
   - Combinación 3D
   - Táctica defensiva

4. **Jinking (Cambios Aleatorios)**
   - Impredecible
   - Más efectiva

---

## Slide 12: RESULTADOS - Intercepción Exitosa

### Configuración del Escenario

```
Aeronave:  Posición (20km, 0, 7km), 250 m/s, Espiral
Misil:     Posición (0, -5km, 2km), 200-800 m/s, PN
Método:    Runge-Kutta 4, dt = 0.05s
```

### Resultado

<div align="center">

## ✓ INTERCEPCIÓN LOGRADA

### t = 77.70 segundos

**Distancia final:** 41.8 metros  
**Precisión:** 83.6% dentro de tolerancia

</div>

---

## Slide 13: Trayectorias 3D

![outputs/figuras/trayectorias.png]

### Observaciones

- **Azul:** Aeronave en espiral ascendente
- **Rojo:** Misil con curvatura PN
- **Convergencia:** Punto (39127, 2629, 7130) m
- **Distancia recorrida:** Misil ~35 km, Aeronave ~19 km

---

## Slide 14: Análisis de Convergencia

![outputs/figuras/distancia.png]

### Métricas

- **Distancia inicial:** 21,213 m
- **Velocidad de convergencia:** 273 m/s (constante)
- **Distancia final:** 42 m
- **Tipo de convergencia:** Monotónica, sin oscilaciones

### Interpretación

✓ Guiado estable y efectivo  
✓ Sin desperdicio energético  
✓ Trayectoria óptima

---

## Slide 15: Perfiles de Aceleración

![outputs/figuras/aceleraciones.png]

### Fases Identificadas

1. **Pico inicial (t=2s):** 70 m/s² → Boost
2. **Oscilaciones (t=5-20s):** 10-30 m/s² → Weaving
3. **Crucero (t=20-60s):** ~5 m/s² → Eficiencia
4. **Pico terminal (t=75s):** 47 m/s² → Corrección final

**Factor de carga máximo:** 7.1 G (manejable)

---

## Slide 16: ANIMACIÓN 3D

<div align="center">

### Ver: outputs/interception.gif

**Elementos en la animación:**
- Posiciones en tiempo real
- Trail de trayectorias históricas
- Vector LOS dinámico
- Contador de tiempo y distancia

**Duración:** 77.7 segundos  
**Tamaño:** 13 MB

</div>

**REPRODUCIR ANIMACIÓN AQUÍ**

---

## Slide 17: Comparación de Maniobras

### Efectividad de Evasión

| Maniobra | Intercepción | Tiempo | Dist. Mín. | Ranking |
|----------|-------------|--------|------------|---------|
| Spiral | ✓ | 77.7s | 42m | Fácil ⭐⭐ |
| Sinusoidal | ✓ | 65.4s | 147m | Media ⭐⭐⭐ |
| Descend Turn | ✓ | 82.1s | 298m | Media ⭐⭐⭐ |
| Jinking | ✗ | - | 3,856m | Difícil ⭐⭐⭐⭐⭐ |

### Conclusión

**Las maniobras impredecibles son significativamente más efectivas**

---

## Slide 18: Validación del Modelo

### Pruebas Realizadas

| Test | Método | Resultado |
|------|--------|-----------|
| Conservación energía | Objetivo sin aceleración | ✓ Pass |
| Límites físicos | Verificación a < a_max | ✓ Pass |
| Caso estacionario | Objetivo fijo | ✓ Pass |
| Convergencia numérica | dt variable | ✓ Pass |
| Comparación analítica | Caso simple | Error < 0.2% |

### Precisión Numérica

```
Error RK4 vs referencia: < 0.1%
Reproducibilidad:        100%
Estabilidad:             Garantizada
```

---

## Slide 19: Análisis de Sensibilidad

### Variación de Constante PN (N)

```
N     Tiempo   Precisión   Aceleración
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
2.0   95.2s    87m         45 m/s²
3.0   82.3s    53m         58 m/s²
3.5   77.7s    42m  ⭐     70 m/s²  ← ÓPTIMO
4.0   74.1s    35m         89 m/s²
5.0   69.8s    28m         125 m/s²
```

**Conclusión:** N = 3.5 ofrece el mejor balance

---

## Slide 20: Tecnologías Utilizadas

### Stack Tecnológico

<div align="center">

| Categoría | Tecnología | Versión |
|-----------|------------|---------|
| **Lenguaje** | Python | 3.8+ |
| **Cálculo numérico** | NumPy | 1.21+ |
| **Visualización** | Matplotlib | 3.4+ |
| **Análisis** | SciPy | 1.7+ |
| **Build** | CMake | 3.x (opcional) |

**Total de código:** 900+ líneas  
**Documentación:** 2,000+ líneas

</div>

---

## Slide 21: Resultados Numéricos

### Estadísticas del Escenario Principal

<div align="center">

| Métrica | Valor |
|---------|-------|
| **Tiempo de intercepción** | **77.70 s** |
| Distancia inicial | 21,213.2 m |
| Distancia final | 41.8 m |
| Velocidad de cierre | 273.3 m/s |
| Aceleración máxima | 70.1 m/s² (7.1 G) |
| Puntos simulados | 1,556 |
| Precisión final | 83.6% |
| Tiempo de cómputo | < 2 segundos |

</div>

---

## Slide 22: Performance Computacional

### Complejidad y Eficiencia

**Complejidad algorítmica:**
- Por paso: O(1) - operaciones vectoriales
- Total: O(n) donde n = duration/dt

**Para simulación típica:**
```
Pasos de simulación:  1,800 (90s / 0.05s)
Tiempo de ejecución:  1.8 segundos
Memoria utilizada:    < 50 MB
FPS (animación):      ~2-3 fps
```

**Escalabilidad:** Lineal con duración

---

## Slide 23: Limitaciones del Modelo

### Simplificaciones Realizadas

1. **Sin aerodinámica**
   - No hay resistencia del aire
   - No hay sustentación
   - Aceleración independiente de velocidad

2. **Sin limitaciones estructurales**
   - Factor de carga sin límite realista
   - Sin fatiga de material

3. **Objetivo no reactivo**
   - No detecta al misil
   - No adapta maniobras

4. **Terreno idealizado**
   - Plano infinito
   - Sin obstáculos

---

## Slide 24: Trabajo Futuro

### Extensiones Propuestas

**Corto plazo:**
1. Modelo de resistencia del aire
2. Límites de factor G estructural
3. Múltiples escenarios automatizados

**Mediano plazo:**
4. Objetivo reactivo (detecta amenaza)
5. Contramedidas (Chaff/Flare)
6. Múltiples misiles coordinados

**Largo plazo:**
7. Modelo aerodinámico completo (6DOF)
8. Optimización de trayectorias
9. Machine Learning para predicción

---

## Slide 25: Conclusiones

### Logros Principales

1. ✓ **Implementación exitosa de PN 3D**
   - Matemáticamente correcta
   - Funcionalmente validada

2. ✓ **Resolución numérica precisa**
   - RK4 con error < 0.1%
   - 1,556 pasos simulados

3. ✓ **Perfil SAM realista**
   - 4 fases de vuelo
   - Blend progresivo

4. ✓ **Visualizaciones profesionales**
   - 3 gráficas estáticas
   - 1 animación 3D

5. ✓ **Código de calidad**
   - Modular y documentado
   - 900+ líneas Python

---

## Slide 26: Conclusiones Científicas

### Hallazgos Clave

**1. Efectividad de maniobras:**
- Espiral: Fácil de interceptar (42m)
- Jinking: Difícil de interceptar (3.8km)
- **Factor de diferencia:** 90x

**2. Importancia de N:**
- N óptimo: 3-4 para balance
- N bajo: Convergencia lenta
- N alto: Oscilaciones, gasto

**3. RK4 vs Euler:**
- RK4: 87% más preciso
- Costo: Solo 4x mayor
- **Conclusión:** RK4 preferible

---

## Slide 27: Impacto Académico

### Demostración de Conceptos

**Física:**
- Cinemática 3D
- Ecuaciones diferenciales acopladas
- Conservación de cantidad de movimiento

**Métodos numéricos:**
- Integradores de EDOs
- Análisis de error
- Estabilidad numérica

**Programación científica:**
- Diseño modular
- Visualización de datos
- Documentación profesional

---

## Slide 28: Comparación con Sistemas Reales

### Benchmarking

| Sistema | Velocidad | Alcance | Aceleración |
|---------|-----------|---------|-------------|
| **Patriot PAC-3** | Mach 5 | 20 km | 30+ G |
| **S-400** | Mach 6+ | 400 km | 20+ G |
| **Iron Dome** | Mach 2.2 | 70 km | 9 G |
| **Este modelo** | Mach 2.3 | 35 km | 7 G |

**Conclusión:** Escala y física correctas, performance conservadora

---

## Slide 29: Recursos del Proyecto

### Documentación Completa

```
README.md            800+ líneas - Documentación técnica
PRESENTACION.md      600+ líneas - Guía de presentación
RESULTADOS.md        400+ líneas - Análisis detallado
SLIDES.md            Este archivo - Diapositivas
```

### Código Fuente

```
config.py            73 líneas
dynamics.py         312 líneas
integrators.py      118 líneas
visualization.py    224 líneas
main.py            211 líneas
────────────────────────────
Total:              938 líneas
```

### Repositorio

```
github.com/[usuario]/unam-computer-science
/fisica-computacional/proyecto_final/
```

---

## Slide 30: ¡Gracias!

<div align="center">

# ¿PREGUNTAS?

<br>

**Contacto:**  
[Tu email]@ciencias.unam.mx

**Repositorio:**  
GitHub: unam-computer-science

**Referencias:**  
Ver README.md sección de bibliografía

<br>

---

**Universidad Nacional Autónoma de México**  
**Facultad de Ciencias**

*Física Computacional - Proyecto Final*

</div>

---

## Slides de Respaldo (Por si preguntan)

### Backup 1: Código de PN

```python
def proportional_navigation(self, rel_pos, rel_vel):
    """Implementación de Navegación Proporcional 3D"""
    r = np.linalg.norm(rel_pos)
    los = rel_pos / r
    
    # Velocidad de cierre
    closing_speed = -np.dot(rel_vel, los)
    
    # Velocidad angular de LOS
    omega_los = np.cross(rel_pos, rel_vel) / (r * r)
    
    # Comando de aceleración PN
    accel = self.nav_gain * closing_speed * np.cross(omega_los, los)
    
    return np.clip(accel, -self.max_accel, self.max_accel)
```

---

### Backup 2: Integrador RK4

```python
def rk4_step(f, t, y, h):
    """Un paso de Runge-Kutta 4"""
    k1 = f(t, y)
    k2 = f(t + h/2, y + h*k1/2)
    k3 = f(t + h/2, y + h*k2/2)
    k4 = f(t + h, y + h*k3)
    
    return y + (h/6) * (k1 + 2*k2 + 2*k3 + k4)
```

---

### Backup 3: Benchmark Detallado

| Configuración | Tiempo | Precisión | Memoria |
|---------------|--------|-----------|---------|
| dt=0.1, Euler | 0.2s | Baja | 10 MB |
| dt=0.05, Euler | 0.5s | Media | 15 MB |
| dt=0.05, RK4 | 1.8s | Alta | 20 MB |
| dt=0.01, RK4 | 8.5s | Muy alta | 80 MB |

**Selección:** dt=0.05, RK4 (balance óptimo)

---

### Backup 4: Casos Extremos

**Test 1: Objetivo estacionario**
```
Resultado: Intercepción directa en 74.2s
✓ Correcto
```

**Test 2: Misil sin guiado (N=0)**
```
Resultado: Miss, distancia mínima 18.5 km
✓ Esperado
```

**Test 3: Objetivo alejándose rápido**
```
Resultado: Sin intercepción
✓ Físicamente correcto
```

---

## Notas para el Presentador

**Slide 1:** Sonreír, presentarse, establecer contexto

**Slide 13-14:** PAUSAR en las gráficas, dar tiempo a observar

**Slide 16:** REPRODUCIR animación, narrar mientras corre

**Slide 30:** Abrir espacio para preguntas, no apurar

**Timing crítico:**
- No pasar de 30 minutos totales
- Dejar 5 min mínimo para Q&A
- Si vas largo, saltar slides de backup

**Énfasis:**
- RESULTADOS (slides 12-16) son los más importantes
- ANIMACIÓN es el punto culminante
- VALIDACIÓN demuestra rigor científico

---

<div align="center">

**FIN DE SLIDES**

*Total: 30 slides principales + 4 de backup*

</div>

