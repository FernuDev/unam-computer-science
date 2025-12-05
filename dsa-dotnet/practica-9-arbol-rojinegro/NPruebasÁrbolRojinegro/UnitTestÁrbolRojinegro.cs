using ED.Colecciones;
using NPruebasÁrbolBinario;
using ED.PruebasUnitarias;
using ED.Estructuras.NoLineales.ÁrbolBinario;

namespace NPruebasÁrbolRojinegro;

public class PruebasÁrbolRojinegro : PruebasÁrbolBinarioOrdenado
{

    [OneTimeSetUp]
    public static new void TestFixtureSetup() {}

    protected override ICollection<T> CreateCollection<T>()
    {
        if(typeof(T) == typeof(string))
        {
            return (ICollection<T>)(new ÁrbolRojinegro<string>());
        }
        else if(typeof(T) == typeof(int))
        {
            return (ICollection<T>)(new ÁrbolRojinegro<int>());
        }
        else
        {
            Console.WriteLine($"Tipo {typeof(T)} solicitado.");
            throw new NotImplementedException();
        }
    }

    protected override IÁrbolBinarioOrdenado<C> CreateComparableCollection<C>()
    {
        return new ÁrbolRojinegro<C>();
    }

    [Test]
    public override void PruebaAddTodo()
    {
        //             25
        //       13R         51
        //    10    15    45R    63R
        //  0R
        IniciaPrueba("Add", "Agrega varios nodos", 1.5);
        ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateFilledComparableCollection();
        Console.WriteLine(árbol.ToString());
        NodoRojinegro<int> raíz = (NodoRojinegro<int>)árbol.Raíz!;
        Assert.That(raíz.Dato, Is.EqualTo(25));
        Assert.That(raíz.Color, Is.EqualTo(Color.Negro));
        var hI = raíz.HijoI();
        var hD = raíz.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(13));
        Assert.That(hI!.Color, Is.EqualTo(Color.Rojo));
        Assert.That(hD!.Dato, Is.EqualTo(51));
        Assert.That(hD!.Color, Is.EqualTo(Color.Negro));
        hD = hI.HijoD();
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(10));
        Assert.That(hI!.Color, Is.EqualTo(Color.Negro));
        Assert.That(hD!.Dato, Is.EqualTo(15));
        Assert.That(hD!.Color, Is.EqualTo(Color.Negro));
        hI = hI.HijoI();
        Assert.That(hI!.Dato, Is.EqualTo(0));
        Assert.That(hI!.Color, Is.EqualTo(Color.Rojo));
        hD = raíz.HijoD();
        hI = hD!.HijoI();
        hD = hD.HijoD();
        Assert.That(hI!.Dato, Is.EqualTo(45));
        Assert.That(hI!.Color, Is.EqualTo(Color.Rojo));
        Assert.That(hD!.Dato, Is.EqualTo(63));
        Assert.That(hD!.Color, Is.EqualTo(Color.Rojo));

        Assert.That(árbol.Count, Is.EqualTo(8));
        SumaPuntos(1.5);
    }

    
    
	public void PruebaÁrbol2b()
    {
        IniciaPrueba("Add", "Dos elementos derecha [A, B]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("A");
		árbol.Add("B");
        Assert.That(árbol.Count, Is.EqualTo(2));
		SumaPuntos(1.0);
	}

	public void PruebaÁrbolCBDA()
    {
        IniciaPrueba("Add", "Caso 1 (izquierda) [C, B, D, A]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("C");
		árbol.Add("B");
		árbol.Add("D");
		árbol.Add("A");
        Assert.That(árbol.Count, Is.EqualTo(4));
		SumaPuntos(1.0);
	}

	[Test]
	public void PruebaÁrbol4d()
    {
        IniciaPrueba("Add", "Caso 1 (derecha) [B, A, C, D]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("B");
		árbol.Add("A");
		árbol.Add("C");
		árbol.Add("D");
        Assert.That(árbol.Count, Is.EqualTo(4));
		SumaPuntos(1.0);
	}

	[Test]
	public void PruebaÁrbol4e()
    {
        IniciaPrueba("Add", "Caso 1b [D, C, A, B]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("D");
		árbol.Add("C");
		árbol.Add("A");
		árbol.Add("B");
        Assert.That(árbol.Count, Is.EqualTo(4));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaLL()
    {
        IniciaPrueba("Add", "Caso 2 [C, B, A]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("C");
		árbol.Add("B");
		árbol.Add("A");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaRR()
    {
        IniciaPrueba("Add", "Caso 2 (espejo) [A, B, C]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("A");
		árbol.Add("B");
		árbol.Add("C");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaLR()
    {
        IniciaPrueba("Add", "Caso 3 [C, A, B]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("C");
		árbol.Add("A");
		árbol.Add("B");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaRL()
    {
        IniciaPrueba("Add", "Caso 3 (espejo) [B, A, C]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("B");
		árbol.Add("A");
		árbol.Add("C");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaCormen()
    {
        IniciaPrueba("Add", "Cormen [11, 2, 14, 1, 7, 15, 5, 8]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
        int[] datos = {11, 2, 14, 1, 7, 15, 5, 8};
		árbol.Add(11);
		árbol.Add(2);
		árbol.Add(14);
		árbol.Add(1);
		árbol.Add(7);
		árbol.Add(15);
		árbol.Add(5);
		árbol.Add(8);
        Assert.That(árbol.Count, Is.EqualTo(datos.Length));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaCormen3en1()
    {
        IniciaPrueba("Add", "Cormen 3 en 1 [11, 2, 14, 1, 7, 15, 5, 8, 4]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
        int[] datos = {11, 2, 14, 1, 7, 15, 5, 8, 4};
		árbol.Add(11);
		árbol.Add(2);
		árbol.Add(14);
		árbol.Add(1);
		árbol.Add(7);
		árbol.Add(15);
		árbol.Add(5);
		árbol.Add(8);
		árbol.Add(4);
        Assert.That(árbol.Count, Is.EqualTo(datos.Length));
		SumaPuntos(1.0);
	}

	[Test]
	public void dibujaMuchosNodos()
    {
        IniciaPrueba("Add", "Muchos nodos [11, 2, 14, 1, 7, 15, 5, 8, 4, 54, 48, 10, 60, 58, 36, 3, 9, 40, 72, 6, 57, 77]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
        int[] datos = {11, 2, 14, 1, 7, 15, 5, 8, 4, 54, 48, 10, 60, 58, 36, 3, 9, 40, 72, 6, 57, 77};
		árbol.Add(11);
		árbol.Add(2);
		árbol.Add(14);
		árbol.Add(1);
		árbol.Add(7);
		árbol.Add(15);
		árbol.Add(5);
		árbol.Add(8);
		árbol.Add(4);
		árbol.Add(54);
		árbol.Add(48);
		árbol.Add(10);
		árbol.Add(60);
		árbol.Add(58);
		árbol.Add(36);
		árbol.Add(3);
		árbol.Add(9);
		árbol.Add(40);
		árbol.Add(72);
		árbol.Add(6);
		árbol.Add(57);
		árbol.Add(77);
        Assert.That(árbol.Count, Is.EqualTo(datos.Length));
		SumaPuntos(1.0);
	}

    [Test]
	public void PruebaRemueve1Antes()
    {
        IniciaPrueba("Remove", "Remover 1 antes [50]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
		árbol.Add(50);
        Assert.That(árbol.Count, Is.EqualTo(1));
		SumaPuntos(1.0);
	}
	
    [Test]
	public void PruebaRemueve2a()
    {
        IniciaPrueba("Remove", "Remover A de caso 1 izquierda [C, B, D, A]-[A]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("C");
		árbol.Add("B");
		árbol.Add("D");
		árbol.Add("A");
		árbol.Remove("A");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

    [Test]
	public void PruebaRemueve2()
    {
        IniciaPrueba("Remove", "Remover A de caso 1 derecha [B, A, C, D]-[A]", 1.0);
		ÁrbolRojinegro<string> árbol = (ÁrbolRojinegro<string>)CreateCollection<string>();
		árbol.Add("B");
		árbol.Add("A");
		árbol.Add("C");
		árbol.Add("D");
		árbol.Remove("A");
        Assert.That(árbol.Count, Is.EqualTo(3));
		SumaPuntos(1.0);
	}

    [Test]
	public void PruebaRemueveCormen()
    {
        IniciaPrueba("Remove", "Remueve 1 de Cormen (Caso 4) [11, 2, 14, 1, 7, 15, 5, 8]-[1]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
		árbol.Add(11);
		árbol.Add(2);
		árbol.Add(14);
		árbol.Add(1);
		árbol.Add(7);
		árbol.Add(15);
		árbol.Add(5);
		árbol.Add(8);
		árbol.Remove(1);
        Assert.That(árbol.Count, Is.EqualTo(7));
		SumaPuntos(1.0);
	}

    [Test]
	public void PruebaRemueveCormen2()
    {
        IniciaPrueba("Remove", "Remueve 1,15,14 de Cormen (Caso 1) [11, 2, 14, 1, 7, 15, 5, 8]-[1, 15, 14]", 1.0);
		ÁrbolRojinegro<int> árbol = (ÁrbolRojinegro<int>)CreateCollection<int>();
		árbol.Add(11);
		árbol.Add(2);
		árbol.Add(14);
		árbol.Add(1);
		árbol.Add(7);
		árbol.Add(15);
		árbol.Add(5);
		árbol.Add(8);
		árbol.Remove(1);
		árbol.Remove(15);
		árbol.Remove(14);
        Assert.That(árbol.Count, Is.EqualTo(5));
		SumaPuntos(1.0);
	}
}