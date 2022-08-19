using System.ComponentModel.DataAnnotations;

namespace WebApiAutoresyLibros.Dtoos
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
