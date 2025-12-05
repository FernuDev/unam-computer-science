!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
! Fórmula de Kubo-Greenwood para la conductividad eléctrica a T = 0K
! del pino de grafeno. Unidades atómicas: e=hbar=m=a=1
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      PROGRAM KUBO_PINO
      IMPLICIT NONE
      REAL(8) PI,EPS !Energia de saltos y energia de sitios
      REAL(8) EF,EFmax,EFmin !Energia de Fermi EF [EFmin,EFmax]
      REAL(8) TRAZA,Conductividad! Funcion Tr{(Px*ImG)^2}
      REAL(8) ETA! Parte imaginaria de la energia Z =E + i*ETA
      REAL(8) NPI! Numero pi
      REAL(8), ALLOCATABLE :: H(:,:),P(:,:)
      INTEGER N,NG! Tamaño de la matriz H(NxN) y electrodos
      INTEGER Omega! volmen del sistema
      INTEGER nEF,i,j,k!
      
      write(*,*) '============================================='
      write(*,*) 'KUBO-GREENWOOD: PINO DE GRAFENO'
      write(*,*) '============================================='
      
      N=24 !Tamaño de la matriz H(NxN) - 24 átomos
      NG=20! Generacion de creciomiento de los electrodos
      ALLOCATE(H(N,N),P(N,N))
!
      EPS=0.0d0 ! Energia de sitio de los carbonos
      PI=-1.0d0 ! Energia de salto entre carbonos
!
! Construcción del Hamiltoniano del pino de grafeno
      H=0.0d0
      DO i=1,N
       H(i,i)=EPS
      END DO
      
      ! Estructura del pino de grafeno
      ! Fila superior
      H(1,2) = PI; H(2,1) = PI
      H(1,3) = PI; H(3,1) = PI
      H(2,4) = PI; H(4,2) = PI
      
      ! Segunda fila
      H(3,5) = PI; H(5,3) = PI
      H(3,6) = PI; H(6,3) = PI
      H(4,7) = PI; H(7,4) = PI
      H(4,8) = PI; H(8,4) = PI
      H(6,7) = PI; H(7,6) = PI
      
      ! Tercera fila (más ancha)
      H(5,9) = PI; H(9,5) = PI
      H(5,10) = PI; H(10,5) = PI
      H(6,10) = PI; H(10,6) = PI
      H(6,11) = PI; H(11,6) = PI
      H(7,12) = PI; H(12,7) = PI
      H(7,13) = PI; H(13,7) = PI
      H(8,13) = PI; H(13,8) = PI
      H(8,14) = PI; H(14,8) = PI
      H(8,15) = PI; H(15,8) = PI
      
      H(10,11) = PI; H(11,10) = PI
      H(11,12) = PI; H(12,11) = PI
      H(12,13) = PI; H(13,12) = PI
      H(13,14) = PI; H(14,13) = PI
      
      ! Cuarta fila
      H(10,16) = PI; H(16,10) = PI
      H(11,17) = PI; H(17,11) = PI
      H(12,18) = PI; H(18,12) = PI
      H(13,19) = PI; H(19,13) = PI
      H(14,20) = PI; H(20,14) = PI
      
      H(16,17) = PI; H(17,16) = PI
      H(17,18) = PI; H(18,17) = PI
      H(18,19) = PI; H(19,18) = PI
      H(19,20) = PI; H(20,19) = PI
      
      ! Quinta fila (base)
      H(16,21) = PI; H(21,16) = PI
      H(17,22) = PI; H(22,17) = PI
      H(18,23) = PI; H(23,18) = PI
      H(19,24) = PI; H(24,19) = PI
      
      H(22,23) = PI; H(23,22) = PI
      H(23,24) = PI; H(24,23) = PI
