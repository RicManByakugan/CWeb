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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patient.Where(m => m.Receptionne == null).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Patient patient)
        {
            bool Novalidate = true;
            string Ticket = nombre.GenerateRandomNumber();
            string MessageAddOrNot = "";
            
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

            return View(await _context.Patient.Where(m => m.Receptionne == null).ToListAsync());
        
        }
    }
}
