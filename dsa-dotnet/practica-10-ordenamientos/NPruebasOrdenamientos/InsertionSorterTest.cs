using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public class InsertionSorterTest : SupertestOrdenador
{
    [OneTimeSetUp]
    public override void SortTestFixtureSetup()
    {
        CurrentSorter = "Insertion";
        base.SortTestFixtureSetup();
    }

    protected override IOrdenador<C> CreaOrdenador<C>()
    {
        return new InsertionSorter<C>();
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