using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.OrderModule
{
	public enum OrderStatus
	{
		Pending,
		PaymentReceived,
		PaymentFailed
	}
}
