namespace InventarioApp.Repositories;

using InventarioApp.Models;

/// <summary>
/// Implementación en memoria del repositorio de productos.
/// Usa Dictionary para acceso O(1) por ID.
/// Incluye métodos LINQ para búsquedas y agregaciones.
/// </summary>
public class InMemoryProductoRepository : IProductoRepository
{
    private readonly Dictionary<int, Producto> _productos = new();

    public int Cantidad => _productos.Count;

    // ══════════════════════════════════════════════════════════════════
    // CRUD BÁSICO
    // ══════════════════════════════════════════════════════════════════

    public void Agregar(Producto producto)
    {
        _productos[producto.Id] = producto;
    }

    public Producto? ObtenerPorId(int id)
    {
        return _productos.GetValueOrDefault(id);
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        return _productos.Values;
    }

    public bool Actualizar(Producto producto)
    {
        if (!_productos.ContainsKey(producto.Id))
            return false;

        _productos[producto.Id] = producto;
        return true;
    }

    public bool Eliminar(int id)
    {
        return _productos.Remove(id);
    }

    // ══════════════════════════════════════════════════════════════════
    // BÚSQUEDAS CON LINQ
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _productos.Values.Where(p => p.Categoria == categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _productos.Values
            .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Producto> BuscarPorRangoPrecio(decimal min, decimal max)
    {
        return _productos.Values
            .Where(p => p.Precio >= min && p.Precio <= max);
    }

    public IEnumerable<string> ObtenerNombres()
    {
        return _productos.Values.Select(p => p.Nombre);
    }

    public bool HayStockBajo(int minimo = 5)
    {
        return _productos.Values.Any(p => p.Cantidad < minimo);
    }

    // ══════════════════════════════════════════════════════════════════
    // LINQ AVANZADO
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Producto> ObtenerOrdenadosPorPrecio(bool descendente = false)
    {
        return descendente
            ? _productos.Values.OrderByDescending(p => p.Precio)
            : _productos.Values.OrderBy(p => p.Precio);
    }

    public IEnumerable<Producto> ObtenerTopPorPrecio(int cantidad = 5)
    {
        return _productos.Values
            .OrderByDescending(p => p.Precio)
            .Take(cantidad);
    }

    public Dictionary<CategoriaProducto, List<Producto>> AgruparPorCategoria()
    {
        return _productos.Values
            .GroupBy(p => p.Categoria)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public Dictionary<CategoriaProducto, int> ContarPorCategoria()
    {
        return _productos.Values
            .GroupBy(p => p.Categoria)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public decimal ObtenerValorTotalInventario()
    {
        return _productos.Values.Sum(p => p.Precio * p.Cantidad);
    }

    public decimal ObtenerPrecioPromedio()
    {
        return _productos.Values.Any()
            ? _productos.Values.Average(p => p.Precio)
            : 0;
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _productos.Values
            .OrderByDescending(p => p.Precio)
            .FirstOrDefault();
    }

    public Dictionary<CategoriaProducto, decimal> ObtenerValorPorCategoria()
    {
        return _productos.Values
            .GroupBy(p => p.Categoria)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(p => p.Precio * p.Cantidad)
            );
    }

    public IEnumerable<Producto> ObtenerStockBajo(int minimo = 5)
    {
        return _productos.Values
            .Where(p => p.Cantidad < minimo)
            .OrderBy(p => p.Cantidad);
    }
}
