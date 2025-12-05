!===============================================================================
! Program: determinante
! Purpose: Calculate the determinant of a 3x3 matrix using Sarrus' rule
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Computes the determinant of a 3x3 matrix using the Sarrus rule method.
!   This is a direct calculation method specific to 3x3 matrices that involves
!   summing products along diagonals (positive and negative).
!
! Mathematical Method:
!   For a 3x3 matrix:
!   | a11  a12  a13 |
!   | a21  a22  a23 |
!   | a31  a32  a33 |
!
!   det = (a11*a22*a33 + a12*a23*a31 + a13*a21*a32) - 
!         (a13*a22*a31 + a23*a32*a11 + a33*a12*a21)
!
! Compilation:
!   gfortran ejercicio_1_1.f90 -o ejercicio_1
!
! Usage:
!   ./ejercicio_1
!   Enter matrix elements row by row when prompted
!===============================================================================

program determinante
    implicit none
    
    ! Variable declarations
    integer :: i                    ! Loop counter
    real :: pos, neg                ! Positive and negative diagonal sums
    real, allocatable :: v1(:)      ! First row of matrix
    real, allocatable :: v2(:)      ! Second row of matrix
    real, allocatable :: v3(:)      ! Third row of matrix
    
    ! Allocate memory for 3x3 matrix rows
    allocate(v1(3), v2(3), v3(3))

    !---------------------------------------------------------------------------
    ! Input: Read matrix elements row by row
    !---------------------------------------------------------------------------
    print *, "Enter the values of the first row: "
    do i = 1, 3
        read *, v1(i)
    end do

    print *, "Enter the values of the second row: "
    do i = 1, 3
        read *, v2(i)
    end do

    print *, "Enter the values of the third row: "
    do i = 1, 3
        read *, v3(i)
    end do

    !---------------------------------------------------------------------------
    ! Calculate determinant using Sarrus' rule
    !---------------------------------------------------------------------------
    ! Positive diagonal products (main diagonal and wrapping diagonals)
    pos = v1(1)*v2(2)*v3(3) + v2(1)*v3(2)*v1(3) + v3(1)*v1(2)*v2(3)
    
    ! Negative diagonal products (anti-diagonal and wrapping)
    neg = v1(3)*v2(2)*v3(1) + v2(3)*v3(2)*v1(1) + v3(3)*v1(2)*v2(1)

    !---------------------------------------------------------------------------
    ! Output: Display calculated determinant
    !---------------------------------------------------------------------------
    print *, "The value of the determinant is: "
    print *, pos - neg

end program determinante
