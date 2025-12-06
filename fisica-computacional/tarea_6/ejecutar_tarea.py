#!/usr/bin/env python3
"""
Tarea 6 - Examen 3: Ecuación de Laplace 2D
Física Computacional - UNAM
"""

import numpy as np
import matplotlib
matplotlib.use('Agg')  # Backend sin GUI
import matplotlib.pyplot as plt
from matplotlib import cm
from mpl_toolkits.mplot3d import Axes3D
import pandas as pd
from scipy.sparse import lil_matrix
from scipy.linalg import norm
import warnings
warnings.filterwarnings('ignore')

print("="*60)
print("TAREA 6 - ECUACIÓN DE LAPLACE 2D")
print("="*60)

# Configuración de visualización
plt.rcParams['figure.figsize'] = (12, 8)
plt.rcParams['font.size'] = 10
plt.rcParams['axes.grid'] = True

print("\n1. Configuración del problema...")

# Parámetros del problema
Nx = 50
Ny = 50
Lx = 1.0
Ly = 1.0

hx = Lx / (Nx - 1)
hy = Ly / (Ny - 1)

x = np.linspace(0, Lx, Nx)
y = np.linspace(0, Ly, Ny)
X, Y = np.meshgrid(x, y)

N = Nx * Ny

print(f"   - Malla: {Nx} x {Ny}")
print(f"   - Espaciamiento: h = {hx:.4f}")
print(f"   - Sistema: {N} ecuaciones")

# Construir sistema
print("\n2. Construyendo sistema lineal...")

def construir_sistema_laplace(Nx, Ny, hx, hy):
    N = Nx * Ny
    A = lil_matrix((N, N))
    b = np.zeros(N)
    
    def idx(i, j):
        return i * Ny + j
    
    for i in range(Nx):
        for j in range(Ny):
            k = idx(i, j)
            
            if i == 0 or i == Nx-1 or j == 0 or j == Ny-1:
                A[k, k] = 1.0
                if j == Ny-1:
                    b[k] = np.sin(np.pi * i * hx)
                else:
                    b[k] = 0.0
            else:
                A[k, k] = -4.0
                A[k, idx(i+1, j)] = 1.0
                A[k, idx(i-1, j)] = 1.0
                A[k, idx(i, j+1)] = 1.0
                A[k, idx(i, j-1)] = 1.0
                b[k] = 0.0
    
    return A.tocsr(), b

A, b = construir_sistema_laplace(Nx, Ny, hx, hy)
print(f"   - Matriz A: {A.shape}")
print(f"   - Elementos no ceros: {A.nnz}")
print(f"   - Esparsidad: {100 * (1 - A.nnz / (N*N)):.2f}%")

# Métodos iterativos
print("\n3. Implementando métodos iterativos...")

def metodo_jacobi(A, b, x0=None, max_iter=1000, tol=1e-6):
    n = len(b)
    x = np.zeros(n) if x0 is None else x0.copy()
    x_new = np.zeros(n)
    residuos = []
    D = A.diagonal()
    
    for k in range(max_iter):
        for i in range(n):
            suma = A[i, :].dot(x) - D[i] * x[i]
            x_new[i] = (b[i] - suma) / D[i]
        
        r = b - A.dot(x_new)
        residuo_rel = norm(r) / norm(b)
        residuos.append(residuo_rel)
        
        if residuo_rel < tol:
            print(f"   Jacobi: {k+1} iteraciones")
            break
        
        x = x_new.copy()
    
    return x_new, residuos

def metodo_gauss_seidel(A, b, x0=None, max_iter=1000, tol=1e-6):
    n = len(b)
    x = np.zeros(n) if x0 is None else x0.copy()
    residuos = []
    A_lil = A.tolil()
    
    for k in range(max_iter):
        for i in range(n):
            suma = 0.0
            for j in A_lil.rows[i]:
                if j != i:
                    suma += A_lil[i, j] * x[j]
            x[i] = (b[i] - suma) / A_lil[i, i]
        
        r = b - A.dot(x)
        residuo_rel = norm(r) / norm(b)
        residuos.append(residuo_rel)
        
        if residuo_rel < tol:
            print(f"   Gauss-Seidel: {k+1} iteraciones")
            break
    
    return x, residuos

def metodo_gradiente_descendente(A, b, x0=None, max_iter=1000, tol=1e-6):
    n = len(b)
    x = np.zeros(n) if x0 is None else x0.copy()
    residuos = []
    
    for k in range(max_iter):
        r = b - A.dot(x)
        residuo_rel = norm(r) / norm(b)
        residuos.append(residuo_rel)
        
        if residuo_rel < tol:
            print(f"   Gradiente Descendente: {k+1} iteraciones")
            break
        
        Ar = A.dot(r)
        alpha = np.dot(r, r) / np.dot(r, Ar)
        x = x + alpha * r
    
    return x, residuos

# Resolver
print("\n4. Resolviendo sistema...")

max_iteraciones = 1000
tolerancia = 1e-6

