using System.Collections;
using System.Collections.Generic;

namespace ED.Colecciones;

public interface IColección<T> : ICollection<T>
{
    /// <summary>
    /// Indica si no hay elementos en la colección.
    /// </summary>
    /// <returns>Si la colección está vacía.</returns>
    public bool EstáVacía();

}
