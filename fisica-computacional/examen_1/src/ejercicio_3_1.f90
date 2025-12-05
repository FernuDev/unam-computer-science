!===============================================================================
! Program: modulo
! Purpose: Demonstrate the modulo operation in Fortran
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Computes the modulo (remainder) of two numbers using Fortran's intrinsic
!   mod() function. The modulo operation returns the remainder after division.
!
! Mathematical Definition:
!   mod(a, b) = a - b * floor(a/b)
!
! Applications:
!   - Testing divisibility
!   - Cyclic operations (wrapping indices)
!   - Modular arithmetic
!   - Prime number testing
!
! Compilation:
!   gfortran ejercicio_3_1.f90 -o ejercicio_3_1
!
! Usage:
!   ./ejercicio_3_1
!   Enter two numbers when prompted
!===============================================================================

program modulo
    implicit none
    
    ! Variable declarations
    real :: a, b    ! Input numbers for modulo operation

    !---------------------------------------------------------------------------
    ! Input: Read two numbers from user
    !---------------------------------------------------------------------------
    print *, "Enter two numbers to determine their modulo: "
    read *, a
    read *, b

    !---------------------------------------------------------------------------
    ! Compute and display modulo result
    ! mod(a, b) returns the remainder when a is divided by b
    !---------------------------------------------------------------------------
    print *, "Result: ", mod(a, b)

end program modulo
