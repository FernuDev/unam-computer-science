using System;
using System.Collections;
using System.Collections.Generic;
using ED.Colecciones;

namespace ED.Estructuras.Lineales.Cola
{
    public class ColaEnArreglo<T> : ICola<T>, ICollection<T>
    {
        private T[] buffer;
        private int inicio;
        private int fin;
        private int count;
        private static readonly int TamInicial = 100;

        /// <summary>
        /// Constructor que recibe el tamaño inicial de la cola.
        /// </summary>
        /// <param name="tamInicial">Tamaño inicial de la cola.</param>
        public ColaEnArreglo(int tamInicial) 
        {
            buffer = new T[tamInicial];
            inicio = 0;
            fin = -1;
            count = 0;
        }

        /// <summary>
        /// Constructor sin parámetros, que toma el TamInicial.
        /// </summary>
        public ColaEnArreglo() : this(TamInicial) { }

        /// <summary>
        /// Indica si la cola está vacía.
        /// </summary>
        public bool EstáVacía() 
        {
            return count == 0;
        }

        /// <summary>
        /// Muestra el elemento al inicio de la cola.
        /// </summary>
        /// <returns>El elemento siguiente.</returns>
        /// <exception cref="InvalidOperationException">Si la cola está vacía.</exception>
        public T Mira() 
        {
            if (EstáVacía()) throw new InvalidOperationException("La cola está vacía.");
            return buffer[inicio];
        }

        /// <summary>
        /// Agrega un elemento al final de la cola.
        /// </summary>
        /// <param name="e">Elemento a agregar.</param>
        /// <exception cref="ArgumentNullException">Si el elemento es nulo.</exception>
        public void Forma(T e) 
        {
            if (e == null) throw new ArgumentNullException(nameof(e), "El elemento no puede ser nulo.");

            if (count == buffer.Length) throw new InvalidOperationException("La cola está llena.");

            // Avanzar el índice de fin de forma circular
            fin = (fin + 1) % buffer.Length;
            buffer[fin] = e;
            count++;
        }

        /// <summary>
        /// Devuelve y elimina el elemento al inicio de la cola.
        /// </summary>
        /// <returns>El elemento eliminado.</returns>
        /// <exception cref="InvalidOperationException">Si la cola está vacía.</exception>
        public T Atiende()
        {
            if (EstáVacía()) throw new InvalidOperationException("La cola está vacía.");

            T valor = buffer[inicio];
            inicio = (inicio + 1) % buffer.Length;
            count--;
            System.Console.WriteLine(valor);
            return valor;
        }

        /// <summary>
        /// Método Add equivalente al método Forma.
        /// </summary>
        public void Add(T elemento) 
        {
            Forma(elemento);
        }

        /// <summary>
        /// Limpia la cola, vaciándola.
        /// </summary>
        public void Clear() 
        {
            inicio = 0;
            fin = -1;
            count = 0;
        }

        /// <summary>
        /// Copia los elementos de la cola en un arreglo.
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex) 
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < count) throw new ArgumentException("El arreglo no tiene suficiente espacio.");


            for (int i = 0; i < count; i++) 
            {
                array[arrayIndex + i] = buffer[(inicio + i) % buffer.Length];
            }
        }

        /// <summary>
        /// Verifica si un elemento está en la cola.
        /// </summary>
        public bool Contains(T elemento) 
        {
            try{

                for (int i = 0; i < count; i++)
                {
                    if (buffer[(inicio + i) % buffer.Length].Equals(elemento)) 
                    {
                        return true;
                    }
                }

                return false;

            } catch (InvalidOperationException e) {
                return false;
            }
            
        }

        /// <summary>
        /// Elimina el primer elemento que coincide.
        /// </summary>
        public bool Remove(T elemento) 
        {   
            if(EstáVacía()) return false;

            if (Mira().Equals(elemento)) 
            {
                Atiende();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Propiedad Count para el número de elementos de la cola.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Propiedad IsReadOnly (implementación de ICollection).
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Obtiene un enumerador que itera a través de la cola.
        /// </summary>
        public IEnumerator<T> GetEnumerator() 
        {
            for (int i = 0; i < count; i++)
            {
                yield return buffer[(inicio + i) % buffer.Length];
            }
        }

        // Método de la interfaz no genérica IEnumerable.
        IEnumerator IEnumerable.GetEnumerator() 
        {
            return GetEnumerator();
        }
    }
}
