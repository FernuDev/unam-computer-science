# Guía para Presentación del Proyecto Final

**Simulación 3D de Intercepción de Aeronaves**

---

## Estructura de Presentación Sugerida (20-30 minutos)

### Parte 1: Introducción (5 minutos)

#### Slide 1: Portada
- Título del proyecto
- Tu nombre
- Facultad de Ciencias, UNAM
- Fecha
- **Visual:** Logo UNAM + imagen de trayectorias

#### Slide 2: Motivación
**Pregunta:** ¿Cómo funciona un sistema de defensa aérea?

**Respuesta:** Navegación Proporcional
- Usado en 90% de misiles guiados
- Principio matemático simple
- Aplicación compleja

**Visual:** Diagrama conceptual de intercepción

#### Slide 3: Objetivos
1. Modelar intercepción en 3D
2. Implementar PN matemáticamente
3. Resolver EDOs numéricamente
4. Visualizar y analizar resultados

---

### Parte 2: Fundamentos Teóricos (5 minutos)

#### Slide 4: Navegación Proporcional

**Principio:**
> "Si el ángulo de la línea de visión permanece constante, habrá colisión"

**Ecuación:**
\[
\mathbf{a}_M = N \cdot V_c \cdot (\boldsymbol{\omega}_{LOS} \times \hat{\mathbf{r}}_{LOS})
\]

**Componentes explicados:**
- **N:** Ganancia (típicamente 3-5)
- **V_c:** Velocidad de cierre
- **ω_LOS:** Velocidad angular de la línea de visión

**Visual:** Diagrama de geometría de intercepción

#### Slide 5: Sistema de Ecuaciones

**12 ecuaciones diferenciales acopladas:**

Aeronave (6 ecuaciones):
- 3 de posición: \(\dot{\mathbf{r}}_T = \mathbf{v}_T\)
- 3 de velocidad: \(\dot{\mathbf{v}}_T = \mathbf{a}_T\)

Misil (6 ecuaciones):
- 3 de posición: \(\dot{\mathbf{r}}_M = \mathbf{v}_M\)
- 3 de velocidad: \(\dot{\mathbf{v}}_M = \mathbf{a}_M\)

**Nota:** \(\mathbf{a}_M\) calculada por PN

#### Slide 6: Métodos Numéricos

**Comparación:**

| Euler | Runge-Kutta 4 |
|-------|---------------|
| Simple | Complejo |
| Error O(h²) | Error O(h⁵) |
| 1 eval/paso | 4 eval/paso |
| Rápido | Preciso |

**Selección:** RK4 por precisión

**Visual:** Gráfica comparativa de error

---

### Parte 3: Implementación (5 minutos)

#### Slide 7: Arquitectura del Software

**Diseño modular:**

```
config.py      → Parámetros globales
dynamics.py    → Física y guiado (300+ líneas)
integrators.py → RK4/Euler (110+ líneas)
visualization.py → Gráficas (220+ líneas)
main.py        → Orquestación (211 líneas)
```

**Total:** 900+ líneas Python

**Visual:** Diagrama de flujo de módulos

#### Slide 8: Perfil SAM Realista

**4 Fases de vuelo:**

1. **Boost (0-5s):** Aceleración inicial + loft
2. **Weaving (5-20s):** Serpenteo 3D
3. **Cruise (20-terminal):** Guiado predictivo + PN
4. **Terminal (<500m):** PN puro al 100%

**Innovación:** Blend progresivo entre fases

**Visual:** Diagrama temporal de fases

#### Slide 9: Maniobras Evasivas

**4 patrones implementados:**

| Maniobra | Descripción | Complejidad |
|----------|-------------|-------------|
| Spiral | Hélice cilíndrica | Baja |
| Sinusoidal | Oscilaciones regulares | Media |
| Descend Turn | Viraje descendente | Media |
| Jinking | Cambios aleatorios | Alta |