!
! Matriz de momento lineal (proyección)
! Conectamos electrodos en los extremos horizontales (átomos 9 y 15)
      P=0.0d0
      
      ! Conexiones según la estructura
      ! Fila superior
      P(1,2)=-PI; P(2,1)=PI
      P(1,3)=-PI; P(3,1)=PI
      P(2,4)=-PI; P(4,2)=PI
      
      ! Segunda fila
      P(3,5)=-PI; P(5,3)=PI
      P(3,6)=-PI; P(6,3)=PI
      P(4,7)=-PI; P(7,4)=PI
      P(4,8)=-PI; P(8,4)=PI
      P(6,7)=-PI; P(7,6)=PI
      
      ! Tercera fila
      P(5,9)=-PI; P(9,5)=PI
      P(5,10)=-PI; P(10,5)=PI
      P(6,10)=-PI; P(10,6)=PI
      P(6,11)=-PI; P(11,6)=PI
      P(7,12)=-PI; P(12,7)=PI
      P(7,13)=-PI; P(13,7)=PI
      P(8,13)=-PI; P(13,8)=PI
      P(8,14)=-PI; P(14,8)=PI
      P(8,15)=-PI; P(15,8)=PI
      
      P(10,11)=-PI; P(11,10)=PI
      P(11,12)=-PI; P(12,11)=PI
      P(12,13)=-PI; P(13,12)=PI
      P(13,14)=-PI; P(14,13)=PI
      
      ! Cuarta fila
      P(10,16)=-PI; P(16,10)=PI
      P(11,17)=-PI; P(17,11)=PI
      P(12,18)=-PI; P(18,12)=PI
      P(13,19)=-PI; P(19,13)=PI
      P(14,20)=-PI; P(20,14)=PI
      
      P(16,17)=-PI; P(17,16)=PI
      P(17,18)=-PI; P(18,17)=PI
      P(18,19)=-PI; P(19,18)=PI
      P(19,20)=-PI; P(20,19)=PI
      
      ! Quinta fila
      P(16,21)=-PI; P(21,16)=PI
      P(17,22)=-PI; P(22,17)=PI
      P(18,23)=-PI; P(23,18)=PI
      P(19,24)=-PI; P(24,19)=PI
      
      P(22,23)=-PI; P(23,22)=PI
      P(23,24)=-PI; P(24,23)=PI
!
!
      OPEN(15,FILE='kubo_pino.dat')!Archivo de salida
      write(15,*) '# Conductividad del pino de grafeno'
      write(15,*) '# Col 1: EF, Col 2: sigma(EF)'
!
      Efmin=-2.5d0 !Energia de Fermi minima
      Efmax=2.5d0  !Energia de Fermi maxima 
      nEf=500      !Numero de diviciones (501 puntos)
      NPI=4.0d0*ATAN(1.0d0) !Numero pi
!      
      Omega=N! Volumen del sistema, cada atomo aporta una unidad de volumen
      ETA=1.0d-4/N! Parte imaginaria de la energia Z=E+i*eta
!
      write(*,*) 'Calculando conductividad...'
      write(*,*) 'Rango de EF: [', Efmin, ',', Efmax, ']'
      write(*,*) 'Número de puntos:', nEF+1
      write(*,*) ''
!
!!!!!!!!!!!!!!!!!!!!! TRAZA vs E0 o MU  !!!!!!!!!!!!!!!
      DO i=0,nEF
       EF=EFmin+i*(EFmax-EFmin)/nEF! Energia de Fermi
       Conductividad= 2.0d0*TRAZA(EF,ETA,PI,EPS,H,P,N,NG)/(NPI*Omega)!Calculo de la conductividad 
       write(15,*)EF,Conductividad! Impresion de resultados
       if (MOD(i,50) == 0) then
        write(*,'(A,I4,A,F8.3,A,E12.4)') '  Punto ', i, ': EF = ', EF, &
               ', sigma = ', Conductividad
       end if
      END DO
      close(15)
      
      write(*,*) ''
      write(*,*) '¡Cálculo completado!'
      write(*,*) 'Resultados guardados en: kubo_pino.dat'
      write(*,*) 'Total de líneas:', nEF+1
      
      END PROGRAM KUBO_PINO
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      FUNCTION TRAZA(EF,ETA,PI,EPS,H,P,N,NG)
      IMPLICIT NONE
      REAL(8) EF,ETA,PI,EPS,H(N,N),P(N,N),IMG(N,N)
      REAL(8) TRAZA
      REAL(8) T0,E0
      REAL(8) PG(N,N),PGPG(N,N)
      COMPLEX(8) Z,EE
      COMPLEX(8) ZH(N,N),G(N,N)
      COMPLEX(8) TP(NG),EM(NG),EP(NG)
      INTEGER N,NG
      INTEGER I,J,K
!
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      Z=DCMPLX(EF,ETA) !Energia Compleja
!
      T0=-1.0d0 !Enegia salto electrodos
      E0=0.0d0 !Energia de sitio electrodos
