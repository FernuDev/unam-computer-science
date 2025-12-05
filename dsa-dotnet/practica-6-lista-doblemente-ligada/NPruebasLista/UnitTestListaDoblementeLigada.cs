using ED.Colecciones;
using NPruebasColección;
using ED.PruebasUnitarias;
using ED.Estructuras.Lineales.Lista;

namespace NPruebasLista;

/// <summary>
/// Pruebas adaptadas y extendidas de la versión original de mindahrelfen y blackzafiro.
/// </summary>
public class Tests : PruebasColección
{
    [OneTimeSetUp]
    public static new void TestFixtureSetup()
    {
        Calificador.TestFixtureSetup();
        AsignaRubros(new Rubro[] {
            new Rubro("Constructor", 0.2), //
			new Rubro("GetEnumerator", 0.2),
			new Rubro("GetEnumeratorInt", 0.4), //
			new Rubro("HasNext", 0.2),
			new Rubro("HasPrevious", 0.2),
			new Rubro("Index", 0.2),
			new Rubro("MoveNext", 0.4), //
			new Rubro("MovePrevious", 0.4), //
			new Rubro("EnumeratorAgrega", 1.0), //
			new Rubro("EnumeratorRemueve", 1.0),
			new Rubro("EnumeratorAsigna", 0.25), //
			new Rubro("Count", 0.5),
			new Rubro("this[int]Get", 0.25), //
			new Rubro("this[int]Set", 0.25), //
            new Rubro("Add", 0.5), //
			new Rubro("Clear", 0.3),
			new Rubro("IndexOf", 0.5), //
            new Rubro("Insert", 0.5), //
			new Rubro("Remove", 0.5), //
			new Rubro("RemoveAt", 0.5), //
			new Rubro("Contains", 0.5), //
			new Rubro("EstáVacía", 0.25),
			new Rubro("CopyTo", 0.25),
        });
    }

    public override ILista<T> CreateCollection<T>()
    {
        return new ListaDoblementeLigada<T>();
    }

    public ILista<string?> CreaListaLlena()
    {
        ILista<string?> estructura = CreateCollection<string?>();
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
            estructura.Add(cad);
        }
        return estructura;
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaConstructor()
    {
        IniciaPrueba("Constructor", "Constructor por defecto", 1.0);
        ListaDoblementeLigada<int> p = new ListaDoblementeLigada<int>();
        SumaPuntos(1.0);
    }

	/// Iterador : IEnumeradorLista<T>

	[Test]
	public void ListIteratorIndexConstructorTest() {
		int index;
		String str;
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("GetEnumeratorInt", "Revisa que GetEnumerator(int) construya al iterador en la posición correcta", 1.5);

		/**
		 * Agrega elementos a la estructura.
		 */
		estructura = CreaListaLlena();

		/**
		 * Solicita la creación al inicio de la lista.
		 */
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(0));
		Assert.Throws<NoSuchElementException>(
            () => { var i = it.Índice; }
        );
		Assert.Throws<NoSuchElementException>(
            () => { var c  = it.Current; }
        );

		SumaPuntos(0.25);

		/**
		 * Solicita la creación sobre el primer elemento de la lista.
		 */
		it = (IEnumeradorLista<string>)estructura.GetEnumerator(1);
		Assert.That(it.Índice, Is.EqualTo(0));
		Assert.That(it.Current, Is.EqualTo(cadenasAleatoriasConNulls[0]));

		SumaPuntos(0.25);

		/**
		 * Obtiene una posición aleatoria en medio de la lista,
		 * luego con get obtiene el elemento
		 * dentro de la estructura en dicha posición.
		 */
		Random rnd = new Random();
		index = rnd.Next(1, estructura.Count);
		str = cadenasAleatoriasConNulls[index];

