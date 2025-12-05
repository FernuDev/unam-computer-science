namespace ED.Estructuras.Lineales.Arreglos;

/// <summary>
/// Indica tamaños no adecuados para la estructura.
/// </summary>
public class IllegalSizeException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public IllegalSizeException() {}

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="msg">Información detallada sobre el contexto en que ocurre
    /// la excepción.</param>
    public IllegalSizeException(string msg) : base(msg) {}

    public IllegalSizeException(string msg, Exception inner)
        : base(msg, inner) {}
}