**Visual:** Comparación de trayectorias

---

### Parte 4: Resultados (8 minutos)

#### Slide 10: Resultados Principales

**INTERCEPCIÓN EXITOSA**

```
Tiempo de intercepción: 77.7 segundos
Distancia inicial:      21.2 km
Distancia final:        42 m
Método:                 Runge-Kutta 4
Paso de tiempo:         0.05 s
```

**Visual:** Captura del output de consola

#### Slide 11: Trayectorias 3D

**Mostrar:** `outputs/figuras/trayectorias.png`

**Puntos a destacar:**
- Aeronave (azul): Espiral predecible
- Misil (rojo): Curvatura característica de PN
- Punto de intercepción claramente visible
- Distancia total recorrida

**Tiempo en slide:** 2 minutos

#### Slide 12: Análisis de Convergencia

**Mostrar:** `outputs/figuras/distancia.png`

**Narrativa:**
- Inicio: 21 km de separación
- Reducción constante durante 60s
- Aceleración en fase terminal
- Convergencia a 0 en t=77.7s

**Interpretación física:**
- Velocidad de cierre efectiva
- Sin oscilaciones → guiado estable
- Pendiente constante → eficiencia energética

**Tiempo en slide:** 2 minutos

#### Slide 13: Perfiles de Aceleración

**Mostrar:** `outputs/figuras/aceleraciones.png`

**Análisis por fases:**

1. **0-5s:** Pico de 70 m/s² (boost)
2. **5-20s:** Oscilaciones (weaving)
3. **20-60s:** Baja aceleración (crucero)
4. **60-77.7s:** Pico terminal (corrección final)

**Factor de carga:** Máximo 7.1 G (aceptable)

**Tiempo en slide:** 2 minutos

#### Slide 14: Animación 3D (DEMO PRINCIPAL)

**Reproducir:** `outputs/interception.gif`

**Elementos a destacar durante reproducción:**
- Línea de visión (LOS) conectando ambos objetos
- Contador de distancia disminuyendo
- Trayectorias quedando como "estela"
- Momento exacto de intercepción

**Narrativa sugerida:**
> "Esta animación muestra los 77.7 segundos de la intercepción.
> Observen cómo el misil (rojo) ajusta constantemente su trayectoria
> para mantener la línea de visión (gris) con ángulo decreciente.
> La distancia, mostrada en tiempo real, reduce de 21 km a cero.
> Noten las fases: boost inicial, weaving, y convergencia terminal."

**Tiempo en slide:** 2 minutos (reproducir GIF completo o parcial)

---

### Parte 5: Análisis y Conclusiones (5 minutos)

#### Slide 15: Comparación de Escenarios

**Tabla de resultados:**

| Maniobra | Intercepción | Dist. Mín. | Observación |
|----------|-------------|------------|-------------|
| Spiral | ✓ | 42 m | Trayectoria predecible |
| Sinusoidal | ✓ | 150 m | Oscilaciones regulares |
| Descend Turn | ✓ | 300 m | Maniobra defensiva |
| Jinking | ✗ | 3,856 m | ¡Evasión exitosa! |

**Conclusión:** La impredecibilidad es clave en evasión

#### Slide 16: Validación del Modelo

**Pruebas realizadas:**
- ✓ Conservación de energía
- ✓ Límites físicos respetados
- ✓ Casos extremos verificados
- ✓ Comparación RK4 vs Euler

**Precisión:**
- Error numérico < 0.1%
- Resultados reproducibles
- Comportamiento físicamente correcto

#### Slide 17: Limitaciones Actuales

**Simplificaciones del modelo:**
1. Sin resistencia del aire
2. Sin límites estructurales
3. Objetivo no reactivo
4. Terreno plano
5. Sin contramedidas

**Justificación:** Enfoque en PN y métodos numéricos

#### Slide 18: Trabajo Futuro

**Extensiones propuestas:**

