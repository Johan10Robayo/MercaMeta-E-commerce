using MercaMetaApp.Models;
using MercaMetaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MercaMetaApp.Controllers
{
    
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;
        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService= empresaService;
        }

        [Authorize(Roles = "Cliente,Admin")]
        public IActionResult Index()
        {
            var empresas = _empresaService.ObtenerEmpresas();

            return View(empresas);
        }

        [Authorize(Roles = "Cliente,Admin")]
        public IActionResult MostrarProductosEmpresa(int idEmpresa)
        {
            var productos = _empresaService.ObtenerProductosEmpresaCliente(idEmpresa);
            if (productos == null) return NotFound();

            return View(productos);
        }

        

    }
}
