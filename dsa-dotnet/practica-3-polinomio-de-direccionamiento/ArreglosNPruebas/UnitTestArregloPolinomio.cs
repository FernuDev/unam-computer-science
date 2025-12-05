using PruebasUnitarias;
using System.Diagnostics;
using ED.Estructuras.Lineales.Arreglos;

namespace ArreglosNPruebas;

/// <summary>
/// Pruebas unitarias para la implementación con polinomio de direccionamiento de
/// la interfaz IArreglo.
/// </summary>
public class Tests : Calificador
{
    private ArregloPolinomio<int> CreaC()
    {
        return new ArregloPolinomio<int>(new int[]{3, 4});
    }

    private ArregloPolinomio<int> CreaA()
    {
        return new ArregloPolinomio<int>(new int[]{4, 5});
    }

    private ArregloPolinomio<int> CreaB()
    {
        return new ArregloPolinomio<int>(new int[]{3, 5, 2});
    }

    [OneTimeSetUp]
    public static void TestFixtureSetup()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(new StreamWriter(Console.OpenStandardOutput())));
        Trace.AutoFlush = true;
        AsignaRubros(new Rubro[] {
            new Rubro("Constructor", 1.5),
            new Rubro("ObtenerÍndice", 5.0),
            new Rubro("ObtenerElemento", 1.0),
            new Rubro("AlmacenarElemento", 1.0),
        });
    }

    // Se ejecuta antes de cada prueba. 
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaConstructor()
    {
        IniciaPrueba("Constructor", "4-dimensiones con una inválida", 1.0);
        IllegalSizeException e =
            Assert.Throws<IllegalSizeException>(
                () => new ArregloPolinomio<int>(new int[]{5, 3, -1, 2})
            );
        SumaPuntos(1.0);
    }

    [Test]
	public void PruebaObtenerÍndice()
    {
		IniciaPrueba("ObtenerÍndice", "Obtén 9", 3.0);
        ArregloPolinomio<int> c = CreaC();
		int expectedValue = 9;
		int actualValue = c.ObtenerÍndice(new int[] {2, 1});
        Assert.That(actualValue, Is.EqualTo(expectedValue));
		SumaPuntos(3.0);
	}

    [Test]
	public void PruebaObtenerÍndiceCero()
    {
		IniciaPrueba("ObtenerÍndice", "Obtén 0", 1.0);
        ArregloPolinomio<int> b = CreaB();
		int expectedValue = 0;
		int actualValue = b.ObtenerÍndice(new int[] {0, 0, 0});
        Assert.That(actualValue, Is.EqualTo(expectedValue));
		SumaPuntos(1.0);
	}
	
	[Test]
	public void PruebaObtenerÍndiceExtraDim()
    {
		IniciaPrueba("ObtenerÍndice", "Con índices de más", 1.0);
        ArregloPolinomio<int> c = CreaC();
        Assert.Throws<IllegalSizeException>(
            () => c.ObtenerÍndice(new int[] {2, 1, 3, 5})
        );
		SumaPuntos(1.0);
	}
	
	[Test]
	public void PruebaObtenerÍndiceInválido() {
		IniciaPrueba("ObtenerÍndice", "Con índices inválidos", 1.0);
        ArregloPolinomio<int> c = CreaC();
        Assert.Throws<IndexOutOfRangeException>(
            () => c.ObtenerÍndice(new int[] {3, 1})
        );
        SumaPuntos(1.0);
	}

	[Test]
	public void PruebaBidimensional() {
		IniciaPrueba("ObtenerÍndice", "2-dimensiones", 2.0);
        ArregloPolinomio<int> a = CreaA();
		int count = 1;
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 5; j++) {
				a.AlmacenarElemento(new int[]{i, j}, count);
				count++;
			}
		}
        SumaPuntos(1.0);
		int[] indices = {3, 2};
        int expectedValue = 18;
		int actualValue = a.ObtenerElemento(indices);
        Assert.That(actualValue, Is.EqualTo(expectedValue));
		SumaPuntos(1.0);
	}

	[Test]
	public void PruebaTridimensional() {
		IniciaPrueba("ObtenerÍndice", "3-dimensiones", 3.0);
        ArregloPolinomio<int> b = CreaB();
		int count = 1;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 5; j++) {
				for (int k = 0; k < 2; k++) {
					b.AlmacenarElemento(new int[]{i, j, k}, count);
					count++;
				}
			}
		}
        SumaPuntos(2.0);
		int[] indices = {2, 3, 0};
        int expectedValue = 27;
		int actualValue = b.ObtenerElemento(indices);
		Assert.That(actualValue, Is.EqualTo(expectedValue));
		SumaPuntos(1.0);
	}

	[Test]
	public void PruebaLanzaObtener() {
		IniciaPrueba("AlmacenarElemento", "Obtener índice en AlmacenarElemento - excepción", 1.0);
        ArregloPolinomio<int> b = CreaB();
        Assert.Throws<IndexOutOfRangeException>(
            () => b.AlmacenarElemento(new int[]{0, 10, 2}, 100)
        );
        SumaPuntos(1.0);
	}

	[Test]
	public void testThrow2() {
		IniciaPrueba("ObtenerElemento", "Obtener índice en ObtenerElemento - excepción", 1.0);
        ArregloPolinomio<int> b = CreaB();
        Assert.Throws<IndexOutOfRangeException>(
            () => b.ObtenerElemento(new int[]{0, 10, 2})
        );
        SumaPuntos(1.0);
	}
}
