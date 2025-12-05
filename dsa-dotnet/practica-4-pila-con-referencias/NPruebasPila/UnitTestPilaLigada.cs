using ED.Colecciones;
using NPruebasColección;
using ED.PruebasUnitarias;
using ED.Estructuras.Lineales.Pila;

namespace NPruebasPila;

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
            new Rubro("Constructor", 1.5),
			new Rubro("Empuja", 1.0),
			new Rubro("Mira", 0.5),
            new Rubro("Expulsa", 1.0),
			new Rubro("Remove", 0.5),
            new Rubro("Clear", 0.5),
			new Rubro("Contains", 1.0),
			new Rubro("CopyTo", 1.0),
        });
    }

    public override IPila<T> CreateCollection<T>()
    {
        return new PilaLigada<T>();
    }

    public IPila<string?> CreaPilaLlena()
    {
        IPila<string?> estructura = CreateCollection<string?>();
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
            estructura.Empuja(cad);
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
        PilaLigada<int> p = new PilaLigada<int>();
        SumaPuntos(1.0);
    }

    //Expulsa
	[Test]
	public void ExpulsaContainsTest()
	{
		IniciaPrueba("Expulsa", "Prueba que la estructura no contenga elementos después de borrarlos", 1.0);

        IPila<string> estructura = CreateCollection<string>();
		/**
		 * Inserta elementos en la estructura.
		 */
        foreach(string cad in Generadores.GeneraCadenas())
        {
            estructura.Empuja(cad);
        }

		/**
		 * Luego los Expulsa hasta que la estructura este vacía.
		 */
		while (!estructura.EstáVacía())
        {
			estructura.Expulsa();
		}

		/**
		 * Revisa que ningún elemento insertado se encuentre en la estructura.
		 */
		Assert.That(estructura, Is.Empty);
		SumaPuntos(1.0);
	}

	[Test]
	public void ExpulsaCuentaTest()
	{
		int index;
		

		IniciaPrueba("Expulsa", "Revisa que la cantidad de elementos tras borrar sea consistente", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		IPila<string?> estructura = CreaPilaLlena();

		/**
		 * Borra los elementos de la estructura de uno en uno y revisa que cada
		 * borrado mantenga el tamaño deseado.
		 */
		int range = estructura.Count;
		index = 1;
		while (index <= range)
        {
			estructura.Expulsa();
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
	public void ExpulsaEmptyTest()
	{
		IPila<string> estructura;

		IniciaPrueba("Expulsa", "Revisa que se devuelva null cuando se intenta borrar y no hay elementos", 1.0);

		/**
		 * Si se intenta borrar sobre una estructura vacía debe lanzar una excepción.
		 */
		estructura = CreateCollection<string>();
        Assert.Throws<InvalidOperationException>(
            () => estructura.Expulsa()
        );

		SumaPuntos(1.0);
	}

	[Test]
	public void ExpulsaMiraTest()
	{
		IniciaPrueba("Mira", "Prueba que Mira y Expulsa devuelvan el mismo valor", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		IPila<string?> estructura = CreaPilaLlena();

		/**
		 * Revisa hasta que la estructura este vacía que el elemento devuelto
		 * por Mira sea el mismo que devuelve Expulsa.
		 */
        String? str;
		while (!estructura.EstáVacía())
        {
			str = estructura.Mira();
			if (str == null)
            {
                Assert.That(estructura.Expulsa(), Is.Null);
			}
            else
            {
                Assert.That(estructura.Expulsa(), Is.EqualTo(str));
			}
		}

		SumaPuntos(1.0);
	}

	//Empuja
	[Test]
	public void PruebaEmpujaContains()
    {
		IniciaPrueba("Contains", "Revisa que la estructura contenga los elementos insertados", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		IPila<string?> estructura = CreaPilaLlena();

		/**
		 * Se asegura que los elementos insertados estén contenidos dentro de la
		 * estructura.
		 */
		foreach(string? cad in cadenasAleatoriasConNulls)
		{
			Assert.That(estructura.Contains(cad), Is.True);
		}
		SumaPuntos(1.0);
	}

	[Test]
	public void EmpujaSizeTest()
	{
		IniciaPrueba("Empuja", "Revisa que la estructura mantenga la cantidad correcta de elementos, ademas de no estar vacía tras insertar", 1.0);

		int tam;
		IPila<string?> estructura;

		/**
		 * Inserta elementos en la estructura de uno en uno y revisa que cada
		 * inserción mantenga el tamaño deseado. Además, revisa que ninguna
		 * inserción vuelva vacía la estructura.
		 */
		estructura = CreateCollection<string?>();
		tam = 1;
		foreach(string? cad in cadenasAleatoriasConNulls)
		{
			estructura.Empuja(cad);

			Assert.That(estructura.Count, Is.EqualTo(tam++));
			Assert.That(estructura.EstáVacía(), Is.False);
		}
		SumaPuntos(1.0);
	}

	//LIFO
	[Test]
	public void LIFOTest()
	{
		int index;
		string?[] array1, array2;
		IPila<string?> estructura;

		IniciaPrueba("Empuja", "Revisa que la pila sea una estructura LIFO", 1.0);

		/**
		 * Inserta elementos en una estructura y en un arreglo en el mismo orden
		 * y al mismo tiempo.
		 */
		estructura = CreateCollection<string?>();
		int range = cadenasAleatoriasConNulls.Count;
		array1 = new String[range];
		index = range - 1;
		foreach(string? cad in cadenasAleatoriasConNulls)
		{
			estructura.Empuja(cad);
			array1[index--] = cad;
		}

		/**
		 * Borra elementos de la estructura y los guarda en un arreglo.
		 */
		array2 = new string?[range];
		index = 0;
		while (!estructura.EstáVacía())
		{
			array2[index++] = estructura.Expulsa();
		}

		/**
		 * Compara ambos arreglos, deben tener el mismo orden.
		 */
		Assert.That(array1, Is.EqualTo(array2));

		SumaPuntos(1.0);
	}

	// Collection

	[Test]
	public void EmpujaRemoveTest()
	{

		IniciaPrueba("Remove", "Revisa que la estructura remueva el elemento en el tope.", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		IPila<string?> estructura = CreaPilaLlena();

		/**
		 * Se asegura que los elementos insertados estén contenidos dentro de la
		 * estructura.
		 */
		while (!estructura.EstáVacía())
		{
			Assert.That(estructura.Remove(estructura.Mira()), Is.True);
		}

		SumaPuntos(1.0);
	}

	[Test]
	public void EmpujaRemoveTrueFalseTest()
	{
		IPila<string> estructura;

		IniciaPrueba("Remove", "Revisa que la estructura remueva el elemento en el tope o devuelva false.", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		estructura = CreateCollection<string>();
		String[] a = {"1", "2", "3", "4", "5", "6"};
		String[] b = {"6", "7", "5", "6", "7", "4"};
		bool[] r = {true, false, true, false, false, true};

		foreach(string cad in a)
		{
			estructura.Empuja(cad);
		}

		for (int i = 0; i < b.Length; i++)
		{
			Assert.That(estructura.Remove(b[i]), Is.EqualTo(r[i]));
		}

		SumaPuntos(1.0);
	}
}