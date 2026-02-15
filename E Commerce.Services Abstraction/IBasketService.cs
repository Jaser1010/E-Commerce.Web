using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface IBasketService
	{
		Task<BasketDTO> GetBasketAsync(string id);
		Task<BasketDTO> CreatOrUpdateBasketAsync(BasketDTO basket);
		Task<bool> DeleteBasketAsync(string id);
	}
}
