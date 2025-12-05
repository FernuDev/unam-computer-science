using ED.Colecciones;
using System.Collections;
using System.Collections.Generic;


namespace ED.Estructuras.NoLineales.ÁrbolBinario
{
    /// <summary>
    /// Implementación de un nodo binario ordenado.
    /// </summary>
    /// <typeparam name="C">Tipo de dato contenido en el nodo.</typeparam>
    public class NodoBinarioOrdenado<C> : INodoBinarioOrdenado<C> where C : IComparable<C>
    {   

        private INodoBinarioOrdenado<C>? padre;
        private INodoBinarioOrdenado<C>? hijoI;
        private INodoBinarioOrdenado<C>? hijoD;

        /// <summary>
        /// Dato a almacenar en el nodo 
        /// con los metodos getter y setter
        /// </summary>
        public C Dato { get; set; }

        /// <summary>
        /// Referencia al nodo padre, que permitira
        /// devolverse en el arbol binario o simplemente tener referencia
        /// y contexto del padre
        /// </summary>
        void INodoBinarioOrdenado<C>.Padre(INodoBinarioOrdenado<C>? padre)
        {
            Padre(padre);
        }
        INodoBinarioOrdenado<C>? INodoBinarioOrdenado<C>.Padre()
        {
            return padre;
        }

        public INodoBinarioOrdenado<C>? Padre() => padre;

        public void Padre(INodoBinarioOrdenado<C>? padre)
        {
            this.padre = padre;
        }
        
        /// <summary>
        /// Hijo de la izquierda del arbol binario que por definicion 
        /// es menor que el dato raiz o el dato actual del nodo
        /// </summary>
        
        INodoBinarioOrdenado<C>? INodoBinarioOrdenado<C>.HijoI()
        {
            return hijoI;
        }

        void INodoBinarioOrdenado<C>.HijoI(INodoBinarioOrdenado<C>? hijoI)
        {
            HijoI(hijoI);
        }

        public INodoBinarioOrdenado<C>? HijoI() => hijoI;
        
        public void HijoI(INodoBinarioOrdenado<C>? hijoI)
        {
            this.hijoI = hijoI;
        }

        /// <summary>
        /// Hijo de la derecha del arbol binario que por definicion 
        /// es mayor o igual que el dato raiz o el dato actual del nodo
        /// </summary>
        
        INodoBinarioOrdenado<C>? INodoBinarioOrdenado<C>.HijoD()
        {
            return hijoD;
        }

        void INodoBinarioOrdenado<C>.HijoD(INodoBinarioOrdenado<C>? hijoD)
        {
            HijoD(hijoD);
        }
        
        public INodoBinarioOrdenado<C>? HijoD() => hijoD;

        public void HijoD(INodoBinarioOrdenado<C>? hijoD)
        {
            this.hijoD = hijoD;
        }

        public int Altura => 0;

        /// <summary>
        /// Constructor para un nodo con un dato especifico
        /// </summary>
        /// <param name="dato">Es el dato a almacenar en el nodo</param>
        public NodoBinarioOrdenado (C dato) 
        {
            Dato = dato;
            Padre(null);
            HijoD(null);
            HijoI(null);
        }

        /// <summary>
        /// Constructor para un nodo con un dato especifico y un nodo padre
        /// </summary>
        /// <param name="dato">El dato que se almacenara en el nodo</param>
        /// <param name="padre">El nodo padre del nodo actual</param>
        public NodoBinarioOrdenado (C dato, NodoBinarioOrdenado<C> nodo) 
        {
            Dato = dato;
            Padre(nodo);
            HijoD(null);
            HijoI(null);
        }

        /// <summary>
        /// Indica si este nodo no tiene hijos o todos sus hijos son
        /// árboles vacíos.    
        /// </summary>
        /// <returns>¿el nodo es hoja?</returns>
        public bool EsHoja()
        {
            return HijoD() == null && HijoI() == null;
        }


        public int ActualizaAltura()
        {
            // La altura de un nodo es 1 + la altura máxima de sus hijos.
            int alturaIzquierda = HijoI()?.Altura ?? 0;
            int alturaDerecha = HijoD()?.Altura ?? 0;

            // La nueva altura es 1 + el máximo de las alturas de los hijos.
            return 1 + Math.Max(alturaIzquierda, alturaDerecha);
        }
        
