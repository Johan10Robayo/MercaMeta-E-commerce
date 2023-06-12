using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Models
{
    public class EmpresaView
    {
        
        public int Nit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string UrlImagen { get; set; }
        public long Telefono { get; set; }
        public Persona Representante { get; set; }
        public Usuario Usuario { get; set; }
    }
}
