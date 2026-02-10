using AutoMapper;
using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.MappingProfiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile() 
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
				.ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());
			CreateMap<ProductBrand, BrandDTO>();
			CreateMap<ProductType, TypeDTO>();
		}
	}
}