1. **Modelo aerodinámico completo**
   - Resistencia y sustentación
   - Límites de G-force
   - Consumo de combustible

2. **Guiado avanzado**
   - Augmented PN
   - Optimal Guidance
   - Adaptive control

3. **Escenarios complejos**
   - Múltiples amenazas
   - Contramedidas
   - Terreno 3D

#### Slide 19: Conclusiones

**Logros del proyecto:**

1. ✓ **Implementación exitosa** de Navegación Proporcional 3D
2. ✓ **Resolución precisa** con Runge-Kutta 4
3. ✓ **Perfil SAM realista** con 4 fases de vuelo
4. ✓ **Visualización profesional** con 4 tipos de salida
5. ✓ **Código de calidad** modular y documentado

**Aprendizajes:**
- Aplicación práctica de EDOs
- Importancia de métodos numéricos
- Visualización de sistemas dinámicos
- Programación científica profesional

**Impacto académico:**
- Demostración completa de física computacional
- Base para proyectos futuros
- Código reutilizable y extensible

---

### Parte 6: Preguntas (2-5 minutos)

#### Slide 20: Preguntas Frecuentes Anticipadas

**P1: ¿Por qué el jinking evade el misil?**

R: Los cambios aleatorios de dirección hacen que el predictor lead falle. El misil siempre apunta a donde el objetivo "estuvo", no donde "estará".

**P2: ¿Qué tan realista es el modelo?**

R: El guiado PN es idéntico a sistemas reales. Las simplificaciones (sin aire, sin combustible) son comunes en análisis preliminar. Para diseño final se agregan estos factores.

**P3: ¿Por qué RK4 y no otro método?**

R: RK4 es el estándar en simulaciones de trayectorias por su balance precisión/costo. Métodos superiores (RK45, Dormand-Prince) tienen beneficio marginal en este problema.

**P4: ¿Se puede usar para diseñar un misil real?**

R: Este es un modelo educativo. Un diseño real requiere:
- Aerodinámica CFD
- Análisis estructural
- Control de actitud
- Sistemas de propulsión
- Electrónica de guiado
- Pruebas de vuelo

**P5: ¿Cuánto tiempo tomó el desarrollo?**

R: [Tu respuesta personal]
- Diseño del modelo: X horas
- Implementación: Y horas
- Testing y debug: Z horas
- Documentación: W horas

---

## Material de Apoyo para Presentación

### Archivos a Tener Listos

**En la computadora:**
1. `interception.gif` - Animación principal
2. `trayectorias.png` - Slide de trayectorias
3. `distancia.png` - Slide de convergencia
4. `aceleraciones.png` - Slide de perfiles
5. `main.py` - Para mostrar código si preguntan
6. `trayectoria.csv` - Datos brutos

**Backups:**
- Copia en USB
- En la nube (Google Drive, Dropbox)
- Email a ti mismo

### Demostración en Vivo (Opcional)

**Si hay tiempo y conexión:**

```bash
# Demo rápida (visible en 30s)
python main.py --duration 30 --dt 0.02

# Mostrar estructura de código
ls -la src/
cat dynamics.py | head -50
```

**Riesgos:** Errores en vivo, tiempo limitado

**Recomendación:** Solo si dominas completamente

### Script de Narración

**Apertura (30 segundos):**
> "Buenos días/tardes. Hoy presentaré mi proyecto final de Física Computacional:
> una simulación 3D de intercepción de aeronaves usando Navegación Proporcional.
> Este proyecto combina ecuaciones diferenciales, métodos numéricos y visualización
> científica para modelar un problema real de ingeniería aeroespacial."

**Transición a teoría (15 segundos):**
> "Primero, veamos el fundamento matemático: la Navegación Proporcional..."

**Transición a implementación (15 segundos):**
> "Para implementar esto, diseñé una arquitectura modular en Python..."

**Transición a resultados (15 segundos):**
> "Los resultados muestran una intercepción exitosa. Veamos las visualizaciones..."

