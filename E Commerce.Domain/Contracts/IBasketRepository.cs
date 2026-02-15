using E_Commerce.Domain.Entities.BasketMudule;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string basketId);
		Task<CustomerBasket> CreatOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
