using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public class MergeSorterTest : SupertestOrdenador
{
    [OneTimeSetUp]
    public override void SortTestFixtureSetup()
    {
        CurrentSorter = "Merge";
        base.SortTestFixtureSetup();
    }

    protected override IOrdenador<C> CreaOrdenador<C>()
    {
        return new MergeSorter<C>();
    }

    [Test]
    public void PruebaMejor()
    {
        IniciaPrueba($"{CurrentSorter} Mejor", "Comprueba mejor caso.", 1.0);
		int[] mejor = CreaOrdenador<int>().MejorCaso(10);
		Assert.That(mejor.Length, Is.EqualTo(10));
		SumaPuntos(1.0);
    }

    [Test]
	public void PruebaPeor()
    {
		IniciaPrueba($"{CurrentSorter} Peor", "Comprueba peor caso.", 1.0);
		int[] peor = CreaOrdenador<int>().PeorCaso(10);
		Assert.That(peor.Length, Is.EqualTo(10));
		SumaPuntos(1.0);
	}
}