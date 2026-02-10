using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Services_Abstraction;
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

		public async Task<ProductDTO> GetProductByIdAsync(int id)
		{
			var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
			return _mapper.Map<ProductDTO>(product);
		}

		public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
		{
			var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();	
			return _mapper.Map<IEnumerable<ProductDTO>>(products);
		}

		public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
		{
			var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<TypeDTO>>(types);
		}
	}
}
