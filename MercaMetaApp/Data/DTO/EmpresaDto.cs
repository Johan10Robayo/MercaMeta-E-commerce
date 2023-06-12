using MercaMetaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Data.DTO
{
    public class EmpresaDto
    {
       
        public int Nit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string UrlImagen { get; set; }
        public long Telefono { get; set; }
        public int IdRepresentante { get; set; }
        public int IdUsuario { get; set; }
    }

}
