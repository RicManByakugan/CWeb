﻿using CWeb.Data;
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

            ViewData["message"] = Ticket;
            return View();
        
        }

        public async Task<IActionResult> FileAccueil()
        {
            var data = new DataFileAccueil
            {
                Attente = await _context.Patient.Where(m => m.Receptionne == null).ToListAsync(),
                Accueil1 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 1" && m.ResultatConsultation == null).ToListAsync(),
                Accueil2 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 2" && m.ResultatConsultation == null).ToListAsync(),
                Accueil3 = await _context.Patient.Where(m => m.Receptionne == "OK" && m.Accueil == "ACCUEIL 3" && m.ResultatConsultation == null).ToListAsync()
            };
            return View(data);
        }

        [Route("Patient/FileService/{serviceName}")]
        public async Task<IActionResult> FileService(string serviceName)
        {
            var data = new DataFileAccueil
            {
                Attente = await _context.Patient.Where(m => m.Service == serviceName && m.ReceptionneService == null).ToListAsync(),
                Accueil1 = await _context.Patient.Where(m => m.Service == serviceName && m.ReceptionneService == "OK" && m.Finition == null).ToListAsync(),
            };
            ViewData["service"] = serviceName;
            return View(data);
        }
    }

    
}
