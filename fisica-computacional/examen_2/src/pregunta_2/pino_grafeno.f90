!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
! Programa para calcular energías y funciones de onda del fragmento de grafeno
! Modelo tight-binding para electrones π
! Fragmento "pino de navidad": estructura de hexágonos fusionados
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      PROGRAM Pino_Grafeno
      IMPLICIT NONE
      
      ! Parámetros
      REAL(8), PARAMETER :: EPS = 0.0d0    ! Energía de sitio
      REAL(8), PARAMETER :: PI_PARAM = -1.0d0  ! Salto entre vecinos
      
      ! Variables
      INTEGER, PARAMETER :: N = 24  ! Total de átomos de carbono en la estructura
      REAL(8) :: H(N,N)            ! Matriz Hamiltoniana
      REAL(8) :: W(N)              ! Autovalores (energías)
      REAL(8) :: WORK(3*N)         ! Array de trabajo para LAPACK
      INTEGER :: INFO, LDA, LWORK
      INTEGER :: i, j
      
      write(*,*) '============================================='
      write(*,*) 'PINO DE GRAFENO - Modelo Tight-Binding'
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
      
      ! Estructura del pino de grafeno (forma de árbol de navidad)
      ! Numeración de átomos de arriba hacia abajo:
      !
      !        1---2
      !       /     \
      !      3       4
      !     / \     / \
      !    5   6---7   8
      !   / \ / \ / \ / \
      !  9 10-11-12-13-14 15
      !     |  \| /|  |
      !    16 17-18-19 20
      !     |  / | \  |
      !    21 22-23-24
      
      ! Fila superior (1 hexágono)
      H(1,2) = PI_PARAM; H(2,1) = PI_PARAM
      H(1,3) = PI_PARAM; H(3,1) = PI_PARAM
      H(2,4) = PI_PARAM; H(4,2) = PI_PARAM
      
      ! Segunda fila
      H(3,5) = PI_PARAM; H(5,3) = PI_PARAM
      H(3,6) = PI_PARAM; H(6,3) = PI_PARAM
      H(4,7) = PI_PARAM; H(7,4) = PI_PARAM
      H(4,8) = PI_PARAM; H(8,4) = PI_PARAM
      H(6,7) = PI_PARAM; H(7,6) = PI_PARAM
      
      ! Tercera fila (más ancha)
      H(5,9) = PI_PARAM; H(9,5) = PI_PARAM
      H(5,10) = PI_PARAM; H(10,5) = PI_PARAM
      H(6,10) = PI_PARAM; H(10,6) = PI_PARAM
      H(6,11) = PI_PARAM; H(11,6) = PI_PARAM
      H(7,12) = PI_PARAM; H(12,7) = PI_PARAM
      H(7,13) = PI_PARAM; H(13,7) = PI_PARAM
      H(8,13) = PI_PARAM; H(13,8) = PI_PARAM
      H(8,14) = PI_PARAM; H(14,8) = PI_PARAM
      H(8,15) = PI_PARAM; H(15,8) = PI_PARAM
      
      H(10,11) = PI_PARAM; H(11,10) = PI_PARAM
      H(11,12) = PI_PARAM; H(12,11) = PI_PARAM
      H(12,13) = PI_PARAM; H(13,12) = PI_PARAM
      H(13,14) = PI_PARAM; H(14,13) = PI_PARAM
      
      ! Cuarta fila
      H(10,16) = PI_PARAM; H(16,10) = PI_PARAM
      H(11,17) = PI_PARAM; H(17,11) = PI_PARAM
      H(12,18) = PI_PARAM; H(18,12) = PI_PARAM
      H(13,19) = PI_PARAM; H(19,13) = PI_PARAM
      H(14,20) = PI_PARAM; H(20,14) = PI_PARAM
      
      H(16,17) = PI_PARAM; H(17,16) = PI_PARAM
      H(17,18) = PI_PARAM; H(18,17) = PI_PARAM
      H(18,19) = PI_PARAM; H(19,18) = PI_PARAM
      H(19,20) = PI_PARAM; H(20,19) = PI_PARAM
      
      ! Quinta fila (base del pino)
      H(16,21) = PI_PARAM; H(21,16) = PI_PARAM
      H(17,22) = PI_PARAM; H(22,17) = PI_PARAM
      H(18,23) = PI_PARAM; H(23,18) = PI_PARAM
      H(19,24) = PI_PARAM; H(24,19) = PI_PARAM
      
      H(22,23) = PI_PARAM; H(23,22) = PI_PARAM
      H(23,24) = PI_PARAM; H(24,23) = PI_PARAM
      
      write(*,*) 'Matriz Hamiltoniana construida'
      write(*,*) 'Estructura: Fragmento de grafeno (pino de navidad)'
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
      write(*,*) 'ENERGÍAS (Autovalores) - Ordenadas'
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
      
      ! Imprimir solo los primeros estados (más relevantes)
      DO i = 1, MIN(6, N)
         write(*,'(A,I2,A)') 'Estado ', i, ':'
         write(*,'(A,F12.6)') '  Energía: ', W(i)
         write(*,'(A)') '  Coeficientes c_i (primeros 12 átomos):'
         DO j = 1, MIN(12, N)
            write(*,'(A,I2,A,F10.6,A,F10.6)') '    c(', j, ') = ', &
                   H(j,i), '  |c|² = ', H(j,i)**2
         END DO
         write(*,*) ''
      END DO
      
      ! Guardar resultados en archivo
      OPEN(10, FILE='pino_grafeno_energias.dat')
      write(10,*) '# Energías del fragmento de grafeno (pino)'
      write(10,*) '# Estado  Energía'
      DO i = 1, N
         write(10,'(I4,2X,F12.6)') i, W(i)
      END DO
      CLOSE(10)
      
      OPEN(11, FILE='pino_grafeno_wavefunctions.dat')
      write(11,*) '# Funciones de onda del pino de grafeno'
      write(11,*) '# Fila: átomo, Columna: estado'
      DO j = 1, N
         write(11,'(24F12.6)') (H(j,i), i=1,N)
      END DO
      CLOSE(11)
      
      write(*,*) '============================================='
      write(*,*) 'ANÁLISIS DEL ESTADO BASE Y PRIMER EXCITADO'
      write(*,*) '============================================='
      write(*,*) ''
      
      ! Estado base (energía más baja)
      write(*,*) 'ESTADO BASE:'
      write(*,'(A,F12.6)') '  Energía: E₀ = ', W(1)
      write(*,*) '  Densidad de probabilidad |c_i|² (top 10):'
      DO j = 1, MIN(10, N)
         write(*,'(A,I2,A,F10.6)') '    Átomo ', j, ': ', H(j,1)**2
      END DO
      write(*,*) ''
      
      ! Primer estado excitado
      write(*,*) 'PRIMER ESTADO EXCITADO:'
      write(*,'(A,F12.6)') '  Energía: E₁ = ', W(2)
      write(*,*) '  Densidad de probabilidad |c_i|² (top 10):'
      DO j = 1, MIN(10, N)
         write(*,'(A,I2,A,F10.6)') '    Átomo ', j, ': ', H(j,2)**2
      END DO
      
      ! Información adicional
      write(*,*) ''
      write(*,*) '============================================='
      write(*,*) 'INFORMACIÓN ADICIONAL'
      write(*,*) '============================================='
      write(*,'(A,F12.6)') 'Gap de energía (HOMO-LUMO): ', W(N/2+1) - W(N/2)
      write(*,'(A,F12.6)') 'Ancho de banda total: ', W(N) - W(1)
      
      write(*,*) ''
      write(*,*) 'Resultados guardados en:'
      write(*,*) '  - pino_grafeno_energias.dat'
      write(*,*) '  - pino_grafeno_wavefunctions.dat'
      
      END PROGRAM Pino_Grafeno
