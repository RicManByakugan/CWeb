using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWeb.Controllers
{
    public class ManagementController : Controller
    {
        private readonly CWebDbContext _context;
        private readonly StringFunctionCustom stringcustom;

        public ManagementController(CWebDbContext context)
        {
            stringcustom = new StringFunctionCustom();
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Personnel.ToListAsync());
            }
        }

        public async Task<IActionResult> ManagementListe()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Personnel.Where(m => m.Poste == "MANAGEMENT").ToListAsync());
            }
        }

        public async Task<IActionResult> AccueilListe()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Personnel.Where(m => m.Poste == "ACCUEIL 1" || m.Poste == "ACCUEIL 2" || m.Poste == "ACCUEIL 3").ToListAsync());
            }
        }

        public async Task<IActionResult> ServiceListe()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Personnel.Where(m => m.Poste != "ACCUEIL 1" && m.Poste != "ACCUEIL 2" && m.Poste != "ACCUEIL 3" && m.Poste != "MANAGEMENT").ToListAsync());
            }
        }

        public async Task<IActionResult> ListeActivite()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Acitivite.ToListAsync());
            }
        }

        public async Task<IActionResult> MyListeActivite()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(await _context.Acitivite.Where(m => m.Admin == user_verification.Nom && m.AdminPrenom == user_verification.Prenom).ToListAsync());
            }
        }

        public async Task<IActionResult> Details(int? id, string? mdp)
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;

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
                if (mdp != null)
                {
                    ViewData["messageMdp"] = "Mot de passe rénitialiser avec succès";
                }
                ViewData["Id"] = personnel.Id;
                return View(personnel);
            }
        }

        public async Task<IActionResult> EditProfilePassword()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
                return View(user_verification);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfilePassword(Personnel personnel)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                string oldpassword = HttpContext.Request.Form["oldpassword"];
                string newpassword = HttpContext.Request.Form["newpassword"];
                string newpassword2 = HttpContext.Request.Form["newpassword2"];
                if (newpassword == newpassword2)
                {
                    VarDump.Dump(personnel);
                    if (personnel.Password == stringcustom.HashString(oldpassword))
                    {
                        ViewData["USER"] = personnel.Nom;
                        ViewData["POSTE"] = personnel.Poste;
                        personnel.Password = stringcustom.HashString(newpassword);
                        _context.Update(personnel);
                        await _context.SaveChangesAsync();
                        ViewData["messageReussie"] = "Effectuer";
                        return View(personnel);
                    }
                    else
                    {
                        ViewData["USER"] = personnel.Nom;
                        ViewData["POSTE"] = personnel.Poste;
                        ViewData["message"] = "Ancien mot de passe incorrect";
                        return View(personnel);
                    }
                }
                else
                {
                    ViewData["USER"] = personnel.Login;
                    ViewData["POSTE"] = personnel.Poste;
                    ViewData["message"] = "Deux mot de passe incorrect";
                    return View(personnel);
                }

            }
        }

        public async Task<IActionResult> Create()
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
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;
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
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification == null)
                {
                    return NotFound();
                }
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;

                if (ModelState.IsValid)
                {
                    Activite activite = new Activite();
                    activite.Admin = user_verification.Nom;
                    activite.AdminPrenom = user_verification.Prenom; 
                    activite.AdminPoste = user_verification.Poste; 
                    activite.Descirption = "AJOUT NOUVEAU PERSONNEL";
                    activite.Cible = personnel.Nom;
                    activite.CiblePrenom = personnel.Prenom;
                    activite.CreatedDate = DateTime.Now;
                    _context.Add(activite);
                    await _context.SaveChangesAsync();

                    personnel.Password = stringcustom.HashString(personnel.Password);
                    _context.Add(personnel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(personnel);
            }
        }

        public async Task<IActionResult> RenitialiseMDP(int? id)
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

                var user_action = await _context.Personnel.FirstOrDefaultAsync(m => m.Id == id);
                if (user_action != null)
                {
                    Activite activite = new Activite();
                    activite.Admin = user_verification.Nom;
                    activite.AdminPrenom = user_verification.Prenom;
                    activite.AdminPoste = user_verification.Poste;
                    activite.Descirption = "RENITIALISE MOT DE PASSE";
                    activite.Cible = user_action.Nom;
                    activite.CiblePrenom = user_action.Prenom;
                    activite.CreatedDate = DateTime.Now;
                    _context.Add(activite);
                    await _context.SaveChangesAsync();

                    user_action.Password = stringcustom.HashString("1234");
                    _context.Update(user_action);
                    await _context.SaveChangesAsync();

                    return new RedirectResult("/Management/Details/" + user_action.Id + "?mdp=ok");
                }
                else
                {
                    return new RedirectResult("/Management");
                }
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
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification == null)
                {
                    return NotFound();
                }
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;

                if (id == null)
                {
                    return NotFound();
                }

                var personnel = await _context.Personnel.FindAsync(id);
                if (personnel == null)
                {
                    return NotFound();
                }
                ViewData["Id"] = personnel.Id;
                return View(personnel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Personnel personnel)
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
                        var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                        if (user_verification == null)
                        {
                            return NotFound();
                        }
                        Activite activite = new Activite();
                        activite.Admin = user_verification.Nom;
                        activite.AdminPrenom = user_verification.Prenom;
                        activite.AdminPoste = user_verification.Poste;
                        activite.Descirption = "MISE A JOUR PERSONNEL";
                        activite.Cible = personnel.Nom;
                        activite.CiblePrenom = personnel.Prenom;
                        activite.CreatedDate = DateTime.Now;
                        _context.Add(activite);
                        await _context.SaveChangesAsync();

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
                ViewData["Id"] = personnel.Id;
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
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification == null)
                {
                    return NotFound();
                }
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;

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
                var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
                if (user_verification == null)
                {
                    return NotFound();
                }
                ViewData["USER"] = user_verification.Login;
                ViewData["POSTE"] = user_verification.Poste;

                var personnel = await _context.Personnel.FindAsync(id);
                if (personnel != null)
                {
                    Activite activite = new Activite();
                    activite.Admin = user_verification.Nom;
                    activite.AdminPrenom = user_verification.Prenom;
                    activite.AdminPoste = user_verification.Poste;
                    activite.Descirption = "SUPPRESSION PERSONNEL";
                    activite.Cible = personnel.Nom;
                    activite.CiblePrenom = personnel.Prenom;
                    activite.CreatedDate = DateTime.Now;
                    _context.Add(activite);
                    await _context.SaveChangesAsync();

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

        public async Task<IActionResult> Profile()
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
                ViewData["USER"] = user_verification.Nom;
                ViewData["POSTE"] = user_verification.Poste;
                return View(user_verification);
            }
        }

        public async Task<IActionResult> EditProfile()
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
                ViewData["USER"] = user_verification.Nom;
                ViewData["POSTE"] = user_verification.Poste;
                return View(user_verification);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(Personnel personnel)
        {
            var user = HttpContext.Session.GetString("_useradmin");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                ViewData["USER"] = personnel.Nom;
                ViewData["POSTE"] = personnel.Poste;
                _context.Update(personnel);
                await _context.SaveChangesAsync();
                return View(personnel);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_useradmin");
            return new RedirectResult("/Accueil");
        }

    }
}
