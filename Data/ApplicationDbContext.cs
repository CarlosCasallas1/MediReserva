using System;
using System.Collections.Generic;
using MediReserva.Models;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Consultorio> Consultorios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cita__3214EC071D6CA435");

            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Motivo).HasMaxLength(250);

            entity.HasOne(d => d.Medico).WithMany(p => p.Cita)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__MedicoId__4AB81AF0");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__PacienteId__49C3F6B7");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC07FF620ADB");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Consultorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consulto__3214EC07C25AB2CB");

            entity.ToTable("Consultorio");

            entity.Property(e => e.Codigo).HasMaxLength(100);

            entity.Property(e => e.Estado).HasColumnType("bit");

        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medico__3214EC071D949507");

            entity.ToTable("Medico");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Especialidad).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.EspecialidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medico__Especial__3B75D760");

            entity.HasOne(d => d.Consultorio).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.ConsultorioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medico__Especial__3B75D760");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__3214EC07EAC5B08E");

            entity.ToTable("Paciente");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Documento).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
