      Program Pozo_Doble
!     Este programa resuelve numéricamente el pozo doble
!     usando el método de Numerov para encontrar la función de onda Psi y la energía E
!     Potencial: V(x) = infinito para |x|>1, V(x)=1 para |x|<=a, V(x)=0 para a<|x|<=1
      IMPLICIT NONE
      REAL(8) psi,psim1,psip1 ! función de onda en x, x-h, x+h
      REAL(8) psiold,psinew ! parámetros para converger en psi(1)
      REAL(8) eps ! parámetro de convergencia
      REAL(8) energy,de ! energía y su incremento
      REAL(8) x,dx ! posición y su incremento
      REAL(8), ALLOCATABLE :: xstep(:),psifinal(:)
      REAL(8) k0,km1,kp1 ! k(x) = 2(E-V(x))
      REAL(8) h2,denom ! h² y denominador de Numerov
      REAL(8) norm,norma ! para normalización
      REAL(8) a ! parámetro del pozo doble
      Integer paridad ! 1 si psi es par y -1 si psi es impar
      Integer N ! Número de evaluaciones entre (0,1]
      Integer icont,i ! Contador de ciclos e índices
      REAL(8) V ! función potencial
      
      COMMON /params/ a

      write(*,*)'===== POZO DOBLE ====='
      write(*,*)'1.- Proporciona el parámetro a (barrera central)'
      read(*,*)a
      write(*,*)'Parámetro a =',a
      
      write(*,*)'2.- Proporciona un valor de particiones N'
      read(*,*)N
      write(*,*)'El intervalo (0,1] tendrá',N,'particiones.'
      ALLOCATE (xstep(-N:N),psifinal(-N:N))

      write(*,*)'3.- Proporciona un valor inicial de la energía'
      read(*,*)energy
      write(*,*)'La energía inicial es',energy

      write(*,*)'4.- Escribe la paridad de la función, 1 par, -1 impar'
      read(*,*)paridad
      write(*,*)'La paridad seleccionada fue:',paridad

!     Parámetros para converger      
      dx=1.0d0/N
      h2=dx*dx
      eps=1.0d-6 ! criterio de convergencia
      de=ABS(energy)/10.0d0
      
      icont=0
      psiold=1.0d0
      psinew=1.0d0

!     Inicia ciclo para converger psi(1)
      write(*,*)'contador     energy              de              psi(1)'
      DO WHILE (.true.)
       ! Condiciones iniciales según paridad
       IF(paridad.eq.1) then
        psi=1.0d0      ! psi(0)
        psim1=1.0d0    ! psi(-dx) aproximado para paridad par
       ELSE
        psi=0.0d0      ! psi(0)
        psim1=0.0d0    ! psi(-dx) aproximado para paridad impar
       END IF
       
       ! Primer paso: aproximación para psi(dx)
       x=0.0d0
       k0=2.0d0*(energy-V(x))
       IF(paridad.eq.1) then
        psip1=psi*(1.0d0-h2*k0/6.0d0) ! aproximación de Taylor para par
       ELSE
        psip1=dx*(1.0d0-h2*k0/6.0d0) ! aproximación de Taylor para impar
       END IF
       
       psim1=psi
       psi=psip1
       
!      Método de Numerov para el resto de puntos
       DO i=2,N
        x=i*dx
        kp1=2.0d0*(energy-V(x))
        k0=2.0d0*(energy-V(x-dx))
        km1=2.0d0*(energy-V(x-2.0d0*dx))
        
        denom=1.0d0+h2*kp1/12.0d0
        psip1=(2.0d0*psi*(1.0d0-5.0d0*h2*k0/12.0d0) - &
               psim1*(1.0d0+h2*km1/12.0d0))/denom
        
        psim1=psi
        psi=psip1
       END DO
       
       psinew=psi
       write(*,'(I8,3E20.10)')icont,energy,de,psi
       
       IF(DABS(psi).le.eps) EXIT
       IF(psinew*psiold.lt.0.0d0) de=-0.5d0*de
       energy=energy+de
       psiold=psinew
       icont=icont+1
       
       IF(icont.gt.10000) then
        write(*,*)'No se logró convergencia después de 10000 iteraciones'
        STOP
       END IF
      END DO

      write(*,*)'¡Convergencia lograda!'
      write(*,'(A,F15.10)')'Energía del estado base: E = ',energy
      write(*,'(A,I6,A)')'Convergencia en ',icont,' iteraciones'

!     Recalcular la función de onda con la energía convergida
      OPEN(2,file='psi_doble.dat')
      
      IF(paridad.eq.1) then
       psi=1.0d0
       psim1=1.0d0
       xstep(0)=0.0d0
       psifinal(0)=1.0d0
      ELSE
       psi=0.0d0
       psim1=0.0d0
       xstep(0)=0.0d0
       psifinal(0)=0.0d0
      END IF
      
      ! Primer paso
      x=0.0d0
      k0=2.0d0*(energy-V(x))
      IF(paridad.eq.1) then
       psip1=psi*(1.0d0-h2*k0/6.0d0)
      ELSE
       psip1=dx*(1.0d0-h2*k0/6.0d0)
      END IF
      
      xstep(1)=dx
      psifinal(1)=psip1
      xstep(-1)=-dx
      psifinal(-1)=paridad*psip1
      
      psim1=psi
      psi=psip1
      
      DO i=2,N
       x=i*dx
       kp1=2.0d0*(energy-V(x))
       k0=2.0d0*(energy-V(x-dx))
       km1=2.0d0*(energy-V(x-2.0d0*dx))
       
       denom=1.0d0+h2*kp1/12.0d0
       psip1=(2.0d0*psi*(1.0d0-5.0d0*h2*k0/12.0d0) - &
              psim1*(1.0d0+h2*km1/12.0d0))/denom
       
       xstep(i)=x
       psifinal(i)=psip1
       xstep(-i)=-x
       psifinal(-i)=paridad*psip1
       
       psim1=psi
       psi=psip1
      END DO

!     Normalizar la función de onda
      norma=0.0d0
      DO i=-N,N-1
       norma=norma+0.5d0*(psifinal(i)**2+psifinal(i+1)**2)*dx
      END DO
      norm=SQRT(norma)
      
      write(*,'(A,F15.10)')'Norma = ',norm

      DO i=-N,N
       psifinal(i)=psifinal(i)/norm
       write(2,*)xstep(i),psifinal(i)
      END DO
      close(2)
      
      write(*,*)'Datos guardados en psi_doble.dat'

      END Program Pozo_Doble

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      FUNCTION V(x)
!     Potencial doble pozo
      IMPLICIT NONE
      REAL(8) V,x,a
      COMMON /params/ a
      
      IF(ABS(x).gt.1.0d0) then
       V=1.0d10 ! Infinito (valor muy grande)
      ELSE IF(ABS(x).le.a) then
       V=1.0d0
      ELSE
       V=0.0d0
      END IF
      
      RETURN
      END FUNCTION V
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

