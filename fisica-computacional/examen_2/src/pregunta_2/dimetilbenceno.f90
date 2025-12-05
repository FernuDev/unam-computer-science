!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
! Programa para calcular energías y funciones de onda del dimetilbenceno
! Modelo tight-binding (Hückel) para electrones π
! Dimetilbenceno: anillo de benceno (6 C) + 2 grupos metilo (2 C)
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      PROGRAM Dimetilbenceno
      IMPLICIT NONE
      
      ! Parámetros
      REAL(8), PARAMETER :: EPS = 0.0d0    ! Energía de sitio
      REAL(8), PARAMETER :: PI_PARAM = -1.0d0  ! Salto entre vecinos
      
      ! Variables
      INTEGER, PARAMETER :: N = 8  ! Total de átomos de carbono
      REAL(8) :: H(N,N)            ! Matriz Hamiltoniana
      REAL(8) :: W(N)              ! Autovalores (energías)
      REAL(8) :: WORK(3*N)         ! Array de trabajo para LAPACK
      INTEGER :: INFO, LDA, LWORK
      INTEGER :: i, j
      
      write(*,*) '============================================='
      write(*,*) 'DIMETILBENCENO - Modelo Tight-Binding'
      write(*,*) '============================================='
      write(*,*) ''
      write(*,*) 'Número de átomos: ', N
      write(*,*) 'Energía de sitio (ε): ', EPS
      write(*,*) 'Salto entre vecinos (π): ', PI_PARAM
      write(*,*) ''
      
      ! Inicializar Hamiltoniano
      H = 0.0d0
      
      ! Energía de sitio (diagonal)
      DO i = 1, N
         H(i,i) = EPS
      END DO
      
      ! Estructura del dimetilbenceno:
      ! Átomos 1-6: anillo de benceno
      ! Átomo 7: metilo unido al átomo 1
      ! Átomo 8: metilo unido al átomo 4 (posición para)
      
      ! Conexiones del anillo de benceno (hexágono)
      H(1,2) = PI_PARAM
      H(2,1) = PI_PARAM
      
      H(2,3) = PI_PARAM
      H(3,2) = PI_PARAM
      
      H(3,4) = PI_PARAM
      H(4,3) = PI_PARAM
      
      H(4,5) = PI_PARAM
      H(5,4) = PI_PARAM
      
      H(5,6) = PI_PARAM
      H(6,5) = PI_PARAM
      
      H(6,1) = PI_PARAM
      H(1,6) = PI_PARAM
      
      ! Conexiones de los grupos metilo
      H(1,7) = PI_PARAM  ! Metilo en posición 1
      H(7,1) = PI_PARAM
      
      H(4,8) = PI_PARAM  ! Metilo en posición 4 (para)
      H(8,4) = PI_PARAM
      
      write(*,*) 'Matriz Hamiltoniana construida'
      write(*,*) ''
      
      ! Diagonalizar usando LAPACK DSYEV
      LDA = N
      LWORK = 3*N
      
      CALL DSYEV('V', 'U', N, H, LDA, W, WORK, LWORK, INFO)
      
      IF (INFO /= 0) THEN
         write(*,*) 'ERROR en la diagonalización: INFO = ', INFO
         STOP
      END IF
      
      write(*,*) '============================================='
      write(*,*) 'ENERGÍAS (Autovalores)'
      write(*,*) '============================================='
      write(*,*) ''
      
      DO i = 1, N
         write(*,'(A,I2,A,F12.6)') 'E(', i, ') = ', W(i)
      END DO
      
      write(*,*) ''
      write(*,*) '============================================='
      write(*,*) 'FUNCIONES DE ONDA (Autovectores)'
      write(*,*) '============================================='
      write(*,*) ''
      
      ! Imprimir autovectores (funciones de onda)
      DO i = 1, N
         write(*,'(A,I2,A)') 'Estado ', i, ':'
         write(*,'(A,F12.6)') '  Energía: ', W(i)
         write(*,'(A)') '  Coeficientes c_i:'
         DO j = 1, N
            write(*,'(A,I2,A,F10.6)') '    c(', j, ') = ', H(j,i)
         END DO
         write(*,*) ''
      END DO
      
      ! Guardar resultados en archivo
      OPEN(10, FILE='dimetilbenceno_energias.dat')
      write(10,*) '# Energías del dimetilbenceno'
      write(10,*) '# Estado  Energía'
      DO i = 1, N
         write(10,'(I4,2X,F12.6)') i, W(i)
      END DO
      CLOSE(10)
      
      OPEN(11, FILE='dimetilbenceno_wavefunctions.dat')
      write(11,*) '# Funciones de onda del dimetilbenceno'
      write(11,*) '# Fila: átomo, Columna: estado'
      DO j = 1, N
         write(11,'(8F12.6)') (H(j,i), i=1,N)
      END DO
      CLOSE(11)
      
      write(*,*) '============================================='
      write(*,*) 'ANÁLISIS DEL ESTADO BASE Y PRIMER EXCITADO'
      write(*,*) '============================================='
      write(*,*) ''
      
      ! Estado base (energía más baja)
      write(*,*) 'ESTADO BASE:'
      write(*,'(A,F12.6)') '  Energía: E₀ = ', W(1)
      write(*,*) '  Densidad de probabilidad |c_i|²:'
      DO j = 1, N
         write(*,'(A,I2,A,F10.6)') '    Átomo ', j, ': ', H(j,1)**2
      END DO
      write(*,*) ''
      
      ! Primer estado excitado
      write(*,*) 'PRIMER ESTADO EXCITADO:'
      write(*,'(A,F12.6)') '  Energía: E₁ = ', W(2)
      write(*,*) '  Densidad de probabilidad |c_i|²:'
      DO j = 1, N
         write(*,'(A,I2,A,F10.6)') '    Átomo ', j, ': ', H(j,2)**2
      END DO
      
      write(*,*) ''
      write(*,*) 'Resultados guardados en:'
      write(*,*) '  - dimetilbenceno_energias.dat'
      write(*,*) '  - dimetilbenceno_wavefunctions.dat'
      
      END PROGRAM Dimetilbenceno
