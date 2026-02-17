using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Presentation.Attributes
{
	internal class RedisCacheAttribute : ActionFilterAttribute
	{
		private readonly int _durationInMin;

		public RedisCacheAttribute(int durationInMin = 5)
		{
			_durationInMin = durationInMin;
		}






		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			// Get Cache Service From The Dependency Injection Container
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
			// Create Cache Key Based On The Request Path And Query String
			var CacheKey = CreateCacheKey(context.HttpContext.Request);
			// Check if the cache data exists for the current request
			var CacheValue = await cacheService.GetAsync(CacheKey);
			// If Cache Data Exists, Return Cache Data And Skip Executing Of Endpoint
			if (!string.IsNullOrEmpty(CacheValue))
			{
				context.Result = new ContentResult()
				{
					Content = CacheValue,
					ContentType = "application/json",
					StatusCode = StatusCodes.Status200OK
				};
				return;
			}
			// If Cache Data Not Exists, Execute The Endpoint And Store The Result In Cache If 200 OK Response
			var executedContext = await next.Invoke();
			if(executedContext.Result is OkObjectResult result)
			{
				await cacheService.SetAsync(CacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMin));
			}
		}

		private string CreateCacheKey(HttpRequest request)
		{
			StringBuilder key = new StringBuilder();
			key.Append(request.Path); // api/products
			foreach (var item in request.Query.OrderBy(X=>X.Key))
			{
				key.Append($"|{item.Key}-{item.Value}"); // api/products|brandId-1|typeId-2
			}
			return key.ToString();
		}
	}
}
