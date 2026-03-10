// SISTEMA DE INVENTARIO - Módulo 1 Completo
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine();
Console.WriteLine($"Versión: {version}");
Console.WriteLine($".NET: {Environment.Version}");
Console.WriteLine($"Sistema: {Environment.OSVersion.Platform}");
Console.WriteLine();
Console.WriteLine("📁 Estructura:");
Console.WriteLine("   ✓ Configuración .csproj");
Console.WriteLine("   ✓ Estructura src/Models/");
Console.WriteLine("   ✓ .gitignore configurado");
Console.WriteLine("   ✓ README.md documentado");
Console.WriteLine();
Console.WriteLine("═══════════════════════════════════════");
Console.WriteLine("  ✓ MÓDULO 1 COMPLETADO");
Console.WriteLine("  → Siguiente: Módulo 2 - CLI interactiva");
Console.WriteLine("═══════════════════════════════════════");
