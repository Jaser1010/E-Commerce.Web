using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
	public interface ICacheRepository
	{
		Task<string?> GetAsync(string CacheKey);
		Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive);
	}
}
