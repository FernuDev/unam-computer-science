# plot_orbitas.py
import numpy as np
import matplotlib.pyplot as plt

def load(fname):
    data = []
    with open(fname,'r') as f:
        for line in f:
            if line.strip().startswith('#') or len(line.strip())==0:
                continue
            data.append([float(x) for x in line.split()])
    return np.array(data)

files = [("./output/salida_1.dat", "v0=(-0.5, 0.5)"),
         ("./output/salida_2.dat", "v0=(-0.75, 0.75)"),
         ("./output/salida_3.dat", "v0=(-1.0, 1.0)")]

# 1) Trayectorias x-y
plt.figure()
for fn, lab in files:
    A = load(fn)  # columns: t x y vx vy r v E L
    x, y = A[:,1], A[:,2]
    plt.plot(x, y, label=lab)
    plt.plot([x[0]],[y[0]], 'o')  # punto inicial
plt.plot([0],[0],'*', markersize=12)  # masa central
plt.xlabel('x'); plt.ylabel('y'); plt.axis('equal'); plt.legend()
plt.title('Órbitas (x vs y)')
plt.grid(True)
plt.tight_layout()
plt.savefig('trayectorias_xy.png', dpi=200)

# 2) Energía y Momento angular vs tiempo (para un caso representativo)
plt.figure()
A = load(files[0][0])
t, E, L = A[:,0], A[:,7], A[:,8]
plt.plot(t, E)
plt.xlabel('t'); plt.ylabel('Energía específica')
plt.title('Energía vs tiempo (condición 1)')
plt.grid(True)
plt.tight_layout()
plt.savefig('energia_t.png', dpi=200)

plt.figure()
plt.plot(t, L)
plt.xlabel('t'); plt.ylabel('Momento angular específico Lz')
plt.title('Momento angular vs tiempo (condición 1)')
plt.grid(True)
plt.tight_layout()
plt.savefig('momento_t.png', dpi=200)

print("Listo: trayectorias_xy.png, energia_t.png, momento_t.png")
