namespace Matematicas;

public interface IComplejo {
    
    // DATOS z = x + yi
    // Propiedades

    Real Real { get; set; }
    Real Imaginaria { get; set; }

    // Operaciones
    // Norma = |z|

    Real Norma();

    Complejo Conjugado();
}
