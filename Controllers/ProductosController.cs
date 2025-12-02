using Microsoft.AspNetCore.Mvc;
using SistemaControlVentas.DTOs;
using SistemaControlVentas.Services;

namespace SistemaControlVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoResponseDTO>>> GetAll()
    {
        var productos = await _productoService.GetAllAsync();
        return Ok(productos);
    }

    [HttpGet("activos")]
    public async Task<ActionResult<List<ProductoResponseDTO>>> GetActivos()
    {
        var productos = await _productoService.GetActivosAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoResponseDTO>> GetById(int id)
    {
        var producto = await _productoService.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }
        return Ok(producto);
    }

    [HttpGet("buscar/{nombre}")]
    public async Task<ActionResult<List<ProductoResponseDTO>>> BuscarPorNombre(string nombre)
    {
        var productos = await _productoService.BuscarPorNombreAsync(nombre);
        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoResponseDTO>> Create([FromBody] ProductoCreateDTO dto)
    {
        var producto = await _productoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductoResponseDTO>> Update(int id, [FromBody] ProductoUpdateDTO dto)
    {
        var producto = await _productoService.UpdateAsync(id, dto);
        if (producto == null)
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }
        return Ok(producto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var resultado = await _productoService.DeleteAsync(id);
        if (!resultado)
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }
        return NoContent();
    }
}

