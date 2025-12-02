using SistemaControlVentas.Models;

namespace SistemaControlVentas.Repositories;

public interface IVentaRepository
{
    Task<List<Venta>> GetAllAsync();
    Task<Venta?> GetByIdAsync(int id);
    Task<List<Venta>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<List<Venta>> GetByUsuarioAsync(int usuarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    Task<List<Venta>> GetBySeccionAsync(int seccion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    Task<Venta> CreateAsync(Venta venta);
    Task<decimal> GetTotalVentasPorFechaAsync(DateTime fecha);
    Task<decimal> GetTotalVentasPorRangoAsync(DateTime fechaInicio, DateTime fechaFin);
}

