using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Services
{
	public class CacheService : ICacheService
	{
		private readonly ICacheRepository _cacheRepository;

		public CacheService(ICacheRepository cacheRepository)
		{
			_cacheRepository = cacheRepository;
		}
		public async Task<string?> GetAsync(string CacheKey)
		{
			return await _cacheRepository.GetAsync(CacheKey);
		}

		public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
		{
			var value = JsonSerializer.Serialize(CacheValue);
			await _cacheRepository.SetAsync(CacheKey, value, TimeToLive);
		}
	}
}
