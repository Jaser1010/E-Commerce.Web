using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.OrderModule
{
	public class Order : BaseEntity<Guid>
	{
		public string UserEmail { get; set; } = default!;
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
		public OrderAddress Address { get; set; } = default!;
		public int DeliveryMethodId { get; set; } // Foreign key for DeliveryMethod
		public DeliveryMethod DeliveryMethod { get; set; } = default!;
		public ICollection<OrderItem> Items { get; set; } = [];
		public decimal SubTotal { get; set; } // Total price of items before adding delivery cost

		// public decimal Total { get; set; } // Total price including delivery cost
		public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
	}
}
