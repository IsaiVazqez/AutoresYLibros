using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Dtoos
{
    public class LibroCreacionDTO
    {
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
