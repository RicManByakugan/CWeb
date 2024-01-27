using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
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
                ViewData["USER"] = user_verification.Nom;
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
                            _context.Update(patient_verification);
                            await _context.SaveChangesAsync();
                            return new RedirectResult("/Accueil/Consultation/" + patient_verification.Id);
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

        public async Task<IActionResult> Consultation(int? id)
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
                    if (id != null)
                    {
                        var patient_verification = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id && m.Nom == null && m.Prenom == null);
                        if (patient_verification != null)
                        {
                            if (user_verification.Poste == "ACCUEIL 1" || user_verification.Poste == "ACCUEIL 2" || user_verification.Poste == "ACCUEIL 3")
                            {
                                return View(patient_verification);
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
                        return new RedirectResult("/Accueil");
                    }
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Consultation(Patient patient)
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
                    VarDump.Dump(patient);
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                    return new RedirectResult("/Accueil");
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_user");
            return new RedirectResult("/Accueil");
        }

    }
}
