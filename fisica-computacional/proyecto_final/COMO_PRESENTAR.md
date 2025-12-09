# Cómo Preparar y Presentar el Proyecto

**Guía práctica paso a paso**

---

## Paso 1: Convertir SLIDES.md a Presentación

### Opción A: PowerPoint/Keynote (Recomendado)

**Usando Pandoc:**

```bash
# Instalar pandoc (una vez)
# macOS:
brew install pandoc

# Linux:
sudo apt-get install pandoc

# Convertir a PowerPoint
pandoc SLIDES.md -o Presentacion_Proyecto_Final.pptx

# Convertir a PDF
pandoc SLIDES.md -o Presentacion_Proyecto_Final.pdf
```

**Después de convertir:**
1. Abrir en PowerPoint/Keynote
2. Insertar las imágenes manualmente:
   - Slide 13: `outputs/figuras/trayectorias.png`
   - Slide 14: `outputs/figuras/distancia.png`
   - Slide 15: `outputs/figuras/aceleraciones.png`
   - Slide 16: `outputs/interception.gif`
3. Ajustar formato y colores
4. Agregar logos UNAM

### Opción B: Google Slides

1. Convertir a PowerPoint con pandoc
2. Subir a Google Drive
3. Abrir con Google Slides
4. Editar online
5. Compartir link o descargar PDF

### Opción C: Marp (Markdown a Slides)

**Instalar Marp:**
```bash
# Usando npm
npm install -g @marp-team/marp-cli

# O usar Marp para VS Code (extensión)
```

**Convertir:**
```bash
marp SLIDES.md -o presentacion.html
marp SLIDES.md -o presentacion.pdf
marp SLIDES.md -o presentacion.pptx
```

**Ventaja:** Mantiene formato Markdown, fácil de editar

### Opción D: reveal.js (Web Interactiva)

