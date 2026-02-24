using AutoMapper;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Shared.DTOs.OrderDTOs;
using Microsoft.Extensions.Configuration;
using System.Buffers.Text;

namespace E_Commerce.Services.MappingProfiles
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
	{
		private readonly IConfiguration _configuration;
		public OrderItemPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrWhiteSpace(source.Product.PictureUrl)) return string.Empty;
			if (source.Product.PictureUrl.StartsWith("http")) return source.Product.PictureUrl;
			var BaseUrl = _configuration.GetSection("Urls")["BaseUrl"];
			if (string.IsNullOrEmpty(BaseUrl)) return string.Empty;
			var OrderUrl = $"{BaseUrl}{source.Product.PictureUrl}";
			return OrderUrl;
		}
	}
}