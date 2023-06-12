using System.ComponentModel;

namespace MercaMetaApp.Models
{
    public class ProductoEmpresa
    {
        public string Codigo { get; set; } 
        public string Nombre { get; set; }
        public double Precio { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public IFormFile Imagen { get; set; }
        public string UrlImagen { get; set; }
        public int Cantidad { get; set; }

        [DisplayName("Unidad de medida")]
        public string UnidadMedida { get; set; }
    }
}
