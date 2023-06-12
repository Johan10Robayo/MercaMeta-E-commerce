using MercaMetaApp.Models;
using MercaMetaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Data;
using System.Drawing;

namespace MercaMetaApp.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;
        public ProductoController(ProductoService productoService)
        {
            _productoService= productoService;
        }

        public IActionResult InsertarProducto()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertarProducto(Producto producto)
        {
            if (!ModelState.IsValid) return View();

            var resultado = _productoService.InsertarProducto(producto);
            if (resultado == null) return NotFound();

            return RedirectToAction(nameof(MostrarProductosEmpresa));
        }

        
        public IActionResult MostrarProductosEmpresa()
        {
            var usuario = User.Identity.Name;
            var productos = _productoService.ObtenerProductosPorEmpresa(usuario);

            return View(productos);
        }

        public IActionResult EditarProducto(string idProducto)
        {
            var producto = _productoService.ObetenerProductoPorCodigo(idProducto);
            if (producto == null) return NotFound();    


            return View(producto);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProducto(ProductoEmpresa producto)
        {
            int resultado = _productoService.ActualizarProducto(producto);
            if (resultado == 0) return NotFound();

            return RedirectToAction(nameof(MostrarProductosEmpresa));

        }

        public IActionResult EliminarProducto(string idProducto)
        {

            int resultado = _productoService.EliminarProducto(idProducto);
            if (resultado == 0) return BadRequest();

            return RedirectToAction(nameof(MostrarProductosEmpresa));
        }

    }
}
