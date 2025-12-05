!===============================================================================
! Program: es_primo
! Purpose: Test if a given number is prime
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Determines whether an input integer is prime using trial division method.
!   A prime number is divisible only by 1 and itself.
!
! Algorithm:
!   - Numbers 1, 2, 3 are considered prime (base cases)
!   - For n > 3: test divisibility by all integers from 2 to n-1
!   - If any divisor found, number is composite
!   - Otherwise, number is prime
!
! Complexity:
!   Time: O(n) - Could be optimized to O(√n) by testing only up to sqrt(n)
!   Space: O(1)
!
! Compilation:
!   gfortran ejercicio_3_2.f90 -o ejercicio_3_2
!
! Usage:
!   ./ejercicio_3_2
!   Enter an integer when prompted
!===============================================================================

program es_primo
    implicit none
    
    ! Variable declarations
    integer :: a          ! Number to test for primality
    integer :: resultado  ! Result flag: 1 = prime, 0 = composite

    !---------------------------------------------------------------------------
    ! Input: Read number from user
    !---------------------------------------------------------------------------
    print *, "Enter a number: "
    read *, a

    !---------------------------------------------------------------------------
    ! Test primality using subroutine
    !---------------------------------------------------------------------------
    call is_p(a, resultado)

    !---------------------------------------------------------------------------
    ! Output: Display result
    !---------------------------------------------------------------------------
    if (resultado == 1) then
        print *, "true"   ! Number is prime
    else 
        print *, "false"  ! Number is composite
    end if

end program es_primo

!===============================================================================
! Subroutine: is_p
! Purpose: Determine if a number is prime using trial division
!
! Arguments:
!   a         [in]  - Integer to test for primality
!   resultado [out] - Result flag: 1 if prime, 0 if composite
!
! Algorithm:
!   1. Handle base cases (1, 2, 3 are prime)
!   2. Test divisibility by all integers from 2 to a-1
!   3. If divisible by any number, it's composite (resultado = 0)
!   4. If no divisors found, it's prime (resultado = 1)
!
! Note:
!   This is a basic implementation. For large numbers, use optimized
!   algorithms like Miller-Rabin or AKS primality test.
!===============================================================================
subroutine is_p(a, resultado)
    implicit none
    
    ! Argument declarations
    integer, intent(in) :: a
    integer, intent(out) :: resultado
    
    ! Local variables
    integer :: i  ! Loop counter for divisibility testing

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
