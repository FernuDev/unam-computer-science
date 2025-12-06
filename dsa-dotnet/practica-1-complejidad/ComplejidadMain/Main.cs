using System.IO;

namespace Análisis;

public class UsoComplejidad
{
    public static void Main(string[] args)
    {
        Complejidad c = new Complejidad();

        // Numero de operaciones realizadas por cada algoritmo
        // Generamos los datos del report en base a los datos de las funciones

        // Fibonnacci recursivo
        for(var i=0;i<=50;i++) {
            c.FibonacciRec(i);
            IComplejidad.EscribeOperaciones("./Data/FibRec.dat", i, 0, c.LeeContador());
        }

        // Fibonnacii Iterativo
        for(var i=0;i<=50;i++) {
            c.FibonacciIt(i);
            IComplejidad.EscribeOperaciones("./Data/FibIt.dat", i, 0, c.LeeContador());
        }

        // Triangulo de pascal Recursivo
        for(var i=0;i<=30;i++) {
            for(var j=0;j<=i;j++) {
                c.TPascalRec(i,j);
                IComplejidad.EscribeOperaciones("./Data/PascRec.dat",i,j, c.LeeContador());
            }
            IComplejidad.EscribeLineaVacía("./Data/PascRec.dat");
        }

        // Triangulo de pascal Iterativo
        for(var i=0;i<=30;i++) {
            for(var j=0;j<=i;j++) {
                c.TPascalIt(i,j);
                IComplejidad.EscribeOperaciones("./Data/PascIt.dat", i, j, c.LeeContador());
            }
            IComplejidad.EscribeLineaVacía("./Data/PascIt.dat");
        }
    }
}
