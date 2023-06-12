using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Models
{
    public class Producto
    {

        public string Codigo { get; } = Guid.NewGuid().ToString("N");

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public IFormFile Imagen { get; set; } 

        
        public string EmailEmpresa { get; set;}

        [Required(ErrorMessage = "Este campo es requerido")]
        public int Cantidad { get; set; }

        [DisplayName("Unidad de medida")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string UnidadMedida { get; set; }


    }
}
