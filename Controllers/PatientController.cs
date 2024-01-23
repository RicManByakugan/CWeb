using Microsoft.AspNetCore.Mvc;

namespace CWeb.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
