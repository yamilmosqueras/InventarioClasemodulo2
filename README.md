# Sistema de Gestión de Inventario

Proyecto del curso **Fundamentos de .NET** - Platzi

## Módulo 3: Funciones y Modelado de Dominio

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
    │   ├── CategoriaProducto.cs  (enum)
    │   ├── EstadoProducto.cs     (enum)
    │   ├── Producto.cs           (class con validación)
    │   └── Proveedor.cs          (record)
    └── Factories/
        └── ProductoFactory.cs    (creación validada)
```

### Conceptos del Módulo
- Métodos con firmas claras
- Enums para estados y categorías
- Classes vs Records
- Factory Pattern para creación validada
- Guard clauses en setters

### Checklist de Progreso
- [x] Módulo 1: El Ecosistema .NET
- [x] Módulo 2: Entradas, Salidas y Tipos
- [x] Módulo 3: Funciones y Modelado de Dominio
- [ ] Módulo 4: Colecciones y LINQ
- [ ] Módulo 5: Archivos y Procesamiento

### Autor
Sebastian Martinez
