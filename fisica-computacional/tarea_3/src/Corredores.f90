! Corredores.f90
!
! Lee archivos .dat con dos columnas (tiempo y distancia, en cualquier orden),
! calcula la velocidad v = d(distancia)/d(tiempo) usando el método de 3 puntos
! (fórmulas de Lagrange para malla no uniforme) y guarda un archivo .dat
! con columnas: tiempo[min], distancia[km], v[km/min], v[m/s].
! Adicionalmente:
!  - Imprime en terminal la velocidad máxima y mínima (con tiempos).
!  - Integra v(t) con trapecios (2 puntos) para obtener distancia acumulada.
!  - Reporta los tiempos cuando se alcanzan 1,2,3,4,5 km.
!  - Reporta el "pace" por kilómetro (tiempo de cada 1 km) hasta 5 km.
!
! Compilar:  gfortran -O2 -std=f2008 -o Corredores Corredores.f90
! Ejecutar:  ./Corredores archivo1.dat archivo2.dat ...

program Corredores
  use, intrinsic :: iso_fortran_env, only: dp => real64, iostat_end
  implicit none
  integer :: argc, i
  character(len=512) :: iname

  argc = command_argument_count()
  if (argc == 0) then
     print *, "Uso: ./Corredores archivo1.dat [archivo2.dat ...]"
     print *, "Salida: <nombre>_velocidad.dat con tiempo, distancia, v(km/min), v(m/s)"
     stop
  end if

  do i = 1, argc
     call get_command_argument(i, iname)
     call process_file(trim(iname))
  end do
