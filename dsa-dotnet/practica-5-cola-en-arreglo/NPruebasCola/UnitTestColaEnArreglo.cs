using ED.Colecciones;
using NPruebasColección;
using ED.PruebasUnitarias;
using ED.Estructuras.Lineales.Cola;

namespace NPruebasCola;

/// <summary>
/// Pruebas adaptadas y extendidas de la versión de mindahrelfen y blackzafiro.
/// </summary>
public class Tests : PruebasColección
{
    [OneTimeSetUp]
    public static new void TestFixtureSetup()
    {
        Calificador.TestFixtureSetup();
        AsignaRubros(new Rubro[] {
            new Rubro("Constructor", 1.0),
			new Rubro("Forma", 2.0),
			new Rubro("Mira", 0.5),
            new Rubro("Atiende", 2.0),
            new Rubro("Clear", 0.5),
            new Rubro("GetEnumerator", 1.0),
			new Rubro("Remove", 0.5),
			new Rubro("Contains", 0.5),
			new Rubro("CopyTo", 1.0),
        });
    }

    public override ICola<T> CreateCollection<T>()
    {
        return new ColaEnArreglo<T>();
    }

    public ICola<string> CreaColaLlena()
    {
        ICola<string> estructura = CreateCollection<string>();
		foreach(string cad in cadenasAleatorias)
        {
            estructura.Forma(cad);
        }
        return estructura;
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaConstructor()
    {
        IniciaPrueba("Constructor", "Constructor por defecto", 1.0);
        ColaEnArreglo<int> p = new ColaEnArreglo<int>();
        SumaPuntos(1.0);
    }

        //atiende

    [Test]
    public void AtiendeContainsTest()
    {
        IniciaPrueba("Contains", "Prueba que la estructura no contenga elementos después de borrarlos", 1.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreaColaLlena();

        /**
         * Luego los expulsa hasta que la estructura este vacía.
         */
        while (!estructura.EstáVacía())
        {
            estructura.Atiende();
        }

        /**
         * Revisa que ningún elemento insertado se encuentre en la estructura.
         */
        foreach(string? cad in cadenasAleatorias)
		{
			Assert.That(estructura.Contains(cad), Is.False);
		}

        Assert.That(estructura, Is.Empty);
        SumaPuntos(1.0);
    }

    [Test]
    public void AtiendeCountTest()
    {
        IniciaPrueba("Atiende", "Revisa que la cantidad de elementos tras borrar sea consistente", 1.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreaColaLlena();

        /**
         * Borra los elementos de la estructura de uno en uno y revisa que cada
         * borrado mantenga el tamaño deseado.
         */
        int index = 1;
        int range = cadenasAleatorias.Count;
        while (index <= range)
        {
            estructura.Atiende();
            Assert.That(estructura.Count, Is.EqualTo(range - index++));
        }

        /**
         * Revisa que la estructura este vacía al finalizar el borrado de todos
         * sus elementos.
         */
        Assert.That(estructura.EstáVacía(), Is.True);

        SumaPuntos(1.0);
        
    }

    [Test]
    public void AtiendeEmptyTest()
    {
        
        IniciaPrueba("Atiende", "Revisa que se lance la excepción cuando se intenta borrar y no hay elementos", 1.0);

        /**
         * Si se intenta borrar sobre una estructura vacía debe devolver null.
         */
        ICola<string> estructura = CreateCollection<string>();
        Assert.Throws<InvalidOperationException>(
            () => estructura.Atiende()
        );

        SumaPuntos(1.0);
        
    }

    [Test]
    public void AtiendeMiraTest()
    {
        string str;
        

        IniciaPrueba("Mira", "Prueba que mira y atiende devuelvan el mismo valor", 1.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreaColaLlena();

        /**
         * Revisa hasta que la estructura este vacía que el elemento devuelto
         * por mira sea el mismo que devuelve expulsa.
         */
        while (!estructura.EstáVacía())
        {
            str = estructura.Mira();
            Assert.That(estructura.Atiende(), Is.EqualTo(str));
        }

        SumaPuntos(1.0);
        
    }

    //Forma

    [Test]
    public void FormaContainsTest()
    {
        
        IniciaPrueba("Forma", "Revisa que la estructura contenga los elementos insertados", 1.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreaColaLlena();

        /**
         * Se asegura que los elementos insertados estén contenidos dentro de
         * la estructura.
         */
        foreach(string cad in cadenasAleatorias)
		{
			Assert.That(estructura.Contains(cad), Is.True);
        }

        SumaPuntos(1.0);
        
    }

    [Test]
    public void FormaSizeTest()
    {
        int index;
        

        IniciaPrueba("Atiende", "Revisa que la estructura mantenga la cantidad correcta de elementos, ademas de no estar vacía tras insertar", 1.0);

        /**
         * Inserta elementos en la estructura de uno en uno y revisa que cada
         * inserción mantenga el tamaño deseado. Además, revisa que ninguna
         * inserción vuelva vacía la estructura.
         */
        ICola<string> estructura = CreateCollection<string>();
        
        index = 1;
        foreach(string cad in cadenasAleatorias)
        {
            estructura.Forma(cad);
            Assert.That(estructura.Count, Is.EqualTo(index++));
            Assert.That(estructura.EstáVacía(), Is.False);
        }

        SumaPuntos(1.0);
        
    }

    //FIFO

    [Test]
    public void FIFOTest()
    {
        int index;
        string[] array1, array2;
        

        IniciaPrueba("Forma", "Revisa que la cola sea una estructura FIFO", 1.0);

        /**
         * Inserta elementos en una estructura y en un arreglo en el mismo
         * orden y al mismo tiempo.
         */
        ICola<string> estructura = CreateCollection<string>();
        int range = cadenasAleatorias.Count;
        array1 = new string[range];
        index = 0;
        foreach(string cad in cadenasAleatorias)
		{
            estructura.Forma(cad);
            array1[index++] = cad;
        }

        /**
         * Borra elementos de la estructura y los guarda en un arreglo.
         */
        array2 = new string[range];
        index = 0;
        while (!estructura.EstáVacía())
        {
            array2[index++] = estructura.Atiende();
        }

        /**
         * Compara ambos arreglos, deben tener el mismo orden.
         */
        Assert.That(array1, Is.EqualTo(array2));

        SumaPuntos(1.0);
        
    }
	
	// Collection
	
	[Test]
	public void RemoveNullTest()
    {
		ICola<string> estructura;

		IniciaPrueba("Atiende", "Revisa que se lance NullPointerException si se intenta remover null", 1.0);

		estructura = CreateCollection<string>();
        Assert.Throws<InvalidOperationException>(
            () => estructura.Atiende()
        );
    	SumaPuntos(1.0);
	
	}
	
	[Test]
    public void FormaRemoveTest()
    {
        
        IniciaPrueba("Remove", "Revisa que la estructura remueva el elemento en al frente.", 1.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreaColaLlena();

        /**
         * Se asegura que los elementos insertados estén contenidos dentro de
         * la estructura.
         */
        while (!estructura.EstáVacía())
        {
            Assert.That(estructura.Remove(estructura.Mira()));
        }

        SumaPuntos(1.0);
        
    }
	
	[Test]
    public void FormaRemoveTrueFalseTest()
    {
    
        IniciaPrueba("GetEnumerator", "Revisa que la estructura remueva el elemento en al frente o devuelva false.", 2.0);

        /**
         * Inserta elementos en la estructura.
         */
        ICola<string> estructura = CreateCollection<string>();
		string[] a = {"1","2","3","4","5","6"};
		string[] b = {"1","7","2","5","7","3"};
		bool[] r = {true, false, true, false, false, true};
		
		foreach (string str in a)
        {
			estructura.Forma(str);
		}

        for(int i = 0; i < b.Length; i++)
        {
			Assert.That(estructura.Remove(b[i]), Is.EqualTo(r[i]));
		}

        SumaPuntos(2.0);
        
    }
}