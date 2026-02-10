using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
		Task<ProductDTO> GetProductByIdAsync(int id);
		Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
		Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
	}
}
