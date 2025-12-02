using Microsoft.EntityFrameworkCore;
using SistemaControlVentas.Data;
using SistemaControlVentas.Models;
using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly ApplicationDbContext _context;

    public VentaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Venta>> GetAllAsync()
    {
        return await _context.Ventas
            .Include(v => v.Usuario)
            .Include(v => v.DetalleVentas)
                .ThenInclude(d => d.Producto)
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<Venta?> GetByIdAsync(int id)
    {
        return await _context.Ventas
            .Include(v => v.Usuario)
            .Include(v => v.DetalleVentas)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Venta>> GetByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var fechaFinInclusive = fechaFin.Date.AddDays(1).AddSeconds(-1);
        
        return await _context.Ventas
            .Include(v => v.Usuario)
            .Include(v => v.DetalleVentas)
                .ThenInclude(d => d.Producto)
            .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFinInclusive)
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<List<Venta>> GetByUsuarioAsync(int usuarioId, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        var query = _context.Ventas
            .Include(v => v.Usuario)
            .Include(v => v.DetalleVentas)
                .ThenInclude(d => d.Producto)
            .Where(v => v.UsuarioId == usuarioId);

        if (fechaInicio.HasValue)
        {
            query = query.Where(v => v.FechaVenta >= fechaInicio.Value);
        }

        if (fechaFin.HasValue)
        {
            var fechaFinInclusive = fechaFin.Value.Date.AddDays(1).AddSeconds(-1);
            query = query.Where(v => v.FechaVenta <= fechaFinInclusive);
        }

        return await query
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<List<Venta>> GetBySeccionAsync(int seccion, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
        var seccionEnum = (SeccionVenta)seccion;
        var query = _context.Ventas
            .Include(v => v.Usuario)
            .Include(v => v.DetalleVentas)
                .ThenInclude(d => d.Producto)
            .Where(v => v.Seccion == seccionEnum);

        if (fechaInicio.HasValue)
        {
            query = query.Where(v => v.FechaVenta >= fechaInicio.Value);
        }

        if (fechaFin.HasValue)
        {
            var fechaFinInclusive = fechaFin.Value.Date.AddDays(1).AddSeconds(-1);
            query = query.Where(v => v.FechaVenta <= fechaFinInclusive);
        }

        return await query
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<Venta> CreateAsync(Venta venta)
    {
        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();
        return venta;
    }

    public async Task<decimal> GetTotalVentasPorFechaAsync(DateTime fecha)
    {
        var fechaFin = fecha.Date.AddDays(1).AddSeconds(-1);
        
        return await _context.Ventas
            .Where(v => v.FechaVenta >= fecha.Date && v.FechaVenta <= fechaFin)
            .SumAsync(v => v.Total);
    }

    public async Task<decimal> GetTotalVentasPorRangoAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var fechaFinInclusive = fechaFin.Date.AddDays(1).AddSeconds(-1);
        
        return await _context.Ventas
            .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFinInclusive)
            .SumAsync(v => v.Total);
    }
}

