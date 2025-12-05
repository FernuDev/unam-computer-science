!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
! Fórmula de Kubo-Greenwood para la conductividad eléctrica a T = 0K
! del dimetilbenceno. Unidades atómicas: e=hbar=m=a=1
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      PROGRAM KUBO_BENCENO
      IMPLICIT NONE
      REAL(8) PI,EPS !Energia de saltos y energia de sitios
      REAL(8) EF,EFmax,EFmin !Energia de Fermi EF [EFmin,EFmax]
      REAL(8) TRAZA,Conductividad! Funcion Tr{(Px*ImG)^2}
      REAL(8) ETA! Parte imaginaria de la energia Z =E + i*ETA
      REAL(8) NPI! Numero pi
      REAL(8) H,P! Matriz Hamiltoniana, Proyeccion de momento lineal
      INTEGER N,NG! Tamaño de la matriz H(NxN) y electrodos
      INTEGER Omega! volmen del sistema
      INTEGER nEF,i,j,k!
!
      ALLOCATABLE :: H(:,:),P(:,:)
      
      write(*,*) '============================================='
      write(*,*) 'KUBO-GREENWOOD: DIMETILBENCENO'
      write(*,*) '============================================='
      
      N=8 !Tamaño de la matriz H(NxN) - 8 átomos
      NG=20! Generacion de creciomiento de los electrodos
      ALLOCATE(H(N,N),P(N,N))
!
      EPS=0.0d0 ! Energia de sitio de los carbonos
      PI=-1.0d0 ! Energia de salto entre carbonos
!
! Construcción del Hamiltoniano del dimetilbenceno
      H=0.0d0
      DO i=1,N
       H(i,i)=EPS
      END DO
      
      ! Anillo de benceno (átomos 1-6)
      H(1,2) = PI; H(2,1) = PI
      H(2,3) = PI; H(3,2) = PI
      H(3,4) = PI; H(4,3) = PI
      H(4,5) = PI; H(5,4) = PI
      H(5,6) = PI; H(6,5) = PI
      H(6,1) = PI; H(1,6) = PI
      
      ! Grupos metilo
      H(1,7) = PI; H(7,1) = PI  ! Metilo en posición 1
      H(4,8) = PI; H(8,4) = PI  ! Metilo en posición 4
!
! Matriz de momento lineal (proyección)
! Conectamos electrodos en los extremos horizontales (metilos 7 y 8)
      P=0.0d0
      ! Conexiones del anillo
      P(1,2)=-PI; P(2,1)=PI
      P(2,3)=-PI; P(3,2)=PI
      P(3,4)=-PI; P(4,3)=PI
      P(4,5)=-PI; P(5,4)=PI
      P(5,6)=-PI; P(6,5)=PI
      P(6,1)=-PI; P(1,6)=PI
      
      ! Conexiones de metilos (extremos horizontales)
      P(1,7)=-PI; P(7,1)=PI
      P(4,8)=-PI; P(8,4)=PI
!
!
      OPEN(15,FILE='kubo_benceno.dat')!Archivo de salida
      write(15,*) '# Conductividad del dimetilbenceno'
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
      write(*,*) 'Resultados guardados en: kubo_benceno.dat'
      write(*,*) 'Total de líneas:', nEF+1
      
      END PROGRAM KUBO_BENCENO
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
!!!!!!!!!!Conexion con electrodos en los extremos (átomos 7 y 8)
      ZH(7,7)=ZH(7,7)-EE  ! Electrodo izquierdo en metilo 7
      ZH(8,8)=ZH(8,8)-EE  ! Electrodo derecho en metilo 8
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