        public void RemueveHijo(INodoBinarioOrdenado<C> hijo) 
        {
            if(hijo != HijoD() && hijo != HijoI()) throw new NoSuchElementException();
            
            if(hijo != HijoD())
            {
                HijoD(null);
            }else if (hijo != HijoI())
            {
                HijoI(null);
            }
        }
        public INodoBinarioOrdenado<C> MásGrande()
        {   

            INodoBinarioOrdenado<C>? másGrande = this;

            if (HijoD() != null && HijoD()?.Dato.CompareTo(másGrande.Dato) > 0)
            {
                másGrande = HijoD();
            }

            if (HijoI() != null && HijoI()?.Dato.CompareTo(másGrande.Dato) > 0)
            {
                másGrande = HijoI();
            }

            return másGrande;
        }
        public INodoBinarioOrdenado<C> MásChico()
        {
            INodoBinarioOrdenado<C>? másChico = this;

            if (HijoD() != null && HijoD()?.Dato.CompareTo(másChico.Dato) < 0)
            {
                másChico = HijoD();
            }

            if (HijoI() != null && HijoI()?.Dato.CompareTo(másChico.Dato) < 0)
            {
                másChico = HijoI();
            }

            return másChico;
        }
    }

    /// <summary>
    /// Implementación de un árbol binario ordenado.
    /// </summary>
    /// <typeparam name="C">Tipo de dato contenido en el árbol.</typeparam>
    public class ÁrbolBinarioOrdenado<C> : IÁrbolBinarioOrdenado<C> where C : IComparable<C>
    {
        public INodoBinarioOrdenado<C>? Raíz { get; private set; }

        public bool IsReadOnly => false;


        /// <summary>
        /// Indica si el objeto comparable <code>o</code> se encuentra en este árbol.
        /// La complejidad de este método es log(n) en promedio.
        /// </summary>
        /// <param name="o">Si el objeto se encuentra en el árbol.</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">
        /// Si <code>o</code> es <code>null</code>.
        /// </exception>
        public bool Contains(C o)
        {
            // Evitamos argumentos nulos
            if(o == null) throw new NullReferenceException();

            INodoBinarioOrdenado<C>? current = Raíz;
            
            // Comenzamos a iterar el arbol y analizar los casos posibles
            while(current!=null)
            {
                int comparison = o.CompareTo(current.Dato);

                // Comenzamos preguntando al nodo actual si es el que estamos buscando
                if(comparison == 0) return true;

                // Si el dato es menor, procedemos a preguntar en el sub arbol izquierdo
                if(comparison < 0) {
                    current = current.HijoI();
                } else {
                    // Si el dato es mayor o igual, procedemos a preguntar al sub arbol derecho
                    current = current.HijoD();
                }

            }
            return false;
        }

        /// <summary>
        /// Se encarga de remover un nodo del arbol después
        /// de verificar que exista de manera efectiva
        /// </summary>
        /// <param name="o"></param>
        /// <returns>Si se pudo eliminar o no</returns>
        /// <exception cref="NullReferenceException">
        /// Si <code>o</code> es <code>null</code>
        /// </exception>
        public bool Remove(C o)
        {
            if (o == null) throw new NullReferenceException();

                if(Raíz is not null)
                {
                    bool result = RemoveNodo(Raíz,o);

                if(result && Raíz?.Dato.Equals(o) == true)
                {
                    Raíz = null; // Actualizamos si la raiz ha sido eliminada
                }

                // Iniciar la eliminación llamando a un método auxiliar
                return result;
            }
            
            return false;
        }

