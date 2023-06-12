using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Data.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Paswd { get; set; }
        public string Rol { get; set; }
    }
}
