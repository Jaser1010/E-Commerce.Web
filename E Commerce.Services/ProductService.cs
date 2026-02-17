using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork , IMapper mapper) 
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}







		public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<BrandDTO>>(brands);
		}

		public async Task<PaginatedResuIt<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
		{
			// Specifications => Get All Products Including Product Type and Product Brand
			var Repo = _unitOfWork.GetRepository<Product, int>();
			var Specification = new ProductWithTypeAndBrandSpecification(queryParams);
			var products = await Repo.GetAllAsync(Specification);	
			var DataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(products);
			var CountSpec = new ProductCountSpecifications(queryParams);
			var CountOfAllProducts = await Repo.CountAsync(CountSpec);
			return new PaginatedResuIt<ProductDTO>(queryParams.PageIndex, DataToReturn.Count(), CountOfAllProducts, DataToReturn);

		}

		public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
		{
			var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<TypeDTO>>(types);
		}

		public async Task<Result<ProductDTO>> GetProductByIdAsync(int id)
		{
			var Specification = new ProductWithTypeAndBrandSpecification(id);
			var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specification);
			if (product is null)
				return Error.NotFound("Product.NotFound", $"Product With Id {id} Is Not Found");
			return _mapper.Map<ProductDTO>(product);
			// Implicit Casting To Result<ProductDTO>.Fail - Result<ProductDTO>.Ok
		}
	}
}
