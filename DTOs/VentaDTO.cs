using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.DTOs;

public class DetalleVentaCreateDTO
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}

public class VentaCreateDTO
{
    public int UsuarioId { get; set; }
    public SeccionVenta Seccion { get; set; }
    public List<DetalleVentaCreateDTO> Detalles { get; set; } = new();
    public string? Observaciones { get; set; }
}

public class DetalleVentaResponseDTO
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

public class VentaResponseDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public SeccionVenta Seccion { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaVenta { get; set; }
    public string? Observaciones { get; set; }
    public List<DetalleVentaResponseDTO> Detalles { get; set; } = new();
}

