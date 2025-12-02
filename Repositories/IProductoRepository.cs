using SistemaControlVentas.Models;

namespace SistemaControlVentas.Repositories;

public interface IProductoRepository
{
    Task<List<Producto>> GetAllAsync();
    Task<List<Producto>> GetActivosAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<List<Producto>> BuscarPorNombreAsync(string nombre);
    Task<Producto> CreateAsync(Producto producto);
    Task<Producto> UpdateAsync(Producto producto);
    Task<bool> DeleteAsync(int id);
}

