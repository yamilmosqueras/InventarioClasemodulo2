namespace InventarioApp.Infrastructure;

/// <summary>
/// Clase de utilidad para operaciones de archivos.
/// Centraliza toda la lógica de I/O en un solo lugar.
/// 
/// Métodos de System.IO utilizados:
/// - File.WriteAllText: Crea/sobreescribe archivo
/// - File.ReadAllText: Lee todo el contenido
/// - File.AppendAllText: Agrega al final
/// - File.Exists: Verifica existencia
/// - File.Delete: Elimina archivo
/// - File.ReadAllLines: Lee línea por línea
/// - File.WriteAllLines: Escribe líneas
/// - Directory.CreateDirectory: Crea carpetas
/// - Directory.GetFiles: Lista archivos con patrón
/// </summary>
public class FileManager
{
    public void Escribir(string ruta, string contenido)
    {
        File.WriteAllText(ruta, contenido);
    }

    public string Leer(string ruta)
    {
        return File.ReadAllText(ruta);
    }

    public void Agregar(string ruta, string contenido)
    {
        File.AppendAllText(ruta, contenido);
    }

    public bool Existe(string ruta)
    {
        return File.Exists(ruta);
    }

    public void Eliminar(string ruta)
    {
        if (Existe(ruta))
        {
            File.Delete(ruta);
        }
    }

    public string[] LeerLineas(string ruta)
    {
        return File.ReadAllLines(ruta);
    }

    public void EscribirLineas(string ruta, IEnumerable<string> lineas)
    {
        File.WriteAllLines(ruta, lineas);
    }

    public void CrearDirectorio(string ruta)
    {
        Directory.CreateDirectory(ruta);
    }

    public string[] ObtenerArchivos(string directorio, string patron = "*")
    {
        if (!Directory.Exists(directorio))
            return Array.Empty<string>();

        return Directory.GetFiles(directorio, patron);
    }
}
