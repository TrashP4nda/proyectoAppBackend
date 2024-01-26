using Microsoft.EntityFrameworkCore;
using proyectoApi.Models;

namespace proyectoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Camara> Camaras { get; set; } // Add this
        public DbSet<UsuarioCamaraFavorite> UsuarioCamaraFavorites { get; set; } // Add this
        public DbSet<UsuarioIncidenciaFavorite> UsuarioIncidenciaFavorites { get; set; } // Add this

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure UsuarioCamaraFavorite
            modelBuilder.Entity<UsuarioCamaraFavorite>()
                .HasKey(ucf => new { ucf.UsuarioId, ucf.CamaraId });
            modelBuilder.Entity<UsuarioCamaraFavorite>()
                .HasOne(ucf => ucf.Usuario)
                .WithMany(u => u.FavoriteCameras)
                .HasForeignKey(ucf => ucf.UsuarioId);
            modelBuilder.Entity<UsuarioCamaraFavorite>()
                .HasOne(ucf => ucf.Camara)
                .WithMany(c => c.FavoriteByUsuarios)
                .HasForeignKey(ucf => ucf.CamaraId);

            // Configure UsuarioIncidenciaFavorite
            modelBuilder.Entity<UsuarioIncidenciaFavorite>()
                .HasKey(uif => new { uif.UsuarioId, uif.IncidenciaId });
            modelBuilder.Entity<UsuarioIncidenciaFavorite>()
                .HasOne(uif => uif.Usuario)
                .WithMany(u => u.FavoriteIncidencias)
                .HasForeignKey(uif => uif.UsuarioId);
            modelBuilder.Entity<UsuarioIncidenciaFavorite>()
                .HasOne(uif => uif.Incidencia)
                .WithMany(i => i.FavoriteByUsuarios)
                .HasForeignKey(uif => uif.IncidenciaId);
        }
    }
}