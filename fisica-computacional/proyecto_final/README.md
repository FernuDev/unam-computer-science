# Simulación 3D de Intercepción de Aeronaves

Este proyecto es un MVP educativo que muestra cómo modelar la intercepción tridimensional de una aeronave por un misil guiado por radar mediante ecuaciones diferenciales y métodos numéricos. Fue diseñado como base para cursos de física computacional y control.

## Objetivos didácticos
- Representar el movimiento relativo misil-objetivo con vectores en \(\mathbb{R}^3\).
- Implementar leyes de guiado simples (Navegación Proporcional) y maniobras evasivas.
- Resolver las ecuaciones diferenciales con integradores de paso fijo (Euler y RK4).
- Visualizar trayectorias, distancias y perfiles de aceleración, además de una animación 3D del encuentro.

## Modelo matemático

El estado del sistema está dado por las posiciones y velocidades de ambas plataformas:
\[
\mathbf{s} = [\mathbf{r}_T, \dot{\mathbf{r}}_T, \mathbf{r}_M, \dot{\mathbf{r}}_M]^T \in \mathbb{R}^{12}
\]

Las ecuaciones diferenciales se escriben como:
\[
\dot{\mathbf{r}}_T = \dot{\mathbf{r}}_T, \quad \ddot{\mathbf{r}}_T = \mathbf{a}_T( t )
\]
\[
\dot{\mathbf{r}}_M = \dot{\mathbf{r}}_M, \quad \ddot{\mathbf{r}}_M = \mathbf{a}_M( t, \mathbf{r}_T - \mathbf{r}_M, \dot{\mathbf{r}}_T - \dot{\mathbf{r}}_M )
\]

La aceleración del misil sigue la ley de Navegación Proporcional en 3D:
\[
\mathbf{a}_M = N \cdot V_c \; (\boldsymbol{\omega}_{LOS} \times \hat{\mathbf{r}}_{LOS})
\]
con \(N\) la ganancia PN, \(V_c\) la velocidad de cierre, y \(\boldsymbol{\omega}_{LOS}\) la velocidad angular de la línea de visión.

Para acercar el comportamiento a un sistema SAM real, el modelo mezcla la navegación proporcional con un perfil preprogramado y guiado predictivo:
- **Boost/loft**: el misil asciende con un empuje vertical y un ligero viraje bancado para ganar energía.
- **Weaving 3D**: entra en oscilaciones laterales y verticales que generan una trayectoria serpenteante.
- **Lead predictor**: se calcula un punto adelantado usando una estimación del tiempo de llegada y la velocidad del objetivo; el misil se orienta hacia ese punto.
- **Blend progresivo**: un factor de mezcla lleva el peso desde el perfil preprogramado hacia el guiado PN conforme avanza el tiempo o se reduce la distancia al objetivo.
Cuando el misil se acerca por debajo de la distancia terminal, el 100% del control vuelve a ser Navegación Proporcional.

## Estructura del código

```
config.py          # Parámetros por defecto (simulación, aeronave, misil, visualización)
dynamics.py        # Modelos dinámicos, maniobras evasivas y guiado proporcional
integrators.py     # Integradores Euler/RK4 y lazo principal de simulación
visualization.py   # Gráficas 3D, distancias, aceleraciones y animación
main.py            # Script CLI que configura, ejecuta y visualiza el escenario
README.md          # Documentación
requirements.txt   # Dependencias científicas
```

## Instalación

```bash
python -m venv .venv
source .venv/bin/activate  # En Windows usa .venv\\Scripts\\activate
pip install -r requirements.txt
```

## Ejecución rápida

```bash
python main.py --maneuver jinking --method rk4 --duration 90 --save-plots --save-animation --export-csv
```

Argumentos útiles:
- `--maneuver {spiral,jinking,descend_turn,sinusoidal}`: patrón evasivo.
- `--method {rk4,euler}`: integrador.
- `--dt <float>` y `--duration <float>`: resolución temporal.
- `--no-animate` / `--no-static`: controlan la visualización interactiva.
- `--save-plots`, `--save-animation`, `--export-csv`: generan artefactos en `outputs/`.

## Resultados esperados
- Trayectorias 3D mostrando la convergencia del misil hacia la aeronave.
- Curva distancia-tiempo que evidencia la ventana de intercepción.
- Perfiles de aceleración para analizar exigencias de maniobrabilidad.
- Animación donde el misil realiza un patrón loft + weaving y luego se alinea con un punto adelantado.
- Animación que resalta el vector Línea-de-Vista (LOS) durante el guiado.

### Parámetros relevantes del perfil SAM
En `config.py` (`MissileSettings`) puedes ajustar:
- `align_initial_heading`: hace que el misil salga apuntando hacia el objetivo automáticamente.
- `boost_duration`, `boost_accel`, `bank_accel`: definen cuánto dura y qué tan agresivo es el ascenso inicial.
- `warmup_duration`: tiempo durante el cual el guiado PN se mantiene con peso reducido.
- `loft_angle_deg`: ángulo objetivo del ascenso.
- `weave_duration`, `weave_frequency`, `weave_accel`: controlan la cantidad e intensidad de los giros sinusoidales.
- `pn_blend_duration`, `pn_warmup_bias`: gobiernan cómo se transfiere el control del perfil hacia PN.
- `terminal_distance`: una vez alcanzada, el perfil se desactiva y solo queda PN.
- `min_closing_speed`: evita que la PN pierda efectividad si la distancia aumenta momentáneamente.
- `lead_time_constant`, `max_lead_time`, `lead_response`: configuran el predictor adelantado que dirige el misil hacia la posición futura estimada del objetivo.

## Referencias breves
- Blake, J. *Guidance of Tactical Missiles*, McGraw-Hill.
- Zarchan, P. *Tactical and Strategic Missile Guidance*, AIAA.
- Vallado, D. *Fundamentals of Astrodynamics and Applications*.

Este MVP es intencionalmente modular para que puedas extenderlo con integradores más avanzados (SciPy), modelos aerodinámicos o leyes de guiado alternativas.