sol_jacobi, res_jacobi = metodo_jacobi(A, b, max_iter=max_iteraciones, tol=tolerancia)
sol_gauss, res_gauss = metodo_gauss_seidel(A, b, max_iter=max_iteraciones, tol=tolerancia)
sol_gradiente, res_gradiente = metodo_gradiente_descendente(A, b, max_iter=max_iteraciones, tol=tolerancia)

# Convertir a matriz 2D
print("\n5. Generando visualizaciones...")

def vector_a_matriz(sol, Nx, Ny):
    U = np.zeros((Nx, Ny))
    for i in range(Nx):
        for j in range(Ny):
            k = i * Ny + j
            U[i, j] = sol[k]
    return U

U_gauss = vector_a_matriz(sol_gauss, Nx, Ny)

# Gráfica 1: Convergencia
plt.figure(figsize=(14, 6))

plt.subplot(1, 2, 1)
plt.semilogy(res_jacobi, 'b-', label='Jacobi', linewidth=2)
plt.semilogy(res_gauss, 'r-', label='Gauss-Seidel', linewidth=2)
plt.semilogy(res_gradiente, 'g-', label='Gradiente Descendente', linewidth=2)
plt.xlabel('Iteración', fontsize=12)
plt.ylabel('Residuo Relativo', fontsize=12)
plt.title('Convergencia de Métodos Iterativos (escala log)', fontsize=14, fontweight='bold')
plt.legend(fontsize=11)
plt.grid(True, alpha=0.3)

plt.subplot(1, 2, 2)
plt.plot(res_jacobi, 'b-', label='Jacobi', linewidth=2)
plt.plot(res_gauss, 'r-', label='Gauss-Seidel', linewidth=2)
plt.plot(res_gradiente, 'g-', label='Gradiente Descendente', linewidth=2)
plt.xlabel('Iteración', fontsize=12)
plt.ylabel('Residuo Relativo', fontsize=12)
plt.title('Convergencia de Métodos Iterativos (escala lineal)', fontsize=14, fontweight='bold')
plt.legend(fontsize=11)
plt.grid(True, alpha=0.3)

plt.tight_layout()
plt.savefig('convergencia_metodos.png', dpi=300, bbox_inches='tight')
print("   - Guardada: convergencia_metodos.png")
plt.close()

# Gráfica 2: Solución
fig = plt.figure(figsize=(16, 5))

# 3D
ax1 = fig.add_subplot(131, projection='3d')
surf = ax1.plot_surface(X, Y, U_gauss, cmap='viridis', edgecolor='none', alpha=0.9)
ax1.set_xlabel('X', fontsize=11)
ax1.set_ylabel('Y', fontsize=11)
ax1.set_zlabel('u(x,y)', fontsize=11)
ax1.set_title('Solución 3D de la Ecuación de Laplace', fontsize=12, fontweight='bold')
fig.colorbar(surf, ax=ax1, shrink=0.5)

# Contorno
ax2 = fig.add_subplot(132)
contour = ax2.contourf(X, Y, U_gauss, levels=20, cmap='viridis')
ax2.contour(X, Y, U_gauss, levels=10, colors='black', alpha=0.3, linewidths=0.5)
ax2.set_xlabel('X', fontsize=11)
ax2.set_ylabel('Y', fontsize=11)
ax2.set_title('Curvas de Nivel', fontsize=12, fontweight='bold')
ax2.set_aspect('equal')
fig.colorbar(contour, ax=ax2)

# Mapa de calor
ax3 = fig.add_subplot(133)
im = ax3.imshow(U_gauss, extent=[0, Lx, 0, Ly], origin='lower', cmap='hot', aspect='auto')
ax3.set_xlabel('X', fontsize=11)
ax3.set_ylabel('Y', fontsize=11)
ax3.set_title('Mapa de Calor', fontsize=12, fontweight='bold')
fig.colorbar(im, ax=ax3)

plt.tight_layout()
plt.savefig('solucion_laplace.png', dpi=300, bbox_inches='tight')
print("   - Guardada: solucion_laplace.png")
plt.close()

# Guardar datos
print("\n6. Guardando datos...")

df_solution = pd.DataFrame(U_gauss)
df_solution.index = [f'x_{i}' for i in range(Nx)]
df_solution.columns = [f'y_{j}' for j in range(Ny)]
df_solution.to_csv('solucion_laplace.csv')
print("   - Guardado: solucion_laplace.csv")

max_len = max(len(res_jacobi), len(res_gauss), len(res_gradiente))
df_convergencia = pd.DataFrame({
    'Iteracion': range(1, max_len + 1),
    'Jacobi': res_jacobi + [np.nan] * (max_len - len(res_jacobi)),
    'Gauss_Seidel': res_gauss + [np.nan] * (max_len - len(res_gauss)),
    'Gradiente_Descendente': res_gradiente + [np.nan] * (max_len - len(res_gradiente))
})
df_convergencia.to_csv('convergencia_metodos.csv', index=False)
print("   - Guardado: convergencia_metodos.csv")

# Optimización
print("\n7. Ejecutando algoritmos de optimización...")

def funcion_objetivo(x):
    return sum(100.0 * (x[1:] - x[:-1]**2)**2 + (1 - x[:-1])**2)

def optimizacion_aleatoria(func, dim=2, n_iter=100):
    mejor_x = np.random.uniform(-5, 5, dim)
    mejor_f = func(mejor_x)
    historia = [mejor_f]
    for _ in range(n_iter):
        x = np.random.uniform(-5, 5, dim)
        f = func(x)
        if f < mejor_f:
            mejor_f = f
            mejor_x = x
        historia.append(mejor_f)
    return mejor_x, mejor_f, historia

def optimizacion_gradiente_simple(func, dim=2, n_iter=100):
    x = np.random.uniform(-2, 2, dim)
    mejor_f = func(x)
    historia = [mejor_f]
    alpha = 0.01
    eps = 1e-5
    for _ in range(n_iter):
        grad = np.zeros(dim)
        f0 = func(x)
        for i in range(dim):
            x_plus = x.copy()
            x_plus[i] += eps
            grad[i] = (func(x_plus) - f0) / eps
        x = x - alpha * grad
        f = func(x)
        if f < mejor_f:
            mejor_f = f
        historia.append(mejor_f)
    return x, mejor_f, historia

def optimizacion_hibrida(func, dim=2, n_iter=100):
    x = np.random.uniform(-5, 5, dim)
    mejor_f = func(x)
    historia = [mejor_f]
    for i in range(n_iter):
        if i < n_iter // 2:
            x_new = x + np.random.normal(0, 1, dim)
        else:
            eps = 1e-5
            grad = np.zeros(dim)
            f0 = func(x)
            for j in range(dim):
                x_plus = x.copy()
                x_plus[j] += eps
                grad[j] = (func(x_plus) - f0) / eps
            x_new = x - 0.01 * grad
        f_new = func(x_new)
        if f_new < mejor_f:
            mejor_f = f_new
            x = x_new
        historia.append(mejor_f)
    return x, mejor_f, historia

n_ejecuciones = 10
dimensiones = 2
iteraciones = 200

resultados = {
    'Búsqueda Aleatoria': [],
    'Gradiente Simple': [],
    'Método Híbrido': []
}

historias = {}

for i in range(n_ejecuciones):
    _, f1, h1 = optimizacion_aleatoria(funcion_objetivo, dimensiones, iteraciones)
    resultados['Búsqueda Aleatoria'].append(f1)
    if i == 0:
        historias['Búsqueda Aleatoria'] = h1
    
    _, f2, h2 = optimizacion_gradiente_simple(funcion_objetivo, dimensiones, iteraciones)
    resultados['Gradiente Simple'].append(f2)
    if i == 0:
        historias['Gradiente Simple'] = h2
    
    _, f3, h3 = optimizacion_hibrida(funcion_objetivo, dimensiones, iteraciones)
    resultados['Método Híbrido'].append(f3)
    if i == 0:
        historias['Método Híbrido'] = h3

estadisticas = []
for nombre, valores in resultados.items():
    estadisticas.append({
        'Algoritmo': nombre,
        'Mejor': np.min(valores),
        'Peor': np.max(valores),
        'Media': np.mean(valores),
        'Mediana': np.median(valores),
        'Desv. Est.': np.std(valores)
    })

df_estadisticas = pd.DataFrame(estadisticas)
df_estadisticas.to_csv('estadisticas_optimizacion.csv', index=False)
print("   - Guardado: estadisticas_optimizacion.csv")

# Gráfica de optimización
plt.figure(figsize=(14, 6))

plt.subplot(1, 2, 1)
for nombre, historia in historias.items():
    plt.semilogy(historia, label=nombre, linewidth=2)
plt.xlabel('Iteración', fontsize=12)
plt.ylabel('Valor de la Función Objetivo', fontsize=12)
plt.title('Convergencia de Algoritmos de Optimización', fontsize=14, fontweight='bold')
plt.legend(fontsize=11)
plt.grid(True, alpha=0.3)

plt.subplot(1, 2, 2)
datos_boxplot = [resultados[nombre] for nombre in resultados.keys()]
plt.boxplot(datos_boxplot, labels=list(resultados.keys()))
plt.ylabel('Valor de la Función Objetivo', fontsize=12)
plt.title('Distribución de Resultados', fontsize=14, fontweight='bold')
plt.xticks(rotation=15, ha='right')
plt.grid(True, alpha=0.3, axis='y')

plt.tight_layout()
plt.savefig('optimizacion_comparacion.png', dpi=300, bbox_inches='tight')
print("   - Guardada: optimizacion_comparacion.png")
plt.close()

print("\n" + "="*60)
print("EJECUCIÓN COMPLETADA")
print("="*60)
print("\nArchivos generados:")
print("  - convergencia_metodos.png")
print("  - solucion_laplace.png")
print("  - optimizacion_comparacion.png")
print("  - solucion_laplace.csv")
print("  - convergencia_metodos.csv")
print("  - estadisticas_optimizacion.csv")
print("\nEstadísticas de la solución:")
print(f"  Valor máximo: {U_gauss.max():.6f}")
print(f"  Valor mínimo: {U_gauss.min():.6f}")
print(f"  Valor medio:  {U_gauss.mean():.6f}")

