#include <iostream>
#include "./src/Solution.h"

int main(int argc, char **argv) {
    Solution solution;

    // Caso 1: Generación de Nota
    const std::string nota = "aa";
    const std::string texto = "aba";
    std::cout << "¿Se puede generar la nota? " << solution.puedeGenerarNota(nota, texto) << "\n";

    // Caso 2: Números duplicados
    const std::vector<int> A = {1, 2, 3, 1};
    constexpr int k = 3;
    std::cout << "¿Hay duplicados cercanos? " << solution.hayDuplicadoCercano(A, k) << "\n";

    // Caso 3: Número feliz
    constexpr int n = 19;
    std::cout << "¿Es número feliz? " << solution.esNumeroFeliz(n) << "\n";

    // Caso 4: Buscar Subcadena
    const std::string s = "tristes";
    const std::string t = "trestristestigrestragabantrigoenuntrigal";
    std::cout << "Índice de subcadena: " << solution.buscarSubcadena(s, t) << "\n";
    return EXIT_SUCCESS;
}