# Sistema de Gestión de Inventario

Proyecto del curso **Fundamentos de .NET** - Platzi

## Módulo 4: Colecciones y LINQ

### Requisitos
- .NET 9 SDK

### Cómo ejecutar
```bash
dotnet run
```

### Estructura del Proyecto
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
    └── Repositories/
        ├── IProductoRepository.cs
        └── InMemoryProductoRepository.cs
```

### Conceptos del Módulo
- Patrón Repository (abstracción de datos)
- Interfaces para contratos
- LINQ básico: Where, Select, FirstOrDefault
- LINQ avanzado: GroupBy, OrderBy, Sum, Average
- Dictionary para acceso O(1)

### LINQ Destacado

| Método | Propósito |
|--------|-----------|
| `Where` | Filtrar elementos |
| `Select` | Transformar/proyectar |
| `OrderBy` | Ordenar |
| `GroupBy` | Agrupar |
| `Sum` | Sumar valores |
| `Average` | Calcular promedio |
| `FirstOrDefault` | Obtener primero o null |
| `Any` | ¿Existe alguno? |

### Checklist de Progreso
- [x] Módulo 1: El Ecosistema .NET
- [x] Módulo 2: Entradas, Salidas y Tipos
- [x] Módulo 3: Funciones y Modelado de Dominio
- [x] Módulo 4: Colecciones y LINQ
- [ ] Módulo 5: Archivos y Procesamiento

### Autor
Sebastian Martinez
