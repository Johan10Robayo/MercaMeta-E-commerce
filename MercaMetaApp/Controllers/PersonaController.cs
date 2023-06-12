using Microsoft.AspNetCore.Mvc;

namespace MercaMetaApp.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
