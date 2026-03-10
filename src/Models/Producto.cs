namespace InventarioApp.Models;

/// <summary>
/// Representa un producto en el inventario.
/// Incluye validación en setters (guard clauses).
/// </summary>
public class Producto
{
    private string _nombre = "";
    private decimal _precio;
    private int _cantidad;

    public int Id { get; set; }

    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no puede estar vacío.");
            _nombre = value;
        }
    }

    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            _precio = value;
        }
    }

    public int Cantidad
    {
        get => _cantidad;
        set
        {
            if (value < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");
            _cantidad = value;
        }
    }

    public CategoriaProducto Categoria { get; set; } = CategoriaProducto.Otros;
    public EstadoProducto Estado { get; set; } = EstadoProducto.Activo;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    /// <summary>
    /// Propiedad calculada: Precio × Cantidad
    /// </summary>
    public decimal ValorTotal => Precio * Cantidad;
}
