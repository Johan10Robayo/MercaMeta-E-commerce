using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "Campo requerido")]
        public Persona Persona { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public Usuario Usuario { get; set; }
    }
}
