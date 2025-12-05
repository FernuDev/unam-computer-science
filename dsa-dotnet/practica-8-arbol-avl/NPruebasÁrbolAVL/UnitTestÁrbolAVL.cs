using ED.Colecciones;
using NPruebasÁrbolBinario;
using ED.PruebasUnitarias;
using ED.Estructuras.NoLineales.ÁrbolBinario;

namespace NPruebasÁrbolAVL;

public class PruebasÁrbolAVL : PruebasÁrbolBinarioOrdenado
{
    [OneTimeSetUp]
    public static new void TestFixtureSetup()
    {
        AsignaRubros(new Rubro[] {
            new Rubro("Rotación izquierda", 2.0),
            new Rubro("Rotación derecha", 2.0),
            new Rubro("Rebalanceo árbol", 1.0),
        });
    }

    protected override ICollection<T> CreateCollection<T>()
    {
        /*Console.WriteLine("Create " + typeof(T));
        Console.WriteLine($"String: {typeof(T)} is {typeof(T) == typeof(string)}");
        Console.WriteLine($"int: {typeof(T)} is  {typeof(T) == typeof(int)}");*/
        if(typeof(T) == typeof(string))
        {
            return (ICollection<T>)(new ÁrbolAVL<string>());
        }
        else if(typeof(T) == typeof(int))
        {
            return (ICollection<T>)(new ÁrbolAVL<int>());
        }
        else
        {
            Console.WriteLine($"Tipo {typeof(T)} solicitado.");
            throw new NotImplementedException();
        }
    }

    protected override IÁrbolBinarioOrdenado<C> CreateComparableCollection<C>()
    {
        return new ÁrbolAVL<C>();
    }

