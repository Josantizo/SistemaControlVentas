using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Rol Rol { get; set; }

    // Navegación
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}

