using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.BasketDTOs
{
	public record BasketDTO(string Id, ICollection<BasketItemDTO> Items);
}
