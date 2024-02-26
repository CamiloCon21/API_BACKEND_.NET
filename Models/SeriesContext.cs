using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIPruebas.Models;

public partial class SeriesContext : DbContext
{
    public SeriesContext()
    {
    }

    public SeriesContext(DbContextOptions<SeriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.IdSerie).HasName("PK__Series__E8A3AF9DC24B6303");

            entity.Property(e => e.IdSerie)
                .ValueGeneratedNever()
                .HasColumnName("id_Serie");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__8E901EAA30C753D9");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__531402F3480FA6EE").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_Usuario");
            entity.Property(e => e.Activo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.oSeries).WithMany(p => p.oUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioSeries",
                    r => r.HasOne<Series>().WithMany()
                        .HasForeignKey("SerieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioSe__Serie__4F7CD00D"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioSe__Usuar__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("UsuarioId", "SerieId").HasName("PK__UsuarioS__DEF5C28B9A4549F4");
                        j.IndexerProperty<int>("UsuarioId").HasColumnName("UsuarioID");
                        j.IndexerProperty<int>("SerieId").HasColumnName("SerieID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
