using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using CWeb.Services;

namespace CWeb.Controllers
{
	public class PatientController : Controller
	{

		private readonly PatientService _patientService;
		private readonly Nombre nombre;

		public PatientController(PatientService patientService)
		{
			_patientService = patientService;
			nombre = new Nombre();
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
				bool ticketExists = await _patientService.TicketExists(Ticket);
				if (ticketExists)
				{
					Novalidate = true;
				}
				else
				{
					patient.Ticket = Ticket;
					patient.CreatedDate = DateTime.Now;
					bool added = await _patientService.AddPatient(patient);
					if (added)
					{
						Novalidate = false;
					}
				}
			}
			ViewData["message"] = Ticket;
			return View();
		}

		public async Task<IActionResult> FileAccueil()
		{
			var today = DateTime.Now.Date;
			var data = new DataFileAccueil
			{
				Attente = await _patientService.GetPatients(),
				Accueil1 = await _patientService.GetPatientsByAccueil("ACCUEIL 1"),
				Accueil2 = await _patientService.GetPatientsByAccueil("ACCUEIL 2"),
				Accueil3 = await _patientService.GetPatientsByAccueil("ACCUEIL 3")
			};
			var CountFA = await _patientService.GetPatients();
			ViewData["CountFA"] = CountFA.Count();
			return View(data);
		}

		[Route("Patient/FileService/{serviceName}")]
		public async Task<IActionResult> FileService(string serviceName)
		{
			var today = DateTime.Now.Date;
			var data = new DataFileAccueil
			{
				Attente = await _patientService.GetPatientsByServiceIsNull(serviceName),
				Accueil1 = await _patientService.GetPatientsByServiceNotNull(serviceName)
			};
			ViewData["service"] = serviceName;
			var CountFA = await _patientService.GetPatientsByServiceIsNull(serviceName);
			ViewData["CountFA"] = CountFA.Count();
			return View(data);
		}

	}
}
