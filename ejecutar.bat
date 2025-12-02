@echo off
echo ========================================
echo Sistema de Control de Ventas
echo ========================================
echo.

echo [1/5] Restaurando paquetes NuGet...
dotnet restore
if %errorlevel% neq 0 (
    echo Error al restaurar paquetes!
    pause
    exit /b %errorlevel%
)

echo.
echo [2/5] Creando migracion inicial...
dotnet ef migrations add InitialCreate
if %errorlevel% neq 0 (
    echo Error al crear la migracion!
    echo Asegurate de tener MySQL corriendo y la cadena de conexion correcta.
    pause
    exit /b %errorlevel%
)

echo.
echo [3/5] Creando base de datos...
dotnet ef database update
if %errorlevel% neq 0 (
    echo Error al crear la base de datos!
    echo Verifica que MySQL este corriendo y que tengas permisos.
    pause
    exit /b %errorlevel%
)

echo.
echo [4/5] Compilando proyecto...
dotnet build
if %errorlevel% neq 0 (
    echo Error al compilar!
    pause
    exit /b %errorlevel%
)

echo.
echo [5/5] Ejecutando proyecto...
echo.
echo ========================================
echo Proyecto iniciado!
echo Accede a Swagger en: https://localhost:5001/swagger
echo ========================================
echo.
dotnet run

pause

