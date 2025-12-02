# Sistema de Control de Ventas

Sistema de control de ventas desarrollado en C# con .NET 8.0 y MySQL.

## Estructura del Proyecto

```
SistemaControlVentas/
├── Controllers/      # Controladores de la API
├── Data/            # Contexto de base de datos y configuración de Entity Framework
├── DTOs/            # Data Transfer Objects
├── Migrations/      # Migraciones de Entity Framework
├── Models/          # Modelos de datos (entidades)
├── Repositories/    # Repositorios para acceso a datos
├── Services/        # Servicios de lógica de negocio
└── Config/          # Archivos de configuración adicionales
```

## Requisitos

- .NET 8.0 SDK
- MySQL Server 8.0 o superior
- Visual Studio 2022 o VS Code (recomendado)

## Configuración

1. Actualiza la cadena de conexión en `appsettings.json` con tus credenciales de MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaControlVentas;User=root;Password=tu_password;Port=3306;"
}
```

2. Restaura las dependencias:

```bash
dotnet restore
```

3. Ejecuta las migraciones (cuando estén creadas):

```bash
dotnet ef database update
```

## Ejecutar el Proyecto

```bash
dotnet run
```

La API estará disponible en `https://localhost:5001` o `http://localhost:5000`

## Dependencias Principales

- **Pomelo.EntityFrameworkCore.MySql**: Proveedor de MySQL para Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design**: Herramientas de diseño para EF Core
- **Swashbuckle.AspNetCore**: Documentación Swagger/OpenAPI

