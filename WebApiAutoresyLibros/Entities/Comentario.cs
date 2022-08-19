using Microsoft.AspNetCore.Identity;
using WebApiAutoresyLibros.Entitys;

namespace WebApiAutoresyLibros.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
