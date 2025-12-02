using SistemaControlVentas.DTOs;
using SistemaControlVentas.Models;
using SistemaControlVentas.Repositories;

namespace SistemaControlVentas.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<UsuarioResponseDTO>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(MapToDTO).ToList();
    }

    public async Task<UsuarioResponseDTO?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return usuario == null ? null : MapToDTO(usuario);
    }

    public async Task<UsuarioResponseDTO> CreateAsync(UsuarioCreateDTO dto)
    {
        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = dto.Rol,
        };

        var usuarioCreado = await _usuarioRepository.CreateAsync(usuario);
        return MapToDTO(usuarioCreado);
    }

    public async Task<UsuarioResponseDTO?> UpdateAsync(int id, UsuarioUpdateDTO dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return null;

        if (!string.IsNullOrEmpty(dto.Nombre))
            usuario.Nombre = dto.Nombre;

        if (!string.IsNullOrEmpty(dto.Password))
            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        if (dto.Rol.HasValue)
            usuario.Rol = dto.Rol.Value;

        var usuarioActualizado = await _usuarioRepository.UpdateAsync(usuario);
        return MapToDTO(usuarioActualizado);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _usuarioRepository.DeleteAsync(id);
    }

    private static UsuarioResponseDTO MapToDTO(Usuario usuario)
    {
        return new UsuarioResponseDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Rol = usuario.Rol,
        };
    }
}

