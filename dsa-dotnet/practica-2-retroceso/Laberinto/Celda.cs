using Avalonia.Controls;

using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Laberinto;

internal class Celda : Canvas
{
	private readonly ISolidColorBrush ColorDeFondo = Brushes.Beige;
	private Rectangle _fondo;
    private Sprites _sprites;
    private Sprites.Sprite _sprite;

    /**
	 * Paredes en el orden:
	 * 0 - norte
	 * 1 - este
	 * 2 - sur
	 * 3 - oeste
	 * según el enum Dirección.
	 */
    internal bool[] paredes = new bool[4];
	
	private bool _visitada;
	private bool _esMeta;

    public Celda(Sprites sprites)
    {
        _sprites = sprites;
		double lado = _sprites.TamCelda;

        ClipToBounds = true;
		_fondo = new Rectangle
			{
				Width = lado,
				Height = lado,
				Fill = Brushes.Beige
			};
		Children.Add(_fondo);

        
		for(int i = 0; i < paredes.Length; i++) paredes[i] = true;
        AsignaSprite();
    }

	[System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_sprite))]
    private void AsignaSprite() {
		if (_sprite != null) Children.Remove(_sprite);
		_sprite = _sprites.ObténMosaicos(paredes);
		Children.Add(_sprite);
	}

	public bool Visitada
	{
		set
		{
			_visitada = value;
			if(_visitada)
			{
				if(_esMeta)
				{
					_fondo.Fill = Brushes.AliceBlue;
				}
				else
				{
					_fondo.Fill = Brushes.Aquamarine;
				}
			}
			else
			{
				if (_esMeta)
				{
					_fondo.Fill = Brushes.Coral;
				}
				else
				{
					_fondo.Fill = ColorDeFondo;
				}
			}
		}

		get => _visitada;
	}

	public bool EsMeta
	{
		set
		{
			_esMeta = value;
			if(_esMeta)
			{
				_fondo.Fill = Brushes.Coral;
			}
			else
			{
				_fondo.Fill = ColorDeFondo;
			}
		}

		get => _esMeta;
	}

	public void DerribaPared(Dirección d)
	{
		paredes[(int)d] = false;
		AsignaSprite();
	}
}
