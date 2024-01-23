using CWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CWeb.Controllers
{
    public class AccueilController : Controller
    {
        public IActionResult Index()
        {
            return View();
            /*var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                return View();
            }*/
        }

    }
}
