using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public class BubbleSorterTest : SupertestOrdenador
{
    [OneTimeSetUp]
    public override void SortTestFixtureSetup()
    {
        CurrentSorter = "Bubble";
        base.SortTestFixtureSetup();
        /*Calificador.TestFixtureSetup();
        AgregaRubro(new Rubro("Bubble sort", 1.5));*/
    }

    protected override IOrdenador<C> CreaOrdenador<C>()
    {
        return new BubbleSorter<C>();
    }

    [Test]
    public void PruebaMejor()
    {
        IniciaPrueba($"{CurrentSorter} Mejor", "Comprueba mejor caso.", 1.0);
		int[] mejor = CreaOrdenador<int>().MejorCaso(10);
		Assert.That(ComprobarSiEstáOrdenado(mejor), Is.True);
		SumaPuntos(1.0);
    }

    [Test]
	public void PruebaPeor()
    {
		IniciaPrueba($"{CurrentSorter} Peor", "Comprueba peor caso.", 1.0);
		int[] peor = CreaOrdenador<int>().PeorCaso(10);
        Assert.That(ComprobarOrdenInverso(peor), Is.True);
		SumaPuntos(1.0);
	}
}