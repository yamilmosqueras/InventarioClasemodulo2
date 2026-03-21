namespace InventarioApp.Factories;

using InventarioApp.Models;

/// <summary>
/// Factory para crear productos con validación centralizada.
/// Genera IDs automáticamente.
/// </summary>
public static class ProductoFactory
{
    private static int _nextId = 1;

    /// <summary>
    /// Crea un producto validado con ID automático.
    /// </summary>
    public static Producto Crear(
        string nombre,
        decimal precio,
        int cantidad,
        CategoriaProducto categoria = CategoriaProducto.Otros)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es requerido.", nameof(nombre));

        if (precio < 0)
            throw new ArgumentException("El precio no puede ser negativo.", nameof(precio));

        if (cantidad < 0)
            throw new ArgumentException("La cantidad no puede ser negativa.", nameof(cantidad));

        return new Producto
        {
            Id = _nextId++,
            Nombre = nombre,
            Precio = precio,
            Cantidad = cantidad,
            Categoria = categoria,
            Estado = EstadoProducto.Activo,
            FechaRegistro = DateTime.Now
        };
    }

    /// <summary>
    /// Crea un producto que requiere stock inicial > 0.
    /// </summary>
    public static Producto CrearConStock(
        string nombre,
        decimal precio,
        int cantidad,
        CategoriaProducto categoria)
    {
        if (cantidad <= 0)
            throw new ArgumentException("CrearConStock requiere cantidad > 0.", nameof(cantidad));

        return Crear(nombre, precio, cantidad, categoria);
    }
}
