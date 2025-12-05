using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Laberinto;

public class Laberinto : Avalonia.Controls.Primitives.UniformGrid
{
    public class ClaseAgente : Panel
    {
        private Laberinto _laberinto;
        private Canvas _avatar;
		private Shape _forma;

        private readonly int _iniRen;
		private readonly int _iniCol;
		
		private int _ren;
		private int _col;

		/// <summary>
		/// Genera eventos periódicamente donde revisaremos la lista
		/// de movmientos por mostrar en pantalla, para desplazar
		/// al agente mientras haya movimientos pendientes.
		/// </summary>
		private DispatcherTimer _timer;
		private record struct Coord(int Ren, int Col);
		private LinkedList<Coord> _destinos = new LinkedList<Coord>();

		private readonly int MaxTicks = 60;
		/// <summary>
		/// Número de ticks en que va la animación.
		/// </summary>
		private int _numTicks = 0;
		private Coord _prevCoords;

        internal ClaseAgente(Laberinto laberinto, int ren, int col) {
            _laberinto = laberinto;
			_iniRen = ren;
			_iniCol = col;
			
            var forma = new Ellipse();
            forma.Fill = new SolidColorBrush(Colors.AliceBlue);
            forma.Width = forma.Height = laberinto._tamCelda/3;
			_forma = forma;
			_avatar = new Canvas();
			_avatar.Children.Add(forma);
			Children.Add(_avatar);

			Reinicia();

			_timer = new()
			{
				Interval = new TimeSpan(0, 0, 0, 0, 100 / MaxTicks)
			};
			_timer.Tick += delegate{ DoTick(); };
		}

		/// <summary>
		/// Método de la animación para desplazar la figura en cada
		/// tick del reloj.
		/// </summary>
		private void DoTick()
    	{
			if (_destinos.Count == 0)
			{
				// La animación terminó
				_timer.IsEnabled = false;
				return;
			}

			if (_numTicks == 0)
			{
				// Registra su punto de partida
				_prevCoords = _destinos.First();
				_destinos.RemoveFirst();
				_numTicks++;
			}
			else{
				// Se acerca a su destino con una interpolación lineal.
				Coord sig = _destinos.First();
				double proporcion = (double)_numTicks / (double)MaxTicks;
				double nuevaX = XCoord(_prevCoords.Col) + (XCoord(sig.Col) - XCoord(_prevCoords.Col)) * proporcion;
				double nuevaY = YCoord(_prevCoords.Ren) + (YCoord(sig.Ren) - YCoord(_prevCoords.Ren)) * proporcion;
				Canvas.SetLeft(_forma, nuevaX);
				Canvas.SetTop(_forma, nuevaY);
				if (_numTicks == MaxTicks)
				{
					_numTicks = 0;
				}
				else
				{
					_numTicks++;
				}
			}
		}

		/// <summary>
		/// Devuelve la distancia del borde izquierdo del laberinto hasta el agente.
		/// </summary>
		/// <param name="col">columna del laberinto en la que se encuentra el agente.</param>
		/// <returns>Distancia al borde izquierdo.</returns>
		private double XCoord(int col)
		{
			return _laberinto._tamCelda * col + _forma.Width;
		}

		/// <summary>
		/// Devuelve la distancia del borde superior del laberinto hasta el agente.
		/// </summary>
		/// <param name="ren">renglón del laberinto en el que se encuentra el agente.</param>
		/// <returns>Distancia al borde izquierdo.</returns>
		private double YCoord(int ren)
		{
			return _laberinto._tamCelda * ren + _forma.Height;
		}

		public double X
		{
			get => XCoord(_col);
		}

		public double Y
		{
			get => YCoord(_ren);
		}

		public bool Avanza(Dirección dirección)
		{
			Celda c = _laberinto._celdas[_ren,_col];
			if(c.paredes[(int)dirección]) return false;

			switch(dirección) {
				case Dirección.Norte:
					_ren--;
					break;
				case Dirección.Sur:
					_ren++;
					break;
				case Dirección.Este:
					_col++;
					break;
				case Dirección.Oeste:
					_col--;
					break;
			}
			_destinos.AddLast(new Coord(_ren, _col));
			return true;
		}

