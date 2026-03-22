namespace InventarioApp.Infrastructure;

using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Models;

/// <summary>
/// Persistencia de inventario en formato JSON.
/// Usa System.Text.Json (nativo de .NET, muy rápido).
/// </summary>
public class JsonInventarioStorage
{
    private readonly FileManager _fileManager;
    private readonly JsonSerializerOptions _options;

    public JsonInventarioStorage()
    {
        _fileManager = new FileManager();

        // Configuración del serializador
        _options = new JsonSerializerOptions
        {
            // JSON legible con saltos de línea e indentación
            WriteIndented = true,

            // "nombre" en JSON = "Nombre" en C# (case insensitive)
            PropertyNameCaseInsensitive = true,

            // Enums como strings ("Electronica") en vez de números (0)
            Converters = { new JsonStringEnumConverter() }
        };
    }

    /// <summary>
    /// Serializa lista de productos a JSON y guarda en archivo.
    /// </summary>
    public void Guardar(List<Producto> productos, string ruta)
    {
        string json = JsonSerializer.Serialize(productos, _options);
        _fileManager.Escribir(ruta, json);
    }

    /// <summary>
    /// Lee archivo JSON y deserializa a lista de productos.
    /// Retorna lista vacía si el archivo no existe o está vacío.
    /// </summary>
    public List<Producto> Cargar(string ruta)
    {
        if (!_fileManager.Existe(ruta))
            return new List<Producto>();

        string json = _fileManager.Leer(ruta);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Producto>();

        // ?? new List<Producto>() es un salvavidas si deserializar retorna null
        return JsonSerializer.Deserialize<List<Producto>>(json, _options)
               ?? new List<Producto>();
    }

    /// <summary>
    /// Crea backup con timestamp antes de sobrescribir.
    /// Patrón defensivo para evitar pérdida de datos.
    /// </summary>
    public void CrearBackup(string ruta)
    {
        if (!_fileManager.Existe(ruta))
            return;

        string? directorio = Path.GetDirectoryName(ruta);
        string nombreSinExtension = Path.GetFileNameWithoutExtension(ruta);
        string extension = Path.GetExtension(ruta);
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        string rutaBackup = Path.Combine(
            directorio ?? ".",
            $"{nombreSinExtension}_backup_{timestamp}{extension}"
        );

        File.Copy(ruta, rutaBackup);
    }
}
