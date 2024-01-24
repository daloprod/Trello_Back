using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Trello1.Models;

public partial class PresidentContext : DbContext
{

    public PresidentContext(DbContextOptions<PresidentContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
        //Database.Migrate();
    }

    public virtual DbSet<Carte> Cartes { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Liste> Listes { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }

    private readonly IConfiguration? _configuration;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _configuration != null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is null.");
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carte>(entity =>
        {
            entity.ToTable("Carte");

            entity.Property(e => e.DateCreation).HasColumnType("DATE");
            entity.Property(e => e.Description).HasColumnType("VARCHAR(1000)");
            entity.Property(e => e.Titre).HasColumnType("VARCHAR(200)");

            entity.HasOne(d => d.IdListeNavigation).WithMany(p => p.Cartes).HasForeignKey(d => d.IdListe);
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.ToTable("Commentaire");

            entity.Property(e => e.Contenu).HasColumnType("VARCHAR(1000)");
            entity.Property(e => e.DateCreation).HasColumnType("DATE");
            entity.Property(e => e.Utilisateur).HasColumnType("VARCHAR(200)");

            entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Commentaires).HasForeignKey(d => d.IdCarte);
        });

        modelBuilder.Entity<Liste>(entity =>
        {
            entity.ToTable("Liste");

            entity.Property(e => e.Nom).HasColumnType("VARCHAR(200)");

            entity.HasOne(d => d.IdProjetNavigation).WithMany(p => p.Listes).HasForeignKey(d => d.IdProjet);
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.ToTable("Projet");

            entity.Property(e => e.DateCreation).HasColumnType("DATE");
            entity.Property(e => e.Description).HasColumnType("VARCHAR(1000)");
            entity.Property(e => e.Nom).HasColumnType("VARCHAR(200)");
        }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
