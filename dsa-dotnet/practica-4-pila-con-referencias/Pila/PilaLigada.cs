using System.Collections;
using System.Collections.Generic;
using ED.Colecciones;

namespace ED.Estructuras.Lineales.Pila;

public class PilaLigada<T> : IPila<T>
{
    private Nodo<T>? tope;
    public int Count { get; set; }
    public bool IsReadOnly => false;

    public PilaLigada()
    {
        tope = null;
        Count = 0;
    }

    /// <summary>
    /// Guarda el valor indicado al tope de la pila
    /// </summary>
    /// <param name="e">Valor a guardar</param>
    public void Empuja(T item) {
        Nodo<T> nuevoNodo = new Nodo<T>(item);
        nuevoNodo.Siguiente = tope;
        tope = nuevoNodo;
        Count++;
    }

    /// <summary>
    /// Devuelve el objeto al tope de la pila, eliminandolo de la misma
    /// </summary>
    /// <returns> El objeto en el tope </returns>
    /// <exception cref="InvalidOperationException">Si la pila esta vacia. </exception>
    public T Expulsa() {

        if (EstáVacía()) {
            throw new InvalidOperationException("La pila esta vacia.");
        }

        T valorExpulsado = tope.Valor;
        tope = tope.Siguiente;
        Count--;

        return valorExpulsado;

    }

    /// <summary>
    /// Devuelve el objeto al tope de la pila, sin eliminarlo
    /// </summary>
    /// <returns>El objeto en el tope.</returns>
    /// <exception cref="InvalidOperationException">Si la pila esta vacia </exception>
    public T Mira() {

        if ( EstáVacía() ) {
            throw new InvalidOperationException("La pila esta vacia");
        }

        return tope.Valor;
    }

    /// <summary>
    /// Indica si no hay elementos en la colección.
    /// </summary>
    /// <returns>Si la colección está vacía.</returns>
    public bool EstáVacía() {
        return Count == 0;
    }

    // Implementacion del metodo Empuja(Add)
    public void Add(T item) { 
        Empuja(item); 
    }

    // Implementacion del metodo Remove
    public bool Remove(T item) {
        if (EstáVacía()) return false;

        if(EqualityComparer<T>.Default.Equals(tope!.Valor,item)){
            Expulsa();
            return true;
        }

        return false;
    }

    // Implementacion del metodo Clear para vaciar la pila
    public void Clear() {
        tope = null;
        Count = 0;
    }

    // Implementacion del metodo Contains
    public bool Contains(T item) {
        var actual = tope; // Iterador para la busqueda secuencial
        
        while(actual!=null){
            
            if(item == null) {
                if(actual.Valor == null){
                    return true;
                }
            }else if (EqualityComparer<T>.Default.Equals(actual.Valor, item)){
                return true;
            }
            actual = actual.Siguiente;
        }

        return false;
    }

            // Implementación de CopyTo
    public void CopyTo(T[] array, int arrayIndex)
    {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex + Count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            var actual = tope;
            for (int i = 0; i < Count && actual != null; i++, actual = actual.Siguiente)
            {
                array[arrayIndex + i] = actual.Valor;
            }
        }

        // Implementación del enumerador genérico
    public IEnumerator<T> GetEnumerator()
    {
        var actual = tope;
        while (actual != null)
        {
            yield return actual.Valor;
            actual = actual.Siguiente;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
