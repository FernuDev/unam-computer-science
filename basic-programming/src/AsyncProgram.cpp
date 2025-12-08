#include <iostream>
#include <future>
#include <vector>

// Función que será ejecutada de forma asíncrona
int asyncFunction(int threadId, int iterations) {
    for (int i = 0; i < iterations; ++i) {
        std::cout << "Hilo " << threadId << ": Iteración " << i << std::endl;
    }
    return threadId * iterations;
}

int main() {
    const int numThreads = 4; // Número de hilos
    const int iterationsPerThread = 5; // Iteraciones por hilo

    std::vector<std::future<int>> futures; // Vector para almacenar los futuros resultados

    // Inicia las tareas asíncronas
    for (int i = 0; i < numThreads; ++i) {
        futures.push_back(std::async(std::launch::async, asyncFunction, i, iterationsPerThread));
    }

    // Recupera los resultados cuando sea necesario
    for (std::future<int> &f : futures) {
        int result = f.get();
        std::cout << "Resultado: " << result << std::endl;
    }

    std::cout << "Todos los hilos han terminado." << std::endl;

    return 0;
}
