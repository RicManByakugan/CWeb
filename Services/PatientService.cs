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

		public async Task<bool> AddPatient(Patient patient)
		{
			bool ticketExists = await TicketExists(patient.Ticket);
			if (ticketExists)
			{
				return false;
			}

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = @"INSERT INTO Patient (Ticket, CreatedDate) VALUES (@Ticket, @CreatedDate)";
					command.Parameters.AddWithValue("@Ticket", patient.Ticket);
					command.Parameters.AddWithValue("@CreatedDate", patient.CreatedDate);

					int rowsAffected = await command.ExecuteNonQueryAsync();
					return rowsAffected > 0;
				}
			}
		}

		public async Task<bool> TicketExists(string ticket)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = "SELECT COUNT(*) FROM Patient WHERE Ticket = @Ticket AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)";
					command.Parameters.AddWithValue("@Ticket", ticket);
					int count = (int)await command.ExecuteScalarAsync();
					return count > 0;
				}
			}
		}

		public async Task<IEnumerable<Patient>> GetPatients()
		{
			var patients = new List<Patient>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand("SELECT * FROM Patient WHERE Receptionne IS NULL AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)", connection))
				{
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							patients.Add(ReadPatient(reader));
						}
					}
				}
			}

			return patients;
		}

		public async Task<Patient> GetPatient(int id)
		{
			Patient patient = null;

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand("SELECT * FROM Patient WHERE Id = @Id AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)", connection))
				{
					command.Parameters.Add(new SqlParameter("@Id", id));
					using (var reader = await command.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							patient = ReadPatient(reader);
						}
					}
				}
			}

			return patient;
		}

		public async Task<IEnumerable<Patient>> GetPatientsByAccueil(string accueil)
		{
			var patients = new List<Patient>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand("SELECT * FROM Patient WHERE Accueil = @Accueil AND Receptionne='OK' AND ResultatConsultation IS NULL AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)", connection))
				{
					command.Parameters.Add(new SqlParameter("@Accueil", accueil));
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							patients.Add(ReadPatient(reader));
						}
					}
				}
			}

			return patients;
		}

		public async Task<IEnumerable<Patient>> GetPatientsByServiceNotNull(string service)
		{
			var patients = new List<Patient>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand("SELECT * FROM Patient WHERE Service = @Service AND ReceptionneService='OK' AND Finition IS NULL AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)", connection))
				{
					command.Parameters.Add(new SqlParameter("@Service", service));
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							patients.Add(ReadPatient(reader));
						}
					}
				}
			}

			return patients;
		}

		public async Task<IEnumerable<Patient>> GetPatientsByServiceIsNull(string service)
		{
			var patients = new List<Patient>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				using (var command = new SqlCommand("SELECT * FROM Patient WHERE Service = @Service AND ReceptionneService IS NULL AND CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)", connection))
				{
					command.Parameters.Add(new SqlParameter("@Service", service));
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							patients.Add(ReadPatient(reader));
						}
					}
				}
			}

			return patients;
		}

		private Patient ReadPatient(SqlDataReader reader)
		{
			return new Patient
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
