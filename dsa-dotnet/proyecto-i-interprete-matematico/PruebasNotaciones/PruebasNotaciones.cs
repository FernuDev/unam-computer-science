using ED.Notaciones;

namespace PruebasNotaciones;

public class UnitTestNotaciones
{
    [Test]
	public void EvalúaPrefijaSimpleErrorTest() {
		String[] símbolos = {"+", "43", "9.8", "3"};
		Console.WriteLine(string.Join(" ", símbolos) + " -> Error");
        FormatException e = Assert.Throws<FormatException>(
            () => Notaciones.EvalúaPrefija(símbolos)
        );
	}
	
	[Test]
	public void EvalúaPrefijaSimpleTest() {
		String[] símbolos = {"+", "43", "9.8"};
		double respuestaEsperada = 52.8;
		double respuestaObtenida = Notaciones.EvalúaPrefija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " = " + respuestaObtenida);
		Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
	}
	
	[Test]
	public void EvalúaPostfijaSimpleErrorTest() {
		String[] símbolos = {"+", "43", "9.8"};
		Console.WriteLine(string.Join(" ", símbolos) + " -> Error");
        FormatException e = Assert.Throws<FormatException>(
            () => Notaciones.EvalúaPostfija(símbolos)
        );
	}
	
	[Test]
	public void EvalúaPostfijaSimpleTest() {
		String[] símbolos = {"43", "9.8", "+"};
		double respuestaEsperada = 52.8;
		double respuestaObtenida = Notaciones.EvalúaPostfija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " = " + respuestaObtenida);
		Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
	}

    [Test]
	public void InfijaASufijaBasicTest() {
		Console.WriteLine("\n>> infijaASufijaBasicTest <<");
		String[] símbolos = {"5", "*", "-2"};
		String[] respuestaEsperada = {"5", "-2", "*"};
		String[] respuestaObtenida = Notaciones.InfijaASufija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " -> "
			+ string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}

	[Test]
	public void EvalúaInfijaBasicTest() {
		Console.WriteLine("\n>> EvalúaInfijaBasicTest <<");
		String[] símbolos = {"5", "*", "-2"};
		double respuestaEsperada = -10;
		double respuestaObtenida = Notaciones.EvalúaInfija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " = " + respuestaObtenida);
		Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
	}

	[Test]
	public void InfijaASufijaSimpleTest() {
		Console.WriteLine("\n>> infijaASufijaSimpleTest <<");
		String[] símbolos = {"5", "*", "(", "3", "+", "-2", ")"};
		String[] respuestaEsperada = {"5", "3", "-2", "+", "*"};
		String[] respuestaObtenida = Notaciones.InfijaASufija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " -> "
			+ string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}

	[Test]
	public void EvalúaInfijaSimpleTest() {
		Console.WriteLine("\n>> EvalúaInfijaSimpleTest <<");
		String[] símbolos = {"5", "*", "(", "3", "+", "-2", ")"};
		double respuestaEsperada = 5.0;
		double respuestaObtenida = Notaciones.EvalúaInfija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " = " + respuestaObtenida);
		Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
	}

	[Test]
	public void InfijaASufijaCompuestaTest() {
		Console.WriteLine("\n>> infijaASufijaCompuestaTest <<");
		String[] símbolos = {"5", "*", "(", "(", "-3", "+", "-2", ")", "*", "-1", ")"};
		String[] respuestaEsperada = {"5", "-3", "-2", "+", "-1", "*", "*"};
		String[] respuestaObtenida = Notaciones.InfijaASufija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " -> "
			+ string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}

	[Test]
	public void EvalúaInfijaCompuestaTest() {
		Console.WriteLine("\n>> EvalúaInfijaCompuestaTest <<");
		String[] símbolos = {"5", "*", "(", "(", "-3", "+", "-2", ")", "*", "-1", ")"};
		double respuestaEsperada = 25;
		double respuestaObtenida = Notaciones.EvalúaInfija(símbolos);
		Console.WriteLine(string.Join(" ", símbolos) + " = " + respuestaObtenida);
		Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
	}
}