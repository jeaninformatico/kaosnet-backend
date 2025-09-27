using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Api_Res_Kaosnet.Models;

public partial class KaosnetOriginalContext : DbContext
{
    public KaosnetOriginalContext()
    {
    }

    public KaosnetOriginalContext(DbContextOptions<KaosnetOriginalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alerta> Alertas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Egreso> Egresos { get; set; }

    public virtual DbSet<Flujocaja> Flujocajas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Recarga> Recargas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Suscripcione> Suscripciones { get; set; }

    public virtual DbSet<Tasacambio> Tasacambios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=kaosnetOriginal;user=root;password=Dios123**@", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.4.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alerta>(entity =>
        {
            entity.HasKey(e => e.IdAlerta).HasName("PRIMARY");

            entity.ToTable("alertas");

            entity.HasIndex(e => e.IdCuenta, "idCuenta");

            entity.Property(e => e.IdAlerta).HasColumnName("idAlerta");
            entity.Property(e => e.DiasRestantes).HasColumnName("dias_restantes");
            entity.Property(e => e.EstadoAlerta)
                .HasColumnType("enum('pendiente','renovada','vencida')")
                .HasColumnName("estado_alerta");
            entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.IdCuenta)
                .HasConstraintName("alertas_ibfk_1");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Contacto)
                .HasMaxLength(50)
                .HasColumnName("contacto");
            entity.Property(e => e.País)
                .HasMaxLength(50)
                .HasColumnName("país");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PRIMARY");

            entity.ToTable("cuentas");

            entity.HasIndex(e => e.IdServicio, "idServicio");

            entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");
            entity.Property(e => e.ClaveAcceso)
                .HasColumnType("text")
                .HasColumnName("clave_acceso");
            entity.Property(e => e.CorreoAcceso)
                .HasMaxLength(100)
                .HasColumnName("correo_acceso");
            entity.Property(e => e.Dispositivos).HasColumnName("dispositivos");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('activa','vendida','vencida')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaExpira).HasColumnName("fecha_expira");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.País)
                .HasMaxLength(50)
                .HasColumnName("país");
            entity.Property(e => e.Pin)
                .HasMaxLength(10)
                .HasColumnName("pin");
            entity.Property(e => e.TipoCuenta)
                .HasColumnType("enum('perfil','completa','premium')")
                .HasColumnName("tipo_cuenta");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("cuentas_ibfk_1");
        });

        modelBuilder.Entity<Egreso>(entity =>
        {
            entity.HasKey(e => e.Idegreso).HasName("PRIMARY");

            entity.ToTable("egresos");

            entity.HasIndex(e => e.IdServicio, "idServicio");

            entity.Property(e => e.Idegreso).HasColumnName("idegreso");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(100)
                .HasColumnName("proveedor");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Egresos)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("egresos_ibfk_1");
        });

        modelBuilder.Entity<Flujocaja>(entity =>
        {
            entity.HasKey(e => e.Idflujo).HasName("PRIMARY");

            entity.ToTable("flujocaja");

            entity.Property(e => e.Idflujo).HasColumnName("idflujo");
            entity.Property(e => e.BalanceBs)
                .HasPrecision(10, 2)
                .HasColumnName("balance_bs");
            entity.Property(e => e.BalanceUsd)
                .HasPrecision(10, 2)
                .HasColumnName("balance_usd");
            entity.Property(e => e.EgresosBs)
                .HasPrecision(10, 2)
                .HasColumnName("egresos_bs");
            entity.Property(e => e.EgresosUsd)
                .HasPrecision(10, 2)
                .HasColumnName("egresos_usd");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IngresosBs)
                .HasPrecision(10, 2)
                .HasColumnName("ingresos_bs");
            entity.Property(e => e.IngresosUsd)
                .HasPrecision(10, 2)
                .HasColumnName("ingresos_usd");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pagos");

            entity.HasIndex(e => e.IdSuscripcion, "idSuscripcion");

            entity.HasIndex(e => e.IdUsuario, "idUsuario");

            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.Banco)
                .HasMaxLength(100)
                .HasColumnName("banco");
            entity.Property(e => e.Cedula)
                .HasMaxLength(100)
                .HasColumnName("cedula");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.IdSuscripcion).HasColumnName("idSuscripcion");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.MetodoPago)
                .HasColumnType("enum('Zelle','Pago Móvil','Binance','Efectivo')")
                .HasColumnName("metodo_pago");
            entity.Property(e => e.MontoLocal)
                .HasPrecision(10, 2)
                .HasColumnName("monto_local");
            entity.Property(e => e.MontoUsd)
                .HasPrecision(10, 2)
                .HasColumnName("monto_usd");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .HasColumnName("referencia");
            entity.Property(e => e.TasaUsd)
                .HasPrecision(10, 2)
                .HasColumnName("tasa_usd");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdSuscripcionNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdSuscripcion)
                .HasConstraintName("pagos_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("pagos_ibfk_2");
        });

        modelBuilder.Entity<Recarga>(entity =>
        {
            entity.HasKey(e => e.IdRecarga).HasName("PRIMARY");

            entity.ToTable("recargas");

            entity.HasIndex(e => e.IdUsuario, "idUsuario");

            entity.Property(e => e.IdRecarga).HasColumnName("idRecarga");
            entity.Property(e => e.FechaRecarga).HasColumnName("fecha_recarga");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.MontoBs)
                .HasPrecision(10, 2)
                .HasColumnName("monto_bs");
            entity.Property(e => e.MontoUsd)
                .HasPrecision(10, 2)
                .HasColumnName("monto_usd");
            entity.Property(e => e.TasaCambio)
                .HasPrecision(10, 2)
                .HasColumnName("tasa_cambio");
            entity.Property(e => e.TipoJuego)
                .HasMaxLength(50)
                .HasColumnName("tipo_juego");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Recargas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("recargas_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.ToTable("servicios");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.MonedaBae)
                .HasMaxLength(10)
                .HasColumnName("moneda_bae");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PaísOrigen)
                .HasMaxLength(50)
                .HasColumnName("país_origen");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Suscripcione>(entity =>
        {
            entity.HasKey(e => e.IdSuscripcion).HasName("PRIMARY");

            entity.ToTable("suscripciones");

            entity.HasIndex(e => e.IdCliente, "idCliente");

            entity.HasIndex(e => e.IdCuenta, "idCuenta");

            entity.Property(e => e.IdSuscripcion).HasColumnName("idSuscripcion");
            entity.Property(e => e.CicloPago)
                .HasColumnType("enum('mensual','trimestral','anual')")
                .HasColumnName("ciclo_pago");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('activa','vencida','cancelada')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaRenovacion).HasColumnName("fecha_renovacion");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");
            entity.Property(e => e.PrecioLocal)
                .HasPrecision(10, 2)
                .HasColumnName("precio_local");
            entity.Property(e => e.PrecioUsd)
                .HasPrecision(10, 2)
                .HasColumnName("precio_usd");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("suscripciones_ibfk_2");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Suscripciones)
                .HasForeignKey(d => d.IdCuenta)
                .HasConstraintName("suscripciones_ibfk_1");
        });

        modelBuilder.Entity<Tasacambio>(entity =>
        {
            entity.HasKey(e => e.Idtasa).HasName("PRIMARY");

            entity.ToTable("tasacambio");

            entity.Property(e => e.Idtasa).HasColumnName("idtasa");
            entity.Property(e => e.Activa)
                .HasDefaultValueSql("'0'")
                .HasColumnName("activa");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Fuente)
                .HasMaxLength(50)
                .HasColumnName("fuente");
            entity.Property(e => e.TasaUsd)
                .HasPrecision(10, 2)
                .HasColumnName("tasa_usd");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Cedula, "cedula").IsUnique();

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.HasIndex(e => e.Telefono, "telefono").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(100)
                .HasColumnName("cedula");
            entity.Property(e => e.ClaveHash)
                .HasColumnType("text")
                .HasColumnName("clave_hash");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
