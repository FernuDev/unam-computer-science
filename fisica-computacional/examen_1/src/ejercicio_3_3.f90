!===============================================================================
! Program: primeros_100_primos
! Purpose: Generate and display the first 100 prime numbers
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Generates a sequence of the first 100 prime numbers by testing each
!   integer sequentially starting from 1 until 100 primes are found.
!
! Algorithm:
!   1. Initialize counter i = 1 and prime count = 0
!   2. For each integer i:
!      a. Test if i is prime using is_p subroutine
!      b. If prime, print i and increment count
!      c. If count reaches 100, terminate
!      d. Increment i and repeat
!   3. Uses same primality test as ejercicio_3_2.f90
!
! Output:
!   Prints the first 100 prime numbers, one per line
!
! Performance Note:
!   For large-scale prime generation, sieve methods (Eratosthenes, Atkin)
!   are more efficient than trial division per number.
!
! Compilation:
!   gfortran ejercicio_3_3.f90 -o ejercicio_3_3
!
! Usage:
!   ./ejercicio_3_3
!   (No input required - automatically generates 100 primes)
!===============================================================================

program primeros_100_primos
    implicit none
    
    ! Variable declarations
    integer :: i = 1        ! Current number being tested
    integer :: resultado    ! Primality test result (1=prime, 0=composite)
    integer :: count = 0    ! Count of primes found so far

    !---------------------------------------------------------------------------
    ! Main loop: test integers sequentially until 100 primes found
    !---------------------------------------------------------------------------
    do
        ! Test current number for primality
        call is_p(i, resultado)

        if (resultado == 1) then
            ! Number is prime - print it
            print *, i
            count = count + 1
        end if

        ! Check if we've found 100 primes
        if (count == 100) then
            return  ! Exit program
        end if

        ! Move to next number
        i = i + 1
    end do

end program primeros_100_primos

!===============================================================================
! Subroutine: is_p
! Purpose: Determine if a number is prime using trial division
!
! Arguments:
!   a         [in]  - Integer to test for primality
!   resultado [out] - Result flag: 1 if prime, 0 if composite
!
! Algorithm:
!   Same as in ejercicio_3_2.f90 for consistency
!
! Expected Primes Sequence:
!   2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, ...
!   The 100th prime number is 541
!===============================================================================
subroutine is_p(a, resultado)
    implicit none
    
    ! Argument declarations
    integer, intent(in) :: a
    integer, intent(out) :: resultado
    
    ! Local variables
    integer :: i

    !---------------------------------------------------------------------------
    ! Base case: small numbers (1, 2, 3) are prime
    !---------------------------------------------------------------------------
    if (a >= 1 .AND. a <= 3) then 
        resultado = 1
        return 
    end if

    !---------------------------------------------------------------------------
    ! Trial division: test all potential divisors from 2 to a-1
    !---------------------------------------------------------------------------
    do i = 2, a - 1
        if (mod(a, i) == 0) then
            ! Found a divisor - number is composite
            resultado = 0
            return
        end if 
    end do

    !---------------------------------------------------------------------------
    ! No divisors found - number is prime
    !---------------------------------------------------------------------------
    resultado = 1

end subroutine is_p
