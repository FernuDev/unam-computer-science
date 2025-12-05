//using NUnit.Framework;
using ED.Notaciones;

namespace PruebasNotaciones;

public class UnitTestCalculadora
{
    private Calculadora calculadora = new Calculadora();

    [Test]
	public void PrefijaSencilla() {
		String cadena = "+ 5 -1";
		String[] respuestaEsperada = {"+", "5", "-1"};
		String[] respuestaObtenida = calculadora.SeparaSímbolos(cadena);
		Console.WriteLine(cadena + " -> " + string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}
	
	[Test]
	public void PrefijaSencilla2() {
		String cadena = "+ 43 -9.8";
		String[] respuestaEsperada = {"+", "43", "-9.8"};
		String[] respuestaObtenida = calculadora.SeparaSímbolos(cadena);
		Console.WriteLine(cadena + " -> " + string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}
	
	[Test]
	public void InfijaParéntesisConEspacios() {
		String cadena = "( 5 + -1 )";
		String[] respuestaEsperada = {"(", "5", "+", "-1", ")"};
		String[] respuestaObtenida = calculadora.SeparaSímbolos(cadena);
		Console.WriteLine(cadena + " -> " + string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}
	
	[Test]
	public void InfijaParéntesis() {
		String cadena = "(5 + -1)";
		String[] respuestaEsperada = {"(", "5", "+", "-1", ")"};
		String[] respuestaObtenida = calculadora.SeparaSímbolos(cadena);
		Console.WriteLine(cadena + " -> " + string.Join(" ", respuestaObtenida));
		CollectionAssert.AreEqual(respuestaEsperada, respuestaObtenida);
	}
}