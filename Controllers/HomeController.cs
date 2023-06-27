using Microsoft.AspNetCore.Mvc;

namespace gestion_devis.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}