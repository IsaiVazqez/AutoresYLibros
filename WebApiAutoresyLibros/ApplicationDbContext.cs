using Microsoft.EntityFrameworkCore;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
    }
}
