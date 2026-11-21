using System.Globalization;
using VideojuegosApp.Data;
using VideojuegosApp.Models;

namespace VideojuegosApp.Services;

public class VideojuegoServicio
{
    private readonly VideojuegoDAO dao = new();

    public List<Videojuego> Listar() => dao.ObtenerTodos();

    public void Agregar(string titulo, string plataforma, string genero, string precio, string estado)
    {
        dao.Insertar(Crear(0, titulo, plataforma, genero, precio, estado));
    }

    public void Modificar(int id, string titulo, string plataforma, string genero, string precio, string estado)
    {
        if (!dao.Actualizar(Crear(id, titulo, plataforma, genero, precio, estado)))
            throw new ArgumentException("No se encontró el registro a modificar.");
    }

    public void Borrar(int id)
    {
        if (!dao.Eliminar(id))
            throw new ArgumentException("No se encontró el registro a eliminar.");
    }

    private static Videojuego Crear(int id, string titulo, string plataforma, string genero, string precio, string estado)
    {
        string limpio = precio.Replace("$", "").Replace(" ", "").Replace(".", "").Replace(',', '.');
        if (!double.TryParse(limpio, NumberStyles.Float, CultureInfo.InvariantCulture, out double valor))
            throw new ArgumentException("El precio debe ser un número válido.");
        return new Videojuego(id, titulo, plataforma, genero, valor, estado);
    }
}
