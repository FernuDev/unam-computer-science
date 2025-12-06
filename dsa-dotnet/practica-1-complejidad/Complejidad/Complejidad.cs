using System.Diagnostics;

namespace Análisis;

/// <summary>
///
/// Clase que permite calcular las funciones Fibonacci y el triangulo
/// de pascal de manera recursiva y manera iterativa para 
/// analizar la complejidad de cada implementacion
///
/// </summary>

public class Complejidad : IComplejidad {
    
    /// <summary>
    /// Numero que lleva la cuenta total de las veces 
    /// que se manda a llamar un metodo especifico
    /// </summary>
    private long contador;
    
    /// <summary>
    /// Devuelve el ultimo valor del contador
    /// desde que se reinició.
    /// <returns>long: El número de operaciones.</returns> 
    /// </summary>
    public long LeeContador() {
        return contador;
    }
    
    /// <summary>
    /// Devuelve el n-esimo elemento de la serie de fibonacci 
    /// calculado de forma recursiva
    /// </summary>
    /// <param name="n"> El indice el elemento que se desea calcular
    /// <returns>El n-esimo elemento de la sucesion de Fibonacci.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si el valor de <code>n</code>
    /// es invalido </exception>
    public int FibonacciRec(int n) {
        if (n<0) throw new ArgumentOutOfRangeException();
        contador = 0;
        return FibonacciRecAux(n);
    }
    
    /// <summary>
    /// Función auxiliar que permite calcular de forma recursiva el 
    /// n-esimo termino de la sucesión
    /// </summary>
    /// <param name="n"> El indice del elemento que se desea calcular
    /// <returns>El n-esimo elemento de la sucesión de Fibonacci</returns>
    private int FibonacciRecAux(int n){
        contador++;
        if (n<2) return n;
        return FibonacciRecAux(n-1) + FibonacciRecAux(n-2);
    }

    /// <summary>
    /// Función que permite calcular el n-esimo termino de forma iterativa
    /// </summary>
    /// <param name="n"> El indice del elemento que se desea calcular
    /// <returns>El n-esimo elemento de la sucesión de Fibonacci</returns>
    /// <exception cref="IndexOutOfBoundsException"> Si el valor de <code>n</code> es
    /// invalido.</exception>
    public int FibonacciIt(int n) {

        ArgumentOutOfRangeException.ThrowIfNegative(n);

        int current = 0;
        int low = 0;
        int fast = 1;

        contador = 0;

        for(int i=1;i<n;i++){
            current = low + fast;
            low = fast;
            fast = current;

            contador++;
        }

        return current;
    }

    /// <summary>
    /// Función que permite calcular, de forma recursiva el elemento de la fila
    /// <code>i</code>, en la columna <code>j</code> del triangulo de Pascal
    /// </summary>
    ///
    /// <param name="ren">El número de fila.</param>
    /// <param name="col">El número de columna.</param>
    /// <returns>El numero de operaciones</returns>
    /// <exception cref="IndexOutOfBoundsException">
    /// Si los indices <code>i</code> i <code>j</code> son invalidos.
    /// </exception>
    public int TPascalRec(int ren, int col) {
        if (ren < 0 || col < 0 || (col > ren && ren < 5)) throw new ArgumentOutOfRangeException();
        contador = 0;
        return TPascalRecAux(ren, col);
    }

    /// <summary>
    /// Método para calcular, de forma recursiva, el elemento en la fila 
    /// <code>i</code> y columna <code>j</code>
    /// del triángulo de Pascal de forma recursiva
    /// </summary>
    ///
    /// <param name="ren">El número de fila.</param>
    /// <param name="col">El número de columna.</param>
    /// <returns>El elemento en la i-ésima fila y la j-ésima columna del 
    /// triángulo de Pascal.</returns>
    private int TPascalRecAux(int ren, int col){
        contador++;
        // Casos fundamentales o triviales
        if (col == 0 || ren == col || ren == 0) return 1;
        return TPascalRecAux(ren-1,col-1) + TPascalRecAux(ren-1,col);
    }


    /// <summary>
    /// Método para calcular, iterativamente, el elemento en la fila
    /// <code>i</code>, en la columna <code>j</code> del triángulo de Pascal.
    /// </summary>
    ///
    /// <param name="ren">El número de fila.</param>
    /// <param name="col">El número de columna.</param>
    /// <returns> El elemento en la i-ésima fila y la j-ésima columna del
    /// triángulo de Pascal.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si los indices <code>i</code>
    /// <code>j</code> son inválidos. </exception>
    public int TPascalIt(int ren, int col){
        if (ren < 0 || col < 0 || (col > ren && ren < 5)) throw new ArgumentOutOfRangeException();

        contador = 0;

        if (col == 0 || ren == col){
            contador++;
            return 1;
        } 

        int number = 1;
        for (int i = 1; i <= col; i++) {
            number = number * (ren - i + 1) / i;
            contador++;
        }

        return number;
    }
}