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
        switch(operador) {
            case "+":
                return operando1 + operando2;
            case "-":
                return operando1 - operando2;
            case "*":
                return operando1 * operando2;
            case "/":
                return operando1 / operando2;
            case "%":
                return operando1 % operando2;
            default:
                throw new FormatException("Operador invalido");
        }

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
        switch(operador) {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
            case "%":
                return 2;
            default:
                return 0;
        }
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
        string[] simbolos_invertidos = PrefijaAPostfija(símbolos);
        return EvalúaPostfija(simbolos_invertidos);
    }


    private static string[] PrefijaAPostfija(string[] símbolos)
    {
        Stack<string> pila = new Stack<string>();
        // Invertir el arreglo de símbolos para procesar la notación prefija
        Array.Reverse(símbolos);
        
        foreach (string simbolo in símbolos)
        {
            if (EsNumero(simbolo))
            {
                pila.Push(simbolo);
            }
            else
            {
                // Asegurarse de que haya suficientes operandos
                if (pila.Count < 2)
                {
                    throw new FormatException("No hay suficientes operandos para realizar la operación");
                }

                // Pop los dos operandos de la pila
                string operando1 = pila.Pop();
                string operando2 = pila.Pop();

                // Concatenar los operandos y el operador en la forma postfija
                string resultado = operando1 + " " + operando2 + " " + simbolo;
                pila.Push(resultado);
            }
        }

        if (pila.Count != 1)
        {
            throw new FormatException("La expresión no es válida");
        }

        // Convertir el resultado de la pila a un arreglo de cadenas
        return pila.Pop().Split(' ');
    }

    /// <summary>
    /// Recibe la secuencia de símbolos de una expresión matemática en notación
    /// postfija y calcula el resultado de evaluarla.
    /// </summary>
    /// <param name="símbolos">Lista de símbolos: operadores y números.</param>
    /// <returns>El resultado de la operación.</returns>
    public static double EvalúaPostfija(string[] símbolos)
    {
        Stack<string> pila = new Stack<string>();

        foreach(string simbolo in símbolos){

            if(EsNumero(simbolo)) {
                pila.Push(simbolo);
            } else {
                // Verificando si la pila no esta vacia
                if(pila.Count < 2) {
                    throw new FormatException("No hay suficientes operandos");
                }
                
                double operando2 = double.Parse(pila.Pop());
                double operando1 = double.Parse(pila.Pop());

                pila.Push(Operaciones.Evalúa(simbolo, operando1, operando2).ToString());
            }
        }

        if(pila.Count != 1) {
            throw new FormatException("Existen caracteres extra");
        }

        return double.Parse(pila.Pop());
    }

    private static bool EsNumero(string s) {
        try{

            if(s == null) {
                throw new FormatException();
            }

            double.Parse(s);
            return true;
        }catch(FormatException){
            return false;
        }
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
        Stack<string> pila = new Stack<string>();
        Queue<string> cola = new Queue<string>();

        foreach(string simbolo in símbolos) {
            if(EsNumero(simbolo)) {
                cola.Enqueue(simbolo);
            }else {
                if(simbolo == ")") {
                    while( pila.Peek() != "(" ) {
                        cola.Enqueue(pila.Pop());
                    }
                    pila.Pop();
                }else if(simbolo == "(") {
                    pila.Push(simbolo);
                }else {
                    int prec = Operaciones.Precedencia(simbolo);
                    while(pila.Count!=0 && pila.Peek()!="(" && Operaciones.Precedencia(pila.Peek()) >= prec){
                        cola.Enqueue(pila.Pop());
                    }
                    pila.Push(simbolo);
                }
            }
        }

        while(pila.Count!=0) {
            cola.Enqueue(pila.Pop());
        }

		return cola.ToArray();
    }

    /// <summary>
    /// Recibe la secuencia de símbolos de una expresión matemática en notación
    /// infija y calcula el resultado de evaluarla.
    /// </summary>
    /// <param name="símbolos">Lista de símbolos: operadores, paréntesis y números.</param>
    /// <returns>El resultado de la operación.</returns>
    public static double EvalúaInfija(string[] símbolos) {
        string[] simbolos_sufija = InfijaASufija(símbolos);
        return EvalúaPostfija(simbolos_sufija);
    }
}
