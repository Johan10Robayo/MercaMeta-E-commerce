using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Models
{
    public class Persona
    {
        [Required(ErrorMessage = "Digite la identificación")]
        [DisplayName("Identificación")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite el nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Digite el apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Digite el teléfono")]
        [DisplayName("Teléfono")]
        public long Telefono { get; set; }
        [Required(ErrorMessage = "Digite la dirección")]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

    }
}
