namespace InventarioApp.Models;

/// <summary>
/// Estado del producto en el inventario.
/// </summary>
public enum EstadoProducto
{
    /// <summary>Producto disponible para venta.</summary>
    Activo,
    
    /// <summary>Producto temporalmente no disponible.</summary>
    Inactivo,
    
    /// <summary>Producto que ya no se vende.</summary>
    Descontinuado
}
