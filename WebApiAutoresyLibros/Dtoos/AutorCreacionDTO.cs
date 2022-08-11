using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Dtoos
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de {1} carácteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
    }
}
