using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Laberinto;

public partial class SpriteTestWindow : Window
{

    public SpriteTestWindow()
    {
        InitializeComponent();
        EncuentraPizarrón();
        DibujaSprites();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// ¿No se supone que debería encontrarse solo?
    /// </summary>
    private void EncuentraPizarrón()
    {
        //Console.WriteLine(Content);
        if (Content == null)
        {
            Content = new Panel();
        }
        Panel p = (Panel)Content;
        foreach(var c in p.Children)
        {
            //Console.WriteLine(c);
            if (c.Name == "pizarrón")
            {
                pizarrón = (Canvas)c;
            }
        }
    }

    private void DibujaSprites()
    {
        Sprites sprites = new Sprites(30);
        Sprites.Sprite[] básicos = new Sprites.Sprite[9];
        for (int i = 0; i < 4; i++)
        {
            for (int numSprite = 0; numSprite < 9; numSprite++)
            {
                var s = sprites.CreaVistaDeImagen(numSprite, i * 90);
                básicos[numSprite] = s;
                Canvas.SetLeft(s, sprites.TamCelda * numSprite);
                Canvas.SetTop(s, sprites.TamCelda * i);
                pizarrón.Children.Add(s); // <- TODO ¿porqué no inicializa esta variable del xaml?
            }
        }
    }

    
}