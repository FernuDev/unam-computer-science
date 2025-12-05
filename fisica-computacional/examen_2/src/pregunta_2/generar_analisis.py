#!/usr/bin/env python3
"""
Script para generar el análisis de estados electrónicos
en dimetilbenceno y fragmento de grafeno (pino de navidad)
Genera energias.pdf con gráficas de |c_i|² para estado base y primer excitado
"""

import numpy as np
import matplotlib.pyplot as plt
from matplotlib.backends.backend_pdf import PdfPages
import matplotlib.patches as mpatches

# Configuración de matplotlib
plt.rcParams['figure.figsize'] = (14, 10)
plt.rcParams['font.size'] = 11
plt.rcParams['axes.titlesize'] = 13
plt.rcParams['axes.labelsize'] = 12

def leer_energias(filename):
    """Lee el archivo de energías"""
    data = []
    with open(filename, 'r') as f:
        for line in f:
            if not line.startswith('#'):
                parts = line.strip().split()
                if len(parts) == 2:
                    data.append([int(parts[0]), float(parts[1])])
    return np.array(data)

def leer_wavefunctions(filename):
    """Lee el archivo de funciones de onda"""
    data = []
    with open(filename, 'r') as f:
        for line in f:
            line = line.strip()
            if line and not line.startswith('#'):
                try:
                    values = [float(x) for x in line.split()]
                    if values:
                        data.append(values)
                except ValueError:
                    # Ignorar líneas que no puedan ser convertidas a float
                    continue
    return np.array(data)

# Leer datos del dimetilbenceno
print("Cargando datos del dimetilbenceno...")
energias_dimetil = leer_energias('dimetilbenceno_energias.dat')
wavefunc_dimetil = leer_wavefunctions('dimetilbenceno_wavefunctions.dat')

print(f"Dimetilbenceno: {len(energias_dimetil)} estados")
print(f"Energía estado base: E₀ = {energias_dimetil[0, 1]:.6f}")
print(f"Energía primer excitado: E₁ = {energias_dimetil[1, 1]:.6f}")

# Leer datos del pino de grafeno
print("\nCargando datos del pino de grafeno...")
energias_pino = leer_energias('pino_grafeno_energias.dat')
wavefunc_pino = leer_wavefunctions('pino_grafeno_wavefunctions.dat')

print(f"Pino de grafeno: {len(energias_pino)} estados")
print(f"Energía estado base: E₀ = {energias_pino[0, 1]:.6f}")
print(f"Energía primer excitado: E₁ = {energias_pino[1, 1]:.6f}")

# Calcular |c_i|² para ambas moléculas
prob_dimetil_0 = wavefunc_dimetil[:, 0]**2
prob_dimetil_1 = wavefunc_dimetil[:, 1]**2
prob_pino_0 = wavefunc_pino[:, 0]**2
prob_pino_1 = wavefunc_pino[:, 1]**2

