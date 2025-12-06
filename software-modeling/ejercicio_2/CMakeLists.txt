cmake_minimum_required(VERSION 3.14)
project(SolutionProject)

# Configuración básica
set(CMAKE_CXX_STANDARD 17)

# Google Test
find_package(GTest REQUIRED)
include(GoogleTest)

# Todos los archivos fuente
set(SRC_FILES
        src/Solution.cpp
        src/LinkedList.cpp
)

# Pruebas unitarias
add_executable(SolutionTests
        test/UnitTestingSolution.cpp
        ${SRC_FILES}
)
target_include_directories(SolutionTests PRIVATE include)
target_link_libraries(SolutionTests GTest::GTest GTest::Main pthread)

# Ejecutable principal (opcional)
if(EXISTS "main.cpp")
    add_executable(Ejercicios_2 main.cpp ${SRC_FILES})
    target_include_directories(Ejercicios_2 PRIVATE include)
endif()