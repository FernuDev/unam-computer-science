namespace ED.Uso;

using ED.Estructuras.Lineales.Arreglos;
public class UsoArreglos
{
    public static void Main()
    {
        int d0 = 3, d1 = 10, d2 = 5;
        IArreglo<string> a = new ArregloPolinomio<string>(new int [] {d0, d1, d2});
        a.AlmacenarElemento(new int[]{0, 0, 0}, "Mo");
        a.AlmacenarElemento(new int[]{0, 0, 1}, "na");
        a.AlmacenarElemento(new int[]{0, 0, 2}, "gui");
        a.AlmacenarElemento(new int[]{0, 0, 3}, "llo");
        a.AlmacenarElemento(new int[]{0, 1, 0}, "Mo");
        a.AlmacenarElemento(new int[]{0, 1, 1}, "na");
        a.AlmacenarElemento(new int[]{0, 1, 2}, "gui");
        a.AlmacenarElemento(new int[]{0, 1, 3}, "llo");
        a.AlmacenarElemento(new int[]{1, 0, 0}, "¿");
        a.AlmacenarElemento(new int[]{1, 1, 0}, "Dón");
        a.AlmacenarElemento(new int[]{1, 1, 1}, "de");
        a.AlmacenarElemento(new int[]{1, 2, 0}, "es");
        a.AlmacenarElemento(new int[]{1, 2, 1}, "tás");
        a.AlmacenarElemento(new int[]{1, 3, 0}, "?");
        a.AlmacenarElemento(new int[]{1, 4, 0}, "¿");
        a.AlmacenarElemento(new int[]{1, 5, 0}, "Dón");
        a.AlmacenarElemento(new int[]{1, 5, 1}, "de");
        a.AlmacenarElemento(new int[]{1, 6, 0}, "es");
        a.AlmacenarElemento(new int[]{1, 6, 1}, "tás");
        a.AlmacenarElemento(new int[]{1, 7, 0}, "?");
        a.AlmacenarElemento(new int[]{2, 0, 0}, "To");
        a.AlmacenarElemento(new int[]{2, 0, 1}, "ca");
        a.AlmacenarElemento(new int[]{2, 1, 0}, "la");
        a.AlmacenarElemento(new int[]{2, 2, 0}, "cam");
        a.AlmacenarElemento(new int[]{2, 2, 1}, "pa");
        a.AlmacenarElemento(new int[]{2, 2, 2}, "na");

        for(int i = 0; i < d0; i++)
        {
            for(int j = 0; j < d1; j++)
            {
                for(int k = 0; k < d2; k++)
                {
                    Console.Write("" + a.ObtenerElemento(new int[]{i ,j, k}));
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
