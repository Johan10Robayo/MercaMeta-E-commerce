using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MercaMetaApp.Models
{
    public class Empresa
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Nit { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set;}
        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Teléfono")]
        public long Telefono { get; set; }
        public Persona Representante { get; set; }

        public Usuario Usuario { get; set; }  
    }
}
