namespace ED.Notaciones;

public class Operaciones
{
    /// <summary>
    /// Evalúa la operación indicada por <code>operador</code>.
    /// Por ejemplo: si operador es '*' devuelve operador1 * operador2.
    /// </summary>
    /// <param name="operador">character con el operador correspondiente a la
    /// operación que se desea realizar.</param>
    /// <param name="operando1">primer operando.</param>
    /// <param name="operando2">segundo operando.</param>
    /// <returns>el resultado de aplicar la operación a los operandos.</returns>
    /// <exception cref="NotImplementedException">Si el operador no está soportado.</exception>
    public static double Evalúa(string operador, double operando1, double operando2)
    {
        return 0;
    }

    /// <summary>
    /// Devuelve la precedencia de cada operador.  Entre mayor es la precedencia,
    /// más pronto deberá ejecutarse la operación.
    /// </summary>
    /// <param name="operador">Símbolo que representa a las operaciones: +,-,*,/.</param>
    /// <returns>Valor de precedencia.</returns>
    /// <exception cref="NotImplementedException">para cualquier otro símbolo.</exception>
    public static int Precedencia(string operador)
    {
        return 0;
    }
}

/// <summary>
/// Clase para evaluar expresiones en notaciones prefija, postfija e infija.
/// </summary>
public class Notaciones
{
    /// <summary>
    /// Recibe la secuencia de símbolos de una expresión matemática en notación
    /// prefija y calcula el resultado de evaluarla.
    /// </summary>
    /// <param name="símbolos">Lista de símbolos: operadores y números.</param>
    /// <returns>El resultado de la operación.</returns>
    public static double EvalúaPrefija(string[] símbolos)
    {
        return 0;
    }

    /// <summary>
    /// Recibe la secuencia de símbolos de una expresión matemática en notación
    /// postfija y calcula el resultado de evaluarla.
    /// </summary>
    /// <param name="símbolos">Lista de símbolos: operadores y números.</param>
    /// <returns>El resultado de la operación.</returns>
    public static double EvalúaPostfija(string[] símbolos)
    {
        return 0;
    }

    /// <summary>
    /// Pasa las operaciones indicadas en notación infija a notación sufija o
    /// postfija.
    /// </summary>
    /// <param name="símbolos">Arreglo con símbolos de operaciones (incluyendo
    /// paréntesis) y números (según la definición aceptada por
    /// <code>Double.Parse()</code> en orden infijo.</param>
    /// <returns>Arreglo con símbolos de operaciones (sin incluir paréntesis)
    /// y números en orden postfijo.</returns>
    public static string[] InfijaASufija(string[] símbolos)
    {
		return new string[0];
    }

    /// <summary>
    /// Recibe la secuencia de símbolos de una expresión matemática en notación
    /// infija y calcula el resultado de evaluarla.
    /// </summary>
    /// <param name="símbolos">Lista de símbolos: operadores, paréntesis y números.</param>
    /// <returns>El resultado de la operación.</returns>
    public static double EvalúaInfija(string[] símbolos) {
        return 0;
    }
}