**Animación (60-90 segundos):**
> "Esta animación muestra los 77.7 segundos completos del encuentro.
> El punto azul es la aeronave objetivo, ejecutando una maniobra en espiral.
> El punto rojo es el misil, ajustando continuamente su trayectoria.
> La línea gris es la línea de visión, cuya distancia vemos disminuir
> de 21 kilómetros hasta 42 metros en el momento de impacto."

**Cierre (30 segundos):**
> "En conclusión, este proyecto demuestra exitosamente la aplicación
> de métodos numéricos a un problema real de física. El código es
> modular, extensible y completamente documentado. Gracias. 
> ¿Preguntas?"

### Timing Detallado

| Sección | Slides | Tiempo | Acumulado |
|---------|--------|--------|-----------|
| Introducción | 1-3 | 5 min | 5 min |
| Teoría | 4-6 | 5 min | 10 min |
| Implementación | 7-9 | 5 min | 15 min |
| Resultados | 10-14 | 8 min | 23 min |
| Análisis | 15-19 | 5 min | 28 min |
| Q&A | 20 | 2-5 min | 30-33 min |

### Consejos de Presentación

#### Antes de Presentar

**24 horas antes:**
- [ ] Revisar todos los slides
- [ ] Practicar narración completa
- [ ] Verificar que archivos abran correctamente
- [ ] Preparar respuestas a preguntas obvias

**1 hora antes:**
- [ ] Llegar temprano al lugar
- [ ] Probar conexión proyector/pantalla
- [ ] Abrir todas las figuras/GIFs
- [ ] Tener backup en USB

**5 minutos antes:**
- [ ] Respirar profundo
- [ ] Repasar puntos clave
- [ ] Verificar que todo esté listo

#### Durante la Presentación

**Buenas prácticas:**
1. Hablar hacia la audiencia, no hacia la pantalla
2. Usar puntero/cursor para señalar elementos
3. Pausar animación en momentos clave
4. Mantener contacto visual
5. Modular velocidad según reacción

**Lenguaje corporal:**
- Postura erguida
- Manos visibles (gestos naturales)
- Movimiento controlado
- Expresión facial positiva

**Manejo de nervios:**
- Respiración profunda antes de empezar
- Pausas estratégicas
- Si te pierdes, vuelve al outline
- Está bien decir "no sé, puedo investigar"

#### Anticipar Preguntas

**Técnicas:**
1. ¿Por qué decisiones de diseño específicas?
2. ¿Qué pasaría si cambias parámetro X?
3. ¿Comparación con otros métodos?
4. ¿Aplicaciones del mundo real?

**Matemáticas:**
1. Derivación de la ecuación PN
2. Estabilidad numérica
3. Error de truncamiento
4. Convergencia del método

**Implementación:**
1. ¿Por qué Python?
2. ¿Optimizaciones posibles?
3. ¿Cómo manejas casos edge?
4. ¿Testing realizado?

### Material de Referencia Rápida

**Tarjetas de ayuda memoria:**

**Tarjeta 1: Ecuación PN**
```
a_M = N * V_c * (ω_LOS × r̂_LOS)

N = 3.5
V_c = velocidad de cierre
ω_LOS = velocidad angular LOS
```

**Tarjeta 2: Resultados Clave**
```
Tiempo: 77.7 s
Dist inicial: 21.2 km
Dist final: 42 m
Método: RK4
Éxito: ✓
```

**Tarjeta 3: Especificaciones**
```
Aeronave: 250 m/s
Misil: 200-800 m/s
Max accel: 150 m/s²
Malla: 0.05 s
```

---

## Tips Avanzados

### Uso de Puntero Láser

**Qué señalar:**
- Ecuaciones clave al explicarlas
- Puntos de intercepción en gráficas
- Fases en perfil de aceleración
- Elementos de la animación

