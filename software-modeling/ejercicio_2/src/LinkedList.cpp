//
// Created by fernudev on 4/26/25.
//

#include "../include/LinkedList.h"

/**
 * @brief Inserta un nuevo valor al final de la lista enlazada.
 *
 * @param value El valor que se insertará en la lista.
 */
void LinkedList::insert(const int value) {
    auto *newNode = new Node(value);
    if (head == nullptr) {
        head = newNode;
        return;
    }

    Node* current = head;
    while (current->next != nullptr) {
        current = current->next;
    }
    current->next = newNode;
}

/**
 * @brief Busca si un valor existe dentro de la lista enlazada.
 *
 * @param value El valor a buscar.
 * @return true Si el valor se encuentra en la lista.
 * @return false Si el valor no está en la lista.
 */
bool LinkedList::search(const int value) const {
    const Node* current = head;
    while (current != nullptr) {
        if (current->data == value) {
            return true;
        }
        current = current->next;
    }
    return false;
}

/**
 * @brief Elimina el primer nodo que contiene el valor especificado.
 *
 * @param value El valor del nodo que se desea eliminar.
 */
void LinkedList::remove(const int value) {
    if (head == nullptr) return;

    if (head->data == value) {
        const Node* temp = head;
        head = head->next;
        delete temp;
        return;
    }

    Node* current = head;
    while (current->next != nullptr && current->next->data != value) {
        current = current->next;
    }

    if (current->next != nullptr) {
        const Node* temp = current->next;
        current->next = current->next->next;
        delete temp;
    }
}

/**
 * @brief Imprime todos los elementos de la lista enlazada en orden.
 *
 * El formats de impresión es: 10 -> 20 -> 30 -> 40
 */
void LinkedList::print() const {
    const Node* current = head;
    while (current != nullptr) {
        std::cout << current->data;
        if (current->next != nullptr) std::cout << " -> ";
        current = current->next;
    }
    std::cout << std::endl;
}


/**
 * @brief Retorna la referencia del Head
 */

LinkedList::Node* LinkedList::getHead() const {
    return head;
}