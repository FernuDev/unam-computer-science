namespace ED.PruebasUnitarias;

public class Generadores
{
    private static Random rnd = new Random();
    public static IEnumerable<string> GeneraCadenas()
    {
        
        int nPalabras = rnd.Next(10, 20);
        for(int i = 0; i < nPalabras; i++)
        {
            int nElems = rnd.Next(5);
            int[] dígitos = new int[nElems];
            for(int j = 0; j < nElems; j++)
            {
                dígitos[j] = rnd.Next(10);
            }
            yield return string.Join("", dígitos);
        }
    }

    public static IEnumerable<string?> GeneraCadenasYNull()
    {
        double probaNull = 0.3;
        int nPalabras = rnd.Next(10, 20);
        for(int i = 0; i < nPalabras; i++)
        {
            if(rnd.NextDouble() < probaNull)
            {
                yield return null;
            }
            else
            {
                int nElems = rnd.Next(5);
                int[] dígitos = new int[nElems];
                for(int j = 0; j < nElems; j++)
                {
                    dígitos[j] = rnd.Next(10);
                }
                yield return string.Join("", dígitos);
            }
        }
    }
}