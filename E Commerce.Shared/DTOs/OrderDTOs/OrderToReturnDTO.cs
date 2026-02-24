using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.OrderDTOs
{
	public record OrderToReturnDTO(Guid Id, string UserEmail, ICollection<OrderItemDTO> OrderItems,
									AddressDTO Address, string DeliveryMethod, string OrderStatus,
									DateTimeOffset OrderDate, decimal Subtotal, decimal Total);
}
