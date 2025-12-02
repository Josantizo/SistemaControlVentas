using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.Models;

public class Venta
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public SeccionVenta Seccion { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;
    public string? Observaciones { get; set; }

    // Navegación
    public Usuario Usuario { get; set; } = null!;
    public ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
}

