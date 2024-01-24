using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CWeb.Controllers
{
    public class PatientController : Controller
    {
        private readonly CWebDbContext _context;
        private readonly Nombre nombre;
        
        public PatientController(CWebDbContext context) {
            nombre = new Nombre();
            _context = context;   
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Patient patient)
        {
            bool Novalidate = true;
            string Ticket = nombre.GenerateRandomNumber();
            
            while (Novalidate)
            {
                IEnumerable<Patient> verification = await _context.Patient.Where(m => m.Ticket == Ticket).ToListAsync();
                if (verification != null && verification.Any())
                {
                    Novalidate = true;
                }
                else
                {
                    patient.Ticket = Ticket;
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                    Novalidate = false;
                }
            }

            ViewData["message"] = "Your ticket is : " + Ticket;
            return View();
        
        }


        public async Task<IActionResult> FileAccueil()
        {
            var data = new DataFileAccueil
            {
                Attente = await _context.Patient.Where(m => m.Receptionne == null).ToListAsync(),
                Accueil1 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 1").ToListAsync(),
                Accueil2 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 2").ToListAsync(),
                Accueil3 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 3").ToListAsync()
            };
            return View(data);
        }
    }

    
}
