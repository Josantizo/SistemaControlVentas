using SistemaControlVentas.DTOs;

namespace SistemaControlVentas.Services;

public interface IVentaService
{
    Task<List<VentaResponseDTO>> GetAllAsync();
    Task<VentaResponseDTO?> GetByIdAsync(int id);
    Task<VentaResponseDTO> CreateAsync(VentaCreateDTO dto);
    Task<List<VentaResponseDTO>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<List<VentaResponseDTO>> GetByUsuarioAsync(int usuarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    Task<List<VentaResponseDTO>> GetBySeccionAsync(int seccion, DateTime? fechaInicio = null, DateTime? fechaFin = null);
}

