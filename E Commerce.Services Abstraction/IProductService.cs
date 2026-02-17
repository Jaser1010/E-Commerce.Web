using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface IProductService
	{
		Task<PaginatedResuIt<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
		Task<Result<ProductDTO>> GetProductByIdAsync(int id);
		Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
		Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
	}
}
