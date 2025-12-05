using ED.Ordenamientos;

namespace NPruebasOrdenamientos;

public class QuickSorterTest : SupertestOrdenador
{
    [OneTimeSetUp]
    public override void SortTestFixtureSetup()
    {
        CurrentSorter = "QuickSort";
        base.SortTestFixtureSetup();
    }

    protected override IOrdenador<C> CreaOrdenador<C>()
    {
        return new QuickSorter<C>();
    }

    /*[Test]
    public void PruebaMejor()
    {
        // Hay varias estrategias dependiendo de la implementación.
		// Es mejor revisar a mano
    }*/

    [Test]
	public void PruebaPeor()
    {
		IniciaPrueba($"{CurrentSorter} Peor", "Comprueba peor caso.", 1.0);
		int[] peor = CreaOrdenador<int>().PeorCaso(10);
		Assert.That(
            ComprobarSiEstáOrdenado(peor) ||
            ComprobarOrdenInverso(peor),
            Is.True
        );
		SumaPuntos(1.0);
	}
}