using AutoMapper;
using E_Commerce.Domain.Entities.BasketMudule;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.MappingProfiles
{
	public class BasketProfiIe : Profile
	{
		public BasketProfiIe()
		{
			CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
			CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
		}
	}
}
