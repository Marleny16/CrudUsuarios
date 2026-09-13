using System.ComponentModel.DataAnnotations;

namespace CrudUsuarios.Models
{
    /// <summary>
    /// Modelo (M de MVC): representa la entidad Usuario del CRUD.
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;
    }
}
