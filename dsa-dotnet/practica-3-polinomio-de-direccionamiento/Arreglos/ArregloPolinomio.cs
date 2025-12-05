using System.ComponentModel;
using System.Linq.Expressions;

namespace ED.Estructuras.Lineales.Arreglos;

public class ArregloPolinomio<T> : IArreglo<T> {
   
    /// <summary>
    /// Arreglo unidimensional que almancena los datos
    /// de manera lineal de un arreglo de n-dimensiones
    /// </summary>
    private T[] arreglo;
    private int[] deltas;
    private int size_t = 1;
    private int n = 0;
    
    public ArregloPolinomio (int[] size) {

        deltas = size;
        n = size.Length;

        if(VerificarArrayConstructor(size)) {
            // Calculamos el tamaño del array a crear
            for(int i=0;i<n;i++) {
                size_t*=size[i];
            }

            arreglo = new T[size_t];
        }

    }

    /// <summary>
    /// Implementa una validación general de los arrays, para verificar que no sean arrays vacios
    /// o que contengan dimensiones o indices nulos
    /// </summary>
    /// <param name="índices">Arreglo con los índices de la matriz a aplianar </param>
    /// <returns>Booleano que indica si el arreglo es valido o no, caso contrario lanza alguna excepcion</returns>
    private bool VerificarArrayConstructor(int[] indices) {
        
        if(indices.Length==0) {
            throw new IllegalSizeException();
        }

        for(int i=0;i<indices.Length;i++) {
            if(indices[i]<=0) {
                throw new IllegalSizeException();
            }
        }
        return true;
    }

    /// <summary>
    /// Metodo encargado de las validaciones del metodo de ObtenerIndice, que valida
    /// todas las posibilidades y asi verificar que el arrat de indices es valido
    /// </summary>
    /// <param name="índices">Array de indices</param>
    private void VerificarArray(int[] indices){

        if(indices.Length != n){
            throw new IllegalSizeException();
        }

        for(int i=0;i<n;i++) {
            if(indices[i] >= deltas[i]) {
                throw new IndexOutOfRangeException();
            }
        }

    }

    /// <summary>
    /// Implementación del metodo ObtenerElemento que se encarga de
    /// devolver el elemento que se encuentra en la posición <code>th</code>
    /// en el arreglo multidimensional.
    /// </summary>
    /// <param name="índices">arreglo con los índices del elemento a recuperar.</param>
    /// <returns>el elemento almacenado en la posición <code>i</code>.</returns>
    public T ObtenerElemento(int[] índices){
        return arreglo[ObtenerÍndice(índices)];
    }
    
    /// <summary>
    /// Asigna un elemento en la posición <code>th</code> del arreglo
    /// multidimensional.
    /// </summary>
    /// <param name="índices">Arreglo con los índices donde se almacenará el elemento.</param>
    /// <param name="elem">Elemento a almacenar.</param>
    public void AlmacenarElemento(int[] índices, T elem){
        int ind = ObtenerÍndice(índices);
        arreglo[ind] = elem;
    }


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
    public int ObtenerÍndice(int[] índices){

        VerificarArray(índices);

        int ind = índices[0];
        
        for(int i=1;i<n;i++) {
            ind = índices[i] + deltas[i]*ind;
        }

        return ind;
    }
}
