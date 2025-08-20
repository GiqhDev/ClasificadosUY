using ClasificadosUY.Models;
using Microsoft.EntityFrameworkCore;

namespace ClasificadosUY.Data
{
  public class ClasificadosDbContext: DbContext
  {
    public ClasificadosDbContext(DbContextOptions<ClasificadosDbContext> options) : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Anuncio> Anuncios { get; set; }
    public DbSet<Destacado> Destacados { get; set; }
    public DbSet<Subcategoria> Subcategorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Relación 1–1 Anuncio - Destacado
      modelBuilder.Entity<Anuncio>()
          .HasOne(a => a.Destacado)
          .WithOne(d => d.Anuncio)
          .HasForeignKey<Destacado>(d => d.IdAnuncio);

      // Anuncio - Categoria (1:N) sin cascada
      modelBuilder.Entity<Anuncio>()
          .HasOne(a => a.Categoria)
          .WithMany(c => c.Anuncios)
          .HasForeignKey("IdCategoria")
          .OnDelete(DeleteBehavior.Restrict);

      // Anuncio - Subcategoria (1:N) sin cascada
      modelBuilder.Entity<Anuncio>()
          .HasOne(a => a.Subcategoria)
          .WithMany(s => s.Anuncios)
          .HasForeignKey("IdSubcategoria")  // FK shadow si no la tienes explícita
          .OnDelete(DeleteBehavior.Restrict);

      // Subcategoria - Categoria (1:N) sin cascada
      modelBuilder.Entity<Subcategoria>()
          .HasOne(s => s.Categoria)
          .WithMany(c => c.Subcategorias)
          .HasForeignKey("IdCategoria")
          .OnDelete(DeleteBehavior.Restrict);

      // Subcategoria - Categoria (1:N) sin cascada
      modelBuilder.Entity<Anuncio>()
          .HasOne(u => u.Usuario)
          .WithMany(c => c.Anuncios)
          .HasForeignKey("IdUsuario")
          .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
