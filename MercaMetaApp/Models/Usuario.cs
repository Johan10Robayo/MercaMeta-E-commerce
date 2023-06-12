using Google.Protobuf.WellKnownTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MercaMetaApp.Models
{
    public class Usuario
    {
        
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Digite el email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite la contraseña")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Paswd { get; set; }
        

    }
}
