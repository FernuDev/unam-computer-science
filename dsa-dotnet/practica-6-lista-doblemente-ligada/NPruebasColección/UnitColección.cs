using ED.PruebasUnitarias;

namespace NPruebasColección;

public abstract class PruebasColección : Calificador
{

    [Test]
    public void TestClear()
    {
        IniciaPrueba("Clear", "Vaciar la colección", 1.0);
        ICollection<string> c = CreateCollection<string>();
        c.Add("A");
        c.Add("b");
        c.Add("c");
        c.Clear();
        Assert.That(c, Is.Empty);
        SumaPuntos(0.5);
        // Funciona después de limpiarla.
        c.Add("d");
        Assert.That(c.Count, Is.EqualTo(1));
        SumaPuntos(0.5);
    }

    [Test]
    public void TestCopyTo()
    {
        IniciaPrueba("CopyTo", "Copia la colección en el arreglo", 1.0);
        ICollection<string> c = CreateCollection<string>();
        foreach(string cad in cadenasAleatorias)
        {
            c.Add(cad);
        }

        string[] vals = {"Hola", "Hallo", "Hello"};
        string[] arr = new string[vals.Length + cadenasAleatorias.Count];

        // Llena el arreglo con algunos datos al inicio.
        for(int i = 0; i < vals.Length; i++)
        {
            arr[i] = vals[i];
        }


        c.CopyTo(arr, vals.Length);

        // Los datos que estaban se deben preservar
        for(int i = 0; i < vals.Length; i++)
        {
            Assert.That(arr[i], Is.EqualTo(vals[i]));
        }

        // Verifica que haya copiado bien los datos en las casillas siguientes.
        int index = vals.Length;
        foreach(string cad in c)
        {
            //Console.WriteLine(arr[index] + " - " + cad);
            Assert.That(arr[index], Is.EqualTo(cad));
            index++;
        }
        SumaPuntos(1.0);
    }
}