!!!!!!!!!!!!RENORMALIZACION DE ELECTRODOS!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      EE=DCMPLX(0.0,0.0)
      TP(1)=DCMPLX(T0,0.0d0)
      EP(1)=DCMPLX(E0,0.0d0)
      DO J=2,NG
       EM(J)=2.0d0*EP(J-1)
       TP(J)=TP(J-1)*TP(J-1)/(Z-EM(J)) 
       EP(J)=EP(J-1)+TP(J-1)*TP(J-1)/(Z-EM(J))
      END DO
      EP(NG)=EP(NG)+TP(NG)*TP(NG)/(Z-EP(NG))
      EE=EE+T0*T0/(Z-EP(NG))
!!!!!!!!!!!!!! MATRIZ  ZI-H  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      ZH=DCMPLX(0.0,0.0d0)
      ZH=-H
      DO I=1,N
       ZH(I,I)=Z-H(I,I)
      END DO
!!!!!!!!!!Conexion con electrodos en los extremos horizontales
! Átomo 9 (extremo izquierdo) y átomo 15 (extremo derecho)
      ZH(9,9)=ZH(9,9)-EE    ! Electrodo izquierdo
      ZH(15,15)=ZH(15,15)-EE  ! Electrodo derecho
!!!!!!!!! Obtencion Matriz G 
      CALL INVERS(N,ZH,G)
      IMG=aimag(G)
!!!!!!Calculo de P*IMG(z)     
      CALL MUL(N,P,IMG,PG)
!!!!!!Calculo de P*IMG(z)P*ImG(z)
      CALL MUL(N,PG,PG,PGPG)
!!!!!!Calculo de la Tr{(PIMG(z))^2}
      TRAZA=0.0d0
      DO I=1,N
      TRAZA=TRAZA+PGPG(I,I)
      END DO
      TRAZA=-TRAZA
      RETURN
      END
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      subroutine MUL(N,A,B,C)
      IMPLICIT NONE
      REAL(8) A(N,N),B(N,N),C(N,N)
      Integer N,i,j,k
!
      DO i=1,N
      DO j=1,N
      C(i,j)=0.0
      DO k=1,N
      C(i,j)= C(i,j)+A(i,k)*B(k,j)
      END DO
      END DO
      END DO
      RETURN
      END
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      SUBROUTINE INVERS(N, A, B)
      IMPLICIT NONE
      INTEGER, INTENT(IN) :: N
      COMPLEX(8), INTENT(IN) :: A(N,N)
      COMPLEX(8), INTENT(OUT) :: B(N,N)

      COMPLEX(8) :: AUG(N,2*N)
      COMPLEX(8) :: FACTOR, TEMP
      INTEGER :: I, J, K, PIV
      REAL(8) :: MAXVAL

  ! Construir matriz aumentada [A | I]
      AUG = (0.0D0, 0.0D0)
      AUG(:,1:N) = A
      DO I = 1, N
       AUG(I,N+I) = (1.0D0,0.0D0)
      END DO

      ! Eliminación Gauss-Jordan
      DO I = 1, N

      ! Pivoteo parcial
      MAXVAL = 0.0D0
      PIV = I
      DO K = I, N
        IF (ABS(AUG(K,I)) > MAXVAL) THEN
           MAXVAL = ABS(AUG(K,I))
           PIV = K
        END IF
       END DO

     ! Si el pivote es cero --> matriz singular
       IF (MAXVAL == 0.0D0) THEN
          PRINT *, "ERROR: La matriz es singular, no tiene inversa."
          STOP
       END IF

     ! Intercambio de filas
      IF (PIV /= I) THEN
        DO J = 1, 2*N
           TEMP = AUG(I,J)
           AUG(I,J) = AUG(PIV,J)
           AUG(PIV,J) = TEMP
        END DO
      END IF

     ! Normalizar fila del pivote
       FACTOR = AUG(I,I)
       DO J = 1, 2*N
        AUG(I,J) = AUG(I,J) / FACTOR
      END DO

     ! Eliminar otros elementos en la columna
      DO K = 1, N
        IF (K /= I) THEN
           FACTOR = AUG(K,I)
           DO J = 1, 2*N
              AUG(K,J) = AUG(K,J) - FACTOR*AUG(I,J)
           END DO
        END IF
      END DO

      END DO

      ! Extraer la inversa
      B = AUG(:,N+1:2*N)

      END SUBROUTINE INVERS
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

