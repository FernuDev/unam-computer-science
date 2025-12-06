//
// Created by fernudev on 4/24/25.
//

#include "../include/Solution.h"

/**
 * @brief Modifica la matriz dada para que si una celda contiene 0,
 *        toda su fila y columna sean establecidas a 0.
 *
 * Esta operación se realiza en el lugar, sin utilizar espacio adicional
 * más allá de algunas variables booleanas.
 *
 * @param zeroes Una referencia a una matriz de enteros de tamaño m x n.
 */
void Solution::setZeroes(std::vector<std::vector<int>>& zeroes) {
    const int rows = zeroes.size();
    const int cols = zeroes[0].size();
    bool firstRowZero = false;
    bool firstColZero = false;

    // Verifica si la primera fila contiene algún cero
    for (int j = 0; j < cols; j++) {
        if (zeroes[0][j] == 0) {
            firstRowZero = true;
            break;
        }
    }

    // Verifica si la primera columna contiene algún cero
    for (int i = 0; i < rows; i++) {
        if (zeroes[i][0] == 0) {
            firstColZero = true;
            break;
        }
    }

    // Usa la primera fila y columna como marcadores para los ceros
    for (int i = 1; i < rows; i++) {
        for (int j = 1; j < cols; j++) {
            if (zeroes[i][j] == 0) {
                zeroes[i][0] = 0;
                zeroes[0][j] = 0;
            }
        }
    }

    // Establece las celdas correspondientes a cero según los marcadores
    for (int i = 1; i < rows; i++) {
        for (int j = 1; j < cols; j++) {
            if (zeroes[i][0] == 0 || zeroes[0][j] == 0) {
                zeroes[i][j] = 0;
            }
        }
    }

    // Aplica ceros a la primera fila si es necesario
    if (firstRowZero) {
        for (int j = 0; j < cols; j++) {
            zeroes[0][j] = 0;
        }
    }

    // Aplica ceros a la primera columna si es necesario
    if (firstColZero) {
        for (int i = 0; i < rows; i++) {
            zeroes[i][0] = 0;
        }
    }
}

/**
 * @brief Imprime la matriz en consola.
 *
 * Cada fila se imprime en una nueva línea, y los elementos están separados por espacios.
 *
 * @param matrix Una referencia constante a la matriz a imprimir.
 */
void Solution::printMatrix(const std::vector<std::vector<int>>& matrix) {
    for (int i = 0; i < matrix.size(); i++) {
        for (int j = 0; j < matrix[0].size(); j++) {
            std::cout << matrix[i][j] << " ";
        }
        std::cout << std::endl;
    }
}

void Solution::printIntervals(const std::vector<std::vector<int>> &matrix) {
    std::cout << "[";
    for (int i = 0; i < matrix.size(); i++) {
        std::cout << "[";
        for (int j = 0; j < matrix[i].size(); j++) {
            std::cout << matrix[i][j];
            if (j != matrix[i].size() - 1) std::cout << ",";
        }
        std::cout << "]";
        if (i != matrix.size() - 1) std::cout << ",";
    }
    std::cout << "]" << std::endl;
}

/**
 * @brief Hace el merge de una lista de intervalos
 * Verifica en los sub intervalos si estan superpuestos, de ser el caso hace el merge, y lo reemplaza con un intervalo
 * con los limites actualizados
 * @param intervals Una referencia constante a la matriz a hacer merge
 ***/

std::vector<std::vector<int>> Solution::merge(const std::vector<std::vector<int>> &intervals) {
    if (intervals.empty()) return {};

    std::vector<std::vector<int>> sortedIntervals = intervals; // Hacemos una copia
    std::sort(sortedIntervals.begin(), sortedIntervals.end()); // Ahora sí podemos ordenar

    std::vector<std::vector<int>> merged;
    merged.push_back(sortedIntervals[0]); // Agregamos el primer intervalo

    for (int i = 1; i < sortedIntervals.size(); ++i) {
        if (std::vector<int>& last = merged.back(); sortedIntervals[i][0] <= last[1]) {
            last[1] = std::max(last[1], sortedIntervals[i][1]);
        } else {
            merged.push_back(sortedIntervals[i]);
        }
    }

    return merged;
}

/**
 * @brief Busca si en las referencias de la lista enlazada hay un bucle
 * @param list Lista enlazada
 * @return Retorna true si encuentra un bucle y false si es una lista simple
 */
bool Solution::findCycle(const LinkedList &list) {
    const LinkedList::Node* slow = list.getHead();
    const LinkedList::Node* fast = list.getHead();

    while (fast && fast->next) {
        slow = slow->next;
        fast = fast->next->next;

        if (slow == fast) {
            return true; // Hay un ciclo
        }
    }

    return false; // No hay ciclo
}

/**
 * @brief Busca por el número que solo aparece una vez
 * @param nums
 * @return Retorna el numero que solo aparece una vez
 */
int Solution::singleNumber(std::vector<int> &nums) {
    int result = 0;
    for (const int num : nums) {
        result ^= num; // Utilizamos XOR para encontrar el numero unico
    }
    return result;
}
