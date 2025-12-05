using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public class SelectionSorterTest : SupertestOrdenador
{
    [OneTimeSetUp]
    public override void SortTestFixtureSetup()
    {
        CurrentSorter = "Selection";
        base.SortTestFixtureSetup();
    }

    protected override IOrdenador<C> CreaOrdenador<C>()
    {
        return new SelectionSorter<C>();
    }

    [Test]
    public void PruebaMejor()
    {
        IniciaPrueba($"{CurrentSorter} Mejor", "Comprueba mejor caso.", 1.0);
		int[] mejor = CreaOrdenador<int>().MejorCaso(10);
        // Todos dan igual
		Assert.That(mejor.Length, Is.EqualTo(10));
		SumaPuntos(1.0);
    }

    [Test]
	public void PruebaPeor()
    {
		IniciaPrueba($"{CurrentSorter} Peor", "Comprueba peor caso.", 1.0);
		int[] peor = CreaOrdenador<int>().PeorCaso(10);
        // Todos dan igual
		Assert.That(peor.Length, Is.EqualTo(10));
		SumaPuntos(1.0);
	}
}