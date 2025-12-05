namespace ED.Ordenamientos;

/// <summary>
/// Interfaz que representa las acciones que debe realizar cualquier objeto
/// capaz de ordenar los elementos en un arreglo.
/// </summary>
/// <typeparam name="C"></typeparam>
public interface IOrdenador<C> where C : IComparable<C>
{
    /// <summary>
    /// Crea un arreglo nuevo con los elementos ordenados.
    /// </summary>
    /// <param name="a">El arreglo cuyos elementos se quieren ordenar.</param>
    /// <returns>Un arreglo nuevo, del mismo tipo de <code>a</code> pero con los
    /// elementos en el orden dictado por <code>CompareTo</code>.</returns>
    C[] Ordena(C[] a);

    /// <summary>
    /// Devuelve un arreglo de enteros de tal manera que, si es ordenado con un
    /// objeto de esta clase, será el peor caso para la complejidad en tiempo.
    /// </summary>
    /// <param name="tam">La longitud del arreglo a generar.</param>
    /// <returns>Arreglo con el peor caso para el algoritmo de ordenamiento
    /// implementado.</returns>
    int[] PeorCaso(int tam);

    /// <summary>
    /// Devuelve un arreglo de enteros de tal manera que, si es ordenado con un
    /// objeto de esta clase, será el mejor caso para la complejidad en tiempo.
    /// </summary>
    /// <param name="tam">La longitud del arreglo a generar.</param>
    /// <returns>Arreglo con el mejor caso para el algoritmo de ordenamiento
    /// implementado.</returns>
    int[] MejorCaso(int tam);

    /// <summary>
    /// Intercambia indicadas en el arreglo los elementos en las posiciones.
    /// </summary>
    /// <param name="a">Arreglo</param>
    /// <param name="i">Primer índice</param>
    /// <param name="j">Segundo índice</param>
    static void Swap(C[] a, int i, int j)
    {
		C temp = a[i];
		a[i] = a[j];
		a[j] = temp;
	}
}
