using Microsoft.Data.Sqlite;

namespace VideojuegosApp.Data;

public static class BaseDatos
{
    private static readonly string Ruta = Path.Combine(AppContext.BaseDirectory, "videojuegos.db");

    public static SqliteConnection ObtenerConexion()
    {
        var conexion = new SqliteConnection($"Data Source={Ruta}");
        conexion.Open();
        return conexion;
    }

    public static void Inicializar()
    {
        string script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "init.sql"));
        using var conexion = ObtenerConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = script;
        comando.ExecuteNonQuery();
    }
}
