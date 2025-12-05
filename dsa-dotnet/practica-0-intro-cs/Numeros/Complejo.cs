namespace Matematicas;

public class Complejo : IComplejo {
    
    // DATOS z = x + yi
    // Atributos
    private Real _x ;
    private Real _y ;

    // P r o p i e d a d e s
    public Real Real { get => _x ; set => _x = value ; }
    public Real Imaginaria { get => _y ; set => _y = value ; }

    // O P E R A C I O N E S
    public Complejo ( Real x , Real y ) { this . _x = x ; this . _y = y ; }
    // / < summary > Norma = ra í z cuadrada de ( x * x + y * y ) </ summary >
    public Real Norma () {
        return new Real ( Math . Sqrt (( _x * _x + _y * _y ) . Valor ) ) ;
    }
    // / < summary > conjugado ( z ) = x - yi </ summary >
    public Complejo Conjugado ()
    {
        return new Complejo ( _x , - _y ) ;
    }
    public override string ToString () => $"{ Real } + { Imaginaria }i" ;

}
