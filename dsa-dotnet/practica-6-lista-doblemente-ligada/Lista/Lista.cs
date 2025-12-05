using System;
using System.Collections;
using System.Collections.Generic;

using ED.Colecciones;

namespace ED.Estructuras.Lineales.Lista
{
    public class ListaDoblementeLigada<T> : ILista<T>
    {
        private Nodo _centinela;
        private int _count;

        public ListaDoblementeLigada()
        {
            _centinela = new Nodo(default(T));
            _centinela.Siguiente = _centinela;
            _centinela.Anterior = _centinela;
            _count = 0;
        }

        private class Nodo
        {
            public T Dato;
            public Nodo Siguiente;
            public Nodo Anterior;

            public Nodo(T dato)
            {
                Dato = dato;
            }
        }

        // Clase interna EnumeradorLista
        private class EnumeradorLista : IEnumeradorLista<T>
        {
            private Nodo _actual;
            private ListaDoblementeLigada<T> _lista;
            private bool _canRemove; // Indica si se puede remover el elemento
            private bool _hasAdded; // Indica si se ha llamado a Agrega

            public EnumeradorLista(ListaDoblementeLigada<T> lista)
            {
                _lista = lista;
                _actual = _lista._centinela;
                _canRemove = false; // Inicialmente no se puede remover
                _hasAdded = false; // Inicialmente no se ha agregado
            }

            public T Current
            {
                get
                {
                    if (_actual == null )
                    {
                        throw new NoSuchElementException();
                    }
                    return _actual.Dato;
                }
            }
            object IEnumerator.Current => Current;

            public int Índice
            {
                get
                {
                    if (_actual == null || _actual == _lista._centinela)
                    {
                        throw new NoSuchElementException();
                    }
                    return _lista.IndexOf(_actual.Dato);
                }
            }

            public bool HasNext => _actual.Siguiente != _lista._centinela;
            public bool HasPrevious => _actual.Anterior != _lista._centinela;

            public bool MoveNext()
            {   
                if ( _actual.Siguiente == _lista._centinela )
                {
                    return false;
                }
                _actual = _actual.Siguiente;
                _canRemove = true; // Se puede remover después de mover
                return true;
            }

            public bool MovePrevious()
            {   
                if ( _actual.Anterior == _lista._centinela )
                {
                    return false;
                }
                _actual = _actual.Anterior;
                _canRemove = true; // Se puede remover después de mover
                return true;
            }

            public void ColocaEn(int índice)
            {
                if (índice < 0 || índice >= _lista.Count)
                {
                    throw new NoSuchElementException();
                }
                _actual = _lista.ObtenerNodo(índice);
                _canRemove = false; // Resetea el estado al colocar en un índice
            }

            public void Remueve()
            {
                if (!_canRemove) 
                    throw new IllegalStateException();
                if (_hasAdded) 
                    throw new IllegalStateException();

                if (_actual == _lista._centinela) 
                    throw new NoSuchElementException();

                _lista.EliminarNodo(_actual);
                _canRemove = false; // No se puede remover nuevamente
                _actual = _lista._centinela; // Resetea el actual
            }

            public void Asigna(T dato)
            {   
                if (!_canRemove || _actual == _lista._centinela)
                    throw new IllegalStateException();
                _actual.Dato = dato;
            }

            public void Agrega(T dato)
            {
                _lista.InsertarAntes(_actual, dato);
                _canRemove = false; // Se ha agregado un nuevo elemento
                _hasAdded = true; // Se ha llamado a Agrega
            }

            public void Dispose() { }

            public void Reset()
            {
                _actual = _lista._centinela;
                _canRemove = false; // Resetea el estado al reiniciar
                _hasAdded = false;
            }
        }

        public int Count => _count;
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                return ObtenerNodo(index).Dato;
            }
            set
            {   
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (value == null)
                    throw new ArgumentNullException();

                ObtenerNodo(index).Dato = value;
            }
        }

        public void Add(T item)
        {
            InsertarAntes(_centinela, item);
        }

        public void Clear()
        {
            _centinela.Siguiente = _centinela;
            _centinela.Anterior = _centinela;
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var nodo = _centinela.Siguiente;
            for (int i = arrayIndex; i < array.Length && nodo != _centinela; i++)
            {
                array[i] = nodo.Dato;
                nodo = nodo.Siguiente;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeradorLista(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator(int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");

            var enumerador = new EnumeradorLista(this);

            if(index < _count) 
            {
                enumerador.ColocaEn(index);
            }
            else
            {
                enumerador.Reset();
            }

            return enumerador;
        }

        public int IndexOf(T item)
        {
            var nodo = _centinela.Siguiente;
            int index = 0;
            while (nodo != _centinela)
            {
                if (Equals(nodo.Dato, item)) return index;
                nodo = nodo.Siguiente;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            
            if(index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Insertar el elemento en la posición deseada
            if (index == _count) // Si el índice es igual a _count, significa que estamos agregando al final
            {
                InsertarAntes(_centinela, item); // Asumiendo que el centinela está correctamente establecido para apuntar al final
            }
            else
            {
                // Obtener el nodo en el índice deseado
                Nodo nodoObjetivo = ObtenerNodo(index); // Asegúrate de que este método esté bien implementado
                InsertarAntes(nodoObjetivo, item);
            }
        }

        public bool Remove(T item)
        {
            Nodo nodo = _centinela.Siguiente;
            while (nodo != _centinela)
            {
                if (Equals(nodo.Dato, item))
                {
                    EliminarNodo(nodo);
                    return true;
                }
                nodo = nodo.Siguiente;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            Nodo nodo = ObtenerNodo(index);
            EliminarNodo(nodo);
        }

        public bool EstáVacía()
        {
            return _count == 0;
        }

        // Método privado para obtener el nodo en una posición específica
        private Nodo ObtenerNodo(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException();

            Nodo nodo = _centinela.Siguiente;
            for (int i = 0; i < index; i++)
            {   
                nodo = nodo.Siguiente;
            }
            return nodo;
        }

        // Inserta un nuevo nodo antes del nodo dado
        private void InsertarAntes(Nodo nodo, T item)
        {
            // Verificar si el nodo es nulo
            if (nodo == null)
            {
                throw new ArgumentNullException(nameof(nodo), "El nodo no puede ser nulo.");
            }

            // Crear un nuevo nodo
            Nodo nuevoNodo = new Nodo(item);

            // Si el nodo a insertar antes es el primer nodo
            if (nodo.Anterior == null)
            {
                // Este es el primer nodo, por lo que se establece como nuevo nodo de cabeza
                _centinela = nuevoNodo; // Asumiendo que tienes una referencia a la cabeza de la lista
            }
            else
            {
                // Actualizar los punteros de los nodos adyacentes
                nuevoNodo.Anterior = nodo.Anterior;
                nodo.Anterior.Siguiente = nuevoNodo;
            }

            // Establecer el siguiente puntero del nuevo nodo
            nuevoNodo.Siguiente = nodo;
            nodo.Anterior = nuevoNodo;

            // Incrementar el contador de elementos
            _count++;
        }

        // Elimina un nodo dado de la lista
        private void EliminarNodo(Nodo nodo)
        {
            nodo.Anterior.Siguiente = nodo.Siguiente;
            nodo.Siguiente.Anterior = nodo.Anterior;
            _count--;
        }
    }
}
