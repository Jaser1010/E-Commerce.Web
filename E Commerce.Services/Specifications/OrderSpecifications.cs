using E_Commerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.Specifications
{
	public class OrderSpecifications : BaseSpecifications<Order, Guid>
	{
		// Get All Orders For User By Email
		public OrderSpecifications(string Email) : base(order => order.UserEmail == Email)
		{
			AddInclude(order => order.Items);
			AddInclude(order => order.DeliveryMethod);
			AddOrderByDescending(order => order.OrderDate);
		}
		// Get Order By Id For User
		public OrderSpecifications(Guid Id) : base(order => order.Id == Id)
		{
			AddInclude(order => order.Items);
			AddInclude(order => order.DeliveryMethod);		}
	}
}
