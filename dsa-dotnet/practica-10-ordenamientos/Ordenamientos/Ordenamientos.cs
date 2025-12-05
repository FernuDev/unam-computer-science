namespace ED.Ordenamientos;

public class BubbleSorter<C> : IOrdenador<C> where C : IComparable<C>
{
    public C[] Ordena(C[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            for(int j = 0; j < a.Length - i - 1; j++)
                if (a[j].CompareTo(a[j + 1]) > 0)
                {
                    IOrdenador<C>.Swap(a, j, j + 1);
                }
        }

        return a;
    }

    // Podriamos tomar el caso de un array invertido como el peor
    // sin embargo, el algoritmo Bubblesort no depende del orden de los factores
    // si no de la cantidad teniendo siempre una complejidad de O(n²)
    public int[] PeorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=tam, j=0; i>=1; i--, j++)
        {
            array[j] = i;
        }

        return array;
    }

    // El mejor de los casos logicos seria un arreglo ya ordenado 
    // sin embargo el algoritmo bubble sort siempre mantiene una complejidad de 
    // O(n²)

    public int[] MejorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=1;i<=tam;i++) 
        {
            array[i-1] = i; 
        }

        return array;
    }
}

public class SelectionSorter<C> : IOrdenador<C> where C : IComparable<C>
{
    public C[] Ordena(C[] a)
    {
        for(int i = 0; i < a.Length - 1; i++)
        {
            int min_i = i;
            for(int j=i+1; j < a.Length; j++)
            {
                if(a[j].CompareTo(a[min_i]) < 0) 
                {
                    min_i = j;
                }
            }
            
            IOrdenador<C>.Swap(a, i, min_i);

        }
        return a;
    }

    // De la misma manera que bubble sort este algoritmo no depende del 
    // orden de los elementos si no se la cantidad de los mismos, por lo que 
    // es indistinto si colocamos entonces un arreglo ya ordenado o en orden 
    // inverso pues siempre demorará O(n²)

    public int[] PeorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=tam, j=0; i>=1; i--, j++)
        {
            array[j] = i;
        }

        return array;
    }

    public int[] MejorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=1;i<=tam;i++) 
        {
            array[i-1] = i; 
        }

        return array;
    }
}

public class QuickSorter<C> : IOrdenador<C> where C : IComparable<C>
{    
    private int Particion(C[] a, int low, int high)
    {
        C pivot = a[low]; // Elegimos el primer elemento
        int i = high + 1;

        for(int j=high; j > low; j--)
        {
            if(a[j].CompareTo(pivot) >= 0)
            {
                i--;
                IOrdenador<C>.Swap(a,i,j);
            }
        }

        IOrdenador<C>.Swap(a, i-1, low);
        return i -1;
    }

    private C[] QuickSort(C[] a, int low, int high)
    {
        if(low < high)
        {
            int pivoteIndex = Particion(a, low, high);
            QuickSort(a, low, pivoteIndex - 1);
            QuickSort(a, pivoteIndex + 1, high);
        }
        return a;
    }

    public C[] Ordena(C[] a)
    {   
        return QuickSort(a, 0, a.Length - 1);
    }

    public int[] PeorCaso(int tam)
    {
        return [1,2,3,4,5];
    }

    public int[] MejorCaso(int tam)
    {
        return [1,2,3,4,5];
    }
}

public class MergeSorter<C> : IOrdenador<C> where C : IComparable<C>
{

    private C[] Merge(C[] a, C[] left, C[] right)
    {
        int i=0, j=0, k=0;

        // Comenzamos comparando los elementos de las sublistas
        while( i < left.Length && j < right.Length )
        {
            if(left[i].CompareTo(right[j]) <= 0) // left[i] <= right[j]
            {
                a[k++] = left[i++];
            }
            else 
            {
                a[k++] = right[j++];
            }
        }

        // Agregamos los elementos restantes de las sublistas
        while( i < left.Length )
        {
            a[k++] = left[i++];
        }

        while( j < right.Length )
        {
            a[k++] = right[j++];
        }

        return a;
    }

    public C[] Ordena(C[] a)
    {
        if(a.Length <= 1) return a;

        int mid = a.Length / 2;

        // Declaramos los sub arrays
        C[] izquierda = new C[mid];
        C[] derecha = new C[a.Length - mid];

        // Partimos los datos del array con una funcion in-built 
        // Para ahorrar lineas de codigo que serian dedicadas a un for
        Array.Copy(a, 0, izquierda, 0, mid);
        Array.Copy(a, mid, derecha, 0, a.Length - mid);

        Ordena(izquierda);
        Ordena(derecha);

        return Merge(a, izquierda, derecha);
    }

    // Nuevamente vemos una consistencia en el algoritmo
    // ya que depende mas de la cantidad de elementos que de la entrada de los mismos
    public int[] PeorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=tam, j=0; i>=1; i--, j++)
        {
            array[j] = i;
        }

        return array;
    }

    public int[] MejorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=1;i<=tam;i++) 
        {
            array[i-1] = i; 
        }

        return array;
    }
}

public class InsertionSorter<C> : IOrdenador<C> where C : IComparable<C>
{
    public C[] Ordena(C[] a)
    {
        int n = a.Length;

        for(int i=1;i<n;i++) 
        {
            // Elemento en el indice actual del array
            C current = a[i];

            // Indice del elemento anterior 
            int j = i - 1;

            while(j>=0 && a[j].CompareTo(current) > 0)
            {
                a[j+1] = a[j];
                j = j -1;
            }

            a[j+1] = current;

        }

        return a;
    }

    // El peor caso posible O(n²) viene cuando el arreglo se encuentra
    // en orden inverso pues debe recorer n veces el arreglo para poder ordenar
    // cada elemento
    public int[] PeorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=tam, j=0; i>=1; i--, j++)
        {
            array[j] = i;
        }

        return array;
    }

    // El mejor caso posible con complejidad lineal O(n) es cuando el arreglo
    // ya se encuentra ordenado
    public int[] MejorCaso(int tam)
    {
        int[] array = new int[tam];

        for(int i=1;i<=tam;i++) 
        {
            array[i-1] = i; 
        }

        return array;
    }
}