# Crear PDF con múltiples páginas
print("\nGenerando PDF...")
with PdfPages('energias.pdf') as pdf:
    
    # ============================================================
    # PÁGINA 1: Título y resumen
    # ============================================================
    fig = plt.figure(figsize=(11, 8.5))
    fig.text(0.5, 0.85, 'Análisis de Estados Electrónicos en Moléculas', 
             ha='center', fontsize=20, fontweight='bold')
    fig.text(0.5, 0.78, 'Modelo Tight-Binding (Hückel)', 
             ha='center', fontsize=16, style='italic')
    fig.text(0.5, 0.72, 'Dimetilbenceno y Fragmento de Grafeno (Pino de Navidad)', 
             ha='center', fontsize=14)
    
    # Información de parámetros
    fig.text(0.1, 0.60, 'Parámetros del modelo:', fontsize=13, fontweight='bold')
    fig.text(0.15, 0.55, '• Energía de sitio: ε = 0', fontsize=11)
    fig.text(0.15, 0.51, '• Salto entre vecinos: π = -1', fontsize=11)
    fig.text(0.15, 0.47, '• Hamiltoniano: modelo de Hückel para electrones π', fontsize=11)
    
    # Resultados dimetilbenceno
    fig.text(0.1, 0.38, 'Dimetilbenceno (8 átomos):', fontsize=13, fontweight='bold')
    fig.text(0.15, 0.33, f'• Energía estado base (E₀): {energias_dimetil[0, 1]:.6f}', fontsize=11)
    fig.text(0.15, 0.29, f'• Energía primer excitado (E₁): {energias_dimetil[1, 1]:.6f}', fontsize=11)
    fig.text(0.15, 0.25, f'• Gap de energía: ΔE = {energias_dimetil[1, 1] - energias_dimetil[0, 1]:.6f}', fontsize=11)
    
    # Resultados pino de grafeno
    fig.text(0.1, 0.16, 'Pino de Grafeno (24 átomos):', fontsize=13, fontweight='bold')
    fig.text(0.15, 0.11, f'• Energía estado base (E₀): {energias_pino[0, 1]:.6f}', fontsize=11)
    fig.text(0.15, 0.07, f'• Energía primer excitado (E₁): {energias_pino[1, 1]:.6f}', fontsize=11)
    fig.text(0.15, 0.03, f'• Gap de energía: ΔE = {energias_pino[1, 1] - energias_pino[0, 1]:.6f}', fontsize=11)
    
    plt.axis('off')
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    # ============================================================
    # PÁGINA 2: Dimetilbenceno - Espectro de energías
    # ============================================================
    fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(14, 6))
    
    # Diagrama de niveles de energía
    n_estados = len(energias_dimetil)
    for i in range(n_estados):
        color = 'red' if i == 0 else 'blue' if i == 1 else 'gray'
        linewidth = 3 if i < 2 else 1.5
        ax1.hlines(energias_dimetil[i, 1], 0, 1, colors=color, linewidth=linewidth)
        label = f'E₀ = {energias_dimetil[i, 1]:.3f}' if i == 0 else \
                f'E₁ = {energias_dimetil[i, 1]:.3f}' if i == 1 else \
                f'E{i} = {energias_dimetil[i, 1]:.3f}'
        ax1.text(1.05, energias_dimetil[i, 1], label, fontsize=9, va='center')
    
    ax1.set_xlim(-0.5, 2)
    ax1.set_ylabel('Energía', fontsize=12)
    ax1.set_title('Espectro de Energías - Dimetilbenceno', fontsize=13, fontweight='bold')
    ax1.set_xticks([])
    ax1.grid(True, alpha=0.3, axis='y')
    ax1.axhline(y=0, color='k', linestyle='--', alpha=0.5, linewidth=0.8)
    
    # Gráfica de barras de energías
    colores = ['red' if i == 0 else 'blue' if i == 1 else 'lightgray' 
               for i in range(n_estados)]
    ax2.bar(range(1, n_estados+1), energias_dimetil[:, 1], color=colores, edgecolor='black')
    ax2.set_xlabel('Estado', fontsize=12)
    ax2.set_ylabel('Energía', fontsize=12)
    ax2.set_title('Distribución de Energías', fontsize=13, fontweight='bold')
    ax2.grid(True, alpha=0.3, axis='y')
    ax2.axhline(y=0, color='k', linestyle='--', alpha=0.5, linewidth=0.8)
    
    plt.suptitle('DIMETILBENCENO - Análisis de Energías', 
                 fontsize=15, fontweight='bold', y=0.98)
    plt.tight_layout()
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    # ============================================================
    # PÁGINA 3: Dimetilbenceno - Funciones de onda
    # ============================================================
    fig, ((ax1, ax2), (ax3, ax4)) = plt.subplots(2, 2, figsize=(14, 10))
    
    # Estado base - coeficientes
    atomos = np.arange(1, len(prob_dimetil_0) + 1)
    ax1.bar(atomos, wavefunc_dimetil[:, 0], color='darkred', alpha=0.7, edgecolor='black')
    ax1.axhline(y=0, color='k', linestyle='-', linewidth=0.8)
    ax1.set_xlabel('Átomo i', fontsize=11)
    ax1.set_ylabel('c_i', fontsize=11)
    ax1.set_title(f'Estado Base (E₀ = {energias_dimetil[0, 1]:.3f})\nCoeficientes de la función de onda', 
                  fontsize=12, fontweight='bold')
    ax1.grid(True, alpha=0.3, axis='y')
    ax1.set_xticks(atomos)
    
    # Estado base - densidad de probabilidad
    ax2.bar(atomos, prob_dimetil_0, color='darkred', alpha=0.7, edgecolor='black')
    ax2.set_xlabel('Átomo i', fontsize=11)
    ax2.set_ylabel('|c_i|²', fontsize=11)
    ax2.set_title(f'Estado Base (E₀ = {energias_dimetil[0, 1]:.3f})\nDensidad de probabilidad', 
                  fontsize=12, fontweight='bold')
    ax2.grid(True, alpha=0.3, axis='y')
    ax2.set_xticks(atomos)
    
    # Primer excitado - coeficientes
    ax3.bar(atomos, wavefunc_dimetil[:, 1], color='darkblue', alpha=0.7, edgecolor='black')
    ax3.axhline(y=0, color='k', linestyle='-', linewidth=0.8)
    ax3.set_xlabel('Átomo i', fontsize=11)
    ax3.set_ylabel('c_i', fontsize=11)
    ax3.set_title(f'Primer Excitado (E₁ = {energias_dimetil[1, 1]:.3f})\nCoeficientes de la función de onda', 
                  fontsize=12, fontweight='bold')
    ax3.grid(True, alpha=0.3, axis='y')
    ax3.set_xticks(atomos)
    
    # Primer excitado - densidad de probabilidad
    ax4.bar(atomos, prob_dimetil_1, color='darkblue', alpha=0.7, edgecolor='black')
    ax4.set_xlabel('Átomo i', fontsize=11)
    ax4.set_ylabel('|c_i|²', fontsize=11)
    ax4.set_title(f'Primer Excitado (E₁ = {energias_dimetil[1, 1]:.3f})\nDensidad de probabilidad', 
                  fontsize=12, fontweight='bold')
    ax4.grid(True, alpha=0.3, axis='y')
    ax4.set_xticks(atomos)
    
    plt.suptitle('DIMETILBENCENO - Funciones de Onda: ψ = Σ c_i φ_i', 
                 fontsize=15, fontweight='bold', y=0.995)
    plt.tight_layout()
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    # ============================================================
    # PÁGINA 4: Pino de Grafeno - Espectro de energías
    # ============================================================
    fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(14, 6))
    
    # Diagrama de niveles de energía (primeros 12)
    n_estados_pino = len(energias_pino)
    for i in range(min(12, n_estados_pino)):
        color = 'red' if i == 0 else 'blue' if i == 1 else 'gray'
        linewidth = 3 if i < 2 else 1.5
        ax1.hlines(energias_pino[i, 1], 0, 1, colors=color, linewidth=linewidth)
        label = f'E₀ = {energias_pino[i, 1]:.3f}' if i == 0 else \
                f'E₁ = {energias_pino[i, 1]:.3f}' if i == 1 else \
                f'E{i} = {energias_pino[i, 1]:.3f}'
        ax1.text(1.05, energias_pino[i, 1], label, fontsize=9, va='center')
    
    ax1.set_xlim(-0.5, 2.5)
    ax1.set_ylabel('Energía', fontsize=12)
    ax1.set_title('Espectro de Energías - Pino de Grafeno\n(primeros 12 niveles)', 
                  fontsize=13, fontweight='bold')
    ax1.set_xticks([])
    ax1.grid(True, alpha=0.3, axis='y')
    ax1.axhline(y=0, color='k', linestyle='--', alpha=0.5, linewidth=0.8)
    
    # Gráfica de barras de todas las energías
    colores_pino = ['red' if i == 0 else 'blue' if i == 1 else 'lightgray' 
                    for i in range(n_estados_pino)]
    ax2.bar(range(1, n_estados_pino+1), energias_pino[:, 1], 
            color=colores_pino, edgecolor='black', width=0.8)
    ax2.set_xlabel('Estado', fontsize=12)
    ax2.set_ylabel('Energía', fontsize=12)
    ax2.set_title('Distribución de Energías (todos los estados)', 
                  fontsize=13, fontweight='bold')
    ax2.grid(True, alpha=0.3, axis='y')
    ax2.axhline(y=0, color='k', linestyle='--', alpha=0.5, linewidth=0.8)
    
    plt.suptitle('PINO DE GRAFENO - Análisis de Energías', 
                 fontsize=15, fontweight='bold', y=0.98)
    plt.tight_layout()
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    # ============================================================
    # PÁGINA 5: Pino de Grafeno - Funciones de onda
    # ============================================================
    fig, ((ax1, ax2), (ax3, ax4)) = plt.subplots(2, 2, figsize=(14, 10))
    
    # Estado base - coeficientes
    atomos_pino = np.arange(1, len(prob_pino_0) + 1)
    ax1.bar(atomos_pino, wavefunc_pino[:, 0], color='darkred', alpha=0.7, edgecolor='black', width=0.8)
    ax1.axhline(y=0, color='k', linestyle='-', linewidth=0.8)
    ax1.set_xlabel('Átomo i', fontsize=11)
    ax1.set_ylabel('c_i', fontsize=11)
    ax1.set_title(f'Estado Base (E₀ = {energias_pino[0, 1]:.3f})\nCoeficientes de la función de onda', 
                  fontsize=12, fontweight='bold')
    ax1.grid(True, alpha=0.3, axis='y')
    
    # Estado base - densidad de probabilidad
    ax2.bar(atomos_pino, prob_pino_0, color='darkred', alpha=0.7, edgecolor='black', width=0.8)
    ax2.set_xlabel('Átomo i', fontsize=11)
    ax2.set_ylabel('|c_i|²', fontsize=11)
    ax2.set_title(f'Estado Base (E₀ = {energias_pino[0, 1]:.3f})\nDensidad de probabilidad', 
                  fontsize=12, fontweight='bold')
    ax2.grid(True, alpha=0.3, axis='y')
    
    # Primer excitado - coeficientes
    ax3.bar(atomos_pino, wavefunc_pino[:, 1], color='darkblue', alpha=0.7, edgecolor='black', width=0.8)
    ax3.axhline(y=0, color='k', linestyle='-', linewidth=0.8)
    ax3.set_xlabel('Átomo i', fontsize=11)
    ax3.set_ylabel('c_i', fontsize=11)
    ax3.set_title(f'Primer Excitado (E₁ = {energias_pino[1, 1]:.3f})\nCoeficientes de la función de onda', 
                  fontsize=12, fontweight='bold')
    ax3.grid(True, alpha=0.3, axis='y')
    
    # Primer excitado - densidad de probabilidad
    ax4.bar(atomos_pino, prob_pino_1, color='darkblue', alpha=0.7, edgecolor='black', width=0.8)
    ax4.set_xlabel('Átomo i', fontsize=11)
    ax4.set_ylabel('|c_i|²', fontsize=11)
    ax4.set_title(f'Primer Excitado (E₁ = {energias_pino[1, 1]:.3f})\nDensidad de probabilidad', 
                  fontsize=12, fontweight='bold')
    ax4.grid(True, alpha=0.3, axis='y')
    
    plt.suptitle('PINO DE GRAFENO - Funciones de Onda: ψ = Σ c_i φ_i', 
                 fontsize=15, fontweight='bold', y=0.995)
    plt.tight_layout()
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    # ============================================================
    # PÁGINA 6: Comparación entre moléculas
    # ============================================================
    fig = plt.figure(figsize=(14, 10))
    gs = fig.add_gridspec(3, 2, hspace=0.35, wspace=0.25)
    
    # Comparación de espectros
    ax1 = fig.add_subplot(gs[0, :])
    x_dimetil = np.arange(len(energias_dimetil))
    x_pino = np.arange(len(energias_pino))
    ax1.scatter(x_dimetil, energias_dimetil[:, 1], c='blue', s=100, 
                alpha=0.6, label='Dimetilbenceno', marker='o', edgecolors='black')
    ax1.scatter(x_pino, energias_pino[:, 1], c='green', s=100, 
                alpha=0.6, label='Pino de Grafeno', marker='s', edgecolors='black')
    ax1.axhline(y=0, color='k', linestyle='--', alpha=0.5, linewidth=0.8)
    ax1.set_xlabel('Estado', fontsize=12)
    ax1.set_ylabel('Energía', fontsize=12)
    ax1.set_title('Comparación de Espectros de Energía', fontsize=13, fontweight='bold')
    ax1.legend(fontsize=11)
    ax1.grid(True, alpha=0.3)
    
    # Tabla de comparación
    ax2 = fig.add_subplot(gs[1, :])
    ax2.axis('off')
    
    tabla_data = [
        ['Propiedad', 'Dimetilbenceno', 'Pino de Grafeno'],
        ['Número de átomos', f'{len(prob_dimetil_0)}', f'{len(prob_pino_0)}'],
        ['E₀ (estado base)', f'{energias_dimetil[0, 1]:.6f}', f'{energias_pino[0, 1]:.6f}'],
        ['E₁ (primer excitado)', f'{energias_dimetil[1, 1]:.6f}', f'{energias_pino[1, 1]:.6f}'],
        ['Gap ΔE = E₁ - E₀', 
         f'{energias_dimetil[1, 1] - energias_dimetil[0, 1]:.6f}',
         f'{energias_pino[1, 1] - energias_pino[0, 1]:.6f}'],
        ['Energía mínima', f'{energias_dimetil[0, 1]:.6f}', f'{energias_pino[0, 1]:.6f}'],
        ['Energía máxima', f'{energias_dimetil[-1, 1]:.6f}', f'{energias_pino[-1, 1]:.6f}'],
        ['Ancho de banda', 
         f'{energias_dimetil[-1, 1] - energias_dimetil[0, 1]:.6f}',
         f'{energias_pino[-1, 1] - energias_pino[0, 1]:.6f}'],
    ]
    
    tabla = ax2.table(cellText=tabla_data, cellLoc='center', loc='center',
                     colWidths=[0.35, 0.325, 0.325])
    tabla.auto_set_font_size(False)
    tabla.set_fontsize(10)
    tabla.scale(1, 2.5)
    
    # Estilizar la tabla
    for i in range(len(tabla_data)):
        for j in range(3):
            cell = tabla[(i, j)]
            if i == 0:
                cell.set_facecolor('#4472C4')
                cell.set_text_props(weight='bold', color='white')
            else:
                cell.set_facecolor('#E7E6E6' if i % 2 == 0 else 'white')
            cell.set_edgecolor('black')
            cell.set_linewidth(1.5)
    
    # Análisis cualitativo
    ax3 = fig.add_subplot(gs[2, :])
    ax3.axis('off')
    ax3.text(0.5, 0.9, 'Análisis Cualitativo', ha='center', fontsize=13, 
             fontweight='bold', transform=ax3.transAxes)
    
    analisis_text = f"""
Dimetilbenceno:
• Sistema pequeño (8 átomos) con espectro discreto bien definido
• El estado base muestra distribución uniforme de probabilidad en el anillo
• Gap de energía moderado: {energias_dimetil[1, 1] - energias_dimetil[0, 1]:.3f}
• Los grupos metilo contribuyen a la densidad electrónica

Pino de Grafeno:
• Sistema grande (24 átomos) con espectro más denso
• El estado base está deslocalizado sobre toda la estructura
• Gap de energía pequeño: {energias_pino[1, 1] - energias_pino[0, 1]:.3f}
• Comportamiento más cercano al grafeno (semi-metálico)
• Mayor ancho de banda indica mayor deslocalización electrónica
"""
    
    ax3.text(0.05, 0.65, analisis_text, fontsize=10, 
             verticalalignment='top', transform=ax3.transAxes,
             family='monospace')
    
    plt.suptitle('COMPARACIÓN ENTRE MOLÉCULAS', 
                 fontsize=15, fontweight='bold', y=0.98)
    pdf.savefig(fig, bbox_inches='tight')
    plt.close()
    
    print("PDF generado exitosamente!")
    
    # Metadata del PDF
    d = pdf.infodict()
    d['Title'] = 'Análisis de Estados Electrónicos en Moléculas'
    d['Author'] = 'Física Computacional - UNAM'
    d['Subject'] = 'Modelo Tight-Binding - Dimetilbenceno y Pino de Grafeno'
    d['Keywords'] = 'Tight-Binding, Hückel, Grafeno, Molecular Orbitals'

print("\n¡Análisis completado!")
print("Archivo generado: energias.pdf")

