using System.Collections.Generic;
using System.Text.RegularExpressions;
using ED.Notaciones;

namespace ED.Notaciones;

public class Calculadora
{
    private Opciones notaciónActual = Opciones.Prefija;
    public void ImprimeMenú()
    {
        Console.WriteLine($"Tipo de notación por defecto: {notaciónActual}");
        Console.WriteLine("Para cambiar de notación escribe:");
        foreach(var o in Opciones.GetValues<Opciones>())
        {
            Console.WriteLine($":{o.ToString()}");
        }
        Console.WriteLine("Salir    para terminar la ejecución del programa.");
        Console.WriteLine();
    }

    public string[] SeparaSímbolos(string instrucción)
    {
        //char[] delimitadores = { ' ', '\t' };
        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/divide-up-strings
        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
        String patrón = @"([()])"; //@"(\()*(\d+)(\)*)";
        string[] palabras = instrucción.Split();
        LinkedList<string> símbolos = new LinkedList<string>();
        foreach(var palabra in palabras)
        {
            foreach(String m in Regex.Split(palabra, patrón).Where(s => !string.IsNullOrWhiteSpace(s)))
            {    
                //Console.WriteLine(m);
                símbolos.AddLast(m);
            }
        }
        return símbolos.ToArray();
    }

    /// <summary>
    /// Interfaz de texto.  Permite seleccionar el tipo de notación y escribir
    /// las operaciones a realizar. Los operandos y operadores deben estar
    /// separados por espacios.
    /// </summary>
    public static void Main()
    {
        Calculadora c = new Calculadora();
        c.ImprimeMenú();
        string? instrucción;
        string[] símbolos;
        while((instrucción = Console.ReadLine()) != null && instrucción != "Salir")
        {
            símbolos = c.SeparaSímbolos(instrucción);
            if (símbolos[0][0] == ':')
            {
                Opciones opción = Enum.Parse<Opciones>(símbolos[0].Substring(1));
                if (opción == c.notaciónActual)
                {
                    Console.WriteLine($"La calculadora ya está en notación {opción}");
                }
                else
                {
                    c.notaciónActual = opción;
                    Console.WriteLine($"La calculadora ha cambiado a notación {opción}");
                }
                continue;
            }

            // Debe ser una operación.
            double resultado;
            switch(c.notaciónActual)
            {
                case Opciones.Prefija:
                    resultado = Notaciones.EvalúaPrefija(símbolos);
                    break;
                case Opciones.Postfija:
                    resultado = Notaciones.EvalúaPostfija(símbolos);
                    break;
                case Opciones.Infija:
                    resultado = Notaciones.EvalúaInfija(símbolos);
                    break;
                default:
                    Console.WriteLine("Notación inválida");
                    c.ImprimeMenú();
                    continue;
            }
            Console.WriteLine("= " + resultado);
        }
    }
}