		/// <summary>
		/// Indica las direcciones en las que hay pasillos abiertos.
		/// </summary>
		/// <returns>La lista de direcciones.</returns>
		public LinkedList<Dirección> DireccionesPasillos()
		{
			LinkedList<Dirección> l = new LinkedList<Dirección>();
			Celda c = _laberinto._celdas[_ren,_col];
			foreach(Dirección d in Enum.GetValues(typeof(Dirección))) {
				if (!c.paredes[(int)d]) {
					l.AddLast(d);
				}
			}
			return l;
		}

		/// <summary>
		/// Indica si el agente se encuentra parado en una celda meta.
		/// </summary>
		/// <returns>Si está en la meta.</returns>
		public bool EstáEnMeta()
		{
			return _laberinto._celdas[_ren,_col].EsMeta;
		}

		/// <summary>
		/// Marca la celda donde está parado el agente como una celda visitada.
		/// </summary>
		public void MarcaCelda()
		{
			_laberinto._celdas[_ren,_col].Visitada = true;
		}

		/// <summary>
		/// Desmarca la celda donde está parado el agente como una celda visitada.
		/// </summary>
		public void DesmarcaCelda()
		{
			_laberinto._celdas[_ren,_col].Visitada = false;
		}

		/// <summary>
		/// Mira a la celda en la dirección indicada.
		/// Si hay un pasillo en esa dirección y la celda no ha sido visitada
		/// devuelve True.
		/// </summary>
		/// <param name="d">Dirección en la cual mirar desde la celda donde
		/// está el agente.</param>
		/// <returns>Si hay un pasillo sin vistar en esa dirección.</returns>
		public bool MiraSiNoVisitada(Dirección d)
		{
			if (_laberinto._celdas[_ren,_col].paredes[(int)d]) return false;
			Celda? v = _laberinto.DevuelveVecina(_ren, _col, d);
			if (v == null) return false;
			else return !v.Visitada;
		}

		public void EjecutaAnimación()
		{
			_numTicks = 0;
			_destinos.AddFirst(new Coord(_iniRen, _iniCol));
			_timer.IsEnabled = true;
		}

		public void Reinicia()
		{
			_ren = _iniRen;
			_col = _iniCol;
			Canvas.SetLeft(_forma, X);
			Canvas.SetTop(_forma, Y);
			_laberinto.DesmarcaLaberinto();
			_destinos.Clear();
		}
    }
	
	private double _tamCelda;
	
	private Celda[,] _celdas;
	
	private ClaseAgente _agente;

	/// <summary>
	/// Construye un laberinto generándolo aleatoriamente.
	/// </summary>
	/// <param name="padre"></param>
	/// <param name="rens"></param>
	/// <param name="cols"></param>
    public Laberinto(Panel padre, int rens, int cols)
    {
        this.Rows = rens;
        this.Columns = cols;

        AsignaTamaños(padre);
        LlenaCeldas();
        Genera();
    }

    public ClaseAgente Agente {
        get => _agente;
    }

    /// <summary>
	/// Asigna los tamaños de renglones y columnas.
	/// </summary>
	/// <param name="padre"></param>
	private void AsignaTamaños(Panel padre) {
		Width = padre.Width;
		Height = padre.Height;
		
		double altoCelda = Height/Rows;
		double anchoCelda = Width/Columns;
		_tamCelda = (altoCelda <= anchoCelda) ? altoCelda : anchoCelda;
        Width = _tamCelda * Columns;
        Height = _tamCelda * Rows;
	}

	/// <summary>
	/// Carga las imágenes para los mosaicos y llena el arreglo
	/// del celdas.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_celdas))]
    private void LlenaCeldas()
    {
        Sprites sprites = new Sprites(_tamCelda);
        _celdas = new Celda[Rows,Columns];
        Celda celda;

        for(int ren = 0; ren < Rows; ren++) {
            for(int col = 0; col < Columns; col++) {
				celda = new Celda(sprites);
				Children.Add(celda);
				_celdas[ren,col] = celda;
            }
        }
    }

	/// <summary>
	/// Devuelve la celda vecina en la dirección indicada y null
	/// si topa con la pared del laberinto.
	/// </summary>
	/// <param name="ren"></param>
	/// <param name="col"></param>
	/// <param name="d"></param>
	/// <returns></returns>
	private Celda? DevuelveVecina(int ren, int col, Dirección d)
	{
		switch(d)
		{
			case Dirección.Norte:
				if (ren > 0) return _celdas[ren-1,col];
				else return null;
			case Dirección.Sur:
				if(ren < Rows - 1) return _celdas[ren+1,col];
				else return null;
			case Dirección.Este:
				if(col < Columns - 1) return _celdas[ren,col+1];
				else return null;
			case Dirección.Oeste:
				if(col > 0) return _celdas[ren,col-1];
				else return null;
		}
		return null;
	}

