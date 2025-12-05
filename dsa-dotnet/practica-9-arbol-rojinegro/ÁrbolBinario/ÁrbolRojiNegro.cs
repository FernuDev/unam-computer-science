using ED.Colecciones;
using System.Collections;
using System.Collections.Generic;


namespace ED.Estructuras.NoLineales.ÁrbolBinario
{

    public enum Color 
    {
        Rojo,
        Negro
    }

    public class NodoRojinegro<C> :NodoBinarioOrdenado<C>, INodoBinarioOrdenado<C>  where C : IComparable<C>
    {
        public NodoRojinegro(C dato) : base(dato)
        {
        }

        public NodoRojinegro(C dato, NodoBinarioOrdenado<C> nodo) : base(dato, nodo)
        {
        }

        public Color Color { get; private set; }

    }

    public class ÁrbolRojinegro<C> : ÁrbolBinarioOrdenado<C>, IÁrbolBinarioOrdenado<C> where C : IComparable<C>
    {

    }
}