    [Test]
    public override void PruebaAddTodo()
    {
        //             25
        //       13          51
        //    10    15    45    63
        //  0
        IniciaPrueba("Add", "Agrega varios nodos", 1.5);
        IÁrbolBinarioOrdenado<int> árbol = CreateFilledComparableCollection();
        //Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(25));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(13));
        Assert.That(hD!.Dato, Is.EqualTo(51));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(10));
        Assert.That(hD!.Dato, Is.EqualTo(15));
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(0));
        hD = árbol.Raíz!.HijoD();
        hI = hD!.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(45));
        Assert.That(hD!.Dato, Is.EqualTo(63));

        Assert.That(árbol.Count, Is.EqualTo(8));
        SumaPuntos(1.5);
    }

    [Test]
    public void PruebaLL()
    {
        IniciaPrueba("Rotación derecha", "Agrega CBA", 0.5);
        IÁrbolBinarioOrdenado<string> árbol = CreateComparableCollection<string>();
        árbol.Add("C");
		árbol.Add("B");
		árbol.Add("A");

        Assert.That(árbol.Raíz!.Dato, Is.EqualTo("B"));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo("A"));
        Assert.That(hD!.Dato, Is.EqualTo("C"));

        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(1));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        SumaPuntos(0.5);
    }

    [Test]
    public void PruebaRR()
    {
        IniciaPrueba("Rotación izquierda", "Agrega ABC", 0.5);
        IÁrbolBinarioOrdenado<string> árbol = CreateComparableCollection<string>();
        árbol.Add("A");
		árbol.Add("B");
		árbol.Add("C");

        Assert.That(árbol.Raíz!.Dato, Is.EqualTo("B"));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo("A"));
        Assert.That(hD!.Dato, Is.EqualTo("C"));

        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(1));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        SumaPuntos(0.5);
    }

    [Test]
    public void PruebaLR()
    {
        IniciaPrueba("Rebalanceo árbol", "Agrega CAB", 0.5);
        IÁrbolBinarioOrdenado<string> árbol = CreateComparableCollection<string>();
        árbol.Add("C");
		árbol.Add("A");
		árbol.Add("B");

        Assert.That(árbol.Raíz!.Dato, Is.EqualTo("B"));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo("A"));
        Assert.That(hD!.Dato, Is.EqualTo("C"));

        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(1));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        SumaPuntos(0.5);
    }

    [Test]
    public void PruebaRL()
    {
        IniciaPrueba("Rebalanceo árbol", "Agrega ACB", 0.5);
        IÁrbolBinarioOrdenado<string> árbol = CreateComparableCollection<string>();
        árbol.Add("A");
		árbol.Add("C");
		árbol.Add("B");

        Assert.That(árbol.Raíz!.Dato, Is.EqualTo("B"));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo("A"));
        Assert.That(hD!.Dato, Is.EqualTo("C"));

        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(1));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        SumaPuntos(0.5);
    }

    [Test]
    public void PruebaTodasLasRotaciones()
    {
        //              50
        //       23            70
        //          39      65    82
        //                   68
        IniciaPrueba("Rebalanceo árbol", "Todas las rotaciones [65, 50, 23, 70, 82, 68, 39]", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {65, 50, 23, 70, 82, 68, 39};
        /*foreach(var dato in datos)
        {
            árbol.Add(dato);
        }*/

        /*
              50
        23           70
                  65    82
        */
        árbol.Add(65);
        árbol.Add(50);
        árbol.Add(23);
        árbol.Add(70);
        árbol.Add(82);
        //Console.WriteLine(árbol.ToString());

        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(50));
        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(2));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(70));
        hI = hD.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(65));
        Assert.That(hD!.Dato, Is.EqualTo(82));


        árbol.Add(68);
        //Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(65));
        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(2));
        hI = árbol.Raíz!.HijoI();
        hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(50));
        Assert.That(hD!.Dato, Is.EqualTo(70));
        Assert.That(hI!.Altura, Is.EqualTo(1));
        Assert.That(hD!.Altura, Is.EqualTo(1));
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        hI = hD.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(68));
        Assert.That(hD!.Dato, Is.EqualTo(82));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));



        árbol.Add(39);
        //Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(65));
        Assert.That(árbol.Raíz!.Altura, Is.EqualTo(2));
        hI = árbol.Raíz!.HijoI();
        hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(39));
        Assert.That(hD!.Dato, Is.EqualTo(70));
        Assert.That(hI!.Altura, Is.EqualTo(1));
        Assert.That(hD!.Altura, Is.EqualTo(1));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(50));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        hD = árbol.Raíz!.HijoD();
        hI = hD!.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(68));
        Assert.That(hD!.Dato, Is.EqualTo(82));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        Assert.That(hD!.Altura, Is.EqualTo(0));

        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaAddCuatroExtra()
    {
        //             39
        //       23           65
        //    10    30    50      70
        //         25      55   68  82
        IniciaPrueba("Rebalanceo árbol", "Todas las rotaciones hasta raíz", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {65, 50, 23, 70, 82, 68, 39, 10, 30, 55, 25};
        foreach(int val in datos)
        {
            árbol.Add(val);
        }
        //Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(39));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(65));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(10));
        Assert.That(hD!.Dato, Is.EqualTo(30));
        hI = hD.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(25));
        hD = árbol.Raíz!.HijoD();
        hI = hD!.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(50));
        Assert.That(hD!.Dato, Is.EqualTo(70));

        Assert.That(árbol.Count, Is.EqualTo(11));
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaAddAllRemove1()
    {
        //              65
        //       39            70
        //    23    50            82
        IniciaPrueba("Remove", "Todas las rotaciones y remover 68", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {65, 50, 23, 70, 82, 68, 39};
        foreach(int val in datos)
        {
            árbol.Add(val);
        }
        //Console.WriteLine(árbol.ToString());
        árbol.Remove(68);

        //Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(65));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(39));
        Assert.That(hD!.Dato, Is.EqualTo(70));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(50));
        hD = árbol.Raíz!.HijoD();
        hD = hD!.HijoD();
        Assert.That(hD!.Dato, Is.EqualTo(82));

        Assert.That(árbol.Count, Is.EqualTo(6));
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaAddAllRemove2()
    {
        //              65
        //       39            70
        //    23    50         
        IniciaPrueba("Remove", "Todas las rotaciones y remover 68 y 82", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {65, 50, 23, 70, 82, 68, 39};
        foreach(int val in datos)
        {
            árbol.Add(val);
        }
        Console.WriteLine(árbol.ToString());
        árbol.Remove(68);
        árbol.Remove(82);

        Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(65));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(39));
        Assert.That(hD!.Dato, Is.EqualTo(70));
        Assert.That(hD!.Altura, Is.EqualTo(0));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(50));

        Assert.That(árbol.Count, Is.EqualTo(5));
        SumaPuntos(1.0);
    }

    [Test]
    public void PruebaAddAllRemove3()
    {
        //              39
        //       23            65
        //                   50         
        IniciaPrueba("Remove", "Todas las rotaciones y remover 68, 82 y 70", 1.0);
        IÁrbolBinarioOrdenado<int> árbol = CreateComparableCollection<int>();
        int[] datos = {65, 50, 23, 70, 82, 68, 39};
        foreach(int val in datos)
        {
            árbol.Add(val);
        }
        Console.WriteLine(árbol.ToString());
        árbol.Remove(68);
        árbol.Remove(82);
        árbol.Remove(70);

        Console.WriteLine(árbol.ToString());
        Assert.That(árbol.Raíz!.Dato, Is.EqualTo(39));
        var hI = árbol.Raíz!.HijoI();
        var hD = árbol.Raíz!.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(23));
        Assert.That(hD!.Dato, Is.EqualTo(65));
        Assert.That(hI!.Altura, Is.EqualTo(0));
        hI = hD.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(50));

        Assert.That(árbol.Count, Is.EqualTo(4));
        SumaPuntos(1.0);
    }
}