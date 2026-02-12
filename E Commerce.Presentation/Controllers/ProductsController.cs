using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
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





		// GET api/products
		[HttpGet]
		public async Task<ActionResult<PaginatedResuIt<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
		{
			var products = await _productService.GetAllProductsAsync(queryParams);
			return Ok(products);
		}


		// GET api/products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			return Ok(product);
		}



		// GET api/products/brands
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
		{
			var brands = await _productService.GetAllBrandsAsync();
			return Ok(brands);
		}




		// GET api/products/typess
		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
		{
			var types = await _productService.GetAllTypesAsync();
			return Ok(types);
		}
	}
}
