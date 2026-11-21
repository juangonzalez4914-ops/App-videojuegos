using Microsoft.Data.Sqlite;
using VideojuegosApp.Models;

namespace VideojuegosApp.Data;

public class VideojuegoDAO
{
    public void Insertar(Videojuego v)
    {
        using var conexion = BaseDatos.ObtenerConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText =
            "INSERT INTO videojuegos (titulo, plataforma, genero, precio, estado) " +
            "VALUES ($titulo, $plataforma, $genero, $precio, $estado)";
        AgregarParametros(comando, v);
        comando.ExecuteNonQuery();
    }

    public List<Videojuego> ObtenerTodos()
    {
        var lista = new List<Videojuego>();
        using var conexion = BaseDatos.ObtenerConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText =
            "SELECT id, titulo, plataforma, genero, precio, estado FROM videojuegos ORDER BY titulo";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Videojuego(
                lector.GetInt32(0),
                lector.GetString(1),
                lector.GetString(2),
                lector.GetString(3),
                lector.GetDouble(4),
                lector.GetString(5)));
        }
        return lista;
    }

    public bool Actualizar(Videojuego v)
    {
        using var conexion = BaseDatos.ObtenerConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText =
            "UPDATE videojuegos SET titulo = $titulo, plataforma = $plataforma, genero = $genero, " +
            "precio = $precio, estado = $estado WHERE id = $id";
        AgregarParametros(comando, v);
        comando.Parameters.AddWithValue("$id", v.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = BaseDatos.ObtenerConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM videojuegos WHERE id = $id";
        comando.Parameters.AddWithValue("$id", id);
        return comando.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(SqliteCommand comando, Videojuego v)
    {
        comando.Parameters.AddWithValue("$titulo", v.Titulo);
        comando.Parameters.AddWithValue("$plataforma", v.Plataforma);
        comando.Parameters.AddWithValue("$genero", v.Genero);
        comando.Parameters.AddWithValue("$precio", v.Precio);
        comando.Parameters.AddWithValue("$estado", v.Estado);
    }
}
