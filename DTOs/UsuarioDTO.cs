using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.DTOs;

public class UsuarioCreateDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Rol Rol { get; set; }
}

public class UsuarioUpdateDTO
{
    public string? Nombre { get; set; }
    public string? Password { get; set; }
    public Rol? Rol { get; set; }
}

public class UsuarioResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Rol Rol { get; set; }
}

