using MercaMetaApp.Data.DAO;
using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MercaMetaApp.Services
{
    public class AdminService
    {
        private readonly EmpresaDao _empresaDao;
        private readonly PersonaDao _personaDao;
        private readonly UsuarioDao _usuarioDao;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminService(EmpresaDao empresaDao, IWebHostEnvironment hostingEnvironment, PersonaDao personaDao, UsuarioDao usuarioDao)
        {
            _empresaDao = empresaDao;
            _hostingEnvironment = hostingEnvironment;
            _personaDao = personaDao;
            _usuarioDao = usuarioDao;
        }

        public List<EmpresaAdminDto> GetEmpresasAdmin()
        {
            var empresas = _empresaDao.GetEmpresasAdmin();


            return empresas;
        }

        public EmpresaAdminDto EmpresaPorIdAdmin(int idEmpresa)
        {

            var empresa = _empresaDao.GetEmpresaAdminPorId(idEmpresa);
            
            
            return empresa;
        }

        public int EditarEmpresa(EmpresaAdminDto empresa)
        {
            var representante = empresa.Representante;
            var id_persona_actual = _empresaDao.GetIdRepresentante(empresa.Nit);

            var filasSqlPersona = _personaDao.ActualizarPersonaAdmin(representante,id_persona_actual);
            if (filasSqlPersona == 0) return 0;
            int filasSqlEmpresa = 0;

            if (empresa.Imagen!=null)
            {
               
                var urlImagen = _empresaDao.GetUrlEmpresa(empresa.Nit);
                
                string rutaServer = _hostingEnvironment.WebRootPath;
                string rutaImagenEliminar = rutaServer + urlImagen.Replace("/img/","\\img\\");

                //eliminar imagen anterior
                try
                {
                    if (File.Exists(rutaImagenEliminar)) File.Delete(rutaImagenEliminar);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }
                

                //crear nueva imagen
                var imagen = empresa.Imagen;
                
                string nombreArchivo = Path.GetFileName(imagen.FileName);

                string rutaDeGuardado = Path.Combine(rutaServer, "img", nombreArchivo);
                string rutadb = "/img/" + nombreArchivo;
                empresa.UrlImagen = rutadb;

                using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
                {
                    
                    imagen.CopyTo(stream);
                }


            }

            filasSqlEmpresa = _empresaDao.ActualizarEmpresaAdmin(empresa);

            return filasSqlEmpresa;
        }

        public int EliminarEmpresaAdmin(int idEmpresa)
        {
            string urlImagen = _empresaDao.GetUrlEmpresa(idEmpresa);
            int idPersona = _empresaDao.GetIdRepresentante(idEmpresa);
            int idUsuario = _empresaDao.GetIdUsuario(idEmpresa);

            var filasSqlEmpresa = _empresaDao.EliminarEmpresa2(idEmpresa,idUsuario,idPersona);
            if (filasSqlEmpresa == 0) return 0;


            /*int filasSqlPersona = _personaDao.EliminarPersona(idPersona);
            if (filasSqlPersona == 0) return 0;


            int filasSqlUsuario = _usuarioDao.EliminarUsuario(idUsuario); 
            if (filasSqlUsuario == 0) return 0; */

            

            
            string rutaServer = _hostingEnvironment.WebRootPath;
            string rutaImagenEliminar = rutaServer + urlImagen.Replace("/img/", "\\img\\");

            
            try
            {
                if (File.Exists(rutaImagenEliminar)) File.Delete(rutaImagenEliminar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            return filasSqlEmpresa;

        } 


    }
}
