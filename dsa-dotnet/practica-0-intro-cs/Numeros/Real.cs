namespace Matematicas;

public class Real {
    // Datos
    
    // Propiedades
    public double Valor { get; }
    
    // Operaciones
    public Real (double valor) {
        Valor = valor;
    }
    public static Real operator *( Real a , Real b )
        => new Real ( a . Valor * b . Valor ) ;
    public static Real operator +( Real a , Real b )
        => new Real ( a . Valor + b . Valor ) ;
    public static Real operator -( Real a )
        => new Real ( - a . Valor ) ;
    public override string ToString () => $" { Valor } " ;
}
