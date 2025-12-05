namespace ED.Colecciones;

/// <summary>
/// Representa nodos con datos ordenables. Todos los nodos con datos a la
/// izquierda de este nodo anteceden al dato en este nodo, todos los datos en
/// nodos a su derecha son iguales o le suceden.
/// </summary>
/// <typeparam name="C">Tipo de dato contenido en el nodo.</typeparam>
public interface INodoBinarioOrdenado<C> where C : IComparable<C>
{
    /// <summary>
    /// Dato almacenado en este nodo.
    /// </summary>
    /// <exception cref="NullReferenceException">Si se intenta asignar
    /// un dato <code>null</code>.
    /// </exception>
    C Dato
    {
        get;
        set;
    }

    INodoBinarioOrdenado<C>? Padre
    {
        get;
        set;
    }

    INodoBinarioOrdenado<C>? HijoI
    {
        get;
        set;
    }

    INodoBinarioOrdenado<C>? HijoD
    {
        get;
        set;
    }

    int Altura
    {
        get;
    }

    /// <summary>
    /// Indica si este nodo no tiene hijos o todos sus hijos son
    /// árboles vacíos.    
    /// </summary>
    /// <returns>¿el nodo es hoja?</returns>
    public bool EsHoja();

    /// <summary>
    /// Actualiza el valor de la altura de este nodo, asumiendo que
    /// el altura que reportan sus hijos es correcta.
    /// </summary>
    /// <returns>El valor actualizado.</returns>
    public int ActualizaAltura();

    /// <summary>
    /// Remueve su referencia al hijo indicado, asignándole el valor
    /// <code>null</code>.
    /// </summary>
    /// <param name="hijo">Nodo a remover</param>
    /// <exception cref="NoSuchElementException">Si <code>hijo</code>
    /// no es hijo de este nodo.
    /// </exception>
    public void RemueveHijo(INodoBinarioOrdenado<C> hijo);

    /// <summary>
    /// Devuelve el nodo con el datos más grande a partir de este nodo,
    /// puede devolverse a sí mismo.
    /// </summary>
    /// <returns>Nodo con el dato más grande.</returns>
    public INodoBinarioOrdenado<C> MásGrande();

    /// <summary>
    /// Devuelve el nodo con el datos más chico a partir de este nodo,
    /// puede devolverse a sí mismo.
    /// </summary>
    /// <returns>Nodo con el dato más chico.</returns>
    public INodoBinarioOrdenado<C> MásChico();
}

public interface IÁrbolBinarioOrdenado<C> : IColección<C> where C : IComparable<C>
{
    /// <summary>
    /// Devuelve una referencia al nodo raíz.
    /// </summary>
    public INodoBinarioOrdenado<C>? Raíz
    {
        get;
    }

    /// <summary>
    /// Devuelve un enumerador que devuelve los datos del árbol en inorden.
    /// Este iterador no debe remover elementos.
    /// </summary>
    /// <returns>Un iterador inorden.</returns>
    public IEnumerator<C> IteradorInorden
    {
        get;
    }

    /// <summary>
    /// Indica si el objeto comparable <code>o</code> se encuentra en este árbol.
    /// La complejidad de este método es log(n) en promedio.
    /// </summary>
    /// <param name="o">Si el objeto se encuentra en el árbol.</param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException">
    /// Si <code>o</code> es <code>null</code>.
    /// </exception>
    public new bool Contains(C o);
    
    /// <summary>
    /// Remueve el objeto comparable <code>o</code>. La complejidad de este
    /// método es log(n) en promedio.
    /// </summary>
    /// <param name="o">El objeto a remover.</param>
    /// <returns>Si el objeto estuvo presente y lo removió.</returns>
    /// <exception cref="NullReferenceException">
    /// Si <code>o</code> es <code>null</code>.
    /// </exception>
    public new bool Remove(C o);
}