// SISTEMA DE INVENTARIO - Módulo 2 Completo
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

int cantidadProductos = 0;
decimal valorTotalInventario = 0.00m;
bool sistemaActivo = true;

if (args.Length > 0)
{
    switch (args[0].ToLower())
    {
        case "--help":
        case "-h":
            MostrarAyuda();
            Environment.Exit(0);
            break;
        case "--version":
        case "-v":
            Console.WriteLine($"InventarioApp v{version}");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine($"Error: Comando '{args[0]}' no reconocido");
            Console.WriteLine("Use --help para ver comandos disponibles.");
            Environment.Exit(2);
            break;
    }
}

MostrarBanner();
Console.WriteLine("Comandos disponibles: listar, agregar, buscar, salir");
Console.WriteLine();

while (sistemaActivo)
{
    Console.Write("inventario> ");
    string? entrada = Console.ReadLine();
    string comando = entrada?.Trim().ToLower() ?? "salir";

    switch (comando)
    {
        case "salir":
        case "exit":
        case "q":
            sistemaActivo = false;
            Console.WriteLine("¡Hasta luego!");
            break;
        case "listar":
            Console.WriteLine($"📦 Productos en inventario: {cantidadProductos}");
            Console.WriteLine($"💰 Valor total: ${valorTotalInventario:N2}");
            break;
        case "agregar":
            Console.WriteLine("📝 Función agregar (se implementará en Módulo 3)");
            break;
        case "buscar":
            Console.WriteLine("🔍 Función buscar (se implementará en Módulo 4)");
            break;
        case "":
            break;
        default:
            Console.WriteLine($"❌ Comando '{comando}' no reconocido");
            Console.WriteLine("   Use: listar, agregar, buscar, salir");
            break;
    }

    if (sistemaActivo)
        Console.WriteLine();
}

Environment.Exit(0);

void MostrarBanner()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine($"Versión: {version}");
    Console.WriteLine($".NET: {Environment.Version}");
    Console.WriteLine($"Sistema: {Environment.OSVersion.Platform}");
    Console.WriteLine();
}

void MostrarAyuda()
{
    Console.WriteLine("USO: dotnet run [opciones]");
    Console.WriteLine();
    Console.WriteLine("OPCIONES:");
    Console.WriteLine("  --help, -h       Muestra esta ayuda");
    Console.WriteLine("  --version, -v    Muestra la versión");
    Console.WriteLine();
    Console.WriteLine("COMANDOS INTERACTIVOS:");
    Console.WriteLine("  listar           Lista productos del inventario");
    Console.WriteLine("  agregar          Agrega un nuevo producto");
    Console.WriteLine("  buscar           Busca productos");
    Console.WriteLine("  salir            Sale del programa");
    Console.WriteLine();
    Console.WriteLine("EJEMPLOS:");
    Console.WriteLine("  dotnet run");
    Console.WriteLine("  dotnet run --version");
}
