using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CWeb.Services;
using System.Collections.Generic;

namespace CWeb.Controllers
{
	[Route("api/patient")]
	[ApiController]
	public class ApiPatientController : ControllerBase
	{
		private readonly PatientService _patientService;

		public ApiPatientController(PatientService patientService)
		{
			_patientService = patientService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Patient>> GetPatients()
		{
			return Ok(_patientService.GetPatients());
		}

		
		[HttpGet("{id}")]
		public ActionResult<Patient> GetPatient(int id)
		{
			var patient = _patientService.GetPatient(id);
			if (patient == null)
			{
				return NotFound();
			}
			return Ok(patient);
		}

		[HttpGet("accueil/{accueil}")]
		public ActionResult<IEnumerable<Patient>> GetPatientsByAccueil(string accueil)
		{
			var patients = _patientService.GetPatientsByAccueil(accueil);
			if (patients == null || !patients.Any())
			{
				return NotFound();
			}
			return Ok(patients);
		}

		[HttpGet("service/{service}")]
		public ActionResult<IEnumerable<Patient>> GetPatientsByService(string service)
		{
			var patients = _patientService.GetPatientsByService(service);
			if (patients == null || !patients.Any())
			{
				return NotFound();
			}
			return Ok(patients);
		}


	}
}