		/**
		 * Crea un iterador en la misma posición, y el elemento devuelto por el
		 * next debe de ser el mismo que el obtenido por el get.
		 */
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(index));
		it.MoveNext();
		Assert.That(str, Is.EqualTo(it.Current));

		SumaPuntos(0.5);

		/**
		 * Solicita la creación sobre el último elemento.
		 */
		int tam = cadenasAleatoriasConNulls.Count;
		int últimoÍndice = tam - 1;
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(últimoÍndice));
		//Console.WriteLine(" lista: " + estructura.ToString());

		it.MoveNext();
		Assert.That(it.Índice, Is.EqualTo(últimoÍndice));
		//Console.WriteLine( " índice correcto " + últimoÍndice);
		Assert.That(it.Current, Is.EqualTo(cadenasAleatoriasConNulls[últimoÍndice]));
		SumaPuntos(0.25);

		/**
		 * Solicita la creación al final de la lista.
		 */
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(tam));
		it.MoveNext();
		Assert.Throws<NoSuchElementException>(
            () => { var i = it.Índice; }
        );
		Assert.Throws<NoSuchElementException>(
            () => { var c  = it.Current; }
        );
		SumaPuntos(0.25);
		
	}

	[Test]
	public void ListIteratorAddContainsTest() {
		int index;
		ILista<string> estructura;
		IEnumeradorLista<string> it;
		Random rnd = new Random();

		IniciaPrueba("EnumeratorAgrega", "Revisa que la estructura contenga los elementos insertados con Add() de List Iterator", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		estructura = CreateCollection<string>();
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			index = rnd.Next(estructura.Count + 1);
			it = (IEnumeradorLista<string>)(estructura.GetEnumerator(index));
			it.Agrega(cad);
			Assert.That(estructura[index], Is.EqualTo(cad));
			if(index < estructura.Count - 1)
			{
				// El siguiente del elemento insertado está en index + 1
				it.MoveNext();
				Assert.That(it.Índice, Is.EqualTo(index + 1));
			}
		}

		/**
		 * Se asegura que los elementos insertados estén contenidos dentro de la
		 * estructura.
		 */
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			Assert.That(estructura.Contains(cad));
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListiteratorAgregaSizeTest()
	{
		int index;
		ILista<string> estructura;
		IEnumeradorLista<string> it;
		Random rnd = new Random();

		IniciaPrueba("EnumeratorAgrega", "Revisa que la estructura mantenga la cantidad correcta de elementos, ademas de no estar vacía tras insertar con add() de List Iterator", 1.0);

		/**
		 * Inserta elementos en la estructura de uno en uno y revisa que cada
		 * inserción mantenga el tamaño deseado. Además, revisa que ninguna
		 * inserción vuelva vacía la estructura.
		 */
		estructura = CreateCollection<string>();
		index = 1;
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			it = (IEnumeradorLista<string>)(estructura.GetEnumerator(rnd.Next(estructura.Count + 1)));			
			it.Agrega(cad);
			Assert.That(estructura.Count, Is.EqualTo(index++));
			Assert.That(estructura.EstáVacía(), Is.False);
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListIteratoraddOrderTest() {
		int index;
		String[] array1, array2;
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("MoveNext", "Revisa que la estructura mantenga el orden correcto de elementos tras insertar con add() de List Iterator", 1.0);

		/**
		 * Inserta elementos en la estructura, también en un arreglo.
		 */
		estructura = CreateCollection<string>();
		array1 = new String[cadenasAleatoriasConNulls.Count];
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator());
		index = 0;
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			it.Agrega(cad);
			array1[index++] = cad;
		}

		/**
		 * Obtiene el arreglo equivalente a la estructura.
		 */
		array2 = new String[estructura.Count];
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator());
		index = 0;
		while (it.MoveNext()) {
			array2[index++] = it.Current;
		}

		/**
		 * Revisa la igualdad entre arreglos.
		 */
		Assert.That(array1, Is.EqualTo(array2));

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListIteratorsetContainsTest()
	{
		int index;
		ILista<string> estructura;
		IEnumeradorLista<string> it;
		Random rnd = new Random();

		IniciaPrueba("EnumeratorAsigna", "Revisa que la estructura contenga los elementos insertados con set(E) de List Iterator", 1.0);

		/**
		 * Agrega elementos a la estructura.
		 */
		estructura = CreateCollection<string>();
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			estructura.Add(cad);
		}

		/**
		 * Obtiene una posición aleatoria, después crea un iterador en esa
		 * posición, y luego lo modifica a través de next y set.
		 */
		index = rnd.Next(estructura.Count);
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(index));
		it.MoveNext();
		it.Asigna("");

		/**
		 * Obtiene el elemento en la misma posición y lo compara, debe igual al
		 * elemento insertado.
		 */
		Assert.That(estructura[index], Is.EqualTo(""));

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListIteratorIndexOutOfBoundsTest()
	{
		ILista<string> estructura;

		IniciaPrueba("GetEnumeratorInt", "Revisa que se lance IndexOutOfBoundsException si el parámetro en GetEnumerator(int) esta fuera de rango", 1.0);

		estructura = CreateCollection<string>();
		estructura.Add("");
		
		Assert.Throws<ArgumentOutOfRangeException>(
			() => estructura.GetEnumerator(-1)
		);
		SumaPuntos(0.5);

		Assert.Throws<ArgumentOutOfRangeException>(
			() => estructura.GetEnumerator(estructura.Count + 1)
		);
		SumaPuntos(0.5);
		
	}

	[Test]
	public void ListIteratorNoElementsTest() {
		
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("MovePrevious", "Revisa que se devuelva false en MoveNext() y MovePrevious() si no existe ese elemento", 1.0);

		estructura = CreateCollection<string>();
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator());

		Assert.That(it.MoveNext(), Is.False);
		SumaPuntos(0.25);
		

		Assert.That(it.MovePrevious(), Is.False);
		SumaPuntos(0.25);

		estructura.Add("");

		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(0));
		it.MovePrevious();
		Assert.Throws<NoSuchElementException>(
			() => { var i = it.Índice; }
        );
		Assert.Throws<NoSuchElementException>(
			() => { var i = it.Current; }
        );
		SumaPuntos(0.25);

		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(estructura.Count));
		Assert.That(it.MoveNext(), Is.False);
		Assert.Throws<NoSuchElementException>(
			() => { var i = it.Índice; }
        );
		Assert.Throws<NoSuchElementException>(
			() => { var i = it.Current; }
        );
		SumaPuntos(0.25);
	}

	[Test]
	public void ListIteratorIllegalStateTest()
	{
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("EnumeratorRemueve", "Revisa que se lance IllegalStateException cuando se invoca Asigna(E) o Remueve de List Iterator si no se invoca MoveNext() o MovePrevious() primero", 1.0);

		estructura = CreateCollection<string>();
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator());

		Assert.Throws<IllegalStateException>(
			() => it.Remueve()
        );
		SumaPuntos(0.5);

		Assert.Throws<IllegalStateException>(
			() => it.Asigna(null)
        );
		SumaPuntos(0.5);
	}

	[Test]
	public void ListIteratorIllegalStateAddRemoveTest() {
		
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("EnumeratorRemueve", "Revisa que se lance IllegalStateException cuando se invoca remove() de List Iterator si no se invoca next() o previous() tras invocar add(E) o remove() de List Iterator", 1.0);

		estructura = CreateCollection<string>();
		estructura.Add("");
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(0));
		
		Assert.Throws<IllegalStateException>(
			() => {
				it.MoveNext();
				it.Remueve();
				it.Remueve();
			}
        );
		SumaPuntos(0.5);

		Assert.Throws<IllegalStateException>(
			() => {
				it.Agrega("");
				it.Remueve();
			}
        );
		SumaPuntos(0.5);
	}






    
    //add
	[Test]
	public void ListInsertContainsTest()
	{
		ILista<string> estructura;

		IniciaPrueba("Contains", "Revisa que la estructura contenga los elementos insertados con add(int, E)", 1.0);

		/**
		 * Inserta elementos en la estructura.
		 */
		estructura = CreateCollection<string>();
		Random rnd = new Random();
		foreach(string cad in cadenasAleatorias)
        {
			estructura.Insert(rnd.Next(estructura.Count + 1), cad);
		}

		/**
		 * Se asegura que los elementos insertados estén contenidos dentro de la
		 * estructura.
		 */
		foreach(string cad in cadenasAleatorias)
        {
			Assert.That(estructura.Contains(cad), Is.True);
		}

		SumaPuntos(1.0);
		
	}

    [Test]
	public void ListAddCountTest() {
		int index;
		ILista<string> estructura;

		IniciaPrueba("Add", "Revisa que la estructura mantenga la cantidad correcta de elementos, ademas de no estar vacía tras insertar con Add(T)", 1.0);

		/**
		 * Inserta elementos en la estructura de uno en uno y revisa que cada
		 * inserción mantenga el tamaño deseado. Además, revisa que ninguna
		 * inserción vuelva vacía la estructura.
		 */
		estructura = CreateCollection<string>();
		index = 1;
        foreach(string cad in cadenasAleatorias)
        {
			estructura.Add(cad);
			Assert.That(estructura.Count, Is.EqualTo(index++));
			Assert.That(estructura.EstáVacía(), Is.False);
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListAddGetCountTest() {
		int index;
		ILista<string> estructura;

		IniciaPrueba("this[int]Get", "Revisa que la estructura mantenga la cantidad correcta de elementos, ademas de no estar vacía tras insertar con Add(T)", 1.0);

		/**
		 * Inserta elementos en la estructura de uno en uno y revisa que cada
		 * inserción mantenga el tamaño deseado. Además, revisa que ninguna
		 * inserción vuelva vacía la estructura.
		 */
		estructura = CreateCollection<string>();
		index = 0;
        foreach(string cad in cadenasAleatorias)
        {
			estructura.Add(cad);
            Assert.That(estructura[index], Is.EqualTo(cad));
			Assert.That(estructura.Count, Is.EqualTo(++index));
			Assert.That(estructura.EstáVacía(), Is.False);
		}
		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListInsertCountTest() {
		int index, tam;
		ILista<string> estructura;
		Random rnd = new Random();
		string? datoSiguiente = null;

		IniciaPrueba("Insert", "Revisa que la estructura mantenga la cantidad correcta de elementos, además de no estar vacía tras insertar con Insert(int, T)", 1.0);

		/**
		 * Inserta elementos en la estructura de uno en uno y revisa que cada
		 * inserción mantenga el tamaño deseado. Además, revisa que ninguna
		 * inserción vuelva vacía la estructura.
		 */
		estructura = CreateCollection<string>();
		tam = 1;
        foreach(string cad in cadenasAleatorias)
        {
			Console.WriteLine(estructura);
			index = rnd.Next(estructura.Count + 1);
			//Console.WriteLine("  insertar " + cad + " en " + index + " de " + estructura.Count);

			if (index < estructura.Count) datoSiguiente = estructura[index];
			estructura.Insert(index, cad);

			//Console.WriteLine(estructura);
			//Console.WriteLine("  en " + index + "   de " + estructura.Count);
			Assert.That(estructura[index], Is.EqualTo(cad));
			if (index + 1 < estructura.Count) Assert.That(estructura[index + 1], Is.EqualTo(datoSiguiente));
			Assert.That(estructura.Count, Is.EqualTo(tam++));
			Assert.That(estructura.EstáVacía(), Is.False);
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListAddIndexOutOfBoundsTest()
	{
		
		ILista<string> estructura;

		IniciaPrueba("Insert", "Revisa que se lance IndexOutOfBoundsException si el parámetro int de add(int, E) esta fuera de rango", 1.0);

		estructura = CreateCollection<string>();
		estructura.Insert(0, "");
		
		Assert.Throws<ArgumentOutOfRangeException>(
            () => estructura.Insert(-1, "")
        );
		SumaPuntos(0.5);

		Assert.Throws<ArgumentOutOfRangeException>(
            () => estructura.Insert(estructura.Count + 1, "")
		);
		SumaPuntos(0.5);
	}







	//get
	[Test]
	public void GetContainsTest() {
		String str;
		ILista<string?> estructura;

		IniciaPrueba("IndexOf", "Revisa que get(int) devuelva el elemento correcto con respecto a indexOf(E)", 1.0);

		/**
		 * Agrega elementos a la estructura.
		 */
		estructura = CreaListaLlena();

		/**
		 * Revisa que el elemento devuelto por get corresponda al índice de
		 * indexof.
		 */
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			Assert.That(estructura[estructura.IndexOf(cad)], Is.EqualTo(cad));
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void GetIndexOutOfBoundsTest() {
		
		ILista<string> estructura;

		IniciaPrueba("this[int]Get", "Revisa que se lance IndexOutOfBoundsException si el parámetro en get(int) esta fuera de rango", 1.0);

		estructura = CreateCollection<string>();
		estructura.Add("");
		
		Assert.Throws<ArgumentOutOfRangeException>(
            () => { var x = estructura[-1]; }
		);
		SumaPuntos(0.5);

		Assert.Throws<ArgumentOutOfRangeException>(
            () => { var x = estructura[estructura.Count]; }
		);
		SumaPuntos(0.5);
		
	}

	//indexOf
	[Test]
	public void IndexOfNoElementTest()
	{
		ILista<string> estructura;

		IniciaPrueba("IndexOf", "Revisa que indexOf(E) devuelva -1 cuando el elemento dado no esta en la estructura", 1.0);

		/**
		 * Busca un elemento que no esté en la estructura, pues la estructura
		 * esta vacía, su índice debe ser -1.
		 */
		estructura = CreateCollection<string>();
		Assert.That(estructura.IndexOf(""), Is.EqualTo(-1));

		SumaPuntos(0.25);

		/**
		 * Agrega elementos a la estructura.
		 */
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
            estructura.Add(cad);
		}

		/**
		 * Busca un elemento que no este en la estructura.
		 */
		Assert.That(estructura.IndexOf("No está"), Is.EqualTo(-1));

		SumaPuntos(0.75);
		
	}
	

	[Test]
	public void ListIteratorIllegalStateSetRemoveTest()
	{
		
		ILista<string> estructura;
		IEnumeradorLista<string> it;

		IniciaPrueba("EnumeratorAsigna", "Revisa que se lance IllegalStateException cuando se invoca set(E) de List Iterator si no se invoca next() o previous() tras invocar add(E) o remove() de List Iterator", 1.0);

		estructura = CreateCollection<string>();
		estructura.Add("");
		it = (IEnumeradorLista<string>)(estructura.GetEnumerator(0));
		
		Assert.Throws<IllegalStateException>(
            () => {
				it.MoveNext();
				it.Remueve();
				it.Asigna(null);
			}
		);
		SumaPuntos(0.5);
		
		Assert.Throws<IllegalStateException>(
            () => {
				it.Agrega("");
				it.Asigna(null);
			}
		);
		SumaPuntos(0.5);
	}

	//remove(int)
	[Test]
	public void ListremoveContainsTest()
	{
		ILista<string?> estructura;
		Random rnd = new Random();

		IniciaPrueba("RemoveAt", "Prueba que la estructura no contenga elementos después de borrarlos con remove(int)", 1.0);

		/**
		* Inserta elementos en la estructura.
		*/
		estructura = CreaListaLlena();

		/**
		* Luego los expulsa hasta que la estructura este vacía.
		*/
		while (!estructura.EstáVacía())
		{
			estructura.RemoveAt(rnd.Next(estructura.Count));
		}

		/**
		* Revisa que ningún elemento insertado se encuentre en la
		* estructura.
		*/
		foreach(string? cad in cadenasAleatoriasConNulls)
        {
			Assert.That(estructura.Contains(cad), Is.False);
		}

		SumaPuntos(1.0);
		
	}

	[Test]
	public void ListRemoveSizeTest()
	{
		int index;
		ILista<string> estructura;
		Random rnd = new Random();
		int range = cadenasAleatoriasConNulls.Count;

		IniciaPrueba("Remove", "Revisa que la cantidad de elementos tras borrar sea consistente con remove(int)", 1.0);

		/**
			* Inserta elementos en la estructura.
			*/
		estructura = CreaListaLlena();

		/**
			* Borra los elementos de la estructura de uno en uno y revisa que
			* cada borrado mantenga el tamaño deseado.
			*/
		index = 1;
		while (index <= range)
		{
			estructura.Remove(estructura[rnd.Next(estructura.Count)]);
			Assert.That(estructura.Count, Is.EqualTo(range - index++));
		}

		/**
			* Revisa que la estructura este vacía al finalizar el borrado de
			* todos sus elementos.
			*/
		Assert.That(estructura.EstáVacía(), Is.True);

		SumaPuntos(1.0);
	}

	[Test]
	public void RemoveIndexOutOfBoundsTest()
	{
		
		ILista<string> estructura;

		IniciaPrueba("RemoveAt", "Revisa que se lance IndexOutOfBoundsException si el parámetro de remove(int) esta fuera de rango", 1.0);

		estructura = CreateCollection<string>();
		estructura.Add("");
		
		Assert.Throws<ArgumentOutOfRangeException>(
			() => estructura.RemoveAt(-1)
		);
		SumaPuntos(0.5);

		Assert.Throws<ArgumentOutOfRangeException>(
			() => estructura.RemoveAt(estructura.Count)
		);
		SumaPuntos(0.5);
	}

	//set
	[Test]
	public void SetIndexOutOfBoundsTest() {
		
		ILista<string> estructura;

		IniciaPrueba("this[int]Set", "Revisa que se lance IndexOutOfBoundsException si el parámetro int en set(int, E) esta fuera de rango", 1.0);

		estructura = CreateCollection<string>();
		
		Assert.Throws<ArgumentOutOfRangeException>(
			() =>  estructura[-1] = ""
        );
		SumaPuntos(0.25);

		estructura.Add("");
		Assert.Throws<ArgumentOutOfRangeException>(
			() =>  estructura[estructura.Count] = ""
        );
		SumaPuntos(0.25);
	}

	[Test]
	public void ListsetContainsTest()
	{
		int index;
		ILista<string> estructura;
		Random rnd = new Random();

		IniciaPrueba("this[int]Set", "Revisa que la estructura contenga los elementos insertados con set(int, E)", 1.0);

		/**
		 * Agrega elementos a la estructura.
		 */
		estructura = CreaListaLlena();

		/**
		 * En un índice dado modifica a la estructura.
		 */
		index = rnd.Next(estructura.Count);
		estructura[index] = "Set";

		/**
		 * Revisa que el elemento se haya modificado correctamente.
		 */
		Assert.That(estructura[index], Is.EqualTo("Set"));

		SumaPuntos(1.0);
		
	}
}