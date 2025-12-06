using System.Collections.Generic;
using System.Text.RegularExpressions;
using ED.Notaciones;

namespace ED.Notaciones;

public class Calculadora
{
    private Opciones notaciónActual = Opciones.Prefija;
    public void ImprimeMenú()
    {   
        Console.Clear();
        Console.WriteLine("========================= Interprete matematico =========================\n");
        Console.WriteLine("\tPrograma que permite evaluar expresiones matematicas");
        Console.WriteLine("\tDigita la operacion que desees realizar en la notacion");
        Console.WriteLine("\tseleccionada, en caso de necesitar cambiarla puedes hacerlo");
        Console.WriteLine("\tdigitanto :{Opcion}, una vez que termines digita Salir\n");
        Console.WriteLine($"\tTipo de notación por defecto: {notaciónActual}");
        Console.WriteLine("\tPara cambiar de notación escribe:");
        foreach(var o in Opciones.GetValues<Opciones>())
        {
            Console.WriteLine($"\t\t:{o.ToString()}");
        }
        Console.WriteLine("\tSalir    para terminar la ejecución del programa.\n");
        Console.WriteLine("=========================================================================");
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

        Console.Write("\tDigite su operacion: ");
        while((instrucción = Console.ReadLine()) != null && instrucción != "Salir")
        {
            símbolos = c.SeparaSímbolos(instrucción);
            if (símbolos[0][0] == ':')
            {
                Opciones opción = Enum.Parse<Opciones>(símbolos[0].Substring(1));
                if (opción == c.notaciónActual)
                {
                    c.ImprimeMenú();
                    Console.WriteLine($"\tLa calculadora ya está en notación {opción}");
                    Console.Write("\tDigite su operacion: ");

                }
                else
                {
                    c.notaciónActual = opción;
                    c.ImprimeMenú();
                    Console.WriteLine($"\tLa calculadora ha cambiado a notación {opción}");
                    Console.Write("\tDigite su operacion: ");

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
                    Console.WriteLine("\tNotación inválida");
                    c.ImprimeMenú();
                    continue;
            }
            Console.WriteLine($"\t{instrucción} = {resultado}");
            Console.Write("\tDigite su operacion: ");
        }
    }
}