contains

  subroutine process_file(filename)
    implicit none
    character(len=*), intent(in) :: filename
    real(dp), allocatable :: c1(:), c2(:), t(:), d(:), v(:), vms(:), s(:)
    integer :: n, ios
    character(len=:), allocatable :: outname
    integer :: i_vmin, i_vmax
    real(dp) :: goals(5), times_km(5), pace_km(5)
    integer :: k

    call read_two_columns(filename, c1, c2, n, ios)
    if (ios /= 0 .or. n < 2) then
       write(*,*) "No se pudo leer/interpretar el archivo: ", trim(filename)
       return
    end if

    call choose_time_distance(c1, c2, n, t, d, ios)
    if (ios /= 0) then
      write(*,*) "No fue posible identificar columnas tiempo/distancia en: ", trim(filename)
      return
    end if

    allocate(v(n), vms(n), s(n))
    call deriv3_nonuniform(n, t, d, v)

    ! Convertir a m/s: (km/min) * 1000/60
    vms = v * (1000.0_dp/60.0_dp)

    ! Distancia acumulada (km) integrando v(t) por trapecios
    call trapz_cumulative(n, t, v, s)

    ! Velocidades extrema y sus índices
    call argminmax(v, i_vmin, i_vmax)

    ! Buscar tiempos a 1..5 km
    goals = (/ 1.0_dp, 2.0_dp, 3.0_dp, 4.0_dp, 5.0_dp /)
    call times_to_goals(s, t, goals, times_km)

    ! Pace por kilómetro: t_k - t_{k-1}, con t_0=0
    pace_km = -1.0_dp
    if (times_km(1) > 0.0_dp) pace_km(1) = times_km(1)
    do k = 2, 5
       if (times_km(k) > 0.0_dp .and. times_km(k-1) > 0.0_dp) then
          pace_km(k) = times_km(k) - times_km(k-1)
       end if
    end do

    ! ===== Salida a archivo =====
    outname = make_output_name(filename)
    call write_output(outname, t, d, v, vms, n)
    write(*,*) "OK -> ", trim(outname), "  (", n, " filas)"

    ! ===== Reporte en terminal =====
    write(*,'(a)') "------------------------------------------------------------"
    write(*,'(a)') "Archivo: "//trim(filename)
    write(*,'(a,1x,ES12.5,a,1x,ES12.5,a)') "v_min =", v(i_vmin), "km/min  |", vms(i_vmin), "m/s"
    write(*,'(a,1x,ES12.5,a,1x,ES12.5,a)') "v_max =", v(i_vmax), "km/min  |", vms(i_vmax), "m/s"
    write(*,'(a,1x,F10.4,a,1x,a)') "t(v_min)=", t(i_vmin), "min  |", trim(fmt_mmss(t(i_vmin)))
    write(*,'(a,1x,F10.4,a,1x,a)') "t(v_max)=", t(i_vmax), "min  |", trim(fmt_mmss(t(i_vmax)))
    write(*,'(a)') "----- Tiempos (por integración de v) para alcanzar:"
    call print_goal_time("1 km:", times_km(1))
    call print_goal_time("2 km:", times_km(2))
    call print_goal_time("3 km:", times_km(3))
    call print_goal_time("4 km:", times_km(4))
    call print_goal_time("5 km:", times_km(5))
    write(*,'(a)') "----- Pace por kilómetro (min:seg por km):"
    call print_pace("km 1:", pace_km(1))
    call print_pace("km 2:", pace_km(2))
    call print_pace("km 3:", pace_km(3))
    call print_pace("km 4:", pace_km(4))
    call print_pace("km 5:", pace_km(5))
    write(*,'(a)') "------------------------------------------------------------"
  end subroutine process_file

  subroutine read_two_columns(filename, a, b, n, ios)
    implicit none
    character(len=*), intent(in) :: filename
    real(dp), allocatable, intent(out) :: a(:), b(:)
    integer, intent(out) :: n, ios

    integer :: u, iostat_read
    real(dp) :: x, y
    integer :: count

    ios = 0; n = 0
    open(newunit=u, file=filename, status='old', action='read', iostat=ios)
    if (ios /= 0) return

    ! 1) Contar líneas válidas (dos reales por línea).
    count = 0
    do
       read(u, *, iostat=iostat_read) x, y
       if (iostat_read == 0) then
          count = count + 1
       else if (iostat_read == iostat_end) then
          exit
       else
          ! Ignora líneas mal formateadas o con comentarios
          cycle
       end if
    end do

    if (count <= 0) then
       close(u)
       ios = 1
       return
    end if

    n = count
    allocate(a(n), b(n))

    rewind(u)
    count = 0
    do
       read(u, *, iostat=iostat_read) x, y
       if (iostat_read == 0) then
          count = count + 1
          a(count) = x
          b(count) = y
          if (count == n) exit
       else if (iostat_read == iostat_end) then
          exit
       else
          cycle
       end if
    end do
    close(u)
  end subroutine read_two_columns

  pure logical function strictly_increasing(x) result(ok)
    implicit none
    real(dp), intent(in) :: x(:)
    integer :: i
    real(dp), parameter :: eps = 1.0e-12_dp
    ok = .true.
    do i = 2, size(x)
       if (x(i) <= x(i-1) + eps) then
          ok = .false.; return
       end if
    end do
  end function strictly_increasing

  subroutine choose_time_distance(c1, c2, n, t, d, ios)
    implicit none
    ! Heurística:
    !  - Si sólo una columna es estrictamente creciente => ésa es el tiempo.
    !  - Si ambas crecen, se elige como tiempo la de MENOR rango total (minutos suelen < km).
    real(dp), intent(in) :: c1(:), c2(:)
    integer, intent(in) :: n
    real(dp), allocatable, intent(out) :: t(:), d(:)
    integer, intent(out) :: ios
    logical :: inc1, inc2
    real(dp) :: r1, r2
    ios = 0
    inc1 = strictly_increasing(c1)
    inc2 = strictly_increasing(c2)
    r1 = c1(n) - c1(1)
    r2 = c2(n) - c2(1)

    if (inc1 .and. .not. inc2) then
       allocate(t(n), d(n)); t = c1; d = c2
    else if (.not. inc1 .and. inc2) then
        allocate(t(n), d(n)); t = c2; d = c1
    else if (inc1 .and. inc2) then
       if (r1 <= r2) then
          allocate(t(n), d(n)); t = c1; d = c2
       else
          allocate(t(n), d(n)); t = c2; d = c1
       end if
    else
       ios = 1
    end if
  end subroutine choose_time_distance

  subroutine deriv3_nonuniform(n, x, y, dydx)
    implicit none
    ! Derivada de y(x) con 3 puntos (Lagrange) en malla no uniforme.
    integer, intent(in) :: n
    real(dp), intent(in)  :: x(n), y(n)
    real(dp), intent(out) :: dydx(n)
    integer :: i
    real(dp) :: x0, x1, x2, xi, xm1, xp1

    if (n == 1) then
       dydx(1) = 0.0_dp
       return
    else if (n == 2) then
      if (abs(x(2)-x(1)) > 0.0_dp) then
         dydx(1) = (y(2)-y(1))/(x(2)-x(1))
         dydx(2) = dydx(1)
      else
         dydx = 0.0_dp
      end if
      return
    end if

    ! i = 1 (adelantada, puntos 1,2,3)
    x0 = x(1); x1 = x(2); x2 = x(3)
    dydx(1) = y(1) * ( (2.0_dp*x0 - x1 - x2) / ((x0 - x1)*(x0 - x2)) ) &
            + y(2) * ( (x0 - x2) / ((x1 - x0)*(x1 - x2)) )            &
            + y(3) * ( (x0 - x1) / ((x2 - x0)*(x2 - x1)) )

    ! i = 2..n-1 (centrada)
    do i = 2, n-1
       xm1 = x(i-1); xi = x(i); xp1 = x(i+1)
       dydx(i) = y(i-1) * ( (xi - xp1) / ((xm1 - xi)*(xm1 - xp1)) )  &
               + y(i)   * ( (2.0_dp*xi - xm1 - xp1) / ((xi - xm1)*(xi - xp1)) ) &
               + y(i+1) * ( (xi - xm1) / ((xp1 - xm1)*(xp1 - xi)) )
    end do

    ! i = n (atrasada, puntos n-2,n-1,n)
    x0 = x(n-2); x1 = x(n-1); x2 = x(n)
    dydx(n) = y(n-2) * ( (x2 - x1) / ((x0 - x1)*(x0 - x2)) )            &
            + y(n-1) * ( (x2 - x0) / ((x1 - x0)*(x1 - x2)) )            &
            + y(n)   * ( (2.0_dp*x2 - x0 - x1) / ((x2 - x0)*(x2 - x1)) )

  end subroutine deriv3_nonuniform

  subroutine trapz_cumulative(n, t, v, s)
    implicit none
    ! Distancia acumulada (km) integrando v(t) con trapecios.
    integer, intent(in) :: n
    real(dp), intent(in) :: t(n), v(n)  ! t en min, v en km/min
    real(dp), intent(out) :: s(n)       ! s en km
    integer :: i
    s(1) = 0.0_dp
    do i = 2, n
       s(i) = s(i-1) + 0.5_dp * (v(i) + v(i-1)) * (t(i) - t(i-1))
    end do
  end subroutine trapz_cumulative

  subroutine argminmax(a, i_min, i_max)
    implicit none
    real(dp), intent(in) :: a(:)
    integer, intent(out) :: i_min, i_max
    integer :: i, n
    n = size(a)
    i_min = 1; i_max = 1
    do i = 2, n
       if (a(i) < a(i_min)) i_min = i
       if (a(i) > a(i_max)) i_max = i
    end do
  end subroutine argminmax

  subroutine times_to_goals(s, t, goals, tgoals)
    implicit none
    ! s: distancia acumulada (km), t: minutos
    real(dp), intent(in) :: s(:), t(:)
    real(dp), intent(in) :: goals(:)
    real(dp), intent(out) :: tgoals(:)
    integer :: i, g, n
    real(dp) :: s1, s2, t1, t2, frac

    n = size(s)
    tgoals = -1.0_dp
    g = 1
    do i = 2, n
       s1 = s(i-1); s2 = s(i); t1 = t(i-1); t2 = t(i)
       do while (g <= size(goals) .and. s2 >= goals(g))
          if (s2 > s1) then
             frac = (goals(g) - s1) / (s2 - s1)
             tgoals(g) = t1 + frac * (t2 - t1)
          else
             tgoals(g) = t2
          end if
          g = g + 1
       end do
       if (g > size(goals)) exit
    end do
  end subroutine times_to_goals

  pure function fmt_mmss(tmin) result(s)
    implicit none
    real(dp), intent(in) :: tmin
    character(len=12) :: s
    integer :: totsec, mm, ss
    if (tmin < 0.0_dp) then
       s = "N/A"
       return
    end if
    totsec = int(tmin*60.0_dp + 0.5_dp)
    mm = totsec / 60
    ss = mod(totsec, 60)
    write(s,'(I0,":",I2.2)') mm, ss
  end function fmt_mmss

  subroutine print_goal_time(label, tmin)
    implicit none
    character(len=*), intent(in) :: label
    real(dp), intent(in) :: tmin
    if (tmin < 0.0_dp) then
       write(*,'(a,1x,a)') trim(label), "no alcanzado"
    else
       write(*,'(a,1x,F10.4,1x,a)') trim(label), tmin, trim(fmt_mmss(tmin))
    end if
  end subroutine print_goal_time

  subroutine print_pace(label, tmin)
    implicit none
    character(len=*), intent(in) :: label
    real(dp), intent(in) :: tmin
    if (tmin < 0.0_dp) then
       write(*,'(a,1x,a)') trim(label), "N/A"
    else
       write(*,'(a,1x,F10.4,1x,a)') trim(label), tmin, trim(fmt_mmss(tmin))
    end if
  end subroutine print_pace

  pure function make_output_name(filename) result(out)
    implicit none
    character(len=*), intent(in) :: filename
    character(len=:), allocatable :: out
    integer :: dotpos, i, n

    n = len_trim(filename)
    dotpos = 0
    do i = n, 1, -1
       if (filename(i:i) == ".") then
          dotpos = i
          exit
       end if
    end do

    if (dotpos > 0) then
       out = filename(1:dotpos-1) // "_velocidad.dat"
    else
      out = trim(filename) // "_velocidad.dat"
    end if
  end function make_output_name

  subroutine write_output(outname, t, d, v, vms, n)
    implicit none
    character(len=*), intent(in) :: outname
    integer, intent(in) :: n
    real(dp), intent(in) :: t(n), d(n), v(n), vms(n)
    integer :: u, i

    open(newunit=u, file=outname, status='replace', action='write')
    write(u,'(a)') "# tiempo_min   distancia_km   v_km_per_min   v_m_per_s"
    do i = 1, n
       write(u,'(1x,ES16.8,1x,ES16.8,1x,ES16.8,1x,ES16.8)') t(i), d(i), v(i), vms(i)
    end do
    close(u)
  end subroutine write_output

end program Corredores
