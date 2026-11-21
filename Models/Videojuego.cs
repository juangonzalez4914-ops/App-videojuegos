namespace VideojuegosApp.Models;

public class Videojuego
{
    public static readonly string[] Estados = { "Pendiente", "Jugando", "Terminado" };

    private string titulo = "";
    private string plataforma = "";
    private string genero = "";
    private double precio;
    private string estado = "Pendiente";

    public int Id { get; set; }

    public string Titulo
    {
        get => titulo;
        set => titulo = ValidarTexto(value, "El título");
    }

    public string Plataforma
    {
        get => plataforma;
        set => plataforma = ValidarTexto(value, "La plataforma");
    }

    public string Genero
    {
        get => genero;
        set => genero = ValidarTexto(value, "El género");
    }

    public double Precio
    {
        get => precio;
        set
        {
            if (value < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            precio = value;
        }
    }

    public string Estado
    {
        get => estado;
        set
        {
            if (!Estados.Contains(value))
                throw new ArgumentException("El estado no es válido.");
            estado = value;
        }
    }

    public Videojuego(int id, string titulo, string plataforma, string genero, double precio, string estado)
    {
        Id = id;
        Titulo = titulo;
        Plataforma = plataforma;
        Genero = genero;
        Precio = precio;
        Estado = estado;
    }

    private static string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{campo} es obligatorio.");
        return valor.Trim();
    }
}
