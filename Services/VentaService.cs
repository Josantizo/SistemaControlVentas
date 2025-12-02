using SistemaControlVentas.DTOs;
using SistemaControlVentas.Models;
using SistemaControlVentas.Models.Enums;
using SistemaControlVentas.Repositories;

namespace SistemaControlVentas.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public VentaService(
        IVentaRepository ventaRepository,
        IProductoRepository productoRepository,
        IUsuarioRepository usuarioRepository)
    {
        _ventaRepository = ventaRepository;
        _productoRepository = productoRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<VentaResponseDTO>> GetAllAsync()
    {
        var ventas = await _ventaRepository.GetAllAsync();
        return ventas.Select(MapToDTO).ToList();
    }

    public async Task<VentaResponseDTO?> GetByIdAsync(int id)
    {
        var venta = await _ventaRepository.GetByIdAsync(id);
        return venta == null ? null : MapToDTO(venta);
    }

    public async Task<VentaResponseDTO> CreateAsync(VentaCreateDTO dto)
    {
        // Validar que el usuario existe
        var usuario = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
        if (usuario == null)
        {
            throw new InvalidOperationException("El usuario especificado no existe");
        }

        // Validar que hay detalles
        if (dto.Detalles == null || !dto.Detalles.Any())
        {
            throw new InvalidOperationException("La venta debe tener al menos un producto");
        }

        var venta = new Venta
        {
            UsuarioId = dto.UsuarioId,
            Seccion = dto.Seccion,
            FechaVenta = DateTime.Now,
            Observaciones = dto.Observaciones,
            Total = 0
        };

        var detalles = new List<DetalleVenta>();
        decimal totalVenta = 0;

        foreach (var detalleDto in dto.Detalles)
        {
            // Validar producto
            var producto = await _productoRepository.GetByIdAsync(detalleDto.ProductoId);
            if (producto == null)
            {
                throw new InvalidOperationException($"El producto con ID {detalleDto.ProductoId} no existe");
            }

            if (detalleDto.Cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a cero");
            }

            var subtotal = producto.Precio * detalleDto.Cantidad;
            totalVenta += subtotal;

            detalles.Add(new DetalleVenta
            {
                ProductoId = producto.Id,
                Cantidad = detalleDto.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });
        }

        venta.Total = totalVenta;
        venta.DetalleVentas = detalles;

        var ventaCreada = await _ventaRepository.CreateAsync(venta);
        
        // Recargar con relaciones para devolver el DTO completo
        var ventaCompleta = await _ventaRepository.GetByIdAsync(ventaCreada.Id);
        return MapToDTO(ventaCompleta!);
    }

    public async Task<List<VentaResponseDTO>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var ventas = await _ventaRepository.GetByFechaAsync(fechaInicio, fechaFin);
        return ventas.Select(MapToDTO).ToList();
    }

    public async Task<List<VentaResponseDTO>> GetByUsuarioAsync(int usuarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        var ventas = await _ventaRepository.GetByUsuarioAsync(usuarioId, fechaInicio, fechaFin);
        return ventas.Select(MapToDTO).ToList();
    }

    public async Task<List<VentaResponseDTO>> GetBySeccionAsync(int seccion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        var ventas = await _ventaRepository.GetBySeccionAsync(seccion, fechaInicio, fechaFin);
        return ventas.Select(MapToDTO).ToList();
    }

    private static VentaResponseDTO MapToDTO(Venta venta)
    {
        return new VentaResponseDTO
        {
            Id = venta.Id,
            UsuarioId = venta.UsuarioId,
            UsuarioNombre = venta.Usuario?.Nombre ?? "",
            Seccion = venta.Seccion,
            Total = venta.Total,
            FechaVenta = venta.FechaVenta,
            Observaciones = venta.Observaciones,
            Detalles = venta.DetalleVentas.Select(d => new DetalleVentaResponseDTO
            {
                Id = d.Id,
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto?.Nombre ?? "",
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList()
        };
    }
}

