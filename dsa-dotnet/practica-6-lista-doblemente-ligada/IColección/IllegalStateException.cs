namespace ED.Colecciones;

/// <summary>
/// Excepción utilizada cuando se solicita un elemento no existente
/// a una colección.
/// </summary>
public class IllegalStateException : Exception
{
    public IllegalStateException() : base() {}
}