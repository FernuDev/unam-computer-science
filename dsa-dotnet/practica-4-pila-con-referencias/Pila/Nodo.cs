using System.Collections;
using System.Collections.Generic;
using ED.Colecciones;

namespace ED.Estructuras.Lineales {
    internal class Nodo<T> 
    {
        public T Valor { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T valor) {
            Valor = valor;
            Siguiente = null;
        }
    }
};

