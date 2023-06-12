using MercaMetaApp.Models;
using MercaMetaApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace MercaMetaApp.Controllers
{
    public class SiteController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly UsuarioService _usuarioService;

        public SiteController(ClienteService clienteService, UsuarioService usuarioService)
        {
            _clienteService = clienteService;
            _usuarioService = usuarioService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var resultado = _clienteService.InsertarCliente(cliente);
                if (resultado != null) return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }

        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            if (!ModelState.IsValid) return View();

            int VerificacionUsuario = _usuarioService.UsuarioExiste(usuario.Email);
            if (VerificacionUsuario == 0) 
            {
                TempData["Error"] = "El usuario digitado no existe";

                return View();
            }
            

            string hashPasswd = _usuarioService.ObtenerHashPasswd(usuario.Email);

            bool VerificarIdentidad = BCrypt.Net.BCrypt.Verify(usuario.Paswd,hashPasswd);

            if (VerificarIdentidad)
            {
                var Claims = new List<Claim> {
                    new Claim (ClaimTypes.Name, usuario.Email)

                };

                var roles = _usuarioService.ObtenerRoles(usuario.Email);

                foreach (var rol in roles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, rol));

                }

                var identity = new ClaimsIdentity(Claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToAction("IndexUsuario");
            }
            else
            {

                TempData["Error"] = "La contraseña es incorrecta. Por favor, inténtelo de nuevo";
            }

            
            return View(); 
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            return RedirectToAction("Index","Home");
        }

        public IActionResult AccesoDenegado()
        {


            return View();
        }

        [Authorize(Roles = "Admin,Empresa,Cliente")]
        public IActionResult IndexUsuario()
        {

            return View();
        }
    } 
}
