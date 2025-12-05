program orbitas
  implicit none
  integer :: N, iostat, i
  real(8) :: GM, t0, tf, h, t
  real(8) :: x, y, vx, vy, r, v, E, L
  real(8) :: x0, y0, vx0, vy0
  real(8) :: k1x, k2x, k3x, k4x
  real(8) :: k1y, k2y, k3y, k4y
  real(8) :: k1vx, k2vx, k3vx, k4vx
  real(8) :: k1vy, k2vy, k3vy, k4vy
  real(8) :: E0, L0, Eend, Lend, relE, relL
  character(len=256) :: outfile
  integer :: unit

  call get_args(GM, t0, tf, N, x0, y0, vx0, vy0, outfile, iostat)
  if (iostat /= 0) then
     call usage()
     stop 1
  end if

  h = (tf - t0) / real(N,8)
  t = t0
  x = x0; y = y0; vx = vx0; vy = vy0
  r = sqrt(x*x + y*y)
  v = sqrt(vx*vx + vy*vy)
  E = 0.5d0*(vx*vx + vy*vy) - GM/r
  L = x*vy - y*vx
  E0 = E; L0 = L

  open(newunit=unit, file=outfile, status='replace', action='write')
  write(unit,'(A)') '# t   x   y   vx   vy   r   v   E   L'
  write(unit,'(1p,9e22.14)') t, x, y, vx, vy, r, v, E, L

  do i = 1, N
     call rk4_step(t, x, y, vx, vy, h, GM, &
                   k1x, k1y, k1vx, k1vy,  &
                   k2x, k2y, k2vx, k2vy,  &
                   k3x, k3y, k3vx, k3vy,  &
                   k4x, k4y, k4vx, k4vy)

     x  = x  + (k1x + 2d0*k2x + 2d0*k3x + k4x)/6d0
     y  = y  + (k1y + 2d0*k2y + 2d0*k3y + k4y)/6d0
     vx = vx + (k1vx+ 2d0*k2vx+ 2d0*k3vx+ k4vx)/6d0
     vy = vy + (k1vy+ 2d0*k2vy+ 2d0*k3vy+ k4vy)/6d0
     t  = t + h

     r = max(1d-12, sqrt(x*x + y*y))   ! evita división por cero numérica
     v = sqrt(vx*vx + vy*vy)
     E = 0.5d0*(vx*vx + vy*vy) - GM/r
     L = x*vy - y*vx
     write(unit,'(1p,9e22.14)') t, x, y, vx, vy, r, v, E, L
  end do

  close(unit)
  Eend = E; Lend = L
  relE = abs(Eend - E0) / max(1d-16, abs(E0))
  relL = abs(Lend - L0) / max(1d-16, abs(L0))

  write(*,'(A,1p,e11.3)') 'Relative energy drift: ', relE
  write(*,'(A,1p,e11.3)') 'Relative angular momentum drift: ', relL
contains

  subroutine usage()
    print *, 'Uso: ./orbitas GM t0 tf N x0 y0 vx0 vy0 salida.dat'
    print *, 'Ej.: ./orbitas 10.0 0.0 2.5 5000 1.0 1.0 -0.5 0.5 salida_1.dat'
  end subroutine usage

  subroutine get_args(GM, t0, tf, N, x0, y0, vx0, vy0, outfile, iostat)
    real(8), intent(out) :: GM, t0, tf, x0, y0, vx0, vy0
    integer, intent(out) :: N, iostat
    character(len=*), intent(out) :: outfile
    character(len=256) :: arg
    integer :: narg
    narg = command_argument_count()
    if (narg /= 9) then
       iostat = 1; return
    end if
    call get_command_argument(1,arg); read(arg,*,iostat=iostat) GM; if (iostat/=0) return
    call get_command_argument(2,arg); read(arg,*,iostat=iostat) t0; if (iostat/=0) return
    call get_command_argument(3,arg); read(arg,*,iostat=iostat) tf; if (iostat/=0) return
    call get_command_argument(4,arg); read(arg,*,iostat=iostat) N;  if (iostat/=0) return
    call get_command_argument(5,arg); read(arg,*,iostat=iostat) x0; if (iostat/=0) return
    call get_command_argument(6,arg); read(arg,*,iostat=iostat) y0; if (iostat/=0) return
    call get_command_argument(7,arg); read(arg,*,iostat=iostat) vx0; if (iostat/=0) return
    call get_command_argument(8,arg); read(arg,*,iostat=iostat) vy0; if (iostat/=0) return
    call get_command_argument(9,outfile)
    iostat = 0
  end subroutine get_args

  pure real(8) function ax(x,y,GM)
    real(8), intent(in) :: x, y, GM
    real(8) :: r3
    r3 = (x*x + y*y)**1.5d0
    if (r3 < 1d-18) then
       ax = 0d0
    else
       ax = -GM*x/r3
    end if
  end function ax

  pure real(8) function ay(x,y,GM)
    real(8), intent(in) :: x, y, GM
    real(8) :: r3
    r3 = (x*x + y*y)**1.5d0
    if (r3 < 1d-18) then
       ay = 0d0
    else
       ay = -GM*y/r3
    end if
  end function ay

  subroutine rk4_step(t, x, y, vx, vy, h, GM,                 &
                      k1x, k1y, k1vx, k1vy,                   &
                      k2x, k2y, k2vx, k2vy,                   &
                      k3x, k3y, k3vx, k3vy,                   &
                      k4x, k4y, k4vx, k4vy)
    real(8), intent(in) :: t, x, y, vx, vy, h, GM
    real(8), intent(out):: k1x, k1y, k1vx, k1vy,              &
                            k2x, k2y, k2vx, k2vy,              &
                            k3x, k3y, k3vx, k3vy,              &
                            k4x, k4y, k4vx, k4vy
    real(8) :: xt, yt, vxt, vyt

    ! k1
    k1x  = h * vx
    k1y  = h * vy
    k1vx = h * ax(x, y, GM)
    k1vy = h * ay(x, y, GM)

    ! k2
    xt = x + 0.5d0*k1x
    yt = y + 0.5d0*k1y
    vxt = vx + 0.5d0*k1vx
    vyt = vy + 0.5d0*k1vy
    k2x  = h * vxt
    k2y  = h * vyt
    k2vx = h * ax(xt, yt, GM)
    k2vy = h * ay(xt, yt, GM)

    ! k3
    xt = x + 0.5d0*k2x
    yt = y + 0.5d0*k2y
    vxt = vx + 0.5d0*k2vx
    vyt = vy + 0.5d0*k2vy
    k3x  = h * vxt
    k3y  = h * vyt
    k3vx = h * ax(xt, yt, GM)
    k3vy = h * ay(xt, yt, GM)

    ! k4
    xt = x + k3x
    yt = y + k3y
    vxt = vx + k3vx
    vyt = vy + k3vy
    k4x  = h * vxt
    k4y  = h * vyt
    k4vx = h * ax(xt, yt, GM)
    k4vy = h * ay(xt, yt, GM)
  end subroutine rk4_step

end program orbitas
