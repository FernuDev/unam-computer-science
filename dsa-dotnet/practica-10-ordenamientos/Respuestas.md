# Respuestas a las preguntas sobre algoritmos de ordenamiento

### 1. Generación de los peores casos

Explique cómo generó cada uno de los peores casos y por qué es el peor caso para
ese algoritmo, además de mencionar el orden de la complejidad del peor caso

***Bubble Sort***

    Generación: Un arreglo en orden inverso (e.g., [5, 4, 3, 2, 1]).
    Razón: En este caso, el algoritmo debe realizar el máximo número de comparaciones y swaps posibles, recorriendo repetidamente el arreglo para colocar cada elemento en su posición final.
    Complejidad: O(n2)O(n2).

***Selection Sort***

    Generación: Un arreglo en orden inverso (e.g., [5, 4, 3, 2, 1]).
    Razón: Aunque el algoritmo siempre realiza el mismo número de comparaciones independientemente del orden inicial, en el peor caso tiene que realizar el máximo número de asignaciones.
    Complejidad: O(n2)O(n2).

***Quick Sort***

    Generación: Un arreglo ya ordenado o en orden inverso, dependiendo de cómo se elija el pivote (siempre el primer o último elemento).
    Razón: El pivote seleccionado no divide el arreglo de manera balanceada, lo que genera particiones altamente desbalanceadas en cada llamada recursiva.
    Complejidad: O(n2)O(n2).

***Merge Sort***

    Generación: Cualquier arreglo (e.g., [5, 4, 3, 2, 1]).
    Razón: Este algoritmo siempre divide el arreglo en mitades, y el número de operaciones no depende del orden inicial de los elementos.
    Complejidad: O(nlog⁡n)O(nlogn).

***Insertion Sort***

    Generación: Un arreglo en orden inverso (e.g., [5, 4, 3, 2, 1]).
    Razón: Cada elemento debe moverse completamente hacia el inicio del arreglo, lo que genera el máximo número de comparaciones y desplazamientos.
    Complejidad: O(n2)O(n2).

### 2. Mejores casos

***Bubble Sort***

    Generación: Un arreglo ya ordenado (e.g., [1, 2, 3, 4, 5]).
    Complejidad: O(n2)O(n2) (sin optimización para detectar si el arreglo ya está ordenado).

***Selection Sort***

    Generación: Un arreglo ya ordenado (e.g., [1, 2, 3, 4, 5]).
    Complejidad: O(n2)O(n2) (número fijo de comparaciones independientemente del orden).

***Quick Sort***

    Generación: Un arreglo en el que el pivote siempre divide de manera equitativa el arreglo en dos mitades (e.g., [4, 1, 3, 9, 7]).
    Complejidad: O(nlog⁡n)O(nlogn).

***Merge Sort***

    Generación: Cualquier arreglo.
    Complejidad: O(nlog⁡n)O(nlogn) (siempre se divide el arreglo en mitades y realiza fusiones balanceadas).

***Insertion Sort***

    Generación: Un arreglo ya ordenado (e.g., [1, 2, 3, 4, 5]).
    Complejidad: O(n)O(n).

### 3. Algoritmos con la misma complejidad en el peor y mejor caso

    Algoritmos:
        Bubble Sort
        Selection Sort
        Merge Sort
    Complejidad:
        Bubble Sort: O(n2)O(n2)
        Selection Sort: O(n2)O(n2)
        Merge Sort: O(nlog⁡n)O(nlogn)

### 4. Algoritmos donde la complejidad difiere entre el mejor y peor caso

    Quick Sort
        Mejor caso: O(nlog⁡n)O(nlogn)
        Peor caso: O(n2)O(n2)
    Insertion Sort
        Mejor caso: O(n)O(n)
        Peor caso: O(n2)O(n2)