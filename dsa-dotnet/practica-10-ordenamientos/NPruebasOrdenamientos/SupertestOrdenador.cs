using ED.PruebasUnitarias;
using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public abstract class SupertestOrdenador : Calificador
{
    [OneTimeSetUp]
    public virtual void SortTestFixtureSetup()
    {
        Calificador.TestFixtureSetup();
		AsignaRubros(new Rubro[] {
			new Rubro($"{CurrentSorter} Ordena", 0.8),
			new Rubro($"{CurrentSorter} Mejor", 0.1),
			new Rubro($"{CurrentSorter} Peor", 0.1),
		});
		
    }

    protected int[][] arreglosInt = new int[][] {
		new int[] {3, 5, 7, 9, 1, 2, 4, 6},
		new int[] {20, 45, 37, 19, 1, 25},
		new int[] {1, 2, 3, 4, 5},
		new int[] {5, 4, 3, 2, 1},
		new int[] {4, 3, 5, 2, 3, 4}
	};

	protected string[][] arreglosStr = new string[][] {
		new string[] {"d", "e", "c", "a", "b"},
		new string[] {"juan", "pedro", "hugo", "paco", "luis"}
	};

	protected static string CurrentSorter = null!;

    protected abstract IOrdenador<C> CreaOrdenador<C>() where C : IComparable<C>;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaOrdenarInt()
    {
		IniciaPrueba($"{CurrentSorter} Ordena", "Comprueba si ordenó el arreglo de ints.", 1.0);
		for (int i = 0; i < arreglosInt.Length; i++)
        {
			Console.WriteLine("Prueba ordenar arreglo " + String.Join(",", arreglosInt[i]));

			IOrdenador<int> ordenador = CreaOrdenador<int>();
			int[] copia = (int[])arreglosInt[i].Clone();
			int[] ordenado = ordenador.Ordena(copia);

			if (ordenado.Length != copia.Length)
            {
				Assert.Fail("Los arreglos no tienen la misma capacidad.");
			}
			Assert.That(ComprobarSiEstáOrdenado(ordenado), Is.True);

			SumaPuntos(1.0 / arreglosInt.Length);
		}
	}

	[Test]
	public void PruebaOrdenarString()
	{
		IniciaPrueba($"{CurrentSorter} Ordena", "Comprueba si ordenó el arreglo de string.", 1.0);
		for (int i = 0; i < arreglosStr.Length; i++)
		{
			Console.WriteLine("Prueba ordenar arreglo " + String.Join(",", arreglosStr[i]));

			IOrdenador<string> ordenador = CreaOrdenador<string>();
			string[] copia = (string[])arreglosStr[i].Clone();
			string[] ordenado = ordenador.Ordena(copia);

			if (ordenado.Length != copia.Length)
			{
				Assert.Fail("Los arreglos no tienen la misma capacidad.");
			}
			Assert.That(ComprobarSiEstáOrdenado(ordenado), Is.True);

			SumaPuntos(1.0 / arreglosStr.Length);
		}
	}

    /// <summary>
    /// Comprueba que el arreglo esté ordenado.
    /// </summary>
    /// <typeparam name="C"></typeparam>
    /// <param name="array">El arreglo que se desea comprobar si esta ordenado</param>
    protected bool ComprobarSiEstáOrdenado<C> (C[] array) where C : IComparable
    {
		Console.WriteLine("\tresultado >> " + String.Join(",", array));
		int length = array.Length - 1;
        bool menorAMayor;
		for (int i = 0; i < length; i++)
        {
            menorAMayor = array[i].CompareTo(array[i + 1]) <= 0;
			if (!menorAMayor)
            {
				Console.WriteLine("\t Fallo, se obtuvo: " + String.Join(",", array));
				return false;
			}
            //Console.WriteLine($"Comparando {array[i]} con {array[i + 1]}");			
		}
		return true;
	}

    /// <summary>
    /// Comprueba que el arreglo esté en orden inverso.
    /// </summary>
    /// <param name="array">El arreglo que se desea comprobar si está ordenado.</param>
    protected bool ComprobarOrdenInverso<C> (C[] array) where C : IComparable
    {
		Console.WriteLine("\tresultado >> " + String.Join(",", array));
		int length = array.Length - 1;
		for (int i = 0; i < length; i++)
        {
			if ((array[i].CompareTo(array[i + 1])) < 0)
            {
				Console.WriteLine("\t Fallo, se obtuvo: " + String.Join(",", array));
				return false;
			}
			//Assert.That(array[i].CompareTo(array[i + 1]) >= 0, Is.True);
		}
		return true;
	}
}