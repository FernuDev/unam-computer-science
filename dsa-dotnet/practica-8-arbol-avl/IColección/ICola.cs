namespace ED.Colecciones;

/// <summary>
/// Estructura "Primero en entrar, primero en salir".
/// Esta implementación verifica que no se formen elementos nulos.
/// </summary>
/// <typeparam name="T">Tipo de datos que contiene la colección.</typeparam>
public interface ICola<T> : IColección<T>
{
    /// <summary>
    /// Muestra el elemento al inicio de la cola.
    /// </summary>
    /// <returns>Una referencia o valor del elemento siguiente.</returns>
    /// <exception cref="InvalidOperationException">Si la cola está vacía.</exception>
    public T Mira();

    /// <summary>
    /// Devuelve el elemento al inicio de la cola y lo elimina.
    /// </summary>
    /// <returns>Una referencia o valor del elemento siguiente.</returns>
    /// <exception cref="InvalidOperationException">Si la cola está vacía.</exception>
    public T Atiende();

    /// <summary>
    /// Agrega un elemento al final de la cola. Se agrega como referencia
    /// o como valor dependiendo del tipo T.
    /// </summary>
    /// <param name="e">Elemento a agregar.</param>
    /// <exception cref="ArgumentNullException">Si se intenta forma un elemento nulo.</exception>
    public void Forma(T e);
}