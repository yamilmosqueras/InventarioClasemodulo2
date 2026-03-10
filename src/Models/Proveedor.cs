namespace InventarioApp.Models;

/// <summary>
/// Representa un proveedor (record - inmutable por defecto).
/// Ejemplo de cuándo usar record vs class.
/// </summary>
public record Proveedor(
    int Id,
    string Nombre,
    string Email,
    string Telefono
);
