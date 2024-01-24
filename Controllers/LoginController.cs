using CWeb.Data;
using CWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly CWebDbContext _context;
        public LoginController(CWebDbContext context) 
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return View();
            }
            else
            {
                return new RedirectResult("/Personnel");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(Personnel personnel)
        {
            string name = HttpContext.Request.Form["name"];
            string password = HttpContext.Request.Form["password"];

            var perso = await _context.Personnel.FirstOrDefaultAsync(m => m.Login == name && m.Password == password);
            if (perso != null)
            {
                HttpContext.Session.Remove("_user");
                HttpContext.Session.SetString("_user", perso.Id.ToString());
                ViewData["message"] = "Connected";
                return new RedirectResult("/Personnel");
            }
            else
            {
                ViewData["message"] = "Connexion refused";
                return View();
            }
        }
    }
}
