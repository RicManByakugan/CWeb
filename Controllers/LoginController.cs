using Microsoft.AspNetCore.Mvc;

namespace CWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return View();
            }
            else
            {
                return new RedirectResult("/Accueil");
            }
        }
    }
}
