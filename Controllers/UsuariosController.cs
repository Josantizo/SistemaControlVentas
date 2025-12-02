using Microsoft.AspNetCore.Mvc;
using SistemaControlVentas.DTOs;
using SistemaControlVentas.Services;

namespace SistemaControlVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioResponseDTO>>> GetAll()
    {
        var usuarios = await _usuarioService.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponseDTO>> GetById(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound($"Usuario con ID {id} no encontrado");
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDTO>> Create([FromBody] UsuarioCreateDTO dto)
    {
        try
        {
            var usuario = await _usuarioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioResponseDTO>> Update(int id, [FromBody] UsuarioUpdateDTO dto)
    {
        try
        {
            var usuario = await _usuarioService.UpdateAsync(id, dto);
            if (usuario == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }
            return Ok(usuario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var resultado = await _usuarioService.DeleteAsync(id);
        if (!resultado)
        {
            return NotFound($"Usuario con ID {id} no encontrado");
        }
        return NoContent();
    }
}

