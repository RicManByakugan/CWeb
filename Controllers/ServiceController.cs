using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CWeb.Controllers
{
    public class ServiceController : Controller
    {
        private readonly CWebDbContext _context;
        public ServiceController(CWebDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                return View(await _context.Patient.Where(m => m.ReceptionneService == null && m.Service == user_verification.Poste).ToListAsync());
            }
        }

        public async Task<IActionResult> Profile()
        {
            var user = HttpContext.Session.GetString("_userservice");
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

        public async Task<IActionResult> EditProfilePassword()
        {
            var user = HttpContext.Session.GetString("_userservice");
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
            var user = HttpContext.Session.GetString("_userservice");
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
                    if (personnel.Password == oldpassword)
                    {
                        ViewData["USER"] = personnel.Login;
                        ViewData["POSTE"] = personnel.Poste;
                        personnel.Password = newpassword;
                        _context.Update(personnel);
                        await _context.SaveChangesAsync();
                        ViewData["messageReussie"] = "Effectuer";
                        return View(personnel);
                    }
                    else
                    {
                        ViewData["USER"] = personnel.Login;
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

        public async Task<IActionResult> EditProfile()
        {
            var user = HttpContext.Session.GetString("_userservice");
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
        public async Task<IActionResult> EditProfile(Personnel personnel)
        {
            var user = HttpContext.Session.GetString("_userservice");
            if (user == null)
            {
                return new RedirectResult("/Login");
            }
            else
            {
                ViewData["USER"] = personnel.Login;
                ViewData["POSTE"] = personnel.Poste;
                _context.Update(personnel);
                await _context.SaveChangesAsync();
                return View(personnel);
            }
        }

        public async Task<IActionResult> ListeReceptionne()
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                return View(await _context.Patient.Where(m => m.ReceptionneService == "OK" && m.Service == user_verification.Poste && m.Finition == null).ToListAsync());
            }
        }


        [HttpPost]
        public async Task<IActionResult> Receptionne(Patient patient)
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                    var patient_verification = await _context.Patient.FirstOrDefaultAsync(m => m.Id.ToString() == idpatient && m.Service != null && m.ReceptionneService== null);
                    if (patient_verification != null)
                    {
                        if (user_verification.Poste != "INFORMATIQUE" || user_verification.Poste != "ACCUEIL 1" || user_verification.Poste != "ACCUEIL 2" || user_verification.Poste != "ACCUEIL 3")
                        {
                            patient_verification.ReceptionneService = "OK";
                            _context.Update(patient_verification);
                            await _context.SaveChangesAsync();
                            return new RedirectResult("/Service/Validation/" + patient_verification.Id);
                        }
                        else
                        {
                            return new RedirectResult("/Login");
                        }
                    }
                    else
                    {
                        return new RedirectResult("/Service");
                    }
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        public async Task<IActionResult> Retire(int? id)
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                        var patient_verification = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
                        if (patient_verification != null)
                        {
                            patient_verification.ReceptionneService = null;
                            _context.Update(patient_verification);
                            await _context.SaveChangesAsync();
                            return new RedirectResult("/Service/ListeReceptionne");
                        }
                        else
                        {
                            return new RedirectResult("/Service");
                        }
                    }
                    else
                    {
                        return new RedirectResult("/Service");
                    }
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        public async Task<IActionResult> Validation(int? id)
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                        var patient_verification = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id && m.Finition == null && m.ReceptionneService != null);
                        if (patient_verification != null)
                        {
                            if (user_verification.Poste != "INFORMATIQUE" || user_verification.Poste != "ACCUEIL 1" || user_verification.Poste != "ACCUEIL 2" || user_verification.Poste != "ACCUEIL 3")
                            {
                                ViewData["USER"] = user_verification.Login;
                                ViewData["POSTE"] = user_verification.Poste;
                                return View(patient_verification);
                            }
                            else
                            {
                                return new RedirectResult("/Login");
                            }
                        }
                        else
                        {
                            return new RedirectResult("/Service");
                        }
                    }
                    else
                    {
                        return new RedirectResult("/Service");
                    }
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Validation(Patient patient)
        {
            var user = HttpContext.Session.GetString("_userservice");
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
                    patient.ReceptionneService = "OK";
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                    return new RedirectResult("/Service");
                }
                else
                {
                    return new RedirectResult("/Login");
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            var user = HttpContext.Session.GetString("_userservice");
            var user_verification = await _context.Personnel.FirstOrDefaultAsync(m => m.Id.ToString() == user);
            if (user_verification == null)
            {
                return NotFound();
            }
            user_verification.Status = null;
            _context.Update(user_verification);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("_userservice");
            return new RedirectResult("/Accueil");
        }


    }
}
