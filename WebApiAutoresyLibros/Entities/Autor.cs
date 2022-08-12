using System.ComponentModel.DataAnnotations;
using WebApiAutoresyLibros.Entities;
using WebApiAutoresyLibros.Validaciones;

namespace WebApiAutoresyLibros.Entitys
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener más de {1} carácteres")]

        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public List<AutoresYLibros> autoresYLibros { get; set; }

    }
}
