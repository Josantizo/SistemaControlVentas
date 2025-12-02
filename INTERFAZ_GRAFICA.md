# Guía de Interfaz Gráfica - Sistema de Control de Ventas

## Arquitectura

Para la aplicación de escritorio local en el restaurante, tenemos dos opciones:

### Opción 1: Aplicación WPF Independiente (Recomendada)
La aplicación WPF usa directamente los servicios y repositorios sin necesidad de la API HTTP. Esto es más eficiente para una aplicación local.

**Estructura:**
```
SistemaControlVentas.Desktop/
├── Views/          # Ventanas XAML
├── ViewModels/     # Lógica de presentación
├── Services/       # Uso directo de servicios del proyecto API
└── App.xaml        # Configuración de la aplicación
```

### Opción 2: Aplicación WPF que Consume la API
La aplicación WPF hace llamadas HTTP a la API REST. Requiere que la API esté corriendo.

**Para una aplicación local, la Opción 1 es más eficiente.**

## Ventanas Principales

1. **LoginWindow** - Inicio de sesión
2. **MainWindow** - Ventana principal con menú
3. **VentasWindow** - Crear y gestionar ventas
4. **ProductosWindow** - Gestionar productos/menú
5. **ReportesWindow** - Generar reportes Excel
6. **UsuariosWindow** - Gestionar usuarios (solo admin)

## Próximos Pasos

1. Configurar el acceso a los servicios en la aplicación WPF
2. Crear las ventanas principales
3. Implementar la lógica de negocio en cada ventana
4. Diseñar una interfaz moderna y funcional

