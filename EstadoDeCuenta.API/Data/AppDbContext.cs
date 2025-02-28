using Microsoft.EntityFrameworkCore;
using EstadoDeCuenta.API.Models;

namespace EstadoDeCuenta.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TarjetaCredito> TarjetasCredito { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TarjetaCredito>().ToTable("TarjetaCredito");
            modelBuilder.Entity<Compra>().ToTable("Compra");
            modelBuilder.Entity<Pago>().ToTable("Pago");
        }
    }

}
