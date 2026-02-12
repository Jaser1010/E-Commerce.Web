using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.BasketMudule
{
	public class CustomerBasket
	{
		public string Id { get; set; } = default!; // Guid : Created From Client Side [Frontend]
		public ICollection<BasketItem> Items { get; set; } = [];
	}
}
