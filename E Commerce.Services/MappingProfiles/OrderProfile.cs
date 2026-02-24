using AutoMapper;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.MappingProfiles
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<AddressDTO, OrderAddress>().ReverseMap();
			CreateMap<OrderItem, OrderItemDTO>()
				.ForCtorParam("ProductName", opt => opt.MapFrom(s => s.Product.ProductName))
				.ForCtorParam("PictureUrl", opt => opt.MapFrom(s => s.Product.PictureUrl))
				.ForCtorParam("Price", opt => opt.MapFrom(s => s.Price))
				.ForCtorParam("Quantity", opt => opt.MapFrom(s => s.Quantity));
			CreateMap<Order, OrderToReturnDTO>()
				.ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
				.ForCtorParam("UserEmail", opt => opt.MapFrom(src => src.UserEmail))
				.ForCtorParam("OrderItems", opt => opt.MapFrom(src => src.Items))
				.ForCtorParam("Address", opt => opt.MapFrom(src => src.Address)) // or ShipToAddress
				.ForCtorParam("DeliveryMethod", opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
				.ForCtorParam("OrderStatus", opt => opt.MapFrom(src => src.OrderStatus.ToString()))
				.ForCtorParam("OrderDate", opt => opt.MapFrom(src => src.OrderDate))
				.ForCtorParam("Subtotal", opt => opt.MapFrom(src => src.SubTotal))
				.ForCtorParam("Total", opt => opt.MapFrom(src => src.GetTotal()));
			CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();
		}
	}
}
