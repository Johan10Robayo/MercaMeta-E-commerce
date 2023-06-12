using MercaMetaApp.Data;
using MercaMetaApp.Data.DAO;
using MercaMetaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace MercaMetaApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConexionDb _conexion;


        public HomeController(ILogger<HomeController> logger, ConexionDb conexion)
        {
            _logger = logger;
            _conexion = conexion;
        }

        public IActionResult Index()
        {
                

            return View();
        }

        public IActionResult Privacy()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}