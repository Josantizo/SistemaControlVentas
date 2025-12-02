using Microsoft.AspNetCore.Mvc;
using SistemaControlVentas.DTOs;
using SistemaControlVentas.Services;

namespace SistemaControlVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaService _ventaService;

    public VentasController(IVentaService ventaService)
    {
        _ventaService = ventaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<VentaResponseDTO>>> GetAll()
    {
        var ventas = await _ventaService.GetAllAsync();
        return Ok(ventas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VentaResponseDTO>> GetById(int id)
    {
        var venta = await _ventaService.GetByIdAsync(id);
        if (venta == null)
        {
            return NotFound($"Venta con ID {id} no encontrada");
        }
        return Ok(venta);
    }

    [HttpPost]
    public async Task<ActionResult<VentaResponseDTO>> Create([FromBody] VentaCreateDTO dto)
    {
        try
        {
            var venta = await _ventaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = venta.Id }, venta);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("fecha")]
    public async Task<ActionResult<List<VentaResponseDTO>>> GetByFecha(
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin)
    {
        var ventas = await _ventaService.GetByFechaAsync(fechaInicio, fechaFin);
        return Ok(ventas);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<VentaResponseDTO>>> GetByUsuario(
        int usuarioId,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null)
    {
        var ventas = await _ventaService.GetByUsuarioAsync(usuarioId, fechaInicio, fechaFin);
        return Ok(ventas);
    }

    [HttpGet("seccion/{seccion}")]
    public async Task<ActionResult<List<VentaResponseDTO>>> GetBySeccion(
        int seccion,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null)
    {
        var ventas = await _ventaService.GetBySeccionAsync(seccion, fechaInicio, fechaFin);
        return Ok(ventas);
    }
}

