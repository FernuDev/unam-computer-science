#include <iostream>
#include <gtest/gtest.h>
#include "../src/Solution.h"

//
// =======================
// Pruebas para get_repeated_missing_number
// =======================
//

/// @brief Prueba que verifica un caso válido donde hay un número repetido y un número faltante.
/// @details La matriz contiene un valor repetido y un valor faltante en el rango.
/// - Entrada: {{1,2,3}, {4,6,6}, {7,8,9}}
/// - Salida esperada: {6, 5}
TEST(SolutionTest, GetRepeatedMissingNumber_Case1) {
    std::vector<std::vector<int>> matrix = {
        {1, 2, 3},
        {4, 6, 6},
        {7, 8, 9}
    };
    std::vector<int> expected = {6, 5};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

/// @brief Prueba que verifica el comportamiento cuando no hay repetidos ni faltantes.
/// @details La matriz está completa y sin duplicados.
/// - Entrada: {{1,2,3}, {4,5,6}, {7,8,9}}
/// - Salida esperada: {0, 0}
TEST(SolutionTest, GetRepeatedMissingNumber_NoRepetition) {
    std::vector<std::vector<int>> matrix = {
        {1, 2, 3},
        {4, 5, 6},
        {7, 8, 9}
    };
    std::vector<int> expected = {0, 0};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

/// @brief Prueba para una matriz de tamaño mínimo (1x1).
/// @details La matriz solo contiene un valor.
/// - Entrada: {{1}}
/// - Salida esperada: {0, 0}
TEST(SolutionTest, GetRepeatedMissingNumber_OneElement) {
    std::vector<std::vector<int>> matrix = {{1}};
    std::vector<int> expected = {0, 0};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

/// @brief Prueba cuando hay múltiples valores repetidos en la matriz.
/// @details La matriz contiene varios números duplicados.
/// - Entrada: {{1,2,3}, {4,2,6}, {7,8,9}}
/// - Salida esperada: {2, 5}
TEST(SolutionTest, GetRepeatedMissingNumber_MultipleRepetitions) {
    std::vector<std::vector<int>> matrix = {
        {1, 2, 3},
        {4, 2, 6},
        {7, 8, 9}
    };
    std::vector<int> expected = {2, 5};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

/// @brief Prueba cuando el valor faltante es el valor mínimo posible.
/// - Entrada: {{2,3,4}, {5,6,6}, {7,8,9}}
/// - Salida esperada: {6, 1}
TEST(SolutionTest, GetRepeatedMissingNumber_MissingMin) {
    std::vector<std::vector<int>> matrix = {
        {2, 3, 4},
        {5, 6, 6},
        {7, 8, 9}
    };
    std::vector<int> expected = {6, 1};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

/// @brief Prueba cuando el valor faltante es el valor máximo posible.
/// - Entrada: {{1,2,3}, {4,5,5}, {6,7,8}}
/// - Salida esperada: {5, 9}
TEST(SolutionTest, GetRepeatedMissingNumber_MissingMax) {
    std::vector<std::vector<int>> matrix = {
        {1, 2, 3},
        {4, 5, 5},
        {6, 7, 8}
    };
    std::vector<int> expected = {5, 9};
    EXPECT_EQ(Solution::get_repeated_missing_number(matrix), expected);
}

//
// =======================
// Pruebas para integer_to_roman
// =======================
//

/// @brief Prueba para valores básicos de números romanos.
/// - Casos incluidos: 1, 9, 58, 1994
TEST(SolutionTest, IntegerToRoman_ValidCases) {
    EXPECT_EQ(Solution::integer_to_roman(1), "I");
    EXPECT_EQ(Solution::integer_to_roman(9), "IX");
    EXPECT_EQ(Solution::integer_to_roman(58), "LVIII");
    EXPECT_EQ(Solution::integer_to_roman(1994), "MCMXCIV");
}

/// @brief Prueba para el valor cero (sin valor romano definido).
TEST(SolutionTest, IntegerToRoman_Zero) {
    EXPECT_EQ(Solution::integer_to_roman(0), "");
}

/// @brief Prueba para valores fuera del rango permitido.
/// - Valores fuera de rango: -1, 4000
TEST(SolutionTest, IntegerToRoman_OutOfRange) {
    EXPECT_EQ(Solution::integer_to_roman(-1), "");
    EXPECT_EQ(Solution::integer_to_roman(4000), "");
}

//
// =======================
// Pruebas para roman_to_integer
// =======================
//

/// @brief Prueba para valores romanos válidos.
/// - Casos incluidos: "I", "IX", "LVIII", "MCMXCIV"
TEST(SolutionTest, RomanToInteger_ValidCases) {
    EXPECT_EQ(Solution::roman_to_integer("I"), 1);
    EXPECT_EQ(Solution::roman_to_integer("IX"), 9);
    EXPECT_EQ(Solution::roman_to_integer("LVIII"), 58);
    EXPECT_EQ(Solution::roman_to_integer("MCMXCIV"), 1994);
}

/// @brief Prueba para valores inválidos.
/// - Casos incluidos: "IIII", "IC", "ABC"
TEST(SolutionTest, RomanToInteger_InvalidCases) {
    EXPECT_EQ(Solution::roman_to_integer("IIII"), 4);
    EXPECT_EQ(Solution::roman_to_integer("IC"), 99);
    EXPECT_EQ(Solution::roman_to_integer("ABC"), 0);
}

//
// =======================
// Pruebas para get_biggest_prefix
// =======================
//

/// @brief Prueba para casos con prefijo común.
/// - Casos incluidos: "flor", "flores", "floreria"
TEST(SolutionTest, GetBiggestPrefix_ValidCases) {
    std::vector<std::string> words = {"flor", "flores", "floreria"};
    EXPECT_EQ(Solution::get_biggest_prefix(words), "flor");
}

/// @brief Prueba para el caso donde no hay prefijo común.
/// - Casos incluidos: "flor", "flores", "vape"
TEST(SolutionTest, GetBiggestPrefix_NoCommonPrefix) {
    std::vector<std::string> words = {"flor", "flores", "vape"};
    EXPECT_EQ(Solution::get_biggest_prefix(words), "");
}

/// @brief Prueba para lista vacía.
/// - Salida esperada: ""
TEST(SolutionTest, GetBiggestPrefix_EmptyList) {
    std::vector<std::string> words;
    EXPECT_EQ(Solution::get_biggest_prefix(words), "");
}

/// @brief Prueba para el caso donde hay un solo elemento.
/// - Entrada: {"flor"}
/// - Salida esperada: "flor"
TEST(SolutionTest, GetBiggestPrefix_SingleElement) {
    std::vector<std::string> words = {"flor"};
    EXPECT_EQ(Solution::get_biggest_prefix(words), "flor");
}

/// @brief Prueba cuando todas las palabras son idénticas.
/// - Entrada: {"flor", "flor", "flor"}
/// - Salida esperada: "flor"
TEST(SolutionTest, GetBiggestPrefix_FullMatch) {
    std::vector<std::string> words = {"flor", "flor", "flor"};
    EXPECT_EQ(Solution::get_biggest_prefix(words), "flor");
}

//
// =======================
// MAIN
// =======================
//
int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}
