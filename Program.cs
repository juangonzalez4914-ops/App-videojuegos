using System.Globalization;
using Avalonia;

namespace VideojuegosApp;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var cultura = new CultureInfo("es-CO");
        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.CurrentCulture = cultura;
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
}
