using System.Diagnostics;

namespace ED.PruebasUnitarias;

/// <summary>
/// Contenedor para el puntaje por rubro que se va acumulando
/// en las pruebas unitarias.
/// </summary>
public class Rubro
{
    
    private double _puntos;
    /// <summary>
    /// Puntos acumulados en el rubro.
    /// </summary>
    public double Puntos { get { return _puntos; } }

    private double _maxPuntos;
    /// <summary>
    /// Número máximo de puntos obtenibles en el rubro.
    /// </summary>
    public double MaxPuntos { get { return _maxPuntos; } }

    /// <summary>
    /// Nombre del rubro.
    /// </summary>
    public string Nombre { get; }
    /// <summary>
    /// Puntaje total que se obtendrá si se pasan todas las
    /// pruebas en el rubro.
    /// </summary>
    public double Peso { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="nombre">Nombre el rubro</param>
    /// <param name="peso">Peso en la calificación final</param>
    public Rubro(string nombre, double peso)
    {
        Nombre = nombre;
        Peso = peso;
    }

    /// <summary>
    /// Añade puntos ganados durante una prueba, no deben superar
    /// el valor máximo asignado a esa prueba.
    /// </summary>
    /// <param name="puntos"></param>
    public void SumaPuntos(double puntos)
    {
        _puntos += puntos;
    }

    /// <summary>
    /// Añade el máximo valor de una prueba al máximo valor de
    /// puntos disponibles en este rubro.
    /// </summary>
    /// <param name="puntos"></param>
    public void SumaMaxPuntos(double puntos)
    {
        _maxPuntos += puntos;
    }

    /// <summary>
    /// Calcula la calificiación total obtenida según el peso
    /// de este rubro.
    /// </summary>
    /// <returns>Calificación dado el peso del rubro.</returns>
    public double CalculaCalificación()
    {
        return Puntos / MaxPuntos * Peso;
    }
}

/// <summary>
/// Clase base para el conjunto de pruebas unitarias con que se
/// calificará una práctica.  Permite definir los rubros a evaluar,
/// de modo que varias pruebas unitarias puedan contribuir a calificar
/// un sólo rubro.  A cada rubro se puede asignar el peso con que
/// contribuirá a la calificación final.
/// </summary>
[TestFixture]
public abstract class Calificador
{
    /// <summary>
    /// Rubros que darán puntaje en la práctica.
    /// </summary>
    private static Dictionary<string, Rubro> _rubros = new Dictionary<string, Rubro>();

    protected static List<string> cadenasAleatorias;

    /// <summary>
    /// Estructura con datos generados aleatoriamente para hacer pruebas.
    /// </summary>
    protected static List<string?> cadenasAleatoriasConNulls;

    public static void TestFixtureSetup()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(new StreamWriter(Console.OpenStandardOutput())));
        Trace.AutoFlush = true;

        cadenasAleatoriasConNulls = new List<string?>();
        foreach(string? cad in Generadores.GeneraCadenasYNull())
        {
            cadenasAleatoriasConNulls.Add(cad);
        }

        cadenasAleatorias = new List<string>();
        foreach(string cad in Generadores.GeneraCadenas())
        {
            cadenasAleatorias.Add(cad);
        }
    }

    /// <summary>
    /// Define los rubros que evaluará la clase con las pruebas
    /// y el peso de cada uno.
    /// Se debe ejecutar una sóla vez al inicio de las pruebas en la clase.
    /// </summary>
    /// <param name="rubros">Arreglo con las definiciones de los rubros.</param>
    public static void AsignaRubros(Rubro[] rubros)
    {
        foreach (var rubro in rubros)
        {
            _rubros.Add(rubro.Nombre, rubro);
        }         
    }

    private Rubro? _rubroActivo;
    private double _puntos;
    private double _valorTotal;

    /// <summary>
    /// Configura el peso de la prueba para uno de los rubros
    /// preconfigurados e imprime información útil para el usuario.
    /// </summary>
    /// <param name="nombreRubro">Nombre del rubro al que contribuye
    /// la prueba.</param>
    /// <param name="descripciónPrueba">Descripción de lo que evalúa
    /// la prueba que manda llamar.</param>
    /// <param name="valorTotal">Puntaje máximo que se puede obtener
    /// en esta prueba.</param>
    public void IniciaPrueba(string nombreRubro,
                             string descripciónPrueba,
                             double valorTotal)
    {
        Console.WriteLine($"[{nombreRubro}] Prueba {descripciónPrueba}");
        _rubroActivo = _rubros[nombreRubro];
        _rubroActivo.SumaMaxPuntos(valorTotal);
        _puntos = 0.0;
        _valorTotal = valorTotal;
    }

    /// <summary>
    /// Sumaje un puntaje parcial durante la ejecución de un método
    /// prueba.
    /// </summary>
    /// <param name="puntos">Puntos obtenidos.</param>
    public void SumaPuntos(double puntos)
    {
        _puntos += puntos;
    }

    /// <summary>
    /// Se ejecuta al final de cada prueba.
    /// Agrega los puntos obtenidos a la calificación y
    /// notifica al estudiante.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _rubroActivo.SumaPuntos(_puntos);
        Console.WriteLine($"Puntos {_puntos}/{_valorTotal}");
    }

    /// <summary>
    /// Da la calificación final.
    /// Se ejecuta una vez cuando han concluido todas las pruebas.
    /// No está garantizado que se ejecute inmediamtente.
    /// </summary>
    [OneTimeTearDown]
    public static void TestFixtureTearDown()
    {
        var calificación = 0.0;
        var max = 0.0;
        foreach(var r in _rubros)
        {
            var rubro = r.Value;
            double cal = rubro.CalculaCalificación();
            Trace.WriteLine($"Calificación para {rubro.Nombre}: {rubro.Puntos:0.##}/{rubro.MaxPuntos:0.##} -> {cal:0.##}/{rubro.Peso:0.##}");
            calificación += cal;
            max += rubro.Peso;
        }
        Trace.WriteLine("----------------------------------------");
        Trace.WriteLine($"Calificación {calificación:0.##}/{max:0.##}");
        Trace.WriteLine("----------------------------------------");
    }

    /// <summary>
    /// Crea un objeto del tipo de colección que se esté probando.
    /// </summary>
    /// <typeparam name="T">Tipo de datos que contiene la colección.</typeparam>
    /// <returns>Un objeto del subtipo a probar.</returns>
    protected abstract ICollection<T> CreateCollection<T>();
}