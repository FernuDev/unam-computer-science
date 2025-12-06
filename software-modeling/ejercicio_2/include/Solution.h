/**
 * @file Solution.h
 * @author fernudev
 * @date 4/24/25
 * @brief Definición de la clase Solution con métodos estáticos para operaciones comunes en estructuras de datos
 */

#ifndef SOLUTION_H
#define SOLUTION_H

#include <iostream>
#include <vector>
#include <algorithm>
#include "LinkedList.h"

/**
 * @class Solution
 * @brief Clase utilitaria que contiene métodos estáticos para manipulación de matrices, listas y operaciones básicas
 *
 * @details Esta clase proporciona implementaciones de algoritmos comunes como:
 * - Modificación de matrices basada en condiciones
 * - Combinación de intervalos
 * - Detección de ciclos en listas enlazadas
 * - Operaciones bitwise en vectores
 *
 * Todos los métodos son estáticos, por lo que no es necesario instanciar la clase para usarlos.
 */
class Solution {
public:
    /**
     * @brief Modifica una matriz estableciendo filas y columnas completas a cero
     * @param zeroes Matriz de entrada/salida a modificar
     */
    static void setZeroes(std::vector<std::vector<int>> &zeroes);

    /**
     * @brief Imprime una matriz en formato legible
     * @param matrix Matriz a imprimir
     */
    static void printMatrix(const std::vector<std::vector<int>> &matrix);

    /**
     * @brief Imprime intervalos en formato de lista anidada
     * @param matrix Vector de intervalos a imprimir
     */
    static void printIntervals(const std::vector<std::vector<int>> &matrix);

    /**
     * @brief Combina intervalos superpuestos
     * @param intervals Vector de intervalos a combinar
     * @return Vector de intervalos combinados
     */
    static std::vector<std::vector<int>> merge(const std::vector<std::vector<int>> &intervals);

    /**
     * @brief Detecta ciclos en una lista enlazada
     * @param list Lista enlazada a analizar
     * @return true si se encuentra un ciclo, false en caso contrario
     */
    static bool findCycle(const LinkedList &list);

    /**
     * @brief Encuentra el número único en un vector
     * @param nums Vector de números donde todos excepto uno aparecen exactamente dos veces
     * @return El número que aparece una sola vez
     */
    static int singleNumber(std::vector<int> &nums);
};

#endif //SOLUTION_H