#include <gtest/gtest.h>
#include <sstream>
#include <string>
#include "../include/Solution.h"
#include "../include/LinkedList.h"

class SolutionTest : public ::testing::Test {
protected:
    void SetUp() override {
        // Configuración común para las pruebas si es necesaria
    }

    void TearDown() override {
        // Limpieza después de cada prueba si es necesaria
    }
};

// =============================================
// PRUEBAS PARA setZeroes()
// =============================================

TEST_F(SolutionTest, SetZeroesBasic) {
    std::vector<std::vector<int>> matrix = {
        {1, 1, 1},
        {1, 0, 1},
        {1, 1, 1}
    };
    std::vector<std::vector<int>> expected = {
        {1, 0, 1},
        {0, 0, 0},
        {1, 0, 1}
    };
    Solution::setZeroes(matrix);
    EXPECT_EQ(matrix, expected);
}

TEST_F(SolutionTest, SetZeroesFirstRowZero) {
    std::vector<std::vector<int>> matrix = {
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8}
    };
    std::vector<std::vector<int>> expected = {
        {0, 0, 0},
        {0, 4, 5},
        {0, 7, 8}
    };
    Solution::setZeroes(matrix);
    EXPECT_EQ(matrix, expected);
}

TEST_F(SolutionTest, SetZeroesMultipleZeros) {
    std::vector<std::vector<int>> matrix = {
        {1, 0, 1, 1},
        {1, 1, 1, 1},
        {1, 1, 0, 1},
        {0, 1, 1, 1}
    };
    std::vector<std::vector<int>> expected = {
        {0, 0, 0, 0},
        {0, 0, 0, 1},
        {0, 0, 0, 0},
        {0, 0, 0, 0}
    };
    Solution::setZeroes(matrix);
    EXPECT_EQ(matrix, expected);
}

TEST_F(SolutionTest, SetZeroesSingleElement) {
    std::vector<std::vector<int>> matrix = {{0}};
    std::vector<std::vector<int>> expected = {{0}};
    Solution::setZeroes(matrix);
    EXPECT_EQ(matrix, expected);
}

// =============================================
// PRUEBAS PARA merge()
// =============================================

TEST_F(SolutionTest, MergeNoOverlap) {
    std::vector<std::vector<int>> intervals = {
        {1, 3},
        {4, 6},
        {8, 10}
    };
    std::vector<std::vector<int>> expected = {
        {1, 3},
        {4, 6},
        {8, 10}
    };
    auto result = Solution::merge(intervals);
    EXPECT_EQ(result, expected);
}

TEST_F(SolutionTest, MergeWithOverlap) {
    std::vector<std::vector<int>> intervals = {
        {1, 4},
        {4, 5}
    };
    std::vector<std::vector<int>> expected = {
        {1, 5}
    };
    auto result = Solution::merge(intervals);
    EXPECT_EQ(result, expected);
}

TEST_F(SolutionTest, MergeMultipleOverlaps) {
    std::vector<std::vector<int>> intervals = {
        {1, 3},
        {2, 6},
        {8, 10},
        {15, 18}
    };
    std::vector<std::vector<int>> expected = {
        {1, 6},
        {8, 10},
        {15, 18}
    };
    auto result = Solution::merge(intervals);
    EXPECT_EQ(result, expected);
}

TEST_F(SolutionTest, MergeEmptyInput) {
    std::vector<std::vector<int>> intervals = {};
    std::vector<std::vector<int>> expected = {};
    auto result = Solution::merge(intervals);
    EXPECT_EQ(result, expected);
}

// =============================================
// PRUEBAS PARA findCycle()
// =============================================

TEST_F(SolutionTest, FindCycleNoCycle) {
    LinkedList list;
    list.insert(1);
    list.insert(2);
    list.insert(3);
    EXPECT_FALSE(Solution::findCycle(list));
}

TEST_F(SolutionTest, FindCycleWithCycle) {
    LinkedList list;
    list.insert(1);
    list.insert(2);
    list.insert(3);
    
    // Crear un ciclo manualmente (depende de tu implementación de LinkedList)
    // Asumiendo que tienes métodos para acceder a los nodos internos
    // auto head = list.getHead();
    // auto tail = list.getTail();
    // tail->next = head; // Crear ciclo
    
    // EXPECT_TRUE(Solution::findCycle(list));
    // Comenta/descomenta según tu implementación
}

TEST_F(SolutionTest, FindCycleSingleElementNoCycle) {
    LinkedList list;
    list.insert(1);
    EXPECT_FALSE(Solution::findCycle(list));
}

TEST_F(SolutionTest, FindCycleEmptyList) {
    LinkedList list;
    EXPECT_FALSE(Solution::findCycle(list));
}

// =============================================
// PRUEBAS PARA singleNumber()
// =============================================

TEST_F(SolutionTest, SingleNumberBasic) {
    std::vector<int> nums = {2, 2, 1};
    EXPECT_EQ(Solution::singleNumber(nums), 1);
}

TEST_F(SolutionTest, SingleNumberMultiplePairs) {
    std::vector<int> nums = {4, 1, 2, 1, 2};
    EXPECT_EQ(Solution::singleNumber(nums), 4);
}

TEST_F(SolutionTest, SingleNumberNegativeNumbers) {
    std::vector<int> nums = {-1, -1, -2};
    EXPECT_EQ(Solution::singleNumber(nums), -2);
}

TEST_F(SolutionTest, SingleNumberLargeArray) {
    std::vector<int> nums;
    for (int i = 0; i < 10000; i++) {
        nums.push_back(i);
        nums.push_back(i);
    }
    nums.push_back(10001);
    EXPECT_EQ(Solution::singleNumber(nums), 10001);
}

// Punto de entrada para las pruebas
int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}