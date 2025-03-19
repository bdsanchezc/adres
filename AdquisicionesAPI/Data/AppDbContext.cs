using Microsoft.EntityFrameworkCore;
using AdquisicionesAPI.Models;

namespace AdquisicionesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Adquisicion> Adquisiciones { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<HistorialAdquisicion> HistorialAdquisiciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=adquisiciones.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Estado table
            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, Nombre = "Creado" },
                new Estado { Id = 2, Nombre = "En proceso" },
                new Estado { Id = 3, Nombre = "Inactivo" },
                new Estado { Id = 4, Nombre = "Finalizado" }
            );

            // Seed data for Unidad table
            modelBuilder.Entity<Unidad>().HasData(
                new Unidad { Id = 1, Nombre = "Dirección de Medicamentos y Tecnologías en Salud" },
                new Unidad { Id = 2, Nombre = "Dirección de Enfermería" },
                new Unidad { Id = 3, Nombre = "Dirección de Medicamentos básicos" },
                new Unidad { Id = 4, Nombre = "Dirección General de Salud" }
            );

            // Seed data for Proveedor table
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { Id = 1, Nombre = "Laboratorios Bayer S.A." },
                new Proveedor { Id = 2, Nombre = "Laboratorios Distritales S.A." },
                new Proveedor { Id = 3, Nombre = "Laboratirio Medicinal S.A." }
            );
        }
    }
}
