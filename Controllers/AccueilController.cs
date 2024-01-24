using CWeb.Data;
using CWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CWeb.Controllers
{
    public class AccueilController : Controller
    {
        private readonly CWebDbContext _context;
        public AccueilController(CWebDbContext context) { 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification == null)
                {
                    return NotFound();
                }
                ViewData["USER"] = user_verification.Nom + " " + user_verification.Prenom;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Patient.Where(m => m.Receptionne == null).ToListAsync());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Receptionne(Patient patient)
        {
            var user = HttpContext.Session.GetString("_user");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification != null)
                {
                    string idpatient = HttpContext.Request.Form["idpatient"];
                    var patient_verification = await _context.Patient.FirstOrDefaultAsync(m => m.Id.ToString() == idpatient && m.Nom == null && m.Prenom == null);
                    if (patient_verification != null)
                    {
                        if (user_verification.Poste == "ACCUEIL 1" || user_verification.Poste == "ACCUEIL 2" || user_verification.Poste == "ACCUEIL 3")
                        {
                            patient_verification.Receptionne = "OK";
                            patient_verification.Accueil = user_verification.Poste;
                            patient_verification = patient;
                            _context.Update(patient_verification);
                            await _context.SaveChangesAsync();
                            return new RedirectResult("/Accueil/Consultation");
                        }
                        else
                        {
                            return new RedirectResult("/Login");
                        }
                    }
                    else
                    {
                        return new RedirectResult("/Accueil");
                    }
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        public IActionResult Consultation()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_user");
            return new RedirectResult("/Accueil");
        }

    }
}
