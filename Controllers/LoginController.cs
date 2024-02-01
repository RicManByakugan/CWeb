using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly CWebDbContext _context; 
        private readonly StringFunctionCustom stringcustom; 
        public LoginController(CWebDbContext context) 
        { 
            _context = context;
            stringcustom = new StringFunctionCustom();
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
                return new RedirectResult("/Accueil");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(Personnel personnel)
        {
            string name = HttpContext.Request.Form["name"];
            string password = HttpContext.Request.Form["password"];

            var perso = await _context.Personnel.FirstOrDefaultAsync(m => m.Login == name && m.Password == stringcustom.HashString(password));
            if (perso != null)
            {
                if (perso.Poste == "MANAGEMENT")
                {
                    HttpContext.Session.Remove("_useradmin");
                    HttpContext.Session.SetString("_useradmin", perso.Id.ToString());
                    ViewData["message"] = "Connected";
                    perso.Status = "OK";
                    _context.Update(perso);
                    await _context.SaveChangesAsync();
                    return new RedirectResult("/Management");
                }
                else if (perso.Poste == "ACCUEIL 1" || perso.Poste == "ACCUEIL 2" || perso.Poste == "ACCUEIL 3")
                {
                    HttpContext.Session.Remove("_user");
                    HttpContext.Session.SetString("_user", perso.Id.ToString());
                    ViewData["message"] = "Connected";
                    perso.Status = "OK";
                    _context.Update(perso);
                    await _context.SaveChangesAsync();
                    return new RedirectResult("/Accueil/FileAttente");

                }
                else
                {
                    HttpContext.Session.Remove("_userservice");
                    HttpContext.Session.SetString("_userservice", perso.Id.ToString());
                    ViewData["message"] = "Connected";
                    perso.Status = "OK";
                    _context.Update(perso);
                    await _context.SaveChangesAsync();
                    return new RedirectResult("/Service/FileAttente");
                }
            }
            else
            {
                ViewData["message"] = "Connexion refusée";
                return View();
            }
        }
    }
}
