using MercaMetaApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MercaMetaApp.Data.DTO
{
    public class EmpresaAdminDto
    {
        public int Nit { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        public string UrlImagen { get; set; }
        public IFormFile Imagen { get; set; }

        [DisplayName("Teléfono")]
        public long Telefono { get; set; }
        public PersonaDto Representante { get; set; }

    }
}
