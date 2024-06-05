using CWeb.Data;
using CWeb.Models;
using CWeb.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CWeb.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		[HttpPost("add")]
		public async Task<ActionResult> AddPatient([FromBody] Patient request)
		{
			if (request == null || string.IsNullOrEmpty(request.Ticket))
			{
				return BadRequest("Le ticket existe déjà");
			}

			var patient = new Patient
			{
				Ticket = request.Ticket,
				CreatedDate = DateTime.Now
			};
			bool added = await _patientService.AddPatient(patient);
			if (added)
			{
				return Ok("Ticket inserez");
			}
			else
			{
				return BadRequest("Le ticket existe déjà");
			}
		}



		[HttpGet]
		public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
		{
			var patients = await _patientService.GetPatients();
			return Ok(patients);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Patient>> GetPatient(int id)
		{
			var patient = await _patientService.GetPatient(id);
			if (patient == null)
			{
				return NotFound();
			}
			return Ok(patient);
		}

		[HttpGet("accueil/{accueil}")]
		public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByAccueil(string accueil)
		{
			var patients = await _patientService.GetPatientsByAccueil(accueil);
			if (patients == null || !patients.Any())
			{
				return NotFound();
			}
			return Ok(patients);
		}

		[HttpGet("service/{service}")]
		public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByServiceNotNull(string service)
		{
			var patients = await _patientService.GetPatientsByServiceNotNull(service);
			if (patients == null || !patients.Any())
			{
				return NotFound();
			}
			return Ok(patients);
		}

		[HttpGet("servicenull/{service}")]
		public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByServiceIsNull(string service)
		{
			var patients = await _patientService.GetPatientsByServiceIsNull(service);
			if (patients == null || !patients.Any())
			{
				return NotFound();
			}
			return Ok(patients);
		}


	}
}
