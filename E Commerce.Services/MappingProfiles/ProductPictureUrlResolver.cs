using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.MappingProfiles
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration) 
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.PictureUrl))
			{
				return string.Empty;
			}
			if (source.PictureUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
			{
				return source.PictureUrl;
			}
			var BaseUrl = _configuration.GetSection("Urls")["BaseUrl"];
			if (string.IsNullOrEmpty(BaseUrl))
			{
				return string.Empty;
			}
			var picUrl = $"{BaseUrl}{source.PictureUrl}";
			return picUrl;
		}
	}
}