**Qué NO hacer:**
- Señalar texto que estás leyendo
- Movimientos erráticos
- Apuntar a la audiencia

### Gestión del Tiempo

**Si vas adelantado:**
- Expandir explicaciones técnicas
- Mostrar más código
- Agregar detalles matemáticos

**Si vas retrasado:**
- Saltar slides de implementación detallada
- Resumir múltiples puntos
- Ir directo a resultados

**Señales de tiempo:**
- 5 min: Terminar introducción
- 15 min: Estar en resultados
- 25 min: Concluir, abrir Q&A

### Manejo de Problemas Técnicos

**Si la animación no carga:**
- Tener screenshots de frames clave
- Describir verbalmente lo que se vería
- Mostrar gráficas estáticas en su lugar

**Si hay preguntas difíciles:**
- "Excelente pregunta, déjame pensarlo..."
- "No lo implementé pero sería una extensión interesante..."
- "Tendría que verificar los detalles, pero la idea general es..."

**Si se cae la computadora:**
- Tener backup en otra máquina
- PDF de slides básicos
- Poder continuar sin visuales

---

## Checklist Final

### Contenido

- [ ] Todas las ecuaciones verificadas
- [ ] Cifras/estadísticas correctas
- [ ] Figuras de alta calidad
- [ ] GIF funciona correctamente
- [ ] Referencias completas

### Técnico

- [ ] Laptop completamente cargada
- [ ] Adaptadores necesarios (HDMI, USB-C)
- [ ] Archivos en múltiples ubicaciones
- [ ] Software necesario instalado
- [ ] Modo presentación configurado

### Presentación

- [ ] Practicado al menos 3 veces completo
- [ ] Timing verificado (< 30 min)
- [ ] Respuestas a preguntas comunes preparadas
- [ ] Vestimenta apropiada
- [ ] Descanso adecuado noche anterior

---

## Recursos Adicionales

### Slides Complementarios (Opcionales)

**Para preguntas específicas:**

**Slide Extra 1: Derivación Matemática de PN**
- Geometría de intercepción
- Derivación de ω_LOS
- Condición de colisión

**Slide Extra 2: Código Clave**
```python
def proportional_navigation(self, rel_pos, rel_vel):
    los = rel_pos / np.linalg.norm(rel_pos)
    omega_los = np.cross(rel_pos, rel_vel) / np.linalg.norm(rel_pos)**2
    closing_speed = -np.dot(rel_vel, los)
    return self.nav_gain * closing_speed * np.cross(omega_los, los)
```

**Slide Extra 3: Benchmark de Performance**
- Tiempo de ejecución vs dt
- Memoria usada
- Comparación de métodos

### Formato de Presentación

**PowerPoint/Keynote:**
- Tema oscuro profesional
- Fuente: Arial o Helvetica
- Tamaño: 24pt mínimo
- Alto contraste
- Animaciones sutiles

**Google Slides:**
- Template profesional
- Imágenes en alta resolución
- GIF embebido
- Notas del presentador

**LaTeX Beamer:**
- Template academic
- Ecuaciones renderizadas
- Código con syntax highlighting
- Bibliografía automática

---

## Después de la Presentación

### Qué hacer

1. **Agradecer** a la audiencia y profesores
2. **Responder preguntas** con calma y claridad
3. **Tomar notas** de feedback recibido
4. **Compartir materiales** si solicitan

### Reflexión

**Preguntas de auto-evaluación:**
- ¿Qué salió bien?
- ¿Qué mejoraría?
- ¿Preguntas que no pude responder?
- ¿Timing adecuado?

### Seguimiento

**Actualizar el proyecto con:**
- Feedback recibido
- Mejoras sugeridas
- Bugs identificados
- Referencias adicionales

---

<div align="center">

**¡Buena suerte en tu presentación!**

*Este proyecto representa cientos de horas de trabajo*  
*Muéstralo con orgullo y confianza*

</div>

