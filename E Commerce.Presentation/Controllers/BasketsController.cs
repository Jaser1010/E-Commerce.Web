using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	public class BasketsController : ApiBaseController
	{
		private readonly IBasketService _basketService;

		public BasketsController(IBasketService basketService)
		{
			_basketService = basketService;
		}



		// GET: BaseUrl/api/baskets?id=
		[HttpGet]
		public async Task<ActionResult<BasketDTO>> GetBasket(string id)
		{
			var Basket = await _basketService.GetBasketAsync(id);
			return Ok(Basket);
		}



		// POST: BaseUrl/api/baskets
		[HttpPost]
		public async Task<ActionResult<BasketDTO>> UpdateBasket(BasketDTO basket)
		{
			var updatedBasket = await _basketService.CreatOrUpdateBasketAsync(basket);
			return Ok(updatedBasket);
		}



		// Delete: BaseUrl/api/baskets/{id}
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteBasket(string id)
		{
			var Result = await _basketService.DeleteBasketAsync(id);
			return Ok(Result);
		}
	}
}
