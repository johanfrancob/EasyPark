using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EasyPark.Shared.Entities;

namespace EasyPark.Backend;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBahium> TblBahia { get; set; }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblEmpleado> TblEmpleados { get; set; }

    public virtual DbSet<TblFactura> TblFacturas { get; set; }

    public virtual DbSet<TblRol> TblRols { get; set; }

    public virtual DbSet<TblTarifa> TblTarifas { get; set; }

    public virtual DbSet<TblTicketEntradum> TblTicketEntrada { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<TblVehiculo> TblVehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:LocalConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBahium>(entity =>
        {
            entity.HasKey(e => e.IdBahia).HasName("PK__tblBahia__D875E6625E3EDF4F");

            entity.ToTable("tblBahia");

            entity.Property(e => e.IdBahia).HasColumnName("idBahia");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__tblClien__885457EE33DA3B55");

            entity.ToTable("tblCliente");

            entity.HasIndex(e => e.Documento, "UQ__tblClien__A25B3E61B505D38F").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .HasColumnName("documento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<TblEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__tblEmple__5295297C29D6629A");

            entity.ToTable("tblEmpleado");

            entity.HasIndex(e => e.Documento, "UQ__tblEmple__A25B3E61B90254FB").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .HasColumnName("documento");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TblEmpleados)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Empleado");
        });

        modelBuilder.Entity<TblFactura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__tblFactu__3CD5687E48370FA5");

            entity.ToTable("tblFactura");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHoraSalida)
                .HasColumnType("datetime")
                .HasColumnName("fechaHoraSalida");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdTarifa).HasColumnName("idTarifa");
            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TblFacturas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Empleado");

            entity.HasOne(d => d.IdTarifaNavigation).WithMany(p => p.TblFacturas)
                .HasForeignKey(d => d.IdTarifa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Tarifa");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.TblFacturas)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Ticket");
        });

        modelBuilder.Entity<TblRol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__tblRol__3C872F76CDB91D2A");

            entity.ToTable("tblRol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TblTarifa>(entity =>
        {
            entity.HasKey(e => e.IdTarifa).HasName("PK__tblTarif__550711E148F6FA3D");

            entity.ToTable("tblTarifa");

            entity.Property(e => e.IdTarifa).HasColumnName("idTarifa");
            entity.Property(e => e.TipoVehiculo)
                .HasMaxLength(50)
                .HasColumnName("tipoVehiculo");
            entity.Property(e => e.ValorHora)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valorHora");
        });

        modelBuilder.Entity<TblTicketEntradum>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__tblTicke__22B1456F07AC3333");

            entity.ToTable("tblTicketEntrada");

            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.FechaHoraEntrada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaHoraEntrada");
            entity.Property(e => e.IdBahia).HasColumnName("idBahia");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .HasColumnName("placa");

            entity.HasOne(d => d.IdBahiaNavigation).WithMany(p => p.TblTicketEntrada)
                .HasForeignKey(d => d.IdBahia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Bahia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TblTicketEntrada)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Cliente");

            entity.HasOne(d => d.PlacaNavigation).WithMany(p => p.TblTicketEntrada)
                .HasForeignKey(d => d.Placa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Vehiculo");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__tblUsuar__645723A6DE4FF6B2");

            entity.ToTable("tblUsuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .HasColumnName("contrasena");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TblUsuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Empleado");
        });

        modelBuilder.Entity<TblVehiculo>(entity =>
        {
            entity.HasKey(e => e.Placa).HasName("PK__tblVehic__0C057424C1E7058F");

            entity.ToTable("tblVehiculo");

            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .HasColumnName("placa");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
