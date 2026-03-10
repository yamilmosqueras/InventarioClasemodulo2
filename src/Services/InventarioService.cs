namespace InventarioApp.Services;

using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;
using InventarioApp.Factories;

/// <summary>
/// Servicio Facade que orquesta toda la lógica del inventario.
/// Integra: Repository (memoria) + Storage (disco) + Factory (creación) + Reportes.
/// </summary>
public class InventarioService
{
    private readonly InMemoryProductoRepository _repository;
    private readonly JsonInventarioStorage _storage;
    private readonly string _rutaArchivo;

    public InventarioService(string rutaArchivo = "inventario.json")
    {
        _repository = new InMemoryProductoRepository();
        _storage = new JsonInventarioStorage();
        _rutaArchivo = rutaArchivo;

        // Cargar datos existentes al iniciar
        CargarInventario();
    }

    // ══════════════════════════════════════════════════════════════════
    // MÉTODOS PRIVADOS
    // ══════════════════════════════════════════════════════════════════

    private void CargarInventario()
    {
        var productos = _storage.Cargar(_rutaArchivo);
        foreach (var producto in productos)
        {
            _repository.Agregar(producto);
        }

        if (productos.Count > 0)
        {
            Console.WriteLine($"✓ Cargados {productos.Count} productos desde {_rutaArchivo}");
        }
    }

    private void Persistir()
    {
        // Crear backup antes de sobrescribir
        _storage.CrearBackup(_rutaArchivo);

        // Guardar estado actual
        var productos = _repository.ObtenerTodos().ToList();
        _storage.Guardar(productos, _rutaArchivo);
    }

    // ══════════════════════════════════════════════════════════════════
    // OPERACIONES CRUD
    // ══════════════════════════════════════════════════════════════════

    public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
        _repository.Agregar(producto);
        Persistir();
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Producto? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public bool Actualizar(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = _repository.ObtenerPorId(id);
        if (producto == null) return false;

        producto.Nombre = nombre;
        producto.Precio = precio;
        producto.Cantidad = cantidad;
        producto.Categoria = categoria;

        _repository.Actualizar(producto);
        Persistir();
        return true;
    }

    public bool Eliminar(int id)
    {
        bool eliminado = _repository.Eliminar(id);
        if (eliminado)
        {
            Persistir();
        }
        return eliminado;
    }

    // ══════════════════════════════════════════════════════════════════
    // BÚSQUEDAS
    // ══════════════════════════════════════════════════════════════════

    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _repository.BuscarPorCategoria(categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _repository.BuscarPorNombre(nombre);
    }

    public IEnumerable<Producto> ObtenerStockBajo(int minimo = 5)
    {
        return _repository.ObtenerStockBajo(minimo);
    }

    // ══════════════════════════════════════════════════════════════════
    // ESTADÍSTICAS
    // ══════════════════════════════════════════════════════════════════

    public decimal ObtenerValorTotalInventario()
    {
        return _repository.ObtenerValorTotalInventario();
    }

    public decimal ObtenerPrecioPromedio()
    {
        return _repository.ObtenerPrecioPromedio();
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _repository.ObtenerProductoMasCaro();
    }

    public int ObtenerCantidadProductos()
    {
        return _repository.Cantidad;
    }

    // ══════════════════════════════════════════════════════════════════
    // REPORTES (usando GeneradorReportes)
    // ══════════════════════════════════════════════════════════════════

    public string GenerarResumen()
    {
        var generador = new GeneradorReportes(_repository.ObtenerTodos());
        return generador.GenerarResumen();
    }

    public string GenerarReporteStockBajo(int umbral = 5)
    {
        var generador = new GeneradorReportes(_repository.ObtenerTodos());
        return generador.GenerarReporteStockBajo(umbral);
    }

    public string GenerarTopProductos(int cantidad = 5)
    {
        var generador = new GeneradorReportes(_repository.ObtenerTodos());
        return generador.GenerarTopProductos(cantidad);
    }

    public string ExportarCsv()
    {
        var generador = new GeneradorReportes(_repository.ObtenerTodos());
        return generador.ExportarCsv();
    }

    public string ExportarResumenJson()
    {
        var generador = new GeneradorReportes(_repository.ObtenerTodos());
        return generador.ExportarResumenJson();
    }
}
