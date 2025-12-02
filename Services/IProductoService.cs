using SistemaControlVentas.DTOs;

namespace SistemaControlVentas.Services;

public interface IProductoService
{
    Task<List<ProductoResponseDTO>> GetAllAsync();
    Task<List<ProductoResponseDTO>> GetActivosAsync();
    Task<ProductoResponseDTO?> GetByIdAsync(int id);
    Task<List<ProductoResponseDTO>> BuscarPorNombreAsync(string nombre);
    Task<ProductoResponseDTO> CreateAsync(ProductoCreateDTO dto);
    Task<ProductoResponseDTO?> UpdateAsync(int id, ProductoUpdateDTO dto);
    Task<bool> DeleteAsync(int id);
}

