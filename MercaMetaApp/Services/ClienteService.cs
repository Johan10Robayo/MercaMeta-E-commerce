using MercaMetaApp.Data.DAO;
using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using Org.BouncyCastle.Crypto.Generators;

namespace MercaMetaApp.Services
{
    public class ClienteService
    {
        
        private readonly PersonaDao _personaDao;
        private readonly UsuarioDao _usuarioDao;
        private readonly ClienteDao _clienteDao;
        public ClienteService(UsuarioDao usuarioDao, PersonaDao personaDao,ClienteDao cleinteDao)
        {
            _usuarioDao = usuarioDao;
            _clienteDao = cleinteDao; 
            _personaDao = personaDao;
        }

        public ClienteFkDto InsertarCliente(Cliente cliente)
        {
            var persona = cliente.Persona;
            var personaDto = new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            };

            var usuario = cliente.Usuario;
            var passHash = BCrypt.Net.BCrypt.HashPassword(usuario.Paswd);
            var usuarioDto = new UsuarioDto
            {
                Email = usuario.Email,
                Paswd = passHash,
                Rol = "Cliente"

            };

            var personaInsertada = _personaDao.InsertarPersona(personaDto);
            if (personaInsertada == null) return null;

            var usuarioInsertado = _usuarioDao.InsertarUsuario(usuarioDto);
            if (usuarioInsertado == null) return null;

            var idUsuario = _usuarioDao.ObtenerIdUsuario(usuarioInsertado.Email,usuarioInsertado.Rol);
            if (idUsuario == 0) return null;

            var clienteFkDto = new ClienteFkDto { IdPersona = personaInsertada.Id, IdUsario = idUsuario };

            var clienteInsertado = _clienteDao.InsertarCliente(clienteFkDto);


            return clienteInsertado;
        }
    }
}
