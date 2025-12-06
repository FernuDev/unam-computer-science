cmake_minimum_required(VERSION 3.30)
project(Ejercicios_parcial_1)

set(CMAKE_CXX_STANDARD 20)

# Ejecutable para el programa principal
add_executable(Ejercicios_parcial_1
        main.cpp
        src/Solution.cpp
        src/Solution.h
)

# ==============================
# Configuración de Google Test
# ==============================
find_package(GTest REQUIRED)

# Ejecutable para las pruebas
add_executable(test_solution
        test/UnitTestingSolution.cpp
        src/Solution.cpp
)

# Enlace con Google Test
target_link_libraries(test_solution
        GTest::GTest
        GTest::Main
        pthread
)

# Habilita pruebas con CTest
enable_testing()

# Añade las pruebas a CTest
add_test(NAME SolutionTests COMMAND test_solution)
