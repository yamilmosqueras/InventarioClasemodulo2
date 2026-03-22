// SISTEMA DE INVENTARIO - Módulo 4 Completo
using InventarioApp.Models;
using InventarioApp.Factories;
using InventarioApp.Repositories;

var repositorio = new InMemoryProductoRepository();
bool activo = true;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
Console.WriteLine("║        Módulo 4: LINQ y Patrones     ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine();

while (activo)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1": AgregarProducto(); break;
        case "2": ListarProductos(); break;
        case "3": BuscarPorId(); break;
        case "4": EliminarProducto(); break;
        case "5": BuscarPorCategoria(); break;
        case "6": MostrarEstadisticas(); break;
        case "7": MostrarStockBajo(); break;
        case "8": ExportarCsv(); break;
        case "9":
            activo = false;
            Console.WriteLine("\n¡Hasta luego!");
            break;
        default:
            Console.WriteLine("\n⚠ Opción no válida.");
            break;
    }
}

// ══════════════════════════════════════════════════════════════════
// MÉTODOS LOCALES
// ══════════════════════════════════════════════════════════════════

void MostrarMenu()
{
    Console.WriteLine("\n╔══════════════════════════════════════╗");
    Console.WriteLine("║           MENÚ PRINCIPAL             ║");
    Console.WriteLine("╠══════════════════════════════════════╣");
    Console.WriteLine("║  1. Agregar producto                 ║");
    Console.WriteLine("║  2. Listar productos                 ║");
    Console.WriteLine("║  3. Buscar por ID                    ║");
    Console.WriteLine("║  4. Eliminar producto                ║");
    Console.WriteLine("║  5. Buscar por categoría             ║");
    Console.WriteLine("║  6. Ver estadísticas                 ║");
    Console.WriteLine("║  7. Ver stock bajo                   ║");
    Console.WriteLine("║  8. Exportar CSV                     ║");
    Console.WriteLine("║  9. Salir                            ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.Write("\nSelecciona: ");
}

void AgregarProducto()
{
    Console.WriteLine("\n--- Agregar Producto ---");

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
    {
        Console.WriteLine("⚠ Precio inválido.");
        return;
    }

    Console.Write("Cantidad: ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad))
    {
        Console.WriteLine("⚠ Cantidad inválida.");
        return;
    }

    Console.WriteLine("\nCategorías: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Otros");
    Console.Write("Categoría: ");
    string catStr = Console.ReadLine() ?? "Otros";

    if (!Enum.TryParse<CategoriaProducto>(catStr, true, out var categoria))
    {
        categoria = CategoriaProducto.Otros;
    }

    try
    {
        var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
        repositorio.Agregar(producto);
        Console.WriteLine($"\n✓ Producto agregado con ID {producto.Id}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}");
    }
}

void ListarProductos()
{
    var productos = repositorio.ObtenerTodos().ToList();

    if (productos.Count == 0)
    {
        Console.WriteLine("\nNo hay productos registrados.");
        return;
    }

    Console.WriteLine("\n=== PRODUCTOS ===");
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Nombre} | ${p.Precio:F2} | Cant: {p.Cantidad} | Total: ${p.ValorTotal:F2} | {p.Categoria}");
    }
    Console.WriteLine($"\nTotal: {productos.Count} producto(s)");
}

void BuscarPorId()
{
    Console.Write("\nID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("⚠ ID inválido.");
        return;
    }

    var producto = repositorio.ObtenerPorId(id);

    if (producto == null)
    {
        Console.WriteLine($"⚠ No existe producto con ID {id}");
        return;
    }

    Console.WriteLine($"\n--- Producto #{producto.Id} ---");
    Console.WriteLine($"Nombre: {producto.Nombre}");
    Console.WriteLine($"Precio: ${producto.Precio:F2}");
    Console.WriteLine($"Cantidad: {producto.Cantidad}");
    Console.WriteLine($"Valor Total: ${producto.ValorTotal:F2}");
    Console.WriteLine($"Categoría: {producto.Categoria}");
    Console.WriteLine($"Estado: {producto.Estado}");
}

void EliminarProducto()
{
    Console.Write("\nID a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("⚠ ID inválido.");
        return;
    }

    var producto = repositorio.ObtenerPorId(id);
    if (producto == null)
    {
        Console.WriteLine($"⚠ No existe producto con ID {id}");
        return;
    }

    Console.Write($"¿Eliminar '{producto.Nombre}'? (s/n): ");
    if (Console.ReadLine()?.ToLower() == "s")
    {
        repositorio.Eliminar(id);
        Console.WriteLine("✓ Producto eliminado.");
    }
}

void BuscarPorCategoria()
{
    Console.WriteLine("\nCategorías: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Otros");
    Console.Write("Categoría: ");
    string catStr = Console.ReadLine() ?? "";

    if (!Enum.TryParse<CategoriaProducto>(catStr, true, out var categoria))
    {
        Console.WriteLine("⚠ Categoría inválida.");
        return;
    }

    var productos = repositorio.BuscarPorCategoria(categoria).ToList();

    if (productos.Count == 0)
    {
        Console.WriteLine($"\nNo hay productos en {categoria}.");
        return;
    }

    Console.WriteLine($"\n=== PRODUCTOS EN {categoria.ToString().ToUpper()} ===");
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Nombre} | ${p.Precio:F2} | Cant: {p.Cantidad}");
    }
}

void MostrarEstadisticas()
{
    Console.WriteLine("\n=== ESTADÍSTICAS ===");
    Console.WriteLine($"Productos totales: {repositorio.Cantidad}");
    Console.WriteLine($"Valor total inventario: ${repositorio.ObtenerValorTotalInventario():F2}");
    Console.WriteLine($"Precio promedio: ${repositorio.ObtenerPrecioPromedio():F2}");

    var masCaro = repositorio.ObtenerProductoMasCaro();
    if (masCaro != null)
    {
        Console.WriteLine($"Más caro: {masCaro.Nombre} (${masCaro.Precio:F2})");
    }

    Console.WriteLine("\nPor categoría:");
    foreach (var kvp in repositorio.ContarPorCategoria())
    {
        Console.WriteLine($"  {kvp.Key}: {kvp.Value} producto(s)");
    }
}

void MostrarStockBajo()
{
    var stockBajo = repositorio.ObtenerStockBajo(5).ToList();

    if (stockBajo.Count == 0)
    {
        Console.WriteLine("\n✓ No hay productos con stock bajo.");
        return;
    }

    Console.WriteLine("\n=== ALERTA: STOCK BAJO (< 5) ===");
    foreach (var p in stockBajo)
    {
        Console.WriteLine($"⚠ {p.Nombre} | Stock: {p.Cantidad} | ${p.Precio:F2}");
    }
}

void ExportarCsv()
{
    Console.WriteLine("\n=== EXPORTAR CSV ===");
    Console.WriteLine("Id,Nombre,Precio,Cantidad,Categoria,ValorTotal");

    foreach (var p in repositorio.ObtenerTodos().OrderBy(p => p.Id))
    {
        Console.WriteLine($"{p.Id},{p.Nombre},{p.Precio:F2},{p.Cantidad},{p.Categoria},{p.ValorTotal:F2}");
    }

    Console.WriteLine("\n(En módulo 5: guardaremos esto en archivo)");
}
