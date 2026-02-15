using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Repositories
{
	public class CacheRepository : ICacheRepository
	{
		private readonly IDatabase _database;

		public CacheRepository(IConnectionMultiplexer connection)
		{
			_database = connection.GetDatabase();
		}
		public async Task<string?> GetAsync(string CacheKey)
		{
			var cacheValue = await _database.StringGetAsync(CacheKey);
			return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
		}

		public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
		{
			await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
		}
	}
}
