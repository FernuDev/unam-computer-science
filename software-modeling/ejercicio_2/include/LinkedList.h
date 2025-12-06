//
// Created by fernudev on 4/26/25.
//

#ifndef LINKEDLIST_H
#define LINKEDLIST_H

#include <iostream>

/**
 * @brief Clase que representa una lista enlazada simple de enteros.
 */
class LinkedList {
public:
    /**
    * @brief Estructura interna que representa un nodo de la lista.
    */
    struct Node {
        int data;      ///< Valor almacenado en el nodo.
        Node *next;    ///< Puntero al siguiente nodo en la lista.

        /**
         * @brief Constructor que inicializa el nodo con un valor dado.
         * @param value El valor a almacenar en el nodo.
         */
        explicit Node(const int value) : data(value), next(nullptr) {}
    };
private:
    Node *head; ///< Puntero al primer nodo de la lista.

public:
    /**
     * @brief Constructor por defecto. Inicializa una lista vacía.
     */
    LinkedList() : head(nullptr) {};

    /**
     * @brief Destructor. Libera toda la memoria ocupada por los nodos de la lista.
     */
    ~LinkedList() {
        const Node *current = head;
        while (current) {
            const Node *next = current->next;
            delete current;
            current = next;
        }
    }

    /**
     * @brief Inserta un nuevo valor al final de la lista enlazada.
     * @param value El valor a insertar.
     */
    void insert(int value);

    /**
     * @brief Busca si un valor existe dentro de la lista enlazada.
     * @param value El valor que se desea buscar.
     * @return true si el valor se encuentra en la lista, false en caso contrario.
     */
    [[nodiscard]] bool search(int value) const;

    /**
     * @brief Elimina el primer nodo que contiene el valor especificado.
     * @param value El valor del nodo a eliminar.
     */
    void remove(int value);

    /**
     * @brief Imprime todos los elementos de la lista enlazada en orden.
     */
    void print() const;

    /**
     * @brief Retorna la referencia del head
     */
    Node* getHead() const;
};

#endif //LINKEDLIST_H
