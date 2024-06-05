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
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ProductService _productService;

		public ProductsController(ProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Product>> GetProducts()
		{
			return Ok(_productService.GetProducts());
		}

		
		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			var product = _productService.GetProduct(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		public ActionResult<Product> PostProduct(Product product)
		{
			_productService.AddProduct(product);
			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public IActionResult PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_productService.UpdateProduct(id, product);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			_productService.DeleteProduct(id);
			return NoContent();
		}

	}
}
