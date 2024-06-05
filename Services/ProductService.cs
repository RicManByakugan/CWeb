using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CWeb.Models;

namespace CWeb.Services
{
	public class ProductService
	{
		private readonly string _connectionString;

		public ProductService(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("CWebDbContext");
		}

		public IEnumerable<Product> GetProducts()
		{
			var products = new List<Product>();

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("SELECT Id, Name, Price FROM Products", connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							products.Add(new Product
							{
								Id = reader.GetInt32(0),
								Name = reader.GetString(1),
								Price = reader.GetDecimal(2)
							});
						}
					}
				}
			}

			return products;
		}

		public Product GetProduct(int id)
		{
			Product product = null;

			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("SELECT Id, Name, Price FROM Products WHERE Id = @Id", connection))
				{
					command.Parameters.Add(new SqlParameter("@Id", id));
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							product = new Product
							{
								Id = reader.GetInt32(0),
								Name = reader.GetString(1),
								Price = reader.GetDecimal(2)
							};
						}
					}
				}
			}

			return product;
		}

		public void AddProduct(Product product)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("INSERT INTO Products (Name, Price) VALUES (@Name, @Price); SELECT SCOPE_IDENTITY();", connection))
				{
					command.Parameters.Add(new SqlParameter("@Name", product.Name));
					command.Parameters.Add(new SqlParameter("@Price", product.Price));

					product.Id = Convert.ToInt32(command.ExecuteScalar());
				}
			}
		}

		public void UpdateProduct(int id, Product product)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", connection))
				{
					command.Parameters.Add(new SqlParameter("@Id", id));
					command.Parameters.Add(new SqlParameter("@Name", product.Name));
					command.Parameters.Add(new SqlParameter("@Price", product.Price));

					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteProduct(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				using (var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection))
				{
					command.Parameters.Add(new SqlParameter("@Id", id));
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
