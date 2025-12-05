using ED.PruebasUnitarias;
using ED.Estructuras.NoLineales.Gráficas;

namespace NPruebasGráficaListas;

/// <summary>
/// Pruebas unitarias para una gráfica con nodos.
/// </summary>
public class PruebaGráficaLigada : Calificador
{
    [OneTimeSetUp]
    public virtual void SortTestFixtureSetup()
    {
        Calificador.TestFixtureSetup();
		AsignaRubros(new Rubro[] {
            new Rubro("Inserción", 2.0),
            new Rubro("Borrado", 2.0),
            new Rubro("Amplitud", 2.0),
			new Rubro("Dijkstra", 4.0),
		});
		
    }

	public static GráficaLigada<char> CreaGráfica()
    {
		GráficaLigada<char> g = new();
		g.AgregaVértice('A');
		g.AgregaVértice('B');
		g.AgregaVértice('C');
		g.AgregaVértice('D');
		g.AgregaVértice('E');
		g.AgregaVértice('F');
		g.AgregaArista('A', 'B', 3);
		g.AgregaArista('A', 'C', 5);
		g.AgregaArista('A', 'D', 9);
		g.AgregaArista('B', 'C', 3);
		g.AgregaArista('B', 'D', 4);
		g.AgregaArista('B', 'E', 7);
		g.AgregaArista('C', 'A', 3);
		g.AgregaArista('C', 'D', 1);
		g.AgregaArista('C', 'E', 6);
		g.AgregaArista('C', 'F', 8);
		g.AgregaArista('D', 'F', 2);
		g.AgregaArista('E', 'D', 2);
		g.AgregaArista('E', 'F', 5);
		g.AgregaArista('F', 'D', 4);
		return g;
	}


	[Test]
	public void Amplitud()
    {
		IniciaPrueba("Amplitud", "Recorrido", 3.0);
		GráficaLigada<char> g = CreaGráfica();
		// Crear iterador
		IEnumerator<char> it = g.EnumeraciónAmplitud('A');
		char[] resultadoEsperado = {'A', 'B', 'C', 'D', 'E', 'F'};
		foreach (char c in resultadoEsperado)
        {
			//Console.WriteLine(" Visitando " + c + " se obtuvo " + it.next());
            Assert.That(it.MoveNext(), Is.True);
			Assert.That(c, Is.EqualTo(it.Current));
		}
		SumaPuntos(3.0);
	}

	[Test]
	public void GráficaLigadaDijkstra()
    {
		IniciaPrueba("Dijkstra", "Dijkstra", 3.0);
		GráficaLigada<char> g = CreaGráfica();
		List<char> ruta = g.RutaDijkstra('A', 'F')!;
		char[] solución = {'A', 'C', 'D', 'F'};
		int i = 0;
		Console.WriteLine("Ruta = " + ruta);
		Assert.That(ruta.Count, Is.EqualTo(solución.Length));
		foreach (char c in ruta)
        {
			Console.WriteLine("En ruta " + i + ": " + c);
			Assert.That(c == solución[i], Is.True);
			i++;
		}
		SumaPuntos(3.0);
	}

}