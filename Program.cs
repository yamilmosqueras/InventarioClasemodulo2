// ════════════════════════════════════════════════════════════════════
// SISTEMA DE INVENTARIO - VERSIÓN FINAL
// Módulo 5: Archivos y Procesamiento
// ════════════════════════════════════════════════════════════════════

using InventarioApp.Models;
using InventarioApp.Services;

// El servicio carga automáticamente desde inventario.json si existe
var servicio = new InventarioService();
bool activo = true;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
Console.WriteLine("║     (persistencia automática JSON)   ║");
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
        case "9": ExportarJson(); break;
        case "10":
            activo = false;
            Console.WriteLine("\n✓ Datos guardados. ¡Hasta luego!");
            break;
        default:
            Console.WriteLine("\n⚠ Opción no válida.");
            break;
    }
}

// ════════════════════════════════════════════════════════════════════
// MÉTODOS LOCALES
// ════════════════════════════════════════════════════════════════════

void MostrarMenu()
{
    Console.WriteLine("\n╔═══════════════════════════════════════╗");
    Console.WriteLine("║           MENÚ PRINCIPAL              ║");
    Console.WriteLine("╠═══════════════════════════════════════╣");
    Console.WriteLine("║  1. Agregar producto                  ║");
    Console.WriteLine("║  2. Listar productos                  ║");
    Console.WriteLine("║  3. Buscar por ID                     ║");
    Console.WriteLine("║  4. Eliminar producto                 ║");
    Console.WriteLine("║  5. Buscar por categoría              ║");
    Console.WriteLine("║  6. Ver estadísticas                  ║");
    Console.WriteLine("║  7. Ver alertas stock bajo            ║");
    Console.WriteLine("║  8. Exportar a CSV                    ║");
    Console.WriteLine("║  9. Exportar resumen JSON             ║");
    Console.WriteLine("║ 10. Salir                             ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");
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
        servicio.AgregarProducto(nombre, precio, cantidad, categoria);
        Console.WriteLine("\n✓ Producto agregado y guardado en inventario.json");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}");
    }
}

void ListarProductos()
{
    var productos = servicio.ObtenerTodos().ToList();

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

    var producto = servicio.ObtenerPorId(id);

    if (producto == null)
    {
        Console.WriteLine($"⚠ No existe producto con ID {id}");
        return;
    }

    Console.WriteLine($"\n--- Producto #{producto.Id} ---");
    Console.WriteLine($"Nombre:      {producto.Nombre}");
    Console.WriteLine($"Precio:      ${producto.Precio:F2}");
    Console.WriteLine($"Cantidad:    {producto.Cantidad}");
    Console.WriteLine($"Valor Total: ${producto.ValorTotal:F2}");
    Console.WriteLine($"Categoría:   {producto.Categoria}");
    Console.WriteLine($"Estado:      {producto.Estado}");
    Console.WriteLine($"Registrado:  {producto.FechaRegistro:yyyy-MM-dd HH:mm}");
}

void EliminarProducto()
{
    Console.Write("\nID a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("⚠ ID inválido.");
        return;
    }

    var producto = servicio.ObtenerPorId(id);
    if (producto == null)
    {
        Console.WriteLine($"⚠ No existe producto con ID {id}");
        return;
    }

    Console.Write($"¿Eliminar '{producto.Nombre}'? (s/n): ");
    if (Console.ReadLine()?.ToLower() == "s")
    {
        servicio.Eliminar(id);
        Console.WriteLine("✓ Producto eliminado y guardado.");
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

    var productos = servicio.BuscarPorCategoria(categoria).ToList();

    if (productos.Count == 0)
    {
        Console.WriteLine($"\nNo hay productos en {categoria}.");
        return;
    }

    Console.WriteLine($"\n=== PRODUCTOS EN {categoria.ToString().ToUpper()} ===");
    foreach (var p in productos)
    {
        Console.WriteLine($"  {p.Id,3}. {p.Nombre,-20} ${p.Precio,8:F2} x {p.Cantidad}");
    }
}

void MostrarEstadisticas()
{
    Console.WriteLine("\n╔══════════════════════════════════════╗");
    Console.WriteLine("║           ESTADÍSTICAS               ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine($"  Productos totales:     {servicio.ObtenerCantidadProductos()}");
    Console.WriteLine($"  Valor total:           ${servicio.ObtenerValorTotalInventario():F2}");
    Console.WriteLine($"  Precio promedio:       ${servicio.ObtenerPrecioPromedio():F2}");

    var masCaro = servicio.ObtenerProductoMasCaro();
    if (masCaro != null)
    {
        Console.WriteLine($"  Producto más caro:     {masCaro.Nombre} (${masCaro.Precio:F2})");
    }

    Console.WriteLine($"\n{servicio.GenerarTopProductos(3)}");
}

void MostrarStockBajo()
{
    Console.WriteLine($"\n{servicio.GenerarReporteStockBajo(5)}");
}

void ExportarCsv()
{
    Console.WriteLine("\n=== EXPORTAR CSV ===");
    Console.WriteLine(servicio.ExportarCsv());
}

void ExportarJson()
{
    Console.WriteLine("\n=== RESUMEN JSON ===");
    Console.WriteLine(servicio.ExportarResumenJson());
}
