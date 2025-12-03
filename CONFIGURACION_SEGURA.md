# Configuración Segura - Contraseñas y Credenciales

## ⚠️ IMPORTANTE: Seguridad

**NUNCA subas contraseñas o credenciales al control de versiones (Git).**

## Configuración de la Contraseña de MySQL

### Opción 1: Variable de Entorno (RECOMENDADO)

Establece la variable de entorno `DB_PASSWORD` antes de ejecutar la aplicación:

**Windows (PowerShell):**
```powershell
$env:DB_PASSWORD = "tu_contraseña_aqui"
```

**Windows (CMD):**
```cmd
set DB_PASSWORD=tu_contraseña_aqui
```

**Linux/Mac:**
```bash
export DB_PASSWORD="tu_contraseña_aqui"
```

**Para que persista (Windows - PowerShell):**
```powershell
[System.Environment]::SetEnvironmentVariable("DB_PASSWORD", "tu_contraseña_aqui", "User")
```

### Opción 2: Archivo appsettings.json Local

1. Copia `appsettings.example.json` a `appsettings.json`
2. Edita `appsettings.json` y agrega tu contraseña
3. **NO subas este archivo a Git** (ya está en .gitignore)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaControlVentas;User=root;Password=TU_CONTRASENA;Port=3306;"
  }
}
```

### Opción 3: appsettings.Development.json

Crea un archivo `appsettings.Development.json` que solo se usa en desarrollo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaControlVentas;User=root;Password=TU_CONTRASENA;Port=3306;"
  }
}
```

Este archivo también debe estar en `.gitignore`.

## Archivos Ignorados por Git

Los siguientes archivos están configurados para ser ignorados:
- `appsettings.json` - Archivo de configuración local
- `appsettings.*.json` - Archivos de configuración específicos del entorno
- `!appsettings.example.json` - Este sí se incluye como plantilla

## Verificar que Funciona

Después de configurar la contraseña:
1. Verifica que `appsettings.json` no esté en Git:
   ```bash
   git status
   ```
   
2. Si aparece, asegúrate de que esté en `.gitignore` y haz:
   ```bash
   git rm --cached appsettings.json
   ```

## Notas de Seguridad

- ✅ Usa variables de entorno en producción
- ✅ No compartas archivos con contraseñas
- ✅ Usa contraseñas fuertes
- ❌ NO subas contraseñas a repositorios Git
- ❌ NO hardcodees contraseñas en el código

