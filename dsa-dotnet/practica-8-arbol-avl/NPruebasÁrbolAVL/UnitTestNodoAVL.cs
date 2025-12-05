using ED.PruebasUnitarias;
using ED.Estructuras.NoLineales.ÁrbolBinario;

namespace NPruebasÁrbolAVL;

[TestFixture]
public class PruebaNodoAVL : Calificador
{
    [OneTimeSetUp]
    public static new void TestFixtureSetup()
    {
        AsignaRubros(new Rubro[] {
            new Rubro("NodoAVL Constructor", 0.2),
            new Rubro("NodoAVL LL", 0.2),
        });
    }

    public NodoAVL<C> CreaNodo<C>(C dato) where C : IComparable<C>
    {
        return new NodoAVL<C>(dato);
    }

    [Test]
    public void PruebaCreaNodo()
    {
        IniciaPrueba("NodoAVL Constructor", "Prueba constructor de nodo", 1.0);
        NodoAVL<int> n = new NodoAVL<int>(5);
        Assert.That(n.Dato, Is.EqualTo(5));
        SumaPuntos(1.0);
    }



    [Test]
    public void PruebaLL()
    {
        IniciaPrueba("NodoAVL LL", "Rota nodo CBA", 1.0);
        NodoAVL<char> a = CreaNodo<char>('A');
        NodoAVL<char> b = CreaNodo<char>('B');
        NodoAVL<char> c = CreaNodo<char>('C');
        c.HijoI(b); b.Padre(c);
        b.HijoI(a); a.Padre(b);
        b.ActualizaAltura();
        c.ActualizaAltura();
        Assert.That(a.Altura, Is.EqualTo(0));
        Assert.That(b.Altura, Is.EqualTo(1));
        Assert.That(c.Altura, Is.EqualTo(2));

        var n = c.RotaDerecha();
        Assert.That(n == b, Is.True);
        Assert.That(b.HijoI() == a, Is.True);
        Assert.That(b.HijoD() == c, Is.True);
        Assert.That(b.Padre(), Is.Null);
        Assert.That(b.Altura, Is.EqualTo(1));

        Assert.That(a.HijoI(), Is.Null);
        Assert.That(a.HijoD(), Is.Null);
        Assert.That(a.Padre() == b, Is.True);
        Assert.That(a.Altura, Is.EqualTo(0));

        Assert.That(c.HijoI(), Is.Null);
        Assert.That(c.HijoD(), Is.Null);
        Assert.That(c.Padre() == b, Is.True);
        Assert.That(c.Altura, Is.EqualTo(0));

        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaLLEHijos()
    {
        IniciaPrueba("NodoAVL LL", "Rota nodo CBA derecho e hijos", 1.0);
        NodoAVL<string> r = CreaNodo<string>("r");
        NodoAVL<string> a = CreaNodo<string>("A");
        NodoAVL<string> b = CreaNodo<string>("B");
        NodoAVL<string> c = CreaNodo<string>("C");
        NodoAVL<string> hia = CreaNodo<string>("hiA");
        NodoAVL<string> hda = CreaNodo<string>("hdA");
        NodoAVL<string> hdb = CreaNodo<string>("hdB");
        NodoAVL<string> hdc = CreaNodo<string>("hdC");

        r.HijoD(c);
        c.Padre(r);
        a.HijoI(hia);
        hia.Padre(a);
        a.HijoD(hda);
        hda.Padre(a);
        b.HijoD(hdb);
        hdb.Padre(b);
        c.HijoD(hdc);
        hdc.Padre(c);

        c.HijoI(b); b.Padre(c);
        b.HijoI(a); a.Padre(b);
        a.ActualizaAltura();
        b.ActualizaAltura();
        c.ActualizaAltura();
        Assert.That(a.Altura, Is.EqualTo(1));
        Assert.That(b.Altura, Is.EqualTo(2));
        Assert.That(c.Altura, Is.EqualTo(3));

        var n = c.RotaDerecha();

        Assert.That(n == b, Is.True);
        Assert.That(b.HijoI() == a, Is.True);
        Assert.That(b.HijoD() == c, Is.True);
        Assert.That(b.Padre() == r, Is.True);
        Assert.That(b.Altura, Is.EqualTo(2));

        Assert.That(a.HijoI() == hia, Is.True);
        Assert.That(a.HijoD() == hda, Is.True);
        Assert.That(a.Padre() == b, Is.True);
        Assert.That(a.Altura, Is.EqualTo(1));

        Assert.That(c.HijoI() == hdb, Is.True);
        Assert.That(c.HijoD() == hdc, Is.True);
        Assert.That(c.Padre() == b, Is.True);
        Assert.That(c.Altura, Is.EqualTo(1));

        SumaPuntos(1.0);
    }
}