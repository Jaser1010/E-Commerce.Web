using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface ICacheService
	{
		Task<string?> GetAsync(string CacheKey);
		Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive);
	}
}