        private bool RemoveNodo(INodoBinarioOrdenado<C> nodoActual, C o)
        {   
            if (nodoActual is not null)
            {
                int comparison = o.CompareTo(nodoActual.Dato);

                if (comparison < 0)
                {
                    // Si el valor es menor, buscamos en el hijo izquierdo
                    return RemoveNodo(nodoActual?.HijoI(), o);
                }
                else if (comparison > 0)
                {
                    // Si el valor es mayor, buscamos en el hijo derecho
                    return RemoveNodo(nodoActual?.HijoD(), o);
                }
                else
                {
                    // Nodo encontrado: actualizamos el árbol
                    if (nodoActual.HijoI() == null)
                    {
                        ReemplazarNodo(nodoActual, nodoActual?.HijoD()); // Reemplazamos el nodo por su hijo derecho
                    }
                    else if (nodoActual.HijoD() == null)
                    {
                        ReemplazarNodo(nodoActual, nodoActual?.HijoI()); // Reemplazamos el nodo por su hijo izquierdo
                    }
                    else
                    {
                        // Caso 2: El nodo tiene 2 hijos
                        NodoBinarioOrdenado<C> sucesor = (NodoBinarioOrdenado<C>)nodoActual.HijoD()?.MásChico();
                        nodoActual.Dato = sucesor.Dato; // Copiamos el valor del sucesor
                                                        // Eliminamos el sucesor
                        RemoveNodo(nodoActual?.HijoD(), sucesor.Dato);
                    }

                    // Se eliminó el nodo
                    return true;
                }
            }else {
                return false;
            }
        }

        private void ReemplazarNodo(INodoBinarioOrdenado<C> nodo, INodoBinarioOrdenado<C> nuevoNodo)
        {
            // Actualizamos la referencia del nodo padre
            if (nodo.Padre() != null)
            {
                if (nodo.Padre()?.HijoI() == nodo)
                {
                    nodo.Padre()?.HijoI(nuevoNodo); // Actualizamos el hijo izquierdo del padre
                }
                else
                {
                    nodo.Padre()?.HijoD(nuevoNodo); // Actualizamos el hijo derecho del padre
                }
            }

            // Si el nuevo nodo no es nulo, actualizamos su padre
            if (nuevoNodo != null)
            {
                nuevoNodo.Padre(nodo.Padre());
            }
        }


        /// <summary>
        /// Lleva el conteo de la cantidad de nodos
        /// que contiene el arbol en todos sus sub arboles 
        /// </summary>
        public int Count
        {
            get
            {
                if (Raíz == null)
                {
                    return 0;
                }

                return CuentaNodos(Raíz);
            }
        }

        /// <summary>
        /// Metodo que se encarga de contar los nodos 
        /// existentes en el arbol binario
        /// </summary>
        /// <param name="nodo"></param>
        /// <returns></returns>
        private int CuentaNodos(INodoBinarioOrdenado<C> nodo)
        {
            if (nodo == null) return 0;

            int count = 1; // Contamos el nodo actual.

            // Si existe un hijo izquierdo, contamos los nodos de su subárbol.
            if (nodo.HijoI() != null)
            {
                count += CuentaNodos(nodo.HijoI());
            }

            // Si existe un hijo derecho, contamos los nodos de su subárbol.
            if (nodo.HijoD() != null)
            {
                count += CuentaNodos(nodo.HijoD());
            }

            return count;
        }

        /// <summary>
        /// Agrega un nodo nuevo al arbol, verificando que 
        /// todo se encuentre dentro de los parametros establecidos 
        /// y siguiendo los parametros 
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void Add(C item)
        {
            // Evitamos argumentos nulos
            if (item == null) throw new NullReferenceException();

            // Si el árbol está vacío, creamos la raíz
            if (Raíz == null)
            {
                Raíz = CreaNodo(item);
                return;
            }

            // Llamamos a un método auxiliar para encontrar la posición correcta
            AgregarNodo(Raíz, item);
        }

        private void AgregarNodo(INodoBinarioOrdenado<C> nodoActual, C item)
        {
            // Comparamos el item con el dato del nodo actual
            if (item.CompareTo(nodoActual.Dato) < 0) // menor
            {
                // Si no hay hijo izquierdo, lo creamos
                if (nodoActual.HijoI() == null)
                {
                    nodoActual.HijoI(CreaNodo(item));
                    nodoActual.HijoI().Padre(nodoActual);
                }
                else
                {
                    // Si ya existe un hijo izquierdo, seguimos buscando
                    AgregarNodo(nodoActual.HijoI(), item);
                }
            }
            else // mayor o igual
            {
                // Si no hay hijo derecho, lo creamos
                if (nodoActual.HijoD() == null)
                {
                    nodoActual.HijoD(CreaNodo(item));
                    nodoActual.HijoD().Padre((NodoBinarioOrdenado<C>)nodoActual);
                }
                else
                {
                    // Si ya existe un hijo derecho, seguimos buscando
                    AgregarNodo(nodoActual.HijoD(), item);
                }
            }
        }