1. Usar editor online: [slides.com](https://slides.com)
2. O usar pandoc con template reveal:

```bash
pandoc SLIDES.md -t revealjs -s -o presentacion.html
```

3. Abrir `presentacion.html` en navegador
4. Navegar con flechas
5. Presionar 'F' para pantalla completa

---

## Paso 2: Practicar la Presentación

### Primera Práctica (Solo)

**Objetivo:** Familiarizarte con el contenido

1. Lee PRESENTACION.md completamente
2. Pasa cada slide sin tiempo límite
3. Practica explicar cada ecuación
4. Identifica partes difíciles

**Duración:** Sin límite, enfócate en entender

### Segunda Práctica (Con Tiempo)

**Objetivo:** Ajustar timing

1. Configura timer de 30 minutos
2. Presenta como si hubiera audiencia
3. Grábate en video (celular)
4. Anota tiempos por sección

**Revisar:**
- ¿Dónde te atoras?
- ¿Qué slides son lentos/rápidos?
- ¿Muletillas o pausas largas?

### Tercera Práctica (Con Audiencia)

**Objetivo:** Recibir feedback

1. Presenta a amigo/familiar/compañero
2. Pide que hagan preguntas
3. Anota qué no se entendió
4. Ajusta explicaciones

**Enfoque:** Claridad sobre velocidad

### Práctica Final (El Día Anterior)

**Objetivo:** Perfeccionar y memorizar transiciones

1. Presentación completa sin interrupciones
2. Con todas las imágenes/GIF
3. Practicar manejo del proyector (si posible)
4. Preparar respuestas a 5 preguntas obvias

---

## Paso 3: El Día de la Presentación

### Timeline Recomendado

**60 minutos antes:**
- [ ] Llegar al lugar
- [ ] Conectar laptop al proyector
- [ ] Probar que se ve bien
- [ ] Abrir todos los archivos necesarios
- [ ] Verificar audio (si hay animación con sonido)

**30 minutos antes:**
- [ ] Respirar, relajarse
- [ ] Revisar puntos clave (no leer todo)
- [ ] Beber agua
- [ ] Ir al baño

**10 minutos antes:**
- [ ] Posicionarse en el lugar
- [ ] Organizar materiales
- [ ] Silenciar celular
- [ ] Respiraciones profundas

**Durante tu turno:**
- [ ] Presentarse con confianza
- [ ] Hablar claro y pausado
- [ ] Hacer contacto visual
- [ ] Usar las manos naturalmente
- [ ] Pausar en slides clave

---

## Paso 4: Estructura de la Presentación

### Esquema de 25 Minutos

**Introducción (3 min):**
```
Slide 1: Portada (30s)
  - "Buenos días, soy [nombre]"
  - "Hoy presentaré mi proyecto final sobre simulación de intercepción"
  
Slide 2: Índice (30s)
  - Mencionar brevemente las secciones
  - "La presentación durará 25 minutos con 5 para preguntas"
  
Slide 3-4: Motivación y Objetivos (2 min)
  - Contextualizar el problema
  - Explicar por qué es importante
  - Listar objetivos claros
```

**Teoría (5 min):**
```
Slide 5-6: Navegación Proporcional (3 min)
  - Explicar el principio fundamental
  - Mostrar la ecuación
  - Describir cada término
  - "Esta es la ecuación central del proyecto..."
  
Slide 7-8: Sistema de EDOs y Métodos (2 min)
  - 12 ecuaciones diferenciales
  - Por qué RK4 sobre Euler
  - Trade-off precisión vs costo
```

**Implementación (4 min):**
```
Slide 9: Arquitectura (2 min)
  - Diagrama de módulos
  - "Diseñé una arquitectura modular con 5 componentes..."
  - Mencionar líneas de código
  
Slide 10-11: Perfil SAM y Maniobras (2 min)
  - 4 fases del misil
  - 4 tipos de maniobras evasivas
  - "Para hacer la simulación realista..."
```

**Resultados (10 min) ← MÁS IMPORTANTE:**
```
Slide 12: Resultado Principal (1 min)
  - "INTERCEPCIÓN LOGRADA EN 77.7 SEGUNDOS"
  - Enfatizar el éxito
  - Mencionar precisión
  
Slide 13: Trayectorias 3D (2 min)
  - PAUSAR aquí
  - Explicar cada elemento de la gráfica
  - Señalar con puntero
  - "Observen cómo el misil curva su trayectoria..."
  
Slide 14: Convergencia (2 min)
  - Explicar la gráfica de distancia
  - "Comenzamos a 21 kilómetros..."
  - "La convergencia es monotónica, lo que indica..."
  
Slide 15: Aceleraciones (1 min)
  - Mencionar las 4 fases visibles
  - Factor de carga máximo
  
Slide 16: ANIMACIÓN (4 min) ← PUNTO CULMINANTE
  - "Ahora veamos la animación completa..."
  - REPRODUCIR el GIF
  - Narrar mientras corre:
    * "Aquí vemos el lanzamiento inicial..."
    * "Noten el weaving en esta fase..."
    * "Ahora entra en guiado puro..."
    * "Y finalmente... ¡intercepción!"
```

**Análisis (3 min):**
```
Slide 17: Comparación de Maniobras (2 min)
  - Tabla de resultados
  - "El jinking fue la única maniobra que evadió el misil"
  
Slide 18: Validación (1 min)
  - Mencionar las pruebas
  - Enfatizar rigor científico
```

**Cierre (2 min):**
```
Slide 19: Conclusiones (1 min)
  - Recapitular logros
  - "Este proyecto demuestra exitosamente..."
  
Slide 20: Gracias + Q&A (1 min)
  - "¿Preguntas?"
  - Mantener slide visible
```

---

## Paso 5: Responder Preguntas

### Preguntas Probables y Respuestas Sugeridas

**P1: ¿Por qué RK4 y no otro método?**

R: "Excelente pregunta. RK4 es el estándar de facto para simulaciones de trayectorias porque ofrece un balance óptimo entre precisión (error de orden 4) y costo computacional (4 evaluaciones por paso). Métodos más avanzados como RK45 o Dormand-Prince ofrecen paso adaptativo, pero para este problema con dinámica suave, el beneficio es marginal mientras que la implementación es significativamente más compleja."

**P2: ¿Qué tan realista es tu modelo?**

R: "El modelo captura correctamente la física fundamental del guiado proporcional, que es idéntica a la usada en sistemas reales. Sin embargo, he hecho simplificaciones educacionalmente justificables: no incluyo resistencia del aire, límites estructurales, ni consumo de combustible. Estas simplificaciones son estándar en análisis preliminar. Un diseño de ingeniería real agregaría estos factores, pero las ecuaciones de guiado serían las mismas."

**P3: ¿Por qué el jinking evade el misil?**

R: "El jinking es efectivo porque introduce cambios aleatorios e impredecibles en la trayectoria del objetivo. El predictor lead del misil intenta anticipar la posición futura basándose en la velocidad actual, pero con cambios aleatorios, esta predicción falla constantemente. El misil siempre apunta a donde el objetivo 'estuvo' en lugar de donde 'estará'. Esto es consistente con la táctica real de maniobras evasivas impredecibles."

**P4: ¿Cuánto tiempo te tomó este proyecto?**

R: [Personalizar con tu experiencia real]
"El proyecto me tomó aproximadamente [X] semanas. Distribuí el tiempo así:
- Investigación y diseño: [Y] días
- Implementación del código: [Z] días  
- Testing y debugging: [W] días
- Documentación y análisis: [V] días
La parte más desafiante fue [mencionar desafío específico]."

**P5: ¿Qué aplicaciones prácticas tiene esto?**

R: "Más allá de sistemas de defensa, las mismas técnicas se aplican en:
1. Control de drones autónomos
2. Vehículos autónomos (predicción de trayectorias)
3. Videojuegos (IA de enemigos que persiguen)
4. Robótica (interceptación de objetos)
5. Planificación de trayectorias en general
La PN es un controlador muy versátil para problemas de persecución."

**P6: ¿Podrías mostrar el código?**

R: "Por supuesto. La implementación de Navegación Proporcional está en `dynamics.py`, línea 145. [Mostrar en pantalla si es posible]. La función toma la posición y velocidad relativas, calcula la velocidad angular de la LOS, y retorna el comando de aceleración. Son unas 15 líneas de código, muy elegante gracias a NumPy."

**P7: ¿Cómo validaste que funciona correctamente?**

R: "Realicé varias validaciones: 
1. Casos límite: objetivo estacionario intercepta en tiempo teórico
2. Sin guiado (N=0): el misil falla como se espera
3. Comparación Euler vs RK4: resultados consistentes
4. Verificación de límites físicos: aceleraciones y velocidades realistas
5. Reproducibilidad: misma configuración da mismos resultados
Además comparé con literatura técnica de Zarchan y Blake."

---

## Paso 6: Después de Presentar

### Inmediatamente Después

1. **Agradecer** a profesores y audiencia
2. **Quedarse disponible** para preguntas adicionales
3. **Tomar nota mental** de feedback recibido
4. **No auto-criticarte** en público

### Ese Mismo Día

1. **Escribir reflexión:**
   - Qué salió bien
   - Qué mejorarías
   - Preguntas que no anticipaste
   
2. **Actualizar documentación:**
   - Agregar aclaraciones que fueron necesarias
   - Incorporar preguntas frecuentes
   - Corregir errores identificados

3. **Celebrar:**
   - ¡Completaste un proyecto complejo!
   - Mereces reconocimiento

---

## Consejos de Expertos

### Lenguaje Corporal

**Hacer:**
- Mantener contacto visual con diferentes personas
- Usar gestos naturales con las manos
- Moverse sutilmente (no quedarse estático)
- Sonreír apropiadamente
- Postura erguida y abierta

**Evitar:**
- Dar la espalda a la audiencia
- Leer textualmente los slides
- Manos en bolsillos todo el tiempo
- Balancearse o mecerse
- Gestos repetitivos nerviosos

### Uso de la Voz

**Técnicas efectivas:**
- **Variación de tono:** No monotonía
- **Pausas estratégicas:** Después de puntos clave
- **Énfasis:** En resultados importantes
- **Velocidad:** Más lento que conversación normal
- **Volumen:** Proyectar sin gritar

**Práctica:**
Grábate y escucha. ¿Suenas monótono? ¿Muy rápido?

### Manejo del Estrés

**Antes:**
- Respiración profunda 4-7-8 (inhala 4s, mantén 7s, exhala 8s)
- Visualización positiva
- Recordar que dominas el tema

**Durante:**
- Si te trabas: pausa, respira, continúa
- Si olvidas algo: consulta notas sin disculparte
- Si hay problema técnico: mantén calma, ten plan B

**Frases de respaldo:**
- "Déjame verificar ese detalle..."
- "Esa es una pregunta interesante que podría explorar más..."
- "Basándome en los resultados que vimos..."

---

## Elementos Visuales

### Insertar Figuras en Slides

**Para cada figura, al insertar:**

1. **Título descriptivo claro**
2. **Ejes etiquetados**
3. **Leyenda visible**
4. **Tamaño apropiado** (no muy pequeño)
5. **Alta resolución** (usar PNG de 300 DPI)

**Orden de importancia:**
1. trayectorias.png - CRÍTICA
2. distancia.png - CRÍTICA  
3. interception.gif - CRÍTICA
4. aceleraciones.png - Importante
5. Diagramas conceptuales - Útil

### GIF en Presentación

**PowerPoint:**
1. Insertar como imagen normal
2. Se reproduce automáticamente
3. Opción: Convertir a video MP4 (mejor)

**Keynote:**
1. Insertar como archivo multimedia
2. Configurar reproducción automática

**Google Slides:**
1. Subir GIF a Drive
2. Insertar desde Drive
3. Puede requerir conversión a video

**Comando para convertir GIF a MP4:**
```bash
ffmpeg -i interception.gif -movflags faststart -pix_fmt yuv420p \
       -vf "scale=trunc(iw/2)*2:trunc(ih/2)*2" interception.mp4
```

---

## Materiales de Apoyo

### Notas del Presentador

**Preparar fichas (físicas o digitales) con:**

**Ficha 1: Ecuación Principal**
```
PN 3D: a_M = N · V_c · (ω_LOS × r̂_LOS)

Memorizar:
- N = ganancia (3.5 en mi modelo)
- V_c = velocidad de cierre
- ω_LOS = velocidad angular LOS
```

**Ficha 2: Resultados Clave**
```
✓ Intercepción: 77.7s
✓ Distancia: 21.2km → 42m
✓ Método: RK4
✓ Maniobra: Spiral
✗ Jinking: 3.8km (fallo)
```

**Ficha 3: Especificaciones**
```
Sistema: 12 EDOs
Código: 938 líneas
Docs: 2,500+ líneas
Tiempo: <2s
Precisión: 83.6%
```

### Puntero Láser

**Qué señalar:**
- Ecuaciones mientras las explicas
- Elementos de gráficas (trayectorias, puntos)
- Cambios en la animación
- Filas importantes en tablas

**Qué NO señalar:**
- Texto que estás leyendo
- Todo constantemente (distrae)

### Backup Plan

**Si falla la tecnología:**

1. **Laptop se apaga:**
   - Tener backup en USB
   - O en otra laptop
   - O continuar sin slides (último recurso)

2. **Proyector no funciona:**
   - Tener impresiones de figuras clave
   - Mostrar en la pantalla de laptop
   - Describir verbalmente

3. **GIF no se reproduce:**
   - Tener screenshots de frames clave
   - Describir lo que se vería
   - Mostrar gráficas estáticas

---

## Evaluación y Rúbrica

### Criterios Típicos de Evaluación

| Aspecto | Peso | Qué Buscan |
|---------|------|------------|
| **Contenido técnico** | 40% | Corrección, profundidad, rigor |
| **Presentación oral** | 25% | Claridad, organización, timing |
| **Visuales** | 20% | Calidad, relevancia, legibilidad |
| **Manejo de preguntas** | 15% | Conocimiento, honestidad |

### Cómo Maximizar Puntos

**Contenido (40%):**
- Ecuaciones correctas y bien explicadas
- Resultados verificados y reproducibles
- Análisis profundo de datos
- Referencias bibliográficas serias

**Presentación (25%):**
- Estructura lógica clara
- Tiempo respetado (no pasarse ni quedarse corto)
- Transiciones suaves entre temas
- Voz clara y audible

**Visuales (20%):**
- Gráficas de alta calidad (✓ ya las tienes)
- Animación funcional (✓ ya la tienes)
- Slides no sobrecargados de texto
- Figuras grandes y legibles

**Preguntas (15%):**
- Responder con seguridad
- Si no sabes, ser honesto
- Relacionar con lo presentado
- Mostrar profundidad de conocimiento

---

## Frases Útiles Durante Presentación

### Transiciones

- "Pasando ahora a los resultados..."
- "Con esto en mente, veamos la implementación..."
- "Para ilustrar esto, observen esta animación..."
- "Como pueden ver en esta gráfica..."

### Énfasis

- "Es importante notar que..."
- "El resultado clave aquí es..."
- "Particularmente interesante es..."
- "Vale la pena destacar..."

### Para Explicar Complejos

- "En otras palabras..."
- "Para ponerlo en contexto..."
- "Pensemos en esto como..."
- "La intuición aquí es..."

### Manejo de Tiempo

- "Dado el tiempo, me enfocaré en..."
- "Brevemente..."
- "Para resumir..."
- "Los detalles están en el reporte escrito..."

---

## Checklist Final Completo

### Contenido

- [ ] SLIDES.md convertido a formato presentación
- [ ] Todas las imágenes insertadas
- [ ] GIF funciona en slides
- [ ] Ecuaciones renderizadas correctamente
- [ ] Tablas legibles
- [ ] Logos UNAM agregados

### Archivos

- [ ] presentacion.pptx / .pdf
- [ ] interception.gif
- [ ] trayectorias.png
- [ ] distancia.png
- [ ] aceleraciones.png
- [ ] trayectoria.csv (por si preguntan)
- [ ] Código fuente accesible

### Backup

- [ ] USB con todos los archivos
- [ ] Email a ti mismo con attachments
- [ ] Google Drive / Dropbox sincronizado
- [ ] Segunda laptop con archivos (ideal)

### Práctica

- [ ] Al menos 3 ensayos completos
- [ ] Timing verificado (< 25 min)
- [ ] Respuestas a preguntas preparadas
- [ ] Feedback incorporado

### Logística

- [ ] Ropa apropiada lista
- [ ] Laptop cargada
- [ ] Adaptadores empacados
- [ ] Agua para llevar
- [ ] Alarma configurada (llegar temprano)

### Mental

- [ ] Dormir bien noche anterior
- [ ] Desayunar/almorzar antes
- [ ] Actitud positiva
- [ ] Confianza en tu trabajo

---

## Día Después de Presentar

### Auto-Evaluación

**Preguntas de reflexión:**

1. ¿Qué salió mejor de lo esperado?
2. ¿Qué mejorarías para una próxima vez?
3. ¿Qué pregunta te sorprendió?
4. ¿Timing fue adecuado?
5. ¿Audiencia pareció interesada?

### Actualizar Proyecto

**Basado en feedback:**

```bash
# Crear branch para mejoras post-presentación
git checkout -b post-presentation-improvements

# Implementar sugerencias
# Agregar sección FAQ al README
# Mejorar documentación de puntos confusos

git commit -m "docs: incorporate presentation feedback"
```

### Compartir

**Si la presentación fue exitosa:**
- Subir a YouTube (si grabaste)
- Compartir en LinkedIn
- Actualizar portfolio
- Agregar a CV académico

---

<div align="center">

## ¡Mucha Suerte!

**Recuerda:**

*Conoces tu proyecto mejor que nadie*  
*Has hecho un trabajo excelente*  
*La preparación elimina la improvisación*  
*Confía en ti mismo*

**¡Vas a hacerlo genial!**

</div>

