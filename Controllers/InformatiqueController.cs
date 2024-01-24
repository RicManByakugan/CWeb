using CWeb.Data;
using CWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWeb.Controllers
{
    public class InformatiqueController : Controller
    {
        private readonly CWebDbContext _context;
        public InformatiqueController(CWebDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("_useradmin");
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
                return View(await _context.Personnel.ToListAsync());
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_useradmin");
            return new RedirectResult("/Accueil");
        }

        public async Task<IActionResult> Details(int? id)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var personnel = await _context.Personnel
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (personnel == null)
                {
                    return NotFound();
                }

                return View(personnel);
            }
        }

        public IActionResult Create()
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,Poste,Nom,Prenom,Telephone,Adresse,Sexe")] Personnel personnel)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(personnel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(personnel);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var personnel = await _context.Personnel.FindAsync(id);
                if (personnel == null)
                {
                    return NotFound();
                }
                return View(personnel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Poste,Nom,Prenom,Telephone,Adresse,Sexe")] Personnel personnel)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                if (id != personnel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(personnel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonnelExists(personnel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(personnel);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var personnel = await _context.Personnel
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (personnel == null)
                {
                    return NotFound();
                }

                return View(personnel);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                var personnel = await _context.Personnel.FindAsync(id);
                if (personnel != null)
                {
                    _context.Personnel.Remove(personnel);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool PersonnelExists(int id)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return false;
            }
            else
            {
                return _context.Personnel.Any(e => e.Id == id);
            }
        }
    }
}
