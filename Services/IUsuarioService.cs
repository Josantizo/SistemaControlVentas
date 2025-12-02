using SistemaControlVentas.DTOs;

namespace SistemaControlVentas.Services;

public interface IUsuarioService
{
    Task<List<UsuarioResponseDTO>> GetAllAsync();
    Task<UsuarioResponseDTO?> GetByIdAsync(int id);
    Task<UsuarioResponseDTO> CreateAsync(UsuarioCreateDTO dto);
    Task<UsuarioResponseDTO?> UpdateAsync(int id, UsuarioUpdateDTO dto);
    Task<bool> DeleteAsync(int id);
}

