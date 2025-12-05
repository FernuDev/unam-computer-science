// STOUT
Console.WriteLine("¿Como te llamas ?");

// Declaración de Strings
string n = Console.ReadLine(); // STDIN

int x;

while(true) {
    try {
        Console.WriteLine("Escribe un entero: ");
        x = Convert.ToInt32(Console.ReadLine());
        break;
    }
    catch (FormatException fe) {
        Console.WriteLine("Ese no fue un numero entero.");  }
}

Console.WriteLine($"{n} me diste el numero {x}");