	/// <summary>
	/// Calcula las direcciones con paredes que aún pueden ser derribadas.
	/// </summary>
	/// <param name="ren"></param>
	/// <param name="col"></param>
	/// <returns></returns>
	private LinkedList<Dirección> CalculaDireccionesDerribables(int ren, int col)
	{
		Celda c = _celdas[ren,col];
		LinkedList<Dirección> rutas = new LinkedList<Dirección>();
		// Norte
		if (ren > 0
				&& c.paredes[0]
				&& !_celdas[ren-1,col].Visitada)
		{
			rutas.AddLast(Dirección.Norte);
		}
		// Este
		if (col < Columns - 1
				&& c.paredes[1]
				&& !_celdas[ren,col+1].Visitada)
		{
			rutas.AddLast(Dirección.Este);
		}
		// Sur
		if (ren < Rows - 1
				&& c.paredes[2]
				&& !_celdas[ren+1,col].Visitada)
		{
			rutas.AddLast(Dirección.Sur);
		}
		// Oeste
		if (col > 0
				&& c.paredes[3]
				&& !_celdas[ren,col-1].Visitada)
		{
			rutas.AddLast(Dirección.Oeste);
		}
		return rutas;
	}

    /// <summary>
	/// Derriba paredes aleatoriamente siguiendo el algoritmo de retractación.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_agente))]
	private void Genera()
    {
		// Selecciona aleatoriamente una posición inicial:
        int renAzar = RandomNumberGenerator.GetInt32(Rows);
        int colAzar = RandomNumberGenerator.GetInt32(Columns);
		DerribaPared(renAzar, colAzar);
		DesmarcaLaberinto();
		renAzar = RandomNumberGenerator.GetInt32(Rows);
		colAzar = RandomNumberGenerator.GetInt32(Columns);
		_celdas[renAzar,colAzar].EsMeta = true;
		
		do {
			renAzar = RandomNumberGenerator.GetInt32(Rows);
			colAzar = RandomNumberGenerator.GetInt32(Columns);
		} while(_celdas[renAzar,colAzar].EsMeta);
		
		_agente = new ClaseAgente(this, renAzar, colAzar);
	}

	/// <summary>
	/// Selecciona una pared para derribar y pasa el trabajo al descendiente.
	/// </summary>
	/// <param name="ren">Renglón de la celda actual.</param>
	/// <param name="col">Columna de la celda actual.</param>
	private void DerribaPared(int ren, int col) {
		_celdas[ren,col].Visitada = true;
		
		bool tengoVecinos = true;
		LinkedList<Dirección> rutas;
		int tam;
		
		while(tengoVecinos) {
			rutas = CalculaDireccionesDerribables(ren, col);
			tam = rutas.Count;
		
			// Caso base
			if (tam == 0) { return; }
			Dirección d = rutas.ElementAt(RandomNumberGenerator.GetInt32(tam));
			switch(d)
			{
				case Dirección.Norte:
					_celdas[ren-1,col].DerribaPared(Dirección.Sur);
					_celdas[ren,col].DerribaPared(Dirección.Norte);
					DerribaPared(ren-1, col);
					break;
				case Dirección.Sur:
					_celdas[ren+1,col].DerribaPared(Dirección.Norte);
					_celdas[ren,col].DerribaPared(Dirección.Sur);
					DerribaPared(ren+1, col);
					break;
				case Dirección.Este:
					_celdas[ren,col+1].DerribaPared(Dirección.Oeste);
					_celdas[ren,col].DerribaPared(Dirección.Este);
					DerribaPared(ren, col+1);
					break;
				case Dirección.Oeste:
					_celdas[ren,col-1].DerribaPared(Dirección.Este);
					_celdas[ren,col].DerribaPared(Dirección.Oeste);
					DerribaPared(ren, col-1);
					break;
			}
		}
	}

	/// <summary>
	/// Recorre todas las celdas del laberinto marcándolas como
	/// no visitadas.
	/// </summary>
	public void DesmarcaLaberinto()
	{
		foreach(Celda r in _celdas)
		{
			r.Visitada = false;
		}
	}
}
