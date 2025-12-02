using Microsoft.EntityFrameworkCore;
using SistemaControlVentas.Data;
using SistemaControlVentas.Models;

namespace SistemaControlVentas.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly ApplicationDbContext _context;

    public ProductoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<List<Producto>> GetActivosAsync()
    {
        return await _context.Productos
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _context.Productos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Producto>> BuscarPorNombreAsync(string nombre)
    {
        return await _context.Productos
            .Where(p => p.Nombre.Contains(nombre))
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<Producto> CreateAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> UpdateAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
        return producto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var producto = await GetByIdAsync(id);
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }
}

