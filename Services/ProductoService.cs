using SistemaControlVentas.DTOs;
using SistemaControlVentas.Models;
using SistemaControlVentas.Repositories;

namespace SistemaControlVentas.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<List<ProductoResponseDTO>> GetAllAsync()
    {
        var productos = await _productoRepository.GetAllAsync();
        return productos.Select(MapToDTO).ToList();
    }

    public async Task<List<ProductoResponseDTO>> GetActivosAsync()
    {
        var productos = await _productoRepository.GetActivosAsync();
        return productos.Select(MapToDTO).ToList();
    }

    public async Task<ProductoResponseDTO?> GetByIdAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        return producto == null ? null : MapToDTO(producto);
    }

    public async Task<List<ProductoResponseDTO>> BuscarPorNombreAsync(string nombre)
    {
        var productos = await _productoRepository.BuscarPorNombreAsync(nombre);
        return productos.Select(MapToDTO).ToList();
    }

    public async Task<ProductoResponseDTO> CreateAsync(ProductoCreateDTO dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
        };

        var productoCreado = await _productoRepository.CreateAsync(producto);
        return MapToDTO(productoCreado);
    }

    public async Task<ProductoResponseDTO?> UpdateAsync(int id, ProductoUpdateDTO dto)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null) return null;

        if (!string.IsNullOrEmpty(dto.Nombre))
            producto.Nombre = dto.Nombre;

        if (dto.Descripcion != null)
            producto.Descripcion = dto.Descripcion;

        if (dto.Precio.HasValue)
            producto.Precio = dto.Precio.Value;

        var productoActualizado = await _productoRepository.UpdateAsync(producto);
        return MapToDTO(productoActualizado);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _productoRepository.DeleteAsync(id);
    }

    private static ProductoResponseDTO MapToDTO(Producto producto)
    {
        return new ProductoResponseDTO
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
        };
    }
}

