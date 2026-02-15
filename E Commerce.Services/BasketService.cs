using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketMudule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services
{
	public class BasketService : IBasketService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketService(IBasketRepository basketRepository, IMapper mapper) 
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
		}
		public async Task<BasketDTO> CreatOrUpdateBasketAsync(BasketDTO basket)
		{
			var customerBasket = _mapper.Map<BasketDTO,CustomerBasket>(basket);
			var CreatedOrUpdatedBasket = await _basketRepository.CreatOrUpdateBasketAsync(customerBasket);
			return _mapper.Map<CustomerBasket, BasketDTO>(CreatedOrUpdatedBasket);
		}

		public async Task<bool> DeleteBasketAsync(string id) => await _basketRepository.DeleteBasketAsync(id);
		
		public async Task<BasketDTO> GetBasketAsync(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			if(basket is null)
				throw new BasketNotFoundException(id);
			return _mapper.Map<CustomerBasket,BasketDTO>(basket!);
		}
	}
}
