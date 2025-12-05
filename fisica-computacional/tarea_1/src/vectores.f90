!===============================================================================
! Program: vectores
! Purpose: Perform fundamental vector operations in R^n
! Author:  Luis Fernando Núñez Rangel
! Date:    2025
!
! Description:
!   Computes various vector operations for two vectors of arbitrary dimension:
!     - Euclidean norm (magnitude) of each vector
!     - Vector addition and subtraction
!     - Dot product and angle between vectors
!     - Cross product (only for 3D vectors)
!
! Compilation:
!   gfortran vectores.f90 -o vectores
!
! Usage:
!   ./vectores
!   Enter dimension N and vector components when prompted
!===============================================================================

program vectores
    implicit none
    
    ! Variable declarations
    integer :: N                    ! Dimension of vectors
    integer :: i                    ! Loop counter
    
    ! Dynamic arrays for vector operations
    real, allocatable :: v1(:)      ! First vector
    real, allocatable :: v2(:)      ! Second vector
    real, allocatable :: suma(:)    ! Sum of vectors
    real, allocatable :: resta(:)   ! Difference of vectors
    real, allocatable :: prod_vec(:) ! Cross product (3D only)
    
    ! Scalar results
    real :: norma_v1, norma_v2      ! Norms of input vectors
    real :: norma_suma, norma_resta ! Norms of sum and difference
    real :: norma_prod_vec          ! Norm of cross product
    real :: prod_punto              ! Dot product
    real :: angulo_rad, angulo_grados ! Angle between vectors
    
    ! Constants
    real, parameter :: PI = 3.14159265
    
    !---------------------------------------------------------------------------
    ! Input: Vector dimension and components
    !---------------------------------------------------------------------------
    print *, "Enter the dimension N of the vectors:"
    read *, N
    
    ! Allocate memory for vectors
    allocate(v1(N), v2(N), suma(N), resta(N), prod_vec(3))
    
    ! Read first vector components
    print *, "Enter the components of vector v1:"
    do i = 1, N
        read *, v1(i)
    end do
    
    ! Read second vector components
    print *, "Enter the components of vector v2:"
    do i = 1, N
        read *, v2(i)
    end do
    
    !---------------------------------------------------------------------------
    ! Compute vector norms (magnitudes)
    !---------------------------------------------------------------------------
    call norma(v1, N, norma_v1)
    call norma(v2, N, norma_v2)
    
    print *, "Norm of v1 =", norma_v1
    print *, "Norm of v2 =", norma_v2
    
    !---------------------------------------------------------------------------
    ! Vector addition and subtraction
    !---------------------------------------------------------------------------
    do i = 1, N
        suma(i) = v1(i) + v2(i)
        resta(i) = v1(i) - v2(i)
    end do
    
    ! Compute norms of resulting vectors
    call norma(suma, N, norma_suma)
    call norma(resta, N, norma_resta)
    
    print *, "Sum vector =", suma
    print *, "Norm of sum =", norma_suma
    
    print *, "Difference vector =", resta
    print *, "Norm of difference =", norma_resta
    
    !---------------------------------------------------------------------------
    ! Dot product and angle calculation
    !---------------------------------------------------------------------------
    prod_punto = 0.0
    do i = 1, N
        prod_punto = prod_punto + v1(i)*v2(i)
    end do
    
    ! Calculate angle using inverse cosine
    ! Formula: cos(theta) = (v1 · v2) / (||v1|| * ||v2||)
    angulo_rad = acos(prod_punto / (norma_v1 * norma_v2))
    angulo_grados = angulo_rad * 180.0 / PI
    
    print *, "Dot product =", prod_punto
    print *, "Angle between vectors (degrees) =", angulo_grados
    
    !---------------------------------------------------------------------------
    ! Cross product (only for 3D vectors)
    !---------------------------------------------------------------------------
    if (N == 3) then
        ! Cross product formula: v1 × v2
        ! i-component: v1_y * v2_z - v1_z * v2_y
        ! j-component: v1_z * v2_x - v1_x * v2_z
        ! k-component: v1_x * v2_y - v1_y * v2_x
        
        prod_vec(1) = v1(2)*v2(3) - v1(3)*v2(2)
        prod_vec(2) = v1(3)*v2(1) - v1(1)*v2(3)
        prod_vec(3) = v1(1)*v2(2) - v1(2)*v2(1)
        
        call norma(prod_vec, 3, norma_prod_vec)
        
        print *, "Cross product =", prod_vec
        print *, "Norm of cross product =", norma_prod_vec
    else
        print *, "Cross product only defined for N=3"
    end if
    
    ! Free allocated memory
    deallocate(v1, v2, suma, resta, prod_vec)

end program vectores

!===============================================================================
! Subroutine: norma
! Purpose: Calculate the Euclidean norm (magnitude) of a vector
!
! Arguments:
!   v(N)      [in]  - Input vector
!   N         [in]  - Dimension of the vector
!   resultado [out] - Computed norm ||v|| = sqrt(v1^2 + v2^2 + ... + vN^2)
!===============================================================================
subroutine norma(v, N, resultado)
    implicit none
    
    ! Argument declarations
    integer, intent(in) :: N
    real, intent(in) :: v(N)
    real, intent(out) :: resultado
    
    ! Local variables
    integer :: i
    
    ! Initialize accumulator
    resultado = 0.0
    
    ! Sum of squares
    do i = 1, N
        resultado = resultado + v(i)**2
    end do
    
    ! Square root to get norm
    resultado = sqrt(resultado)
    
end subroutine norma
