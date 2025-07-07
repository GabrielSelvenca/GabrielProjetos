using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WS_Tower_api.Models;

namespace WS_Tower_api.Contexts;

public partial class MainContext : DbContext
{
    public MainContext()
    {
    }

    public MainContext(DbContextOptions<MainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Funcao> Funcaos { get; set; }

    public virtual DbSet<Relato> Relatos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("initial catalog=SessaoMobile;Data Source=NOTE-GABRIEL\\SQLEXPRESS;TrustServerCertificate=true;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcao>(entity =>
        {
            entity.ToTable("Funcao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Funcao1)
                .HasMaxLength(50)
                .HasColumnName("funcao");
        });

        modelBuilder.Entity<Relato>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .HasColumnName("imagem");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("longitude");
            entity.Property(e => e.Relato1)
                .HasMaxLength(255)
                .HasColumnName("relato");
            entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Relatos)
                .HasForeignKey(d => d.Usuarioid)
                .HasConstraintName("FK_Relatos_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Funcaoid).HasColumnName("funcaoid");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .HasColumnName("senha");
            entity.Property(e => e.Telefone)
                .HasMaxLength(13)
                .HasColumnName("telefone");

            entity.HasOne(d => d.Funcao).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Funcaoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Funcao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
