using System.Collections.Generic;

namespace ED.Colecciones;

/// <summary>
/// Interfaz que define un iterador sobre la lista, siguiendo el
/// funcionamiento del iterador bidireccional de Java.
/// 
/// Si la colección es modificada mientras el iterador está en uso, el
/// comportamiento del iterador estará indefinido.
/// </summary>
/// <typeparam name="T">Tipo de los datos contenidos en la lista</typeparam>
public interface IEnumeradorLista<T> : IEnumerator<T>
{
    /// <summary>
    /// Índice del elemento almacenado en Current.
    /// </summary>
    /// <exception cref="NoSuchElementException">Si de momento Current no
    /// tiene un valor válido.
    /// </exception>
    public int Índice
    {
        get;
    }

    /// <summary>
    /// Indica si hay un elemento siguiente después de la posición actual
    /// del iterador.
    /// </summary>
    public bool HasNext
    {
        get;
    }

    /// <summary>
    /// Indica si hay un elemento anterior a la posición actual del
    /// iterador.
    /// </summary>
    public bool HasPrevious
    {
        get;
    }

    /// <summary>
    /// Coloca al iterador sobre el elemento anterior. Si no hay elemento anterior
    /// se coloca antes del primer elemento de la lista y tanto Current como 
    /// Índice quedan indefinidos.
    /// </summary>
    /// <returns>Si hubo un elemento anterior sobre el cual
    /// posicionarse.</returns>
    public bool MovePrevious();

    /// <summary>
    /// Coloca al iterador de modo que la siguiente llamada a MoveNext()
    /// coloque al iterador sobre el elemento en la posición
    /// <code>índice</code>.  Una llamada a MovePevious() lo colcará sobre
    /// <code>índice - 1</code>.
    /// Sí índice es cero, el iterador queda al inicio; si es igual al
    /// tamaño de la lista, queda colocado al final, en ese caso no tiene
    /// siguiente, pero sí previo.
    /// </summary>
    /// <param name="índice">Posición de current</param>
    /// <exception cref="ArgumentOutOfRangeException">Si index es negativo
    /// o mayor al tamaño de la lista.</exception>
    public void ColocaEn(int índice);

    /// <summary>
    /// Remueve de la lista el último elemento que señalado por MoveNext()
    /// o MovePrevious().  Este método sólo se puede ejecutar una vez
    /// por llamada a MoveNext o MovePrevious y sólo si no ha llamado
    /// Agrega(T).
    /// </summary>
    /// <exception cref="IllegalStateException">Si no se ha llamado
    /// MoveNext o MovePrevious, o después de ello se llamó Agrega.
    /// </exception>
    public void Remueve();

    /// <summary>
    /// Reemplaza el elemento seleccionado por MoveNext() o MovePrevious().
    /// Este método sólo se puede llamar si no se han llamado Agrega(T) o
    /// Remueve() después de haber llamado MoveNext() o MovePrevious().
    /// </summary>
    /// <param name="dato">Elemento que reemplazará a que estaba en
    /// esa posición.</param>
    /// <exception cref="IllegalStateException">Si no se ha llamado
    /// MoveNext o MovePrevious, o después de ello se llamó Agrega
    /// o Remueve.</exception>
    public void Asigna(T dato);

    /// <summary>
    /// Inserta el elemento justo entre los elementos en las posiciones
    /// previa y siguiente, justo detrás de la posición indicada por este
    /// iterador, de modo que una llamada a MoveNext no se vea afectada
    /// y una a MovePrevious se desplace hacia el elemento recién insertado.
    /// </summary>
    /// <param name="dato">El dato a insertar.</param>
    public void Agrega(T dato);
}

public interface ILista<T> : IList<T>, IColección<T>
{
    /// <summary>
    /// Devuelve un enumerador colocado sobre index.
    /// </summary>
    /// <param name="índice">Posición del elemento que devolverá
    /// Current</param>
    /// <returns>El enumerador en la posición solicitada.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Si index es negativo
    /// o mayor al tamaño de la lista.</exception>
    public IEnumerator<T> GetEnumerator(int index);
}