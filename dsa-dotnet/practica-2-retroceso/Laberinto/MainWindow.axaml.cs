using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using Avalonia.Controls.Primitives;
using System;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Media;
using System.Collections.Generic;

namespace Laberinto;

public partial class MainWindow : Window
{
	private Laberinto.ClaseAgente _agente;

    public MainWindow()
    {
        InitializeComponent();
        ManejaClickEnGenera(null, null);
    }

    private void MuestraMosaicos(object sender, RoutedEventArgs e)
    {
        var dialog = new SpriteTestWindow();
        dialog.Show(this);
    }

    [System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_agente))]
    private void ManejaClickEnGenera(object? sender, RoutedEventArgs? e)
    {
        // Event handling logic goes here
        var tenemosColumnas = Int32.TryParse(columnas.Text, out int cols);
        var tenemosRenglones = Int32.TryParse(renglones.Text, out int rens);
        
        if (!tenemosColumnas || !tenemosRenglones || cols <= 0 || rens <= 0)
        {
            // Asigna valores por defecto
            cols = 10;
            rens = 5;
            columnas.Text = cols.ToString();
            renglones.Text = rens.ToString();
        }

        canvasPrincipal.Children.Clear();
        var gridLaberinto = new Laberinto(canvasPrincipal, rens, cols);
        canvasPrincipal.Children.Add(gridLaberinto);
        _agente = gridLaberinto.Agente;
        canvasPrincipal.Children.Add(_agente);
    }

    /// <summary>
    /// Mueve al agente de celda en celda según el algoritmo de retroceso.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ManejaClickEnResuelve(object sender, RoutedEventArgs e)
    {
        bool resuelto = ResuelveLaberinto();
        if (resuelto) _agente.EjecutaAnimación();
    }

    /// <summary>
    /// Resuelve el laberinto utilizando el algoritmo de retroceso.
    /// </summary>
    /// <returns>true si logró resolverlo, false en caso contrario.</returns>
    private bool ResuelveLaberinto()
    {
        /// 
		/// TODO: Esta es una función recursiva.
		///
		/// Implementa aquí lo que debe hacer el agente.
		/// Caso base: está parado sobre la meta.
		/// Caso recursivo: está en algún otro mosaico.
		///

        if (_agente.EstáEnMeta()){
            return true;
        }

        _agente.MarcaCelda();

        LinkedList<Dirección> direcciones = _agente.DireccionesPasillos();

        // Buscamos en todas las direcciones posibles 
        foreach(Dirección dir in direcciones) {
            
            // Verificamos si no hemos visitado la celta siguiente
            if(_agente.MiraSiNoVisitada(dir)) {
                // Intentamos avanzar en esa dirección, si es una pared retorna false
                if(_agente.Avanza(dir)) {

                    // Implementando recursividad
                    if(ResuelveLaberinto()) {
                        return true;
                    }

                    // En cado de no resolverse con ese llamado
                    _agente.DesmarcaCelda();
                    _agente.Avanza(dir.Opuesta()); 
                }
            }

        }
        return false;
    }

    private void ManejaClickEnReinicia(object sender, RoutedEventArgs e)
    {
        _agente.Reinicia();
    }
}
