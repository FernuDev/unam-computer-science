#!/usr/bin/env python3
"""
Tarea 6 - Ecuación de Laplace 2D (Versión Simplificada)
"""

import numpy as np
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

print("="*60)
print("TAREA 6 - ECUACIÓN DE LAPLACE 2D")
print("="*60)

# Configuración
plt.rcParams['figure.figsize'] = (12, 8)

print("\n1. Configuración del problema...")
Nx = Ny = 50
Lx = Ly = 1.0
hx = Lx / (Nx - 1)
hy = Ly / (Ny - 1)

x = np.linspace(0, Lx, Nx)
y = np.linspace(0, Ly, Ny)
X, Y = np.meshgrid(x, y)

print(f"   - Malla: {Nx} x {Ny}")
print(f"   - Espaciamiento: h = {hx:.4f}")

# Sistema lineal
print("\n2. Construyendo sistema...")

def construir_sistema(Nx, Ny, hx):
    N = Nx * Ny
    A = np.zeros((N, N))
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
                A[k, k] = -4.0
                A[k, idx(i+1, j)] = 1.0
                A[k, idx(i-1, j)] = 1.0
                A[k, idx(i, j+1)] = 1.0
                A[k, idx(i, j-1)] = 1.0
    
    return A, b

A, b = construir_sistema(Nx, Ny, hx)
print(f"   - Sistema: {A.shape[0]} ecuaciones")

# Método de Gauss-Seidel
print("\n3. Resolviendo (Gauss-Seidel)...")

def gauss_seidel(A, b, max_iter=1000, tol=1e-6):
    n = len(b)
    x = np.zeros(n)
    residuos = []
    
    for k in range(max_iter):
        for i in range(n):
            suma = np.dot(A[i, :i], x[:i]) + np.dot(A[i, i+1:], x[i+1:])
            x[i] = (b[i] - suma) / A[i, i]
        
        r = b - np.dot(A, x)
        res_rel = np.linalg.norm(r) / np.linalg.norm(b)
        residuos.append(res_rel)
        
        if res_rel < tol:
            print(f"   - Convergió en {k+1} iteraciones")
            break
    
    return x, residuos

sol, residuos = gauss_seidel(A, b)

# Convertir a matriz
def vec_to_mat(sol, Nx, Ny):
    U = np.zeros((Nx, Ny))
    for i in range(Nx):
        for j in range(Ny):
            U[i, j] = sol[i * Ny + j]
    return U

U = vec_to_mat(sol, Nx, Ny)

# Gráficas
print("\n4. Generando gráficas...")

# Gráfica 1: Convergencia
plt.figure(figsize=(12, 5))
plt.subplot(1, 2, 1)
plt.semilogy(residuos, 'r-', linewidth=2)
plt.xlabel('Iteración')
plt.ylabel('Residuo Relativo')
plt.title('Convergencia Gauss-Seidel')
plt.grid(True, alpha=0.3)

plt.subplot(1, 2, 2)
plt.plot(residuos, 'r-', linewidth=2)
plt.xlabel('Iteración')
plt.ylabel('Residuo Relativo')
plt.title('Convergencia (escala lineal)')
plt.grid(True, alpha=0.3)

plt.tight_layout()
plt.savefig('convergencia.png', dpi=300)
print("   - Guardada: convergencia.png")
plt.close()

# Gráfica 2: Solución
fig = plt.figure(figsize=(15, 5))

ax1 = fig.add_subplot(131, projection='3d')
surf = ax1.plot_surface(X, Y, U, cmap='viridis', edgecolor='none')
ax1.set_xlabel('X')
ax1.set_ylabel('Y')
ax1.set_zlabel('u(x,y)')
ax1.set_title('Solución 3D')
fig.colorbar(surf, ax=ax1, shrink=0.5)

ax2 = fig.add_subplot(132)
cont = ax2.contourf(X, Y, U, levels=20, cmap='viridis')
ax2.contour(X, Y, U, levels=10, colors='black', alpha=0.3, linewidths=0.5)
ax2.set_xlabel('X')
ax2.set_ylabel('Y')
ax2.set_title('Curvas de Nivel')
ax2.set_aspect('equal')
fig.colorbar(cont, ax=ax2)

ax3 = fig.add_subplot(133)
im = ax3.imshow(U, extent=[0, Lx, 0, Ly], origin='lower', cmap='hot')
ax3.set_xlabel('X')
ax3.set_ylabel('Y')
ax3.set_title('Mapa de Calor')
fig.colorbar(im, ax=ax3)

plt.tight_layout()
plt.savefig('solucion_laplace.png', dpi=300)
print("   - Guardada: solucion_laplace.png")
plt.close()

# Guardar datos
print("\n5. Guardando datos...")
np.savetxt('solucion.csv', U, delimiter=',', fmt='%.6f')
np.savetxt('convergencia.csv', residuos, delimiter=',', fmt='%.10f')
print("   - Guardado: solucion.csv")
print("   - Guardado: convergencia.csv")

print("\n" + "="*60)
print("EJECUCIÓN COMPLETADA")
print("="*60)
print("\nEstadísticas:")
print(f"  Valor máximo: {U.max():.6f}")
print(f"  Valor mínimo: {U.min():.6f}")
print(f"  Valor medio:  {U.mean():.6f}")
print(f"  Iteraciones:  {len(residuos)}")


