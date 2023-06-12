using MercaMetaApp.Data.DAO;
using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using Org.BouncyCastle.Bcpg;

namespace MercaMetaApp.Services
{
    public class ProductoService
    {
        private readonly ProductoDao _productoDao;
        private readonly EmpresaDao _empresaDao;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductoService(ProductoDao productoDao, IWebHostEnvironment hostingEnvironment, EmpresaDao empresaDao)
        {
            _productoDao = productoDao;
            _hostingEnvironment = hostingEnvironment;
            _empresaDao = empresaDao;
        }

        public ProductoDto InsertarProducto(Producto producto)
        {
            
            var imagen = producto.Imagen;
            string nombreArchivo = Path.GetFileName(imagen.FileName);

            
            string rutaDeGuardado = Path.Combine(_hostingEnvironment.WebRootPath, "img","productos", nombreArchivo);
            string rutadb = "/img/productos/" + nombreArchivo;

        
            using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
            {
                
                imagen.CopyTo(stream);
            }

            int idEmpresa = _empresaDao.ObetenerIdEmpresaPorEmail(producto.EmailEmpresa);

            var productoDto = new ProductoDto
            {
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                IdEmpresa = idEmpresa,
                Cantidad = producto.Cantidad,
                UnidadMedida = producto.UnidadMedida,
                UrlImagen = rutadb
                
            };

            var resultado = _productoDao.InsertarProducto(productoDto);



            return resultado;
        }

        public List<ProductoDto> ObtenerProductosPorEmpresa(string usuario)
        {

            int idEmpresa = _empresaDao.ObetenerIdEmpresaPorEmail(usuario);
            if (idEmpresa == 0) return null;

            var listaProductos = _productoDao.ObtenerProductosPorEmpresa(idEmpresa);
        
            return listaProductos;
            
        }

        public ProductoEmpresa ObetenerProductoPorCodigo(string codigoProducto)
        {
            var producto = _productoDao.ObtenerProductoPorCodigo(codigoProducto);
        
            return producto;
        }

        public int ActualizarProducto(ProductoEmpresa producto)
        {
            if (producto.Imagen != null)
            {
                var urlImagen = _productoDao.ObtenerUrlImagenProducto(producto.Codigo);

                string rutaServer = _hostingEnvironment.WebRootPath;
                string rutaImagenEliminar = rutaServer + urlImagen.Replace("/img/productos/", "\\img\\productos\\");

                //eliminar imagen anterior
                try
                {
                    if (File.Exists(rutaImagenEliminar))
                    {
                        File.Delete(rutaImagenEliminar);
                    }
                       
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }


                //crear nueva imagen
                var imagen = producto.Imagen;

                string nombreArchivo = Path.GetFileName(imagen.FileName);

                string rutaDeGuardado = Path.Combine(rutaServer, "img","productos", nombreArchivo);
                string rutadb = "/img/productos/" + nombreArchivo;
                producto.UrlImagen = rutadb;

                using (var stream = new FileStream(rutaDeGuardado, FileMode.Create))
                {

                    imagen.CopyTo(stream);
                }

            }

            var productoDto = new ProductoDto
            {
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                UrlImagen= producto.UrlImagen,
                Cantidad= producto.Cantidad,    
                UnidadMedida = producto.UnidadMedida 

            };

            int filasSql = _productoDao.ActualizarProducto(productoDto);

            return filasSql;
        
        }

        public int EliminarProducto(string codigoProducto)
        {
            var urlImagen = _productoDao.ObtenerUrlImagenProducto(codigoProducto);
            int resultado = _productoDao.EliminarProducto(codigoProducto);

            string rutaServer = _hostingEnvironment.WebRootPath;
            string rutaImagenEliminar = rutaServer + urlImagen.Replace("/img/productos/", "\\img\\productos\\");

            //eliminar imagen anterior
            try
            {
                if (File.Exists(rutaImagenEliminar))
                {
                    File.Delete(rutaImagenEliminar);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            return resultado;
        }

    }
}
