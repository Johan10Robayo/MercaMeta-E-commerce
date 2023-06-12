
using System.ComponentModel;

namespace MercaMetaApp.Data.DTO
{
    public class PersonaDto
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public string Apellido { get; set; }
        [DisplayName("Teléfono")]
        public long Telefono { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
    }
}
