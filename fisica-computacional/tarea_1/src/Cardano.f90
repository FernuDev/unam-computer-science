!===============================================================================
! Program: Cardano
! Purpose: Solve cubic equations using Cardano's formula
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Solves cubic equations of the form: a*x^3 + b*x^2 + c*x + d = 0
!   Uses Cardano's formula with discriminant analysis to determine root types:
!     - Delta > 0: One real root and two complex conjugate roots
!     - Delta = 0: Three real roots with at least one repeated
!     - Delta < 0: Three distinct real roots (irreducible case)
!
! Compilation:
!   gfortran Cardano.f90 -o Cardano
!
! Usage:
!   ./Cardano
!   Enter coefficients a, b, c, d when prompted
!===============================================================================

program Cardano
    implicit none
    
    ! Variable declarations
    real :: a, b, c, d              ! Coefficients of the cubic equation
    real :: p, q, det               ! Cardano formula parameters
    real :: x1, x2_real, x3_real    ! Real roots
    complex :: i_unit, u, v, x2, x3 ! Complex numbers and roots
    real :: theta                   ! Angle for trigonometric solution
    
    ! Constants
    real, parameter :: PI = 3.14159265
    
    ! Initialize imaginary unit
    i_unit = (0.0, 1.0)
    
    !---------------------------------------------------------------------------
    ! Input: Read coefficients from user
    !---------------------------------------------------------------------------
    print *, "Enter the coefficients of your third-degree equation"
    
    write(*,'(A)', advance="no"); print *, "a: "; read *, a
    write(*,'(A)', advance="no"); print *, "b: "; read *, b
    write(*,'(A)', advance="no"); print *, "c: "; read *, c
    write(*,'(A)', advance="no"); print *, "d: "; read *, d
    
    ! Display the equation
    print "(A, F6.2, A, F6.2, A, F6.2, A, F6.2, A)", &
          "Your equation is: ", a, "x^3 + ", b, "x^2 + ", c, "x + ", d, " = 0"
    
    !---------------------------------------------------------------------------
    ! Calculate Cardano formula parameters
    !---------------------------------------------------------------------------
    ! Depressed cubic substitution: x = t - b/(3a)
    ! Resulting equation: t^3 + p*t + q = 0
    
    p = (3.0*a*c - b**2) / (3.0*a**2)
    q = (2.0*b**3 - 9.0*a*b*c + 27.0*a**2*d) / (27.0*a**3)
    
    ! Discriminant: determines the nature of roots
    det = q**2 + (4.0*p**3) / 27.0
    
    !---------------------------------------------------------------------------
    ! Case analysis based on discriminant
    !---------------------------------------------------------------------------
    
    if (det > 0.0) then
        !-----------------------------------------------------------------------
        ! Case 1: Delta > 0
        ! One real root and two complex conjugate roots
        !-----------------------------------------------------------------------
        
        u = ((-q + i_unit*sqrt(det)) / 2.0)**(1.0/3.0)
        v = ((-q - i_unit*sqrt(det)) / 2.0)**(1.0/3.0)
        
        ! Calculate roots using Cardano's formulas
        x1 = real(u + v) - b/(3.0*a)
        x2 = -(u + v)/2.0 - b/(3.0*a) + (sqrt(3.0)/2.0)*(u - v)*i_unit
        x3 = -(u + v)/2.0 - b/(3.0*a) - (sqrt(3.0)/2.0)*(u - v)*i_unit
        
        print *, "Delta > 0: one real root and two complex conjugates"
        print *, "x1 =", x1
        print *, "x2 =", x2
        print *, "x3 =", x3
        
    else if (det == 0.0) then
        !-----------------------------------------------------------------------
        ! Case 2: Delta = 0
        ! Three real roots with at least two equal (multiple root)
        !-----------------------------------------------------------------------
        
        x1 = 2.0*((-q)/2.0)**(1.0/3.0) - (b/(3.0*a))
        x2_real = -((-q)/2.0)**(1.0/3.0) - (b/(3.0*a))
        x3_real = -((-q)/2.0)**(1.0/3.0) - (b/(3.0*a))
        
        print *, "Delta = 0: three real roots (at least one multiple)"
        print *, "x1 =", x1
        print *, "x2 =", x2_real
        print *, "x3 =", x3_real
        
    else
        !-----------------------------------------------------------------------
        ! Case 3: Delta < 0
        ! Three distinct real roots (irreducible case)
        ! Uses trigonometric method to avoid complex arithmetic
        !-----------------------------------------------------------------------
        
        theta = acos( (3.0*q / (2.0*p)) * sqrt(-3.0/p) )
        
        ! Three roots using trigonometric formulas
        x1 = 2.0*sqrt(-p/3.0) * cos(theta / 3.0) - (b/(3.0*a))
        x2_real = 2.0*sqrt(-p/3.0) * cos((theta + 2.0*PI) / 3.0) - (b/(3.0*a))
        x3_real = 2.0*sqrt(-p/3.0) * cos((theta + 4.0*PI) / 3.0) - (b/(3.0*a))
        
        print *, "Delta < 0: three distinct real roots"
        print *, "x1 =", x1
        print *, "x2 =", x2_real
        print *, "x3 =", x3_real
    end if

end program Cardano
