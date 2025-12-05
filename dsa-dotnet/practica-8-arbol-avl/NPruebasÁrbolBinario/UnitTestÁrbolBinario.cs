using ED.Colecciones;
using NPruebasColección;
using ED.PruebasUnitarias;
using ED.Estructuras.NoLineales.ÁrbolBinario;

namespace NPruebasÁrbolBinario;

public class PruebasÁrbolBinarioOrdenado : PruebasColección
{
    [OneTimeSetUp]
    public static new void TestFixtureSetup()
    {
        Calificador.TestFixtureSetup();
        AsignaRubros(new Rubro[] {
            new Rubro("Constructor", 0.2),
            new Rubro("Raíz", 0.2),
            new Rubro("Add", 1.5),
            new Rubro("Clear", 0.4),
            new Rubro("CopyTo", 0.2),
            new Rubro("Búsqueda", 1.0),
            new Rubro("EstáVacía", 0.2),
            new Rubro("Remove", 1.5)
        });
    }

    protected override ICollection<T> CreateCollection<T>()
    {
        /*Console.WriteLine("Create " + typeof(T));
        Console.WriteLine($"String: {typeof(T)} is {typeof(T) == typeof(string)}");
        Console.WriteLine($"int: {typeof(T)} is  {typeof(T) == typeof(int)}");*/
        if(typeof(T) == typeof(string))
        {
            return (ICollection<T>)(new ÁrbolBinarioOrdenado<string>());
        }
        else if(typeof(T) == typeof(int))
        {
            return (ICollection<T>)(new ÁrbolBinarioOrdenado<int>());
        }
        else
        {
            Console.WriteLine($"Tipo {typeof(T)} solicitado.");
            throw new NotImplementedException();
        }
    }

    protected virtual IÁrbolBinarioOrdenado<C> CreateComparableCollection<C>() where C : IComparable<C>
    {
        return new ÁrbolBinarioOrdenado<C>();
    }
        
    protected virtual IÁrbolBinarioOrdenado<int> CreateFilledComparableCollection()
    {
        //             25
        //       15          45
        //    10                 63
        //  0   13             51
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {25, 15, 45, 10, 13, 63, 0, 51};
        foreach(var dato in datos)
        {
            árbol.Add(dato);
        }
        return árbol;
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaConstructor()
    {
        IniciaPrueba("Constructor", "Constructor por defecto", 1.0);
        ÁrbolBinarioOrdenado<int> p = new ÁrbolBinarioOrdenado<int>();
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaRaízVacía()
    {
        IniciaPrueba("Raíz", "Pide la raíz de un árbol vacío", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        Assert.That(árbol.Raíz, Is.Null);
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaRaíz()
    {
        IniciaPrueba("Raíz", "Agrega dos nodos y devuelve la raíz", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int dato1 = 10;
        int dato2 = 5;
        árbol.Add(dato1);
        árbol.Add(dato2);
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(dato1));
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaEstáVacía()
    {
        IniciaPrueba("EstáVacía", "Árbol vacío", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        Assert.That(árbol.EstáVacía(), Is.True);
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaNoEstáVacía()
    {
        IniciaPrueba("EstáVacía", "Agrega dos nodos y no está vacío", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int dato1 = 10;
        int dato2 = 5;
        árbol.Add(dato1);
        árbol.Add(dato2);
        Assert.That(árbol.EstáVacía, Is.False);
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaAdd1()
    {
        IniciaPrueba("Add", "Agrega el primer nodo", 0.5);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int dato = 10;
        árbol.Add(dato);
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(dato));
        SumaPuntos(0.5);
    }

    [Test]
    public virtual void PruebaAddTodo()
    {
        //             25
        //       15          45
        //    10                 63
        //  0   13             51
        IniciaPrueba("Add", "Agrega varios nodos", 1.5);
        IÁrbolBinarioOrdenado<int> árbol = CreateFilledComparableCollection();
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(25));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(15));
        Assert.That(hD!.Dato, Is.EqualTo(45));
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(10));
        hD = hI.HijoD();
        Assert.That(hD!.Dato, Is.EqualTo(13));
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(0));
        Assert.That(árbol.Count, Is.EqualTo(8));

        SumaPuntos(1.5);
    }

    [Test]
    public void PruebaRemove()
    {
        //             25
        //       15          45
        //    10                 63
        //  0   13             51
        IniciaPrueba("Remove", "Agrega varios nodos y remueve", 1.5);
        IÁrbolBinarioOrdenado<int> árbol = CreateFilledComparableCollection();
        Assert.That(árbol.Remove(10), Is.True);
        Assert.That(árbol.Contains(10), Is.False);
        Assert.That(árbol.Remove(45), Is.True);
        Assert.That(árbol.Contains(45), Is.False);
        Assert.That(árbol.Remove(13), Is.True);
        Assert.That(árbol.Contains(13), Is.False);

        SumaPuntos(1.0);

        Assert.That(árbol.Count, Is.EqualTo(5));

        SumaPuntos(0.5);
    }

    [Test]
    public void TestClearÁrbol()
    {
        IniciaPrueba("Clear", "Vaciar la colección y desconectar nodos", 1.0);
        IÁrbolBinarioOrdenado<string> c = (IÁrbolBinarioOrdenado<string>)CreateCollection<string>();
        c.Add("A");
        c.Add("c");
        c.Add("b");
        var nodo = c.Raíz!.HijoD();
        c.Clear();
        Assert.That(c, Is.Empty);
        Assert.That(nodo.Padre(), Is.Null);
        Assert.That(nodo.HijoI(), Is.Null);
        Assert.That(nodo.HijoD(), Is.Null);
        SumaPuntos(0.5);
        // Funciona después de limpiarla.
        c.Add("d");
        Assert.That(c.Count, Is.EqualTo(1));
        SumaPuntos(0.5);
    }

    [Test]
    public void PruebaContains()
    {
        //             25
        //       15          45
        //    10                 63
        //  0   13             51
        IniciaPrueba("Búsqueda", "Agrega varios nodos y busca uno que sí está", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateFilledComparableCollection();
        Assert.That(árbol.Contains(63), Is.True);
        SumaPuntos(0.5);
        Assert.That(árbol.Contains(51), Is.True);
        SumaPuntos(0.5);
    }
}