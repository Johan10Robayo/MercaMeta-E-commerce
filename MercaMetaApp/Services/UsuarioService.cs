using MercaMetaApp.Data.DAO;

namespace MercaMetaApp.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDao _usuarioDao;

        public UsuarioService(UsuarioDao usuarioDao)
        {
            _usuarioDao = usuarioDao;   
        }

        public int UsuarioExiste(string email)
        {
            int resultado = _usuarioDao.VerificarUsuario(email);

            return resultado;
        
        }

        public string ObtenerHashPasswd(string email)
        {
            string hashPasswd = _usuarioDao.ObtenerHashPasswd(email);
        
            return hashPasswd;  
        }

        public List<string> ObtenerRoles(string email) 
        {
            List<string> roles = _usuarioDao.ObtenerRolesUsuario(email);

            return roles;
        }
    }
}
