using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CWeb.Models;

namespace CWeb.Services
{
	public class PatientService
	{
		private readonly string _connectionString;

		public PatientService(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("CWebDbContext");
		}

		public IEnumerable<Patient> GetPatients()
		{
			var patients = new List<Patient>();

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand(
					"SELECT Id, Ticket, CreatedDate, Receptionne, Accueil, ResultatConsultation, Cout, Finition, Service, ReceptionneService, Nom, Prenom, Sexe, Age, Telephone, Adresse FROM Patient",
					connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							patients.Add(new Patient
							{
								Id = reader.GetInt32(0),
								Ticket = reader.IsDBNull(1) ? null : reader.GetString(1),
								CreatedDate = reader.GetDateTime(2),
								Receptionne = reader.IsDBNull(3) ? null : reader.GetString(3),
								Accueil = reader.IsDBNull(4) ? null : reader.GetString(4),
								ResultatConsultation = reader.IsDBNull(5) ? null : reader.GetString(5),
								Cout = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
								Finition = reader.IsDBNull(7) ? null : reader.GetString(7),
								Service = reader.IsDBNull(8) ? null : reader.GetString(8),
								ReceptionneService = reader.IsDBNull(9) ? null : reader.GetString(9),
								Nom = reader.IsDBNull(10) ? null : reader.GetString(10),
								Prenom = reader.IsDBNull(11) ? null : reader.GetString(11),
								Sexe = reader.IsDBNull(12) ? null : reader.GetString(12),
								Age = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13),
								Telephone = reader.IsDBNull(14) ? null : reader.GetString(14),
								Adresse = reader.IsDBNull(15) ? null : reader.GetString(15)
							});
						}
					}
				}
			}

			return patients;
		}

		public Patient GetPatient(int id)
		{
			Patient patient = null;

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand(
					"SELECT Id, Ticket, CreatedDate, Receptionne, Accueil, ResultatConsultation, Cout, Finition, Service, ReceptionneService, Nom, Prenom, Sexe, Age, Telephone, Adresse FROM Patient WHERE Id = @Id",
					connection))
				{
					command.Parameters.Add(new SqlParameter("@Id", id));
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							patient = new Patient
							{
								Id = reader.GetInt32(0),
								Ticket = reader.IsDBNull(1) ? null : reader.GetString(1),
								CreatedDate = reader.GetDateTime(2),
								Receptionne = reader.IsDBNull(3) ? null : reader.GetString(3),
								Accueil = reader.IsDBNull(4) ? null : reader.GetString(4),
								ResultatConsultation = reader.IsDBNull(5) ? null : reader.GetString(5),
								Cout = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
								Finition = reader.IsDBNull(7) ? null : reader.GetString(7),
								Service = reader.IsDBNull(8) ? null : reader.GetString(8),
								ReceptionneService = reader.IsDBNull(9) ? null : reader.GetString(9),
								Nom = reader.IsDBNull(10) ? null : reader.GetString(10),
								Prenom = reader.IsDBNull(11) ? null : reader.GetString(11),
								Sexe = reader.IsDBNull(12) ? null : reader.GetString(12),
								Age = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13),
								Telephone = reader.IsDBNull(14) ? null : reader.GetString(14),
								Adresse = reader.IsDBNull(15) ? null : reader.GetString(15)
							};
						}
					}
				}
			}

			return patient;
		}

	}
}
