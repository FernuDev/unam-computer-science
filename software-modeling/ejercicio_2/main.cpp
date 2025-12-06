#include <iostream>
#include "./include/LinkedList.h"
#include "./include/Solution.h" // Asumiendo que tu función findCycle está ahí

int main() {
    LinkedList linked_list;

    for (int i = 0; i < 10; i++) {
        linked_list.insert(i);
    }

    // Crear el ciclo manualmente:
    LinkedList::Node* head = linked_list.getHead();
    LinkedList::Node* tail = head;

    // Encontrar el último nodo
    while (tail->next != nullptr) {
        tail = tail->next;
    }

    // Hacer que el último nodo apunte al nodo con valor 5 (por ejemplo)
    LinkedList::Node* current = head;
    while (current != nullptr && current->data != 5) {
        current = current->next;
    }

    if (current != nullptr) {
        tail->next = current;  // Creando el ciclo
        std::cout << "Ciclo creado apuntando al nodo con valor 5.\n";
    }

    // No llames print() aquí porque como la lista ahora tiene ciclo, ¡entrarías en bucle infinito!

    // Verificar si hay ciclo
    Solution solution;

    if (bool hasCycle = Solution::findCycle(linked_list)) {
        std::cout << "Se detectó un ciclo en la lista.\n";
    } else {
        std::cout << "No se detectó ningún ciclo.\n";
    }

    tail->next = nullptr;

    return 0;
}
