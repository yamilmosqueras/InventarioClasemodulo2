// SISTEMA DE INVENTARIO - Módulo 3 Completo
using System.Reflection;
using InventarioApp.Models;
using InventarioApp.Factories;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

// Lista temporal de productos (se reemplazará por Repository en Módulo 4)
var productos = new List<Producto>();

MostrarBanner();
Console.WriteLine("Comandos: listar, agregar, buscar, salir");
Console.WriteLine();

bool continuar = true;
while (continuar)
{
    var comando = LeerEntrada("inventario> ");
    continuar = ProcesarComando(comando);
}

Environment.Exit(0);

// ══════════════════════════════════════════════════════════════════
// MÉTODOS LOCALES
// ══════════════════════════════════════════════════════════════════

bool ProcesarComando(string comando)
{
    switch (comando)
    {
        case "salir":
        case "exit":
        case "q":
            Console.WriteLine("¡Hasta luego!");
            return false;

        case "listar":
            ListarProductos();
            break;

        case "agregar":
            AgregarProducto();
            break;

        case "buscar":
            BuscarProducto();
            break;

        case "":
            break;

        default:
            Console.WriteLine($"❌ Comando '{comando}' no reconocido");
            Console.WriteLine("   Use: listar, agregar, buscar, salir");
            break;
    }

    Console.WriteLine();
    return true;
}

void MostrarBanner()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine($"Versión: {version}");
    Console.WriteLine($".NET: {Environment.Version}");
    Console.WriteLine();
}

string LeerEntrada(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()?.Trim().ToLower() ?? "";
}

void ListarProductos()
{
    if (productos.Count == 0)
    {
        Console.WriteLine("📦 No hay productos en el inventario.");
        return;
    }

    Console.WriteLine("\n=== PRODUCTOS ===");
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Nombre} | ${p.Precio:F2} | Cant: {p.Cantidad} | Total: ${p.ValorTotal:F2}");
    }
    Console.WriteLine($"\nTotal: {productos.Count} producto(s)");
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
        productos.Add(producto);
        Console.WriteLine($"\n✓ Producto '{producto.Nombre}' agregado con ID {producto.Id}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}");
    }
}

void BuscarProducto()
{
    Console.WriteLine("🔍 Función buscar (se implementará completamente en Módulo 4)");
    
    Console.Write("\nBuscar por nombre: ");
    string termino = Console.ReadLine() ?? "";

    var encontrados = productos
        .Where(p => p.Nombre.Contains(termino, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (encontrados.Count == 0)
    {
        Console.WriteLine($"No se encontraron productos con '{termino}'");
        return;
    }

    Console.WriteLine($"\n=== {encontrados.Count} resultado(s) ===");
    foreach (var p in encontrados)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Nombre} | ${p.Precio:F2}");
    }
}
