using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService) 
		{
			_productService = productService;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsync();
			if (products == null || !products.Any())
				return NotFound();
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
				return NotFound();
			return Ok(product);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
		{
			var brands = await _productService.GetAllBrandsAsync();
			if (brands == null || !brands.Any())
				return NotFound();
			return Ok(brands);
		}
		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
		{
			var types = await _productService.GetAllTypesAsync();
			if (types == null || !types.Any())
				return NotFound();
			return Ok(types);
		}
	}
}
