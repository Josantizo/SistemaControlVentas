# Sistema de Control de Ventas

Sistema de control de ventas desarrollado en C# con .NET 8.0 y MySQL para restaurantes. Permite gestionar ventas, productos, usuarios y generar reportes en Excel.

## Características

- **Gestión de Usuarios**: Crear y administrar usuarios con roles (Administrador y Usuario)
- **Gestión de Productos**: Administrar el menú con productos, precios y descripciones
- **Gestión de Ventas**: Registrar ventas con productos y calcular totales automáticamente
- **Secciones del Restaurante**: 
  - Comedor
  - Para Llevar
  - Autoservicio
  - A Domicilio
- **Reportes en Excel**: Generar reportes diarios, semanales, mensuales y personalizados
- **Búsqueda de Productos**: Buscar productos por nombre con precio predefinido

## Estructura del Proyecto

```
SistemaControlVentas/
├── Controllers/          # Controladores de la API
│   ├── UsuariosController.cs
│   ├── ProductosController.cs
│   ├── VentasController.cs
│   └── ReportesController.cs
├── Data/                 # Contexto de base de datos
│   └── ApplicationDbContext.cs
├── DTOs/                 # Data Transfer Objects
│   ├── UsuarioDTO.cs
│   ├── ProductoDTO.cs
│   ├── VentaDTO.cs
│   └── ReporteDTO.cs
├── Models/               # Modelos de datos (entidades)
│   ├── Enums/
│   │   ├── Rol.cs
│   │   └── SeccionVenta.cs
│   ├── Usuario.cs
│   ├── Producto.cs
│   ├── Venta.cs
│   └── DetalleVenta.cs
├── Repositories/         # Repositorios para acceso a datos
│   ├── IUsuarioRepository.cs
│   ├── UsuarioRepository.cs
│   ├── IProductoRepository.cs
│   ├── ProductoRepository.cs
│   ├── IVentaRepository.cs
│   └── VentaRepository.cs
├── Services/             # Servicios de lógica de negocio
│   ├── IUsuarioService.cs
│   ├── UsuarioService.cs
│   ├── IProductoService.cs
│   ├── ProductoService.cs
│   ├── IVentaService.cs
│   ├── VentaService.cs
│   ├── IReporteExcelService.cs
│   └── ReporteExcelService.cs
└── Program.cs            # Configuración de la aplicación
```

## Requisitos

- .NET 8.0 SDK
- MySQL Server 8.0 o superior
- Visual Studio 2022 o VS Code (recomendado)

## Configuración

1. **Actualiza la cadena de conexión** en `appsettings.json` con tus credenciales de MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaControlVentas;User=root;Password=tu_password;Port=3306;"
}
```

2. **Restaura las dependencias**:

```bash
dotnet restore
```

3. **Crea la migración inicial**:

```bash
dotnet ef migrations add InitialCreate
```

4. **Ejecuta las migraciones** para crear la base de datos:

```bash
dotnet ef database update
```

## Ejecutar el Proyecto

```bash
dotnet run
```

La API estará disponible en `https://localhost:5001` o `http://localhost:5000`

Swagger UI estará disponible en: `https://localhost:5001/swagger` (en modo desarrollo)

## Endpoints Principales

### Usuarios
- `GET /api/usuarios` - Listar todos los usuarios
- `GET /api/usuarios/{id}` - Obtener usuario por ID
- `POST /api/usuarios` - Crear nuevo usuario
- `PUT /api/usuarios/{id}` - Actualizar usuario
- `DELETE /api/usuarios/{id}` - Eliminar (desactivar) usuario

### Productos
- `GET /api/productos` - Listar todos los productos
- `GET /api/productos/activos` - Listar productos activos
- `GET /api/productos/{id}` - Obtener producto por ID
- `GET /api/productos/buscar/{nombre}` - Buscar productos por nombre
- `POST /api/productos` - Crear nuevo producto
- `PUT /api/productos/{id}` - Actualizar producto
- `DELETE /api/productos/{id}` - Eliminar (desactivar) producto

### Ventas
- `GET /api/ventas` - Listar todas las ventas
- `GET /api/ventas/{id}` - Obtener venta por ID
- `POST /api/ventas` - Crear nueva venta
- `GET /api/ventas/fecha?fechaInicio={}&fechaFin={}` - Obtener ventas por rango de fechas
- `GET /api/ventas/usuario/{usuarioId}` - Obtener ventas por usuario
- `GET /api/ventas/seccion/{seccion}` - Obtener ventas por sección

### Reportes (Excel)
- `GET /api/reportes/diario?fecha={}` - Generar reporte diario
- `GET /api/reportes/semanal?fechaInicioSemana={}` - Generar reporte semanal
- `GET /api/reportes/mensual?año={}&mes={}` - Generar reporte mensual
- `POST /api/reportes/personalizado` - Generar reporte personalizado con filtros

## Dependencias Principales

- **Pomelo.EntityFrameworkCore.MySql**: Proveedor de MySQL para Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design**: Herramientas de diseño para EF Core
- **Microsoft.EntityFrameworkCore.Tools**: Herramientas de migración
- **Swashbuckle.AspNetCore**: Documentación Swagger/OpenAPI
- **ClosedXML**: Generación de archivos Excel
- **BCrypt.Net-Next**: Hash de contraseñas

## Roles

- **Administrador**: Puede gestionar usuarios, productos, ventas y generar reportes
- **Usuario**: Puede crear ventas y consultar información básica

## Secciones del Restaurante

1. **Comedor** - Ventas en el comedor
2. **Para Llevar** - Ventas para llevar
3. **Autoservicio** - Ventas de autoservicio
4. **A Domicilio** - Ventas a domicilio

