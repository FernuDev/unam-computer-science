#!/bin/bash

# Script para ejecutar los programas de pozos de potencial
# y generar los archivos de datos necesarios para el notebook

echo "================================================"
echo "Ejecutando programas de Pozos de Potencial 1D"
echo "================================================"
echo ""

# Cambiar al directorio correcto
cd "$(dirname "$0")"

echo "Directorio actual: $(pwd)"
echo ""

# Ejecutar pozo triangular
echo "1. Ejecutando pozo triangular..."
echo "   Parámetros: N=1000, E_inicial=0.5, Paridad=1 (par)"
echo ""

# Crear archivo de entrada para pozo triangular
cat > input_triangular.txt << EOF
1000
0.5
1
EOF

../../bin/pozo_triangular < input_triangular.txt

if [ -f "psi_triangular.dat" ]; then
    echo ""
    echo "✓ Archivo psi_triangular.dat generado exitosamente"
    echo "  Número de líneas: $(wc -l < psi_triangular.dat)"
else
    echo ""
    echo "✗ Error: No se generó psi_triangular.dat"
    exit 1
fi

echo ""
echo "================================================"
echo ""

# Ejecutar pozo doble
echo "2. Ejecutando pozo doble..."
echo "   Parámetros: a=0.3, N=1000, E_inicial=0.5, Paridad=1 (par)"
echo ""

# Crear archivo de entrada para pozo doble
cat > input_doble.txt << EOF
0.3
1000
0.5
1
EOF

../../bin/pozo_doble < input_doble.txt

if [ -f "psi_doble.dat" ]; then
    echo ""
    echo "✓ Archivo psi_doble.dat generado exitosamente"
    echo "  Número de líneas: $(wc -l < psi_doble.dat)"
else
    echo ""
    echo "✗ Error: No se generó psi_doble.dat"
    exit 1
fi

echo ""
echo "================================================"
echo "Resumen de Resultados"
echo "================================================"
echo ""

if [ -f "psi_triangular.dat" ] && [ -f "psi_doble.dat" ]; then
    echo "✓ Ambos programas ejecutados exitosamente"
    echo ""
    echo "Archivos generados:"
    ls -lh psi_*.dat
    echo ""
    echo "Ahora puedes abrir el notebook pozos.ipynb para visualizar los resultados:"
    echo "  jupyter notebook pozos.ipynb"
else
    echo "✗ Error en la ejecución de los programas"
    exit 1
fi

# Limpiar archivos temporales
rm -f input_triangular.txt input_doble.txt

echo ""
echo "¡Listo!"

