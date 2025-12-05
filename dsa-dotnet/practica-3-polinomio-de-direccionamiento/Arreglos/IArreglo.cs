namespace ED.Estructuras.Lineales.Arreglos;

/// <summary>
/// Arreglo de n-dimensiones.
/// </summary>
public interface IArreglo<T>
{
    /// <summary>
    /// Devuelve el elemento que se encuentra en la posición <code>th</code>
    /// en el arreglo multidimensional.
    /// </summary>
    /// <param name="índices">arreglo con los índices del elemento a recuperar.</param>
    /// <returns>el elemento almacenado en la posición <code>i</code>.</returns>
    public T ObtenerElemento(int[] índices);

    /// <summary>
    /// Asigna un elemento en la posición <code>th</code> del arreglo
    /// multidimensional.
    /// </summary>
    /// <param name="índices">Arreglo con los índices donde se almacenará el elemento.</param>
    /// <param name="elem">Elemento a almacenar.</param>
    public void AlmacenarElemento(int[] índices, T elem);

    /// <summary>
    /// Devuelve la posición <code>i</code> del elemento en el arreglo de una
    /// dimensión.
    /// </summary>
    /// <param name="índices">arreglo con los índices donde está el elemento
    /// en el arreglo multidimensional. Se debe cumplir que cada índice es
    /// positivo y menor que el tamaño de la dimensión correspondiente.</param>
    /// <returns>la posición del elemento en el arreglo de una dimensión.</returns>
    /// <exception cref="IllegalSizeException">el número de índices en el parámetro
    /// no coincide con el número de dimensiones del arreglo.</exception>
    /// <exception cref="IndexOutOfRangeException">si alguno de los índices del
    /// arreglo no está dentro del rango..</exception>
    public int ObtenerÍndice(int[] índices);
}