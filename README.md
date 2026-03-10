# Inventario App - Curso Fundamentos de .NET

Este repositorio contiene el código fuente de la aplicación **InventarioApp**, organizado por módulos del curso.

## Estructura del repositorio

Cada módulo del curso tiene su propia branch:

| Branch | Módulo | Descripción |
|--------|--------|-------------|
| `modulo-1` | Módulo 1 | Fundamentos iniciales |
| `modulo-2` | Módulo 2 | Evolución del proyecto |
| `modulo-3` | Módulo 3 | Nuevas funcionalidades |
| `modulo-4` | Módulo 4 | Características avanzadas |
| `modulo-5` | Módulo 5 | Versión final del proyecto |

## Cómo descargar un módulo específico

### Opción 1: Clonar una branch específica

```bash
git clone -b modulo-1 https://github.com/MancoMartinez/inventario-app-releases.git
```

Reemplaza `modulo-1` por la branch del módulo que desees.

### Opción 2: Cambiar entre módulos (si ya clonaste el repo)

```bash
git checkout modulo-1
```

## Cómo comparar módulos

Para ver las diferencias entre dos módulos, usa:

```bash
git diff modulo-1..modulo-2
```

Esto te mostrará todos los cambios introducidos entre el Módulo 1 y el Módulo 2.

### Comparar en GitHub

También puedes comparar directamente en GitHub visitando:

```
https://github.com/MancoMartinez/inventario-app-releases/compare/modulo-1...modulo-2
```
