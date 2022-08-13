using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Entities;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Entitys
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public List<AutoresYLibros> AutoresYLibros { get; set; }

    }
}
