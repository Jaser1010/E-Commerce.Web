using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	public class ProductsController : ApiBaseController
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService) 
		{
			_productService = productService;
		}





		// GET api/products
		[Authorize(Roles = "admin")]
		[HttpGet]
		[RedisCache]
		public async Task<ActionResult<PaginatedResuIt<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
		{
			var products = await _productService.GetAllProductsAsync(queryParams);
			return Ok(products);
		}


		// GET api/products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProductById(int id)
		{
			var Result = await _productService.GetProductByIdAsync(id);
			return HandleReult<ProductDTO>(Result);
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
