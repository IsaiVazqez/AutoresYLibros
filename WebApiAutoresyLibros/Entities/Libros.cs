using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Entitys
{
    public class Libros
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

    }
}
