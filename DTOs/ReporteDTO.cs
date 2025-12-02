namespace SistemaControlVentas.DTOs;

public class ReporteFiltroDTO
{
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? UsuarioId { get; set; }
    public int? Seccion { get; set; }
}

public class ReporteVentaDTO
{
    public int Id { get; set; }
    public DateTime FechaVenta { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Seccion { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string? Observaciones { get; set; }
    public List<DetalleVentaReporteDTO> Detalles { get; set; } = new();
}

public class DetalleVentaReporteDTO
{
    public string ProductoNombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

