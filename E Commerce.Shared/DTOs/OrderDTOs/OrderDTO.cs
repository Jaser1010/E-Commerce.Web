using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.OrderDTOs
{
	public record OrderDTO(string BasketId, int DeliveryMethodId, AddressDTO Address);
}
