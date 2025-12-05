namespace ED.Colecciones;

/// <summary>
/// Estructura primero en entrar, primero en salir.
/// </summary>
/// <typeparam name="T">Tipo de datos que contiene la colección.</typeparam>
public interface IPila<T> : IColección<T>
{

    /// <summary>
    /// Devuelve el objeto al tope de la pila, sin eliminarlo de la estructura.
    /// </summary>
    /// <returns>El objeto en el tope.</returns>
    /// <exception cref="InvalidOperationException">Si la pila está vacía.</exception>
    public T Mira();

    /// <summary>
    /// Devuelve el objeto al tope de la pila, eliminándolo de la estructura.
    /// </summary>
    /// <returns>El objeto en el tope.</returns>
    /// <exception cref="InvalidOperationException">Si la pila está vacía.</exception>
    public T Expulsa();

    /// <summary>
    /// Guarda el valor indicado al tope de la pila.
    /// </summary>
    /// <param name="e">Valor a guardar</param>
    public void Empuja(T e);
}
