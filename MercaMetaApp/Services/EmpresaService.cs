using MercaMetaApp.Controllers;
using MercaMetaApp.Data.DAO;
using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using System.Reflection;

namespace MercaMetaApp.Services
{
    public class EmpresaService
    {
        private readonly PersonaDao _personaDao;
        private readonly UsuarioDao _usuarioDao;
        private readonly ProductoDao _productoDao;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly EmpresaDao _empresaDao; 
        

        public EmpresaService(PersonaDao personaDao, UsuarioDao usuarioDao, IWebHostEnvironment webHostEnvironment, EmpresaDao empresaDao, ProductoDao productoDao)
        {
            _personaDao= personaDao;
            _usuarioDao= usuarioDao;    
            _hostingEnvironment = webHostEnvironment;
            _empresaDao = empresaDao;
            _productoDao =  productoDao;
        }

        public EmpresaDto InsertarEmpresa(Empresa empresa)
        {
            var persona = empresa.Representante;
            var personaDto = new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono
            };

            

            var usuario = empresa.Usuario;
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(usuario.Paswd);
            var usuarioDto = new UsuarioDto
            {
                Email = usuario.Email,
                Paswd = hashPassword,
                Rol = "Empresa"

            };

            var personaInsertada = _personaDao.InsertarPersona(personaDto);
            if (personaInsertada == null)
            {

                return null;

            }
            

            var usuarioInsertado = _usuarioDao.InsertarUsuario(usuarioDto);
            if (usuarioInsertado == null) { return null; }

            int idUsuario = _usuarioDao.ObtenerIdUsuario(usuarioInsertado.Email,usuarioInsertado.Rol);
            if (idUsuario == 0) return null;

            //guardar la imagen
            var imagen = empresa.Imagen;
            // Obtener el nombre del archivo
            string nombreArchivo = Path.GetFileName(imagen.FileName);

            // Crear la ruta de guardado
            string rutaDeGuardado = Path.Combine(_hostingEnvironment.WebRootPath, "img", nombreArchivo);
            string rutadb = "/img/" + nombreArchivo;

            int longitud = rutaDeGuardado.Length;
            // Crear un archivo en la ruta de guardado
            using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
            {
                // Copiar los datos de la imagen en el archivo
                imagen.CopyTo(stream);
            }

            var empresaDto = new EmpresaDto
            {
                Nit = empresa.Nit,
                Nombre = empresa.Nombre,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                UrlImagen = rutadb,
                IdRepresentante = personaInsertada.Id,
                IdUsuario = idUsuario
            };

            var empresaInsertada = _empresaDao.InsertarEmpresa(empresaDto);
            if (empresaInsertada == null) return null;

            return empresaInsertada;
        }

        public List<EmpresaViewDto> ObtenerEmpresas()
        { 
           var empresas =  _empresaDao.GetEmpresas();

            if (empresas == null) return null;

            return empresas;
        }

        public List<ProductoClienteDto> ObtenerProductosEmpresaCliente(int idEmpresa)
        { 
            var productos = _productoDao.ObtenerProductosCliente(idEmpresa);
            return productos;
        }
    }
}
