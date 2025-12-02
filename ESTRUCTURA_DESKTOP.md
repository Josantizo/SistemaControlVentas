# Estructura de la Aplicación Desktop - Sistema de Control de Ventas

## Estado Actual

✅ Proyecto WPF creado: `SistemaControlVentas.Desktop`
✅ Referencia al proyecto API agregada
✅ Archivo de configuración `appsettings.json` creado

## Próximos Pasos para Completar la Interfaz Gráfica

### 1. Configurar Dependencias en el Proyecto Desktop

El proyecto Desktop necesita agregar los siguientes paquetes NuGet para usar los servicios directamente:

```bash
cd SistemaControlVentas.Desktop
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package BCrypt.Net-Next
dotnet add package ClosedXML
```

### 2. Crear Estructura de Carpetas

```
SistemaControlVentas.Desktop/
├── Views/              # Ventanas XAML
│   ├── LoginWindow.xaml
│   ├── MainWindow.xaml
│   ├── VentasWindow.xaml
│   ├── ProductosWindow.xaml
│   ├── ReportesWindow.xaml
│   └── UsuariosWindow.xaml
├── ViewModels/         # Lógica de presentación (MVVM)
│   ├── LoginViewModel.cs
│   ├── MainViewModel.cs
│   ├── VentasViewModel.cs
│   ├── ProductosViewModel.cs
│   ├── ReportesViewModel.cs
│   └── UsuariosViewModel.cs
├── Services/           # Servicios de configuración
│   └── ServiceProvider.cs
└── App.xaml            # Configuración de la aplicación
```

### 3. Ventanas a Crear

#### LoginWindow
- Campo de nombre de usuario
- Campo de contraseña
- Botón de inicio de sesión
- Validación de credenciales

#### MainWindow (Dashboard)
- Menú principal con opciones:
  - Nueva Venta
  - Productos/Menú
  - Reportes
  - Usuarios (solo admin)
- Barra de estado con usuario actual
- Información rápida del día (total de ventas)

#### VentasWindow
- Selección de sección (Comedor, Para Llevar, Autoservicio, A Domicilio)
- Búsqueda de productos
- Lista de productos en la venta
- Cantidades y precios
- Total de la venta
- Botón de guardar venta
- Campo de observaciones

#### ProductosWindow
- Lista de productos
- Búsqueda/filtrado
- Agregar nuevo producto
- Editar producto
- Eliminar producto
- Formulario: Nombre, Descripción, Precio

#### ReportesWindow
- Filtros: Fecha, Rango de fechas
- Opciones: Diario, Semanal, Mensual, Personalizado
- Botón de generar Excel
- Vista previa de ventas

#### UsuariosWindow (Solo Administrador)
- Lista de usuarios
- Agregar usuario
- Editar usuario
- Eliminar usuario
- Formulario: Nombre, Contraseña, Rol

## Configuración Necesaria

### App.xaml.cs
Necesita configurar la inyección de dependencias para usar los servicios:

```csharp
// Configurar Entity Framework
// Configurar Repositorios
// Configurar Servicios
```

## Diseño

- Interfaz moderna y limpia
- Colores apropiados para un restaurante
- Fácil de usar para los empleados
- Diseño responsive
- Iconos claros y descriptivos

## Notas Importantes

1. **Aplicación Local**: Esta aplicación está diseñada para ejecutarse localmente en el restaurante
2. **Base de Datos Compartida**: Tanto la API como la Desktop usarán la misma base de datos MySQL
3. **Sin Internet**: La aplicación puede funcionar completamente offline (solo necesita MySQL local)
4. **Roles**: Diferentes permisos según el rol (Administrador/Usuario)

