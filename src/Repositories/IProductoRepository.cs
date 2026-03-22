namespace InventarioApp.Repositories;

using InventarioApp.Models;

/// <summary>
/// Contrato para el repositorio de productos.
/// Define las operaciones básicas de almacenamiento.
/// </summary>
public interface IProductoRepository
{
    /// <summary>Agrega un producto al repositorio.</summary>
    void Agregar(Producto producto);

    /// <summary>Obtiene un producto por su ID.</summary>
    Producto? ObtenerPorId(int id);

    /// <summary>Obtiene todos los productos.</summary>
    IEnumerable<Producto> ObtenerTodos();

    /// <summary>Actualiza un producto existente.</summary>
    bool Actualizar(Producto producto);

    /// <summary>Elimina un producto por su ID.</summary>
    bool Eliminar(int id);

    /// <summary>Cantidad total de productos.</summary>
    int Cantidad { get; }
}