using System.Text;

namespace Análisis;

/// <summary>
/// Interfaz que define los métodos para utilizar con las pruebas unitarias.
/// </summary>
public interface IComplejidad
{
    /// <summary>
    /// Devuelve el número de operaciones acumuladas desde la última vez que se
    /// reinició el contador.
    /// </summary>
    /// <returns>El número de operaciones.</returns>
	public long LeeContador();

    /// <summary>
    /// Método para guardar las operaciones cuando sólo se usa un parámetro.
    /// </summary>
    /// <param name="archivo">archivo nombre del archivo en el cual se agrega
    /// el reporte del número de operaciones que tardó en ejecutarse el
    /// método.</param>
    /// <param name="par">Valor que se usó al llamar al último método ejecutado.</param>
    /// <param name="ops">Número de operaciones realizadas.</param>
	public static void EscribeOperaciones(string archivo, int par, long ops)
    {
		// TODO: Escribe tu código aquí y borra la excepción.
        throw new NotImplementedException();
	}

    /// <summary>
    /// Método para guardar las operaciones cuando se llamó una función con dos
    /// parámetros.
    /// </summary>
    /// <param name="archivo">archivo nombre del archivo en el cual se agrega
    /// el reporte del número de operaciones que tardó en ejecutarse el
    /// método.</param>
    /// <param name="par1">Primer valor que se usó al llamar al último método ejecutado.</param>
    /// <param name="par2">Segundo valor que se usó al llamar al último método ejecutado.</param>
    /// <param name="ops">Número de operaciones realizadas.</param>
	public static void EscribeOperaciones(String archivo, int par1, int par2, long ops)
    {   
		// TODO: Escribe tu código aquí y borra la excepción.
        FileStream flujo = new FileStream(archivo, FileMode.Append);
        using (StreamWriter escritor = new StreamWriter(flujo, Encoding.UTF8)) 
        {
            if (par2 != 0){
                escritor.WriteLine($"{par1} {par2} {ops}");
            }else {
                escritor.WriteLine($"{par1} {ops}");
            }
        }
	}

    /// <summary>
    /// Método para escribir una línea en blanco en un archivo. Se utilizará para
    /// las gráficas 3D.
    /// </summary>
    /// <param name="archivo">Nombre del archivo.</param>
	public static void EscribeLineaVacía(String archivo)
    {
        FileStream flujo = new FileStream(archivo, FileMode.Append);
        using (StreamWriter escritor = new StreamWriter(flujo, Encoding.UTF8))
        {
            escritor.WriteLine();
        }
	}

    /// <summary>
    /// Devuelve el n-esimo elemento, calculado de forma recursiva, de la
    /// sucesion de Fibonacci.  Por conveción fibonacci(0) = 0, fibonacci(1) = 1.
    /// </summary>
    /// 
    /// <param name="n">El indice del elemento que se desea calcular.</param>
    /// <returns>El n-esimo elemento de la sucesion de Fibonacci.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si el valor de <code>n</code>es
    /// inválido.</exception>
	public int FibonacciRec(int n);


    /// <summary>
    /// Devuelve el n-esimo elemento, calculado de forma iterativa, de la
    /// sucesion de Fibonacci.  Por conveción fibonacci(0) = 0, fibonacci(1) = 1.
    /// </summary>
    /// 
    /// <param name="n">El indice del elemento que se desea calcular.</param>
    /// <returns>El n-esimo elemento de la sucesion de Fibonacci.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si el valor de <code>n</code>es
    /// inválido.</exception>
	public int FibonacciIt(int n);

    /// <summary>
    /// Método para calcular, de forma recursiva, el elemento en la fila
    /// <code>i</code>, en la columna <code>j</code> del triangulo de Pascal
    /// </summary>
    /// 
    /// <param name="ren">El número de fila.</param>
    /// <param name="col">El número de columna.</param>
    /// <returns>El número de operaciones.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si los indices <code>i</code> o
    /// <code>j</code> son inválidos.</exception>
	public int TPascalRec(int ren, int col);

    /// <summary>
    /// Método para calcular, iterativamente, el elemento en la fila
    /// <code>i</code>, en la columna <code>j</code> del triangulo de Pascal
    /// </summary>
    /// 
    /// <param name="ren">El número de fila.</param>
    /// <param name="col">El número de columna.</param>
    /// <returns>El elemento en la i-ésima fila y la j-esima columna del
    /// triángulo de Pascal.</returns>
    /// <exception cref="IndexOutOfBoundsException">Si los indices <code>i</code> o
    /// <code>j</code> son inválidos.</exception>
	public int TPascalIt(int ren, int col);

}