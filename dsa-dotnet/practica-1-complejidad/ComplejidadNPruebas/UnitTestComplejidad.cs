using Análisis;
using PruebasUnitarias;
using System.Diagnostics;

namespace ComplejidadNPruebas;

/// <summary>
/// Pruebas unitarias para los ejercicios de cálculo de complejidad.
/// </summary>
public class Tests : Calificador
{
    [OneTimeSetUp]
    public static void TestFixtureSetup()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(new StreamWriter(Console.OpenStandardOutput())));
        Trace.AutoFlush = true;
        AsignaRubros(new Rubro[] {
            new Rubro("Fibonnacci Iterativo", 1.0),
            new Rubro("Fibonnacci Recursivo", 1.0),
            new Rubro("Pascal Iterativo", 1.0),
            new Rubro("Pascal Recursivo", 1.0),
        });
    }

    /// <summary>
    /// Ejemplar para realizar las pruebas.
    /// </summary>
    protected IComplejidad? c;

    // Se ejecuta antes de cada prueba.
    [SetUp]
    public void Setup()
    {
        c = new Complejidad();
    }

    [Test]
	public void PruebaFibonacciRec() {
        IniciaPrueba("Fibonnacci Recursivo", "FibonacciRec", 2.0);
        Assert.That(c.FibonacciRec(6), Is.EqualTo(8));
		SumaPuntos(1.0);
        Assert.That(c.FibonacciRec(8), Is.EqualTo(21));
		SumaPuntos(1.0);
	}

    [Test]
	public void PruebaFibonacciIt() {
        IniciaPrueba("Fibonnacci Iterativo", "FibonacciIt", 2.0);
        Assert.That(c.FibonacciIt(8), Is.EqualTo(21));
		SumaPuntos(1.0);
        Assert.That(c.FibonacciIt(12), Is.EqualTo(144));
		SumaPuntos(1.0);
	}

    [Test]
    public void PruebaPascalRecursivo()
    {
        IniciaPrueba("Pascal Recursivo", "TPascalRec", 2.0);
        Assert.That(c.TPascalRec(5, 2), Is.EqualTo(10));
        SumaPuntos(1.0);
        Assert.That(c.TPascalRec(3, 2), Is.EqualTo(3));
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaPascalIterativo()
    {
        IniciaPrueba("Pascal Iterativo", "TPascalIt", 2.0);
        Assert.That(c.TPascalIt(5, 2), Is.EqualTo(10));
        SumaPuntos(1.0);
        Assert.That(c.TPascalIt(3, 2), Is.EqualTo(3));
        SumaPuntos(1.0);
    }

    [Test]
	public void PruebaFibItInválido()
    {
        IniciaPrueba("Pascal Iterativo",
                     "Cálculo Fibonacci valor inválido iterativo", 0.2);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.FibonacciIt(-5)
            );
        SumaPuntos(0.2);
	}

    [Test]
	public void PruebaRecItInválido()
    {
        IniciaPrueba("Pascal Recursivo",
                     "Cálculo Fibonacci valor inválido recursivo", 0.2);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.FibonacciRec(-10)
            );
        SumaPuntos(0.2);
	}

    [Test]
	public void PruebaPascalItInválido1()
    {
        IniciaPrueba("Pascal Iterativo",
                     "Cálculo Pascal valor inválido iterativo 1", 0.1);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalIt(-5, 1)
            );
        SumaPuntos(0.1);
	}

    [Test]
	public void PruebaPascalItInválido2()
    {
        IniciaPrueba("Pascal Iterativo",
                     "Cálculo Pascal valor inválido iterativo 2", 0.1);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalIt(5, -1)
            );
        SumaPuntos(0.1);
	}

    [Test]
	public void PruebaPascalItInválido3()
    {
        IniciaPrueba("Pascal Iterativo",
                     "Cálculo Pascal valor inválido iterativo 3", 0.2);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalIt(3, 5)
            );
        SumaPuntos(0.2);
	}

    [Test]
	public void PruebaPascalRecInválido1()
    {
        IniciaPrueba("Pascal Recursivo",
                     "Cálculo Pascal valor inválido iterativo 1", 0.1);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalRec(-5, 1)
            );
        SumaPuntos(0.1);
	}

    [Test]
	public void PruebaPascalRecInválido2()
    {
        IniciaPrueba("Pascal Recursivo",
                     "Cálculo Pascal valor inválido iterativo 2", 0.1);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalRec(5, -1)
            );
        SumaPuntos(0.1);
	}

    [Test]
	public void PruebaPascalRecInválido3()
    {
        IniciaPrueba("Pascal Recursivo",
                     "Cálculo Pascal valor inválido iterativo 3", 0.2);
        ArgumentOutOfRangeException e =
            Assert.Throws<ArgumentOutOfRangeException>(
                () => c.TPascalRec(3, 5)
            );
        SumaPuntos(0.2);
	}
}