using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VideojuegosApp.Data;
using VideojuegosApp.Views;

namespace VideojuegosApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime escritorio)
        {
            BaseDatos.Inicializar();
            escritorio.MainWindow = new MainWindow();
        }
        base.OnFrameworkInitializationCompleted();
    }
}
