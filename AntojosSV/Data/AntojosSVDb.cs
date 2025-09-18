using AntojosSV.Models;
using Microsoft.EntityFrameworkCore;

namespace AntojosSV.Data
{
    public class AntojosSVDb : DbContext
    { 
        public AntojosSVDb(DbContextOptions<AntojosSVDb> options) : base(options){}

        public DbSet<Encargos> Encargos => Set<Encargos>();
        public DbSet<Comidas> Comidas => Set<Comidas>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encargos>(e =>
            {
                e.Property(x => x.Direccion).IsRequired();
                e.Property(x => x.Telefono).IsRequired();
                e.Property(x => x.FechaEntrega).IsRequired();
            });

            modelBuilder.Entity<Categoria>(e =>
            {
                e.Property(x => x.Nombre).IsRequired();
                e.HasMany(x=>x.Comidas)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Usuario>(e =>
            {
                e.Property(x => x.Nombre).IsRequired();
                e.Property(x => x.Apellido).IsRequired();
                e.Property(x => x.Telefono).IsRequired();
                e.Property(x => x.CorreoElectronico).IsRequired();
                e.HasMany(x => x.Encargos)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Comidas>(e =>
            {
                e.Property(x => x.Nombre).IsRequired();
                e.Property(x => x.Precio).HasPrecision(10, 2).HasDefaultValue(0m);
                e.Property(x => x.Descripcion).IsRequired();
                e.Property(x => x.UrlImagen).IsRequired();
                e.HasMany(x => x.Encargos)
                .WithOne(x => x.Comidas)
                .HasForeignKey(x => x.ComidasId)
                .OnDelete(DeleteBehavior.Restrict);

            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
 