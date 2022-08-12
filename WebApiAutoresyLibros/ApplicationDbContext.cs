using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Entities;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutoresYLibros>().HasKey(al => new { al.AutorId, al.LibroId});
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public DbSet<AutoresYLibros> autoresYLibros { get; set; }


    }
}
