
using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using MercaMetaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercaMetaApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;
        private readonly EmpresaService _empresaService;
        public AdminController(AdminService adminService, EmpresaService empresaService)
        {
            _adminService = adminService;
            _empresaService = empresaService;
        }


        public IActionResult Empresas()
        {
            var empresas = _adminService.GetEmpresasAdmin();
            return View(empresas);
        } 


        public IActionResult CrearEmpresa()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CrearEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var resultado = _empresaService.InsertarEmpresa(empresa);
                return RedirectToAction("Empresas");
            }



            return View(empresa);
        }


       

        public IActionResult EditarEmpresa(int idEmpresa)
        {
            var empresa = _adminService.EmpresaPorIdAdmin(idEmpresa);   

            return View(empresa);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditarEmpresa(EmpresaAdminDto empresa)
        {
            int filas = _adminService.EditarEmpresa(empresa);
            if (filas == 0) return NotFound();

            return RedirectToAction("Empresas");
        }


        public IActionResult EliminarEmpresa(int idEmpresa)
        {
            var filas = _adminService.EliminarEmpresaAdmin(idEmpresa);

            if (filas == 0) return NotFound();

            return RedirectToAction("Empresas");
        }


    }
}
