using CWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CWeb.Controllers
{
    public class PersonnelController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                return View();
            }
        }

    }
}
