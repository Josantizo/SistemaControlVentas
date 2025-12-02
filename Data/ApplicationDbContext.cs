using Microsoft.EntityFrameworkCore;
using SistemaControlVentas.Models;
using SistemaControlVentas.Models.Enums;

namespace SistemaControlVentas.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Rol).HasConversion<int>();
        });

        // Configuración de Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("decimal(10,2)").IsRequired();
        });

        // Configuración de Venta
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Total).HasColumnType("decimal(10,2)").IsRequired();
            entity.Property(e => e.Seccion).HasConversion<int>();
            entity.Property(e => e.FechaVenta).IsRequired();
            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Ventas)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de DetalleVenta
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cantidad).IsRequired();
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)").IsRequired();
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10,2)").IsRequired();
            entity.HasOne(e => e.Venta)
                  .WithMany(v => v.DetalleVentas)
                  .HasForeignKey(e => e.VentaId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Producto)
                  .WithMany(p => p.DetalleVentas)
                  .HasForeignKey(e => e.ProductoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

