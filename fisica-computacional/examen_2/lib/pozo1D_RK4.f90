      Program Pozo1D
!     Este programa resuelve numericament el pozo de potencial infinito
!     [-1,1]  1D. Ecuentra la funcion de onda Psi y la energia E
      IMPLICIT NONE
      REAL(8) psi,dpsi! funcion de onda y su derivada
      REAL(8) psiold,dpsiold,psinew,dpsinew !parametros para converger en psi(1) y dpsi(1)
      REAL(8) eps !parametro de convergencia
      REAL(8) energy,de!energia y su incremento
      REAL(8) x,dx !posicion y su incremento
      REAL(8) xstep,psifinal!
      Integer paridad! 1 si psi es par y -1 si psi es impar
      Integer N! Número de evaluaciones entre (0,1]
      Integer icont,i,j! Contador de ciclos para lograr la convergencia e indices i y j
!
!
     ALLOCATABLE :: xstep(:),psifinal(:)
      write(*,*)'1.- Proporciona un valor de particiones N'
      read(*,*)N
      write(*,*)'El inretvalo (0,1] tendra',N,'particiones.'
     ALLOCATE (xstep(-N:N),psifinal(-N:N)) !arreglo de 2N+1 entradas
!
      write(*,*)'2.- Proporciona un valor inicial de la energia'
      read(*,*)energy
      write(*,*)'La energia inicial es',energy
!
      write(*,*)'3.- Escribe la paridad de la fuincion, 1 par, -1 impar'
      read(*,*)paridad
      write(*,*)'La paridad selecionada fue:',paridad
!
!     Parametrso para convereger      
      dx=1.0d0/N ! incremento en x para RK
      eps=1.0d-4! criterio de convergencia de la psi
      de=ABS(energy)/10.0d0 ! icremento en la energia
!      
      icont=0 ! ver cuantos ciclos ocupó el programa
      psiold=1.0d0 !Parametos para comparar psi(1) 
      psinew=1.0d0 !Parametros para comparar psi(1)
!
!     Inicia clico para converger psi(1)
      write(*,*)'      contador        energy                    de                       psi'
      DO WHIlE (.true.) !Ciclo sin fin
       IF(paridad.eq.1) then ! Definimos psi(0) y dpsi(0) por paridad 
        psi=1.0d0
        dpsi=0.0d0
       ELSE
        psi=0.0d0
        dpsi=1.0d0
       END IF
!     Soluciona el problema usando Runge kutta 4to orden
       DO i=1,N
        x=i*dx
        call RKSTEP(dx,x,psi,dpsi,energy) ! llama a la subrutina RKStep
       END DO     
!      Despues del ciclo, psi y dpsi estan evaluadas en x=1
       psinew=psi
       write(*,*)icont,energy,de,psi
       IF(DABS(psi).le.eps) EXIT !Convergemos a la solicion?
       IF(psinew*psiold.lt.0.0d0) de=-0.5d0*de !Si Psi(1)<0, reduce el de y cambialo de signo
       energy=energy+de !Energia para el nuevo ciclo
       psiold=psinew    !Redefinimos parametros de cambio de seigno
       icont=icont+1    !Contador de ciclos para lograr convergencia
      END DO ! se termina el ciclo While
!     Ya se obtuvo la funcion de onda y la energia correctas
!
      OPEN(2,file='psi.dat')!Archivo de salida para la Psi
!
!     Con la energia ya convergida, se pregunta nuevamente la paridad
      IF(paridad.eq.1) then
       psi=1.0d0
       dpsi=0.0d0
       xstep(0)=0.0d0
       psifinal(0)=1.0d0 
      ELSE
       psi=0.0d0
       dpsi=1.0d0
       xstep(0)=0.0d0
       psifinal(0)=0.0d0
      END IF
!      
      DO i=1,N !Evaluamos la psi convergida de [-1,1] 
       x=i*dx
       call RKSTEP(dx,x,psi,dpsi,energy)
       xstep(i)=x
       psifinal(i)=psi
       xstep(-i)=-x
       psifinal(-i)=paridad*psi
      END DO
!
      DO i=-N,N
       write(2,*)xstep(i),psifinal(i) !Exportamos nuestar funcio de onda 
      END DO
      close(2)
!
      END Program Pozo1D
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      SUBROUTINE RKstep(h,t,x1,x2,e)
      IMPLICIT NONE
      real(8) h,t,x1,x2,h2,h6,e
      real(8) k11,k12,k13,k14,k21,k22,k23,k24,f1,f2
!
      h2=h/2.0d0
      h6=h/6.0d0
!
      k11=f1(t,x1,x2,e)
      k21=f2(t,x1,x2,e)
      k12=f1(t+h2,x1+h2*k11,x2+h2*k21,e)
      k22=f2(t+h2,x1+h2*k11,x2+h2*k21,e)
      k13=f1(t+h2,x1+h2*k12,x2+h2*k22,e)
      k23=f2(t+h2,x1+h2*k12,x2+h2*k22,e)
      k14=f1(t+h,x1+h*k13,x2+h*k23,e)
      k24=f2(t+h,x1+h*k13,x2+h*k23,e)
      x1=x1+h6*(k11+2.0d0*(k12+k13)+k14)
      x2=x2+h6*(k21+2.0d0*(k22+k23)+k24)
      RETURN
      END Subroutine
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      FUNCTION f1(t,x1,x2,e)
      IMPLICIT NONE
      REAL(8) f1,t,x1,x2,e
      f1=x2
      RETURN
      END
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      FUNCTION f2(t,x1,x2,e)
      IMPLICIT NONE
      REAL(8) f2,t,x1,x2,e
      f2=-2.0d0*e*x1
      RETURN
      END
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!      



