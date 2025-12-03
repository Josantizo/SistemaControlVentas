# Bugs Corregidos

## Bug 1: Cálculo Incorrecto de Semana en Reporte Semanal ✅ CORREGIDO

**Problema:**
El reporte semanal estaba usando `AddDays(7)` en lugar de `AddDays(6)`, lo que causaba que incluyera datos del día después del final de la semana.

**Archivos Corregidos:**
- `Services/ReporteExcelService.cs` (línea 28)
- `SistemaControlVentas.Desktop/Services/ReporteExcelService.cs` (línea 28)

**Solución:**
Cambiado de `fechaInicio.AddDays(7).AddSeconds(-1)` a `fechaInicio.AddDays(6)`. El repositorio ya convierte automáticamente la fecha fin a inclusivo (23:59:59).

**Resultado:**
Ahora el reporte semanal incluye exactamente 7 días (desde fechaInicio hasta fechaInicio + 6 días), siendo consistente con el nombre del archivo generado por el controlador.

---

## Bug 2: Cálculo Incorrecto del Rango de Bordes en Excel ✅ CORREGIDO

**Problema:**
El cálculo del rango de bordes usaba `row - resumenPorSeccion.Count() - 2` como fila final, lo que apuntaba incorrectamente a la sección de resumen en lugar de la última fila de datos.

**Archivos Corregidos:**
- `Services/ReporteExcelService.cs` (líneas 139-140, 188-191)
- `SistemaControlVentas.Desktop/Services/ReporteExcelService.cs` (líneas 139-140, 188-191)

**Solución:**
1. Guardar la última fila de datos antes de agregar el resumen: `var ultimaFilaDatos = row - 1;`
2. Usar esta variable en el cálculo del rango: `worksheet.Range(6, 1, ultimaFilaDatos, 8);`

**Resultado:**
Los bordes ahora se aplican correctamente solo a las filas de datos de ventas, excluyendo los encabezados y la sección de resumen.

---

## Bug 3: Contraseña Hardcodeada en appsettings.json ✅ CORREGIDO

**Problema:**
La contraseña de MySQL estaba hardcodeada directamente en `appsettings.json`, lo cual es un riesgo de seguridad ya que este archivo está en control de versiones.

**Archivos Modificados:**
- `appsettings.json` - Contraseña removida
- `Program.cs` - Soporte para variables de entorno
- `SistemaControlVentas.Desktop/appsettings.json` - Sin contraseña
- `SistemaControlVentas.Desktop/App.xaml.cs` - Soporte para variables de entorno
- `.gitignore` - Agregado appsettings.json para ignorarlo

**Archivos Creados:**
- `appsettings.example.json` - Plantilla sin contraseña
- `SistemaControlVentas.Desktop/appsettings.example.json` - Plantilla sin contraseña
- `CONFIGURACION_SEGURA.md` - Documentación de configuración segura

**Solución:**
1. Contraseña removida de `appsettings.json`
2. Soporte para variable de entorno `DB_PASSWORD`
3. `appsettings.json` agregado a `.gitignore`
4. Plantillas de ejemplo creadas para documentación

**Uso:**
```powershell
# Establecer variable de entorno
$env:DB_PASSWORD = "tu_contraseña"

# O editar appsettings.json localmente (no se subirá a Git)
```

**Resultado:**
Las contraseñas ya no se subirán al control de versiones. Los usuarios pueden configurar la contraseña mediante variables de entorno o archivos locales.

---

## Verificación

Todos los bugs han sido corregidos en ambos proyectos:
- ✅ SistemaControlVentas (API)
- ✅ SistemaControlVentas.Desktop (Aplicación WPF)

Los cambios son compatibles y mantienen la funcionalidad existente mientras corrigen los problemas identificados.

