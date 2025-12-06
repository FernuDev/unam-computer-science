#include <gtest/gtest.h>
#include "../src/Solution.h"
#include <vector>

// =======================
// TEST 1: Generación de Nota
// =======================
TEST(SolutionTest, PuedeGenerarNota_Positive) {
    Solution solution;
    std::string nota = "aa";
    std::string texto = "aab";
    EXPECT_TRUE(solution.puedeGenerarNota(nota, texto));
}

TEST(SolutionTest, PuedeGenerarNota_Negative) {
    Solution solution;
    std::string nota = "aa";
    std::string texto = "ab";
    EXPECT_FALSE(solution.puedeGenerarNota(nota, texto));
}

// =======================
// TEST 2: Números Duplicados en un Rango
// =======================
TEST(SolutionTest, HayDuplicadoCercano_Positive) {
    Solution solution;
    std::vector<int> A = {1, 2, 3, 1};
    int k = 3;
    EXPECT_TRUE(solution.hayDuplicadoCercano(A, k));
}

TEST(SolutionTest, HayDuplicadoCercano_Negative) {
    Solution solution;
    std::vector<int> A = {1, 2, 3, 1, 2, 3};
    int k = 2;
    EXPECT_FALSE(solution.hayDuplicadoCercano(A, k));
}

// =======================
// TEST 3: Número Feliz
// =======================
TEST(SolutionTest, EsNumeroFeliz_Positive) {
    Solution solution;
    EXPECT_TRUE(solution.esNumeroFeliz(19));
}

TEST(SolutionTest, EsNumeroFeliz_Negative) {
    Solution solution;
    EXPECT_FALSE(solution.esNumeroFeliz(2));
}

// =======================
// TEST 4: Buscar Subcadena
// =======================
TEST(SolutionTest, BuscarSubcadena_Positive) {
    Solution solution;
    std::string s = "tristes";
    std::string t = "trestristestigrestragabantrigoenuntrigal";
    EXPECT_EQ(solution.buscarSubcadena(s, t), 4);
}

TEST(SolutionTest, BuscarSubcadena_Negative) {
    Solution solution;
    std::string s = "tigresa";
    std::string t = "trestristestigrestragabantrigoenuntrigal";
    EXPECT_EQ(solution.buscarSubcadena(s, t), -1);
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}