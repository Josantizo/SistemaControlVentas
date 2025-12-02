# Guía para Ejecutar el Sistema de Control de Ventas

Esta guía te ayudará a configurar y ejecutar el proyecto paso a paso.

## Prerrequisitos

1. **.NET 8.0 SDK** instalado
   - Verifica con: `dotnet --version`
   - Si no lo tienes, descárgalo de: https://dotnet.microsoft.com/download

2. **MySQL Server** instalado y corriendo
   - MySQL 8.0 o superior
   - Asegúrate de que el servicio MySQL esté activo

3. **Editor de código** (opcional pero recomendado)
   - Visual Studio 2022
   - VS Code
   - O cualquier editor que soporte C#

## Pasos para Ejecutar el Proyecto

### Paso 1: Configurar la Cadena de Conexión

Edita el archivo `appsettings.json` y configura tu cadena de conexión de MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaControlVentas;User=root;Password=TU_PASSWORD;Port=3306;"
  }
}
```

**Reemplaza:**
- `TU_PASSWORD` con tu contraseña de MySQL (si tienes una)
- El puerto si usas uno diferente al 3306
- El nombre de la base de datos si prefieres otro nombre

### Paso 2: Restaurar los Paquetes NuGet

Abre una terminal en la carpeta del proyecto y ejecuta:

```bash
dotnet restore
```

Esto descargará todos los paquetes NuGet necesarios (Entity Framework, ClosedXML, BCrypt, etc.).

### Paso 3: Crear la Migración Inicial

Ejecuta el siguiente comando para crear la migración de la base de datos:

```bash
dotnet ef migrations add InitialCreate
```

Este comando creará los archivos de migración en la carpeta `Migrations/` basándose en tus modelos.

### Paso 4: Crear la Base de Datos

Ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:

```bash
dotnet ef database update
```

**Nota:** Asegúrate de que:
- MySQL esté corriendo
- Tengas permisos para crear bases de datos
- La cadena de conexión sea correcta

Este comando creará la base de datos `SistemaControlVentas` (o la que hayas configurado) con todas las tablas necesarias.

### Paso 5: Ejecutar el Proyecto

Una vez que la base de datos esté creada, ejecuta:

```bash
dotnet run
```

O si prefieres ejecutarlo en modo desarrollo:

```bash
dotnet watch run
```

El comando `dotnet watch run` recarga automáticamente la aplicación cuando detecta cambios en el código.

### Paso 6: Acceder a la API

Una vez que el proyecto esté corriendo, verás un mensaje similar a:

```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

**Acceso a Swagger UI (Documentación de la API):**
- Abre tu navegador y ve a: `https://localhost:5001/swagger` o `http://localhost:5000/swagger`

Desde Swagger podrás:
- Ver todos los endpoints disponibles
- Probar la API directamente desde el navegador
- Ver la estructura de los datos (DTOs)

## Comandos Rápidos (Resumen)

```bash
# 1. Restaurar paquetes
dotnet restore

# 2. Crear migración
dotnet ef migrations add InitialCreate

# 3. Crear/actualizar base de datos
dotnet ef database update

# 4. Ejecutar proyecto
dotnet run

# O ejecutar en modo desarrollo (auto-reload)
dotnet watch run
```

## Verificar que Todo Funciona

1. **Verifica que MySQL está corriendo:**
   ```bash
   # En Windows (PowerShell)
   Get-Service MySQL*
   
   # O verifica en el Administrador de Tareas
   ```

2. **Verifica la conexión a MySQL:**
   - Intenta conectarte con MySQL Workbench o cualquier cliente MySQL
   - Usa las mismas credenciales de tu `appsettings.json`

3. **Verifica que la base de datos se creó:**
   - Abre MySQL Workbench
   - Deberías ver la base de datos `SistemaControlVentas`
   - Deberías ver las tablas: `Usuarios`, `Productos`, `Ventas`, `DetalleVentas`

## Problemas Comunes y Soluciones

### Error: "No se puede conectar al servidor MySQL"
- Verifica que MySQL esté corriendo
- Verifica la cadena de conexión en `appsettings.json`
- Verifica el puerto (por defecto es 3306)
- Verifica el usuario y contraseña

### Error: "No se puede crear la base de datos"
- Asegúrate de tener permisos para crear bases de datos
- Intenta crear la base de datos manualmente en MySQL:
  ```sql
  CREATE DATABASE SistemaControlVentas;
  ```

### Error: "dotnet ef no se reconoce como comando"
- Instala las herramientas de Entity Framework:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Error de certificado HTTPS en el navegador
- Ejecuta este comando para confiar en el certificado de desarrollo:
  ```bash
  dotnet dev-certs https --trust
  ```

## Próximos Pasos

Una vez que el proyecto esté corriendo:

1. **Crear un usuario administrador:**
   - Usa Swagger o Postman para crear un usuario
   - POST a `/api/usuarios`
   - Usa Rol = 1 (Administrador)

2. **Crear productos:**
   - POST a `/api/productos` para agregar productos al menú

3. **Crear ventas:**
   - POST a `/api/ventas` para registrar ventas

4. **Generar reportes:**
   - GET a `/api/reportes/diario?fecha=2024-01-01` para descargar un Excel

## Estructura de las Secciones del Restaurante

Cuando crees una venta, usa estos valores para la sección:
- `1` = Comedor
- `2` = Para Llevar
- `3` = Autoservicio
- `4` = A Domicilio

## Estructura de Roles

Para usuarios:
- `1` = Administrador
- `2` = Usuario

¡Listo! Ya deberías tener el sistema corriendo. Si tienes algún problema, revisa los logs en la consola o los errores que aparezcan.

