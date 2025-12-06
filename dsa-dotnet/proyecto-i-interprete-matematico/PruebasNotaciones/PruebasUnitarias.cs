using ED.Notaciones;

namespace PruebasNotaciones;

public class UnitTest {

    [Test]
    public void EvaluarPrefijaTest1()
    {
        string[] simbolos = {"%", "/", "100", "5", "3"};
        double respuestaEsperada = 2;
        double respuestaObtenida = Notaciones.EvalúaPrefija(simbolos);
        Console.WriteLine(string.Join(" ", simbolos) + " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

    [Test]
    public void EvaluarPrefijaTest2()
    {
        string[] simbolos = {"+", "3", "*", "2", "-", "8", "5"};
        double respuestaEsperada = 9;
        double respuestaObtenida = Notaciones.EvalúaPrefija(simbolos);
        Console.WriteLine(string.Join(" ", simbolos) + " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

    // Prueba para EvalúaPostfija
    [Test]
    public void EvaluarPostfijaTest1(){
        string[] simbolos = {"5", "6", "*", "12", "20", "4", "/", "-", "+"};
        double respuestaEsperada = 37.0;
        double respuestaObtenida = Notaciones.EvalúaPostfija(simbolos);
        Console.WriteLine(string.Join(" ",simbolos)+ " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

    [Test]
    public void EvaluarPostfijaTest2(){
        string[] simbolos = {"15", "7", "3", "+", "/", "2", "5", "*", "-"};
        double respuestaEsperada = -8.5;
        double respuestaObtenida = Notaciones.EvalúaPostfija(simbolos);
        Console.WriteLine(string.Join(" ",simbolos)+ " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

    // Prueba para InfijaASufija
    [Test]
    public void InfijaASufijaTest1(){
        string[] simbolosInfijos = {"(", "5", "*", "6", ")", "+", "(", "12", "-", "(", "20", "/", "4", ")", ")"};
        string[] respuestaEsperada = {"5", "6", "*", "12", "20", "4", "/", "-", "+"};
        string[] respuestaObtenida = Notaciones.InfijaASufija(simbolosInfijos);
        Console.WriteLine(string.Join(" ",simbolosInfijos) + " => " + string.Join(" ", respuestaObtenida));
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada));
    }

    [Test]
    public void InfijaASufijaTest2(){
        string[] simbolosInfijos = {"15", "/", "(", "7", "+", "3", ")", "-", "(", "2", "*", "5", ")"};
        string[] respuestaEsperada = {"15", "7", "3", "+", "/", "2", "5", "*", "-"};
        string[] respuestaObtenida = Notaciones.InfijaASufija(simbolosInfijos);
        Console.WriteLine(string.Join(" ",simbolosInfijos) + " => " + string.Join(" ", respuestaObtenida));
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada));
    }

    // Prueba para EvalúaInfija
    [Test]
    public void EvaluarInfijaTest1(){
        string[] simbolos = {"(", "5", "*", "6", ")", "+", "(", "12", "-", "(", "20", "/", "4", ")", ")"};
        double respuestaEsperada = 37.0;
        double respuestaObtenida = Notaciones.EvalúaInfija(simbolos);
        Console.WriteLine(string.Join(" ",simbolos)+ " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

    [Test]
    public void EvaluarInfijaTest2(){
        string[] simbolos = {"15", "/", "(", "7", "+", "3", ")", "-", "(", "2", "*", "5", ")"};
        double respuestaEsperada = -8.5;
        double respuestaObtenida = Notaciones.EvalúaInfija(simbolos);
        Console.WriteLine(string.Join(" ",simbolos)+ " = " + respuestaObtenida);
        Assert.That(respuestaObtenida, Is.EqualTo(respuestaEsperada).Within(0.0));
    }

}
