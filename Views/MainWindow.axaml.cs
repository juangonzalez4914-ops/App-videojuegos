using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using VideojuegosApp.Models;
using VideojuegosApp.Services;

namespace VideojuegosApp.Views;

public partial class MainWindow : Window
{
    private readonly VideojuegoServicio servicio = new();
    private int idSeleccionado = 0;

    public MainWindow()
    {
        InitializeComponent();
        CmbEstado.ItemsSource = Videojuego.Estados;
        CmbEstado.SelectedIndex = 0;
        Cargar();
    }

    private void Cargar()
    {
        try
        {
            LstVideojuegos.ItemsSource = servicio.Listar();
        }
        catch (Exception ex)
        {
            Mostrar("Error al leer la base de datos: " + ex.Message, true);
        }
    }

    private void Limpiar()
    {
        TxtTitulo.Text = "";
        TxtPlataforma.Text = "";
        TxtGenero.Text = "";
        TxtPrecio.Text = "";
        CmbEstado.SelectedIndex = 0;
        LstVideojuegos.SelectedItem = null;
        idSeleccionado = 0;
    }

    private void Mostrar(string texto, bool esError)
    {
        TxtMensaje.Text = texto;
        TxtMensaje.Foreground = esError ? Brushes.Firebrick : Brushes.SeaGreen;
    }

    private string Estado() => CmbEstado.SelectedItem?.ToString() ?? "";

    private void BtnGuardar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            servicio.Agregar(TxtTitulo.Text ?? "", TxtPlataforma.Text ?? "", TxtGenero.Text ?? "",
                TxtPrecio.Text ?? "", Estado());
            Cargar();
            Limpiar();
            Mostrar("Videojuego guardado correctamente.", false);
        }
        catch (ArgumentException ex)
        {
            Mostrar(ex.Message, true);
        }
        catch (Exception ex)
        {
            Mostrar("Error inesperado: " + ex.Message, true);
        }
    }

    private void BtnActualizar_Click(object? sender, RoutedEventArgs e)
    {
        if (idSeleccionado == 0)
        {
            Mostrar("Seleccione un videojuego de la lista para actualizar.", true);
            return;
        }
        try
        {
            servicio.Modificar(idSeleccionado, TxtTitulo.Text ?? "", TxtPlataforma.Text ?? "",
                TxtGenero.Text ?? "", TxtPrecio.Text ?? "", Estado());
            Cargar();
            Limpiar();
            Mostrar("Videojuego actualizado correctamente.", false);
        }
        catch (ArgumentException ex)
        {
            Mostrar(ex.Message, true);
        }
        catch (Exception ex)
        {
            Mostrar("Error inesperado: " + ex.Message, true);
        }
    }

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
    {
        if (idSeleccionado == 0)
        {
            Mostrar("Seleccione un videojuego de la lista para eliminar.", true);
            return;
        }
        try
        {
            servicio.Borrar(idSeleccionado);
            Cargar();
            Limpiar();
            Mostrar("Videojuego eliminado correctamente.", false);
        }
        catch (Exception ex)
        {
            Mostrar(ex.Message, true);
        }
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        Limpiar();
        Mostrar("", false);
    }

    private void Lista_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LstVideojuegos.SelectedItem is Videojuego v)
        {
            idSeleccionado = v.Id;
            TxtTitulo.Text = v.Titulo;
            TxtPlataforma.Text = v.Plataforma;
            TxtGenero.Text = v.Genero;
            TxtPrecio.Text = v.Precio.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture).Replace('.', ',');
            CmbEstado.SelectedItem = v.Estado;
        }
    }
}
