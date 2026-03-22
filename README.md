# Sistema de Gestión de Inventario

Proyecto del curso **Fundamentos de .NET** - Platzi

## Módulo 5: Archivos y Procesamiento (VERSIÓN FINAL)

### Requisitos
- .NET 9 SDK

### Cómo ejecutar
```bash
dotnet run
```

### Archivos generados
- `inventario.json` - Base de datos (persistencia automática)
- `inventario_backup_*.json` - Backups automáticos

### Estructura del Proyecto (Final)
```
InventarioApp/
├── Program.cs
├── InventarioApp.csproj
├── .gitignore
├── README.md
└── src/
    ├── Models/
    │   ├── CategoriaProducto.cs
    │   ├── EstadoProducto.cs
    │   ├── Producto.cs
    │   └── Proveedor.cs
    ├── Factories/
    │   └── ProductoFactory.cs
    ├── Repositories/
    │   ├── IProductoRepository.cs
    │   └── InMemoryProductoRepository.cs
    ├── Services/
    │   └── InventarioService.cs
    └── Infrastructure/
        ├── FileManager.cs
        ├── JsonInventarioStorage.cs
        └── GeneradorReportes.cs
```

### Arquitectura

```
┌─────────────────────────────────────────────────────────────────┐
│                        Program.cs                               │
│                      (UI de Consola)                            │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────────┐
│                   InventarioService                             │
│              (Orquestación + Persistencia)                      │
└──────┬──────────────────┬───────────────────┬───────────────────┘
       │                  │                   │
┌──────▼──────┐   ┌───────▼───────┐   ┌───────▼───────┐
│ Repository  │   │    Factory    │   │Infrastructure │
│ (Memoria)   │   │  (Creación)   │   │   (I/O)       │
└─────────────┘   └───────────────┘   └───────────────┘
```

### Conceptos del Módulo 5
- FileManager: abstracción de operaciones de archivo
- JsonInventarioStorage: serialización/deserialización JSON
- GeneradorReportes: reportes con StringBuilder y LINQ
- InventarioService: patrón Facade con persistencia automática
- System.Text.Json: serializador nativo de .NET

### Checklist de Progreso
- [x] Módulo 1: El Ecosistema .NET
- [x] Módulo 2: Entradas, Salidas y Tipos
- [x] Módulo 3: Funciones y Modelado de Dominio
- [x] Módulo 4: Colecciones y LINQ
- [x] Módulo 5: Archivos y Procesamiento

---

## 🎓 CURSO COMPLETADO

Este proyecto demuestra comprensión estructural de .NET:
- Decisiones de diseño justificadas (class vs record, List vs Dictionary)
- Patrones aplicados donde agregan valor (Repository, Factory, Facade)
- Código mantenible y extensible

### Autor
Sebastian Martinez
