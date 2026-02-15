using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketMudule;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
		public BasketRepository(IConnectionMultiplexer connection) // We can also IDistributedCache : Redis Cache
		{
			_database = connection.GetDatabase();
		}

		public async Task<CustomerBasket> CreatOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
		{
			var JsonBasket = JsonSerializer.Serialize(basket);
			var ttl = (timeToLive == default) ? TimeSpan.FromDays(7) : timeToLive;
			var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket,ttl);
			if (IsCreatedOrUpdated)
			{
				return await GetBasketAsync(basket.Id);
			}
			else
			{
				return null!;
			}
		}
		public async Task<bool> DeleteBasketAsync(string basketId) => await _database.KeyDeleteAsync(basketId);
		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var data = await _database.StringGetAsync(basketId);
			if (data.IsNullOrEmpty)
			{
				return null;
			}
			return JsonSerializer.Deserialize<CustomerBasket>(data.ToString());
		}
	}
}
