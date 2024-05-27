using Microsoft.EntityFrameworkCore;
using NeonTechAspNetCore.Models;
namespace NeonTechAspNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir claves primarias
            modelBuilder.Entity<Proveedor>().HasKey(p => p.IdProveedor);
            modelBuilder.Entity<Producto>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Compra>().HasKey(c => c.IdCompra);
            modelBuilder.Entity<DetalleCompra>().HasKey(dc => dc.IdDetalleCompra);
            modelBuilder.Entity<Lote>().HasKey(l => l.IdLote);

            // Configurar relaciones
            modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany()
                .HasForeignKey(c => c.IdProveedor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Compra)
                .WithMany(c => c.DetallesCompra)
                .HasForeignKey(dc => dc.IdCompra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Producto)
                .WithMany()
                .HasForeignKey(dc => dc.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lote>()
                .HasOne(l => l.Producto)
                .WithMany()
                .HasForeignKey(l => l.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar restricciones adicionales y propiedades si es necesario
            modelBuilder.Entity<Proveedor>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Compra>()
                .Property(c => c.Fecha)
                .IsRequired();

            modelBuilder.Entity<DetalleCompra>()
                .Property(dc => dc.Cantidad)
                .IsRequired();

            modelBuilder.Entity<DetalleCompra>()
                .Property(dc => dc.PrecioCompra)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Lote>()
                .Property(l => l.Fecha)
                .IsRequired();
        }
    }
}