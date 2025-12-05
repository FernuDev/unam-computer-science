using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Media.Transformation;
using Avalonia.Animation;

namespace Laberinto;

/// <summary>
/// Clase para cargar los mosaicos desde el archivo de imágen svg.
/// @author blackzafiro
/// </summary>
public class Sprites
{
    private enum SpriteCode
    {
        C,                     // Sin paredes
        N, S, E, O,            // Una pared
        NE, ES, SO, ON,        // Dos paredes esquina
        NS, EO,                // Dos paredes paralelas
        NES, ESO, SON, ONE,    // Tres paredes
        NESO                   // Cuatro paredes
    }

    public class Sprite : Canvas
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="índiceDelSprite">
        /// 1 2 3
        /// 4 5 6
        /// 7 8 9
        /// </param>
        /// <param name="rotación"></param>
        public Sprite(Sprites sprites, int índiceDelSprite, int rotación)
        {
            Width = sprites._tamCelda;
            Height = sprites._tamCelda;
            ClipToBounds = true;
            RenderTransform = new MatrixTransform(Matrix.CreateRotation((Math.PI / 180) * rotación));
            var bitmap = new Bitmap(AssetLoader.Open(sprites.ArchivoMosaicosUri));
            var spriteImage = new Viewbox
            {
                Stretch = Stretch.Uniform,
                Width = sprites._anchoHojaEscalada,
                Height = sprites._altoHojaEscalada,
                Child = new Image
                {
                    Source = bitmap
                }
            };
            var mosaico = new Decorator
            {
                Child = spriteImage
            };
            int col = índiceDelSprite % XMosaicos;
            int ren = índiceDelSprite / XMosaicos;
            Canvas.SetLeft(mosaico, -sprites._anchoMosaicoEscalado * col);
            Canvas.SetTop(mosaico, -sprites._altoMosaicoEscalado * ren);
            //Console.WriteLine("(" + ren + "," + col + "," + rotación + ") * " + Width);
            Children.Add(mosaico);
        }
    }

    private const int AnchoHoja = 150;
    private const int AltoHoja = 150;
    private const int XMosaicos = 3, YMosaicos = 3;
    private const int AnchoMosaico = AnchoHoja / XMosaicos;
    private const int AltoMosaico = AltoHoja / YMosaicos;
    private const string ArchivoMosaicos = "avares://Laberinto/Assets/sprites_laberinto.png";
    private readonly Uri ArchivoMosaicosUri = new Uri(ArchivoMosaicos);
    private double _tamCelda;
    private double _anchoHojaEscalada;
    private double _altoHojaEscalada;
    private double _anchoMosaicoEscalado;
    private double _altoMosaicoEscalado;

    public double TamCelda { get => _tamCelda; }

    public Sprites(double tamCelda)
    {
        // Escala la plana de mosaicos
        _tamCelda = tamCelda;
        double escala = tamCelda / AnchoMosaico;
        _anchoHojaEscalada = AnchoHoja * escala;
        _altoHojaEscalada = AltoHoja * escala;
        _anchoMosaicoEscalado = AnchoMosaico * escala;
        _altoMosaicoEscalado = AltoMosaico * escala;
    }

    /**
    *  Utiliza un árbol de decisión para saber qué sprite/view corresponde.
    */
    private SpriteCode DeParedesACódigo(bool[] paredes)
    {
        if (paredes[(int)Dirección.Norte])
        {
            if (paredes[(int)Dirección.Este])
            {
                if (paredes[(int)Dirección.Sur])
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.NESO;
                    }
                    else
                    {
                        return SpriteCode.NES;
                    }
                }
                else
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.ONE;
                    }
                    else
                    {
                        return SpriteCode.NE;
                    }
                }
            }
            else
            {
                if (paredes[(int)Dirección.Sur])
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.SON;
                    }
                    else
                    {
                        return SpriteCode.NS;
                    }
                }
                else
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.ON;
                    }
                    else
                    {
                        return SpriteCode.N;
                    }
                }
            }
        }
        else
        {
            if (paredes[(int)Dirección.Este])
            {
                if (paredes[(int)Dirección.Sur])
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.ESO;
                    }
                    else
                    {
                        return SpriteCode.ES;
                    }
                }
                else
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.EO;
                    }
                    else
                    {
                        return SpriteCode.E;
                    }
                }
            }
            else
            {
                if (paredes[(int)Dirección.Sur])
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.SO;
                    }
                    else
                    {
                        return SpriteCode.S;
                    }
                }
                else
                {
                    if (paredes[(int)Dirección.Oeste])
                    {
                        return SpriteCode.O;
                    }
                    else
                    {
                        return SpriteCode.C;
                    }
                }
            }
        }
    }

    public Sprite CreaVistaDeImagen(int índiceDelSprite,
			                          int rotación)
    {
        return new Sprite(this, índiceDelSprite, rotación);
	}

    public Sprite ObténMosaicos(bool[] paredes)
    {
		SpriteCode code = DeParedesACódigo(paredes);
		switch(code)
        {
			// Sin paredes
			case SpriteCode.C:
				return CreaVistaDeImagen(4, 0);
				
			// Una pared
			case SpriteCode.N:
				return CreaVistaDeImagen(7, 180);
			case SpriteCode.E:
				return CreaVistaDeImagen(7, 270);
			case SpriteCode.S:
				return CreaVistaDeImagen(7, 0);
			case SpriteCode.O:
				return CreaVistaDeImagen(7, 90);
				
			// Dos paredes esquina
			case SpriteCode.NE:
				return CreaVistaDeImagen(6, 180);
			case SpriteCode.ON:
				return CreaVistaDeImagen(6, 90);
			case SpriteCode.ES:
				return CreaVistaDeImagen(6, 270);
			case SpriteCode.SO:
				return CreaVistaDeImagen(6, 0);
			// Dos paredes paralelas
			case SpriteCode.EO:
				return CreaVistaDeImagen(3, 0);
			case SpriteCode.NS:
				return  CreaVistaDeImagen(3, 90);
				
			// Tres paredes
			case SpriteCode.SON:
				return CreaVistaDeImagen(0, 0);
			case SpriteCode.ONE:
				return CreaVistaDeImagen(0, 90);
			case SpriteCode.NES:
				return CreaVistaDeImagen(0, 180);
			case SpriteCode.ESO:
				return CreaVistaDeImagen(0, 270);
				
			// Cuatro paredes
			case SpriteCode.NESO:
			default:
				return CreaVistaDeImagen(8, 0);
		}
	}
}