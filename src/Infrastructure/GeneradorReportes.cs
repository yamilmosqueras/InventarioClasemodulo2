namespace InventarioApp.Infrastructure;

using System.Text;
using System.Text.Json;
using InventarioApp.Models;

/// <summary>
/// Genera reportes en múltiples formatos.
/// Usa StringBuilder (más eficiente que concatenar strings en loop).
/// </summary>
public class GeneradorReportes
{
    private readonly IEnumerable<Producto> _productos;

    public GeneradorReportes(IEnumerable<Producto> productos)
    {
        _productos = productos;
    }

    /// <summary>
    /// Resumen general del inventario.
    /// </summary>
    public string GenerarResumen()
    {
        var sb = new StringBuilder();
        var productos = _productos.ToList();

        sb.AppendLine("╔══════════════════════════════════════╗");
        sb.AppendLine("║       RESUMEN DE INVENTARIO          ║");
        sb.AppendLine("╚══════════════════════════════════════╝");
        sb.AppendLine();
        sb.AppendLine($"  Total de productos: {productos.Count}");
        sb.AppendLine($"  Valor total:        ${productos.Sum(p => p.ValorTotal):F2}");

        if (productos.Count > 0)
        {
            sb.AppendLine($"  Precio promedio:    ${productos.Average(p => p.Precio):F2}");
            sb.AppendLine();
            sb.AppendLine("  Por categoría:");

            var porCategoria = productos
                .GroupBy(p => p.Categoria)
                .OrderByDescending(g => g.Count());

            foreach (var grupo in porCategoria)
            {
                var valor = grupo.Sum(p => p.ValorTotal);
                sb.AppendLine($"    • {grupo.Key,-15} {grupo.Count(),3} productos  ${valor,10:F2}");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Productos con stock bajo (alerta de reposición).
    /// </summary>
    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var sb = new StringBuilder();
        var stockBajo = _productos
            .Where(p => p.Cantidad < minimo)
            .OrderBy(p => p.Cantidad)
            .ToList();

        sb.AppendLine($"╔══════════════════════════════════════╗");
        sb.AppendLine($"║    ALERTA: STOCK BAJO (< {minimo})          ║");
        sb.AppendLine($"╚══════════════════════════════════════╝");
        sb.AppendLine();

        if (!stockBajo.Any())
        {
            sb.AppendLine("  ✓ No hay productos con stock bajo.");
            return sb.ToString();
        }

        foreach (var p in stockBajo)
        {
            string alerta = p.Cantidad == 0 ? "⛔ AGOTADO" : $"⚠ {p.Cantidad} unidades";
            sb.AppendLine($"  {p.Id,3}. {p.Nombre,-20} {alerta,-15} ${p.Precio:F2}");
        }

        sb.AppendLine();
        sb.AppendLine($"  Total: {stockBajo.Count} producto(s) requieren atención");

        return sb.ToString();
    }

    /// <summary>
    /// Top N productos por valor total.
    /// </summary>
    public string GenerarTopProductos(int cantidad = 5)
    {
        var sb = new StringBuilder();
        var top = _productos
            .OrderByDescending(p => p.ValorTotal)
            .Take(cantidad)
            .ToList();

        sb.AppendLine($"╔══════════════════════════════════════╗");
        sb.AppendLine($"║   TOP {cantidad} PRODUCTOS POR VALOR        ║");
        sb.AppendLine($"╚══════════════════════════════════════╝");
        sb.AppendLine();

        if (!top.Any())
        {
            sb.AppendLine("  No hay productos disponibles.");
            return sb.ToString();
        }

        int posicion = 1;
        foreach (var p in top)
        {
            sb.AppendLine($"  {posicion}. {p.Nombre,-20} ${p.ValorTotal,10:F2}");
            sb.AppendLine($"     ({p.Cantidad} × ${p.Precio:F2})");
            posicion++;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exporta a CSV (valores separados por coma).
    /// </summary>
    public string ExportarCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Nombre,Precio,Cantidad,Categoria,Estado,ValorTotal");

        foreach (var p in _productos.OrderBy(p => p.Id))
        {
            sb.AppendLine($"{p.Id},\"{p.Nombre}\",{p.Precio:F2},{p.Cantidad},{p.Categoria},{p.Estado},{p.ValorTotal:F2}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exporta resumen a JSON (para APIs o integración).
    /// </summary>
    public string ExportarResumenJson()
    {
        var productos = _productos.ToList();

        var resumen = new
        {
            FechaGeneracion = DateTime.Now,
            TotalProductos = productos.Count,
            ValorTotalInventario = productos.Sum(p => p.ValorTotal),
            PrecioPromedio = productos.Count > 0 ? productos.Average(p => p.Precio) : 0,
            ProductosPorCategoria = productos
                .GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key.ToString(), Cantidad = g.Count(), Valor = g.Sum(x => x.ValorTotal) }),
            Top5Productos = productos
                .OrderByDescending(p => p.ValorTotal)
                .Take(5)
                .Select(p => new { p.Id, p.Nombre, p.Cantidad, p.ValorTotal })
        };

        return JsonSerializer.Serialize(resumen, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
