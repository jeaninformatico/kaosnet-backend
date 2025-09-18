using api_Kaosnet.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using api_Kaosnet.Models;

namespace api_Kaosnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Cuenta> Cuentas => Set<Cuenta>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();
        public DbSet<Configuracion> Configuraciones => Set<Configuracion>();
        public DbSet<TasaDolar> TasasDolar => Set<TasaDolar>();
        public DbSet<Recuperacion> Recuperaciones => Set<Recuperacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo).IsUnique();

            modelBuilder.Entity<Configuracion>()
                .HasIndex(c => c.Clave).IsUnique();

            modelBuilder.Entity<Recuperacion>()
                .HasIndex(r => r.Token).IsUnique();
        }
    }
}