        private NodoBinarioOrdenado<C> CreaNodo( C item )
        {
            return new NodoBinarioOrdenado<C>(item);
        }
        

        /// <summary>
        /// Metodo que limpia el arbol y permite al 
        /// recolector de basura de .NET limpiar las referencias
        /// y recuperar la memoria
        /// </summary>
        public void Clear()
        {
            // Método auxiliar para limpiar nodos de manera recursiva
            void LimpiarNodos(INodoBinarioOrdenado<C> nodo)
            {
                if (nodo is not null)
                {
                    LimpiarNodos(nodo.HijoI());
                    LimpiarNodos(nodo.HijoD());

                    // Desconectamos el nodo
                    nodo.HijoI(null);
                    nodo.HijoD(null);
                    nodo.Padre(null);
                }
            }

            LimpiarNodos(Raíz);
            Raíz = null; // Finalmente establecemos la raíz en null
        }

        /// <summary>
        /// Copia los elementos del árbol a una matriz, comenzando en el índice especificado.
        /// </summary>
        /// <param name="array">La matriz a la que se copian los elementos.</param>
        /// <param name="arrayIndex">El índice en la matriz en el que comenzar a copiar.</param>
        /// <exception cref="ArgumentNullException">Si la matriz es <code>null</code>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si <code>arrayIndex</code> es menor que cero.</exception>
        /// <exception cref="ArgumentException">Si la matriz no es lo suficientemente grande para contener todos los elementos.</exception>
        public void CopyTo(C[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            // Verificamos que hay suficiente espacio en el arreglo
            if (array.Length - arrayIndex < Count) // Count debe devolver el número de elementos en el árbol
            {
                throw new ArgumentException("El arreglo no es lo suficientemente grande.");
            }

            // Usamos una lista para almacenar los elementos en orden
            List<C> elementos = new List<C>();
            if(Raíz is not null)
            {
                ObtenerElementosEnOrden(Raíz, elementos); // Llenamos la lista con elementos en orden

                // Copiamos los elementos a la matriz
                for (int i = 0; i < elementos.Count; i++)
                {
                    array[arrayIndex + i] = elementos[i];
                }
            }
        }

        /// <summary>
        /// Método auxiliar que realiza un recorrido en orden para obtener los elementos del árbol.
        /// </summary>
        /// <param name="nodo">El nodo actual.</param>
        /// <param name="elementos">La lista donde se almacenarán los elementos.</param>
        private void ObtenerElementosEnOrden(INodoBinarioOrdenado<C> nodo, List<C> elementos)
        {
            if (nodo == null) return;

            // Recorrer el hijo izquierdo
            ObtenerElementosEnOrden(nodo.HijoI(), elementos);

            // Agregar el dato del nodo actual
            elementos.Add(nodo.Dato);

            // Recorrer el hijo derecho
            ObtenerElementosEnOrden(nodo.HijoD(), elementos);
        }

        // Implementación del método EstáVacía
        public bool EstáVacía()
        {
            // Un nodo está vacío si no tiene un dato asignado
            // y no tiene hijos izquierdo ni derecho.

            return Raíz == null;
        }
        // Implementación del método IteradorInorden
         public IEnumerator<C> IteradorInorden
        {
            get
            {
                // Aquí va el código para recorrer el árbol en orden (Inorden).
                Stack<INodoBinarioOrdenado<C>> stack = new Stack<INodoBinarioOrdenado<C>>();
                INodoBinarioOrdenado<C>? current = Raíz;

                while (current != null || stack.Count > 0)
                {
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.HijoI();
                    }

                    current = stack.Pop();
                    yield return current.Dato;
                    current = current.HijoD();
                }
            }
        }

        // Implementación de IEnumerable<C>
        public IEnumerator<C> GetEnumerator()
        {
            return IteradorInorden;
        }

        // Implementación de IEnumerable (no genérico)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    

}
