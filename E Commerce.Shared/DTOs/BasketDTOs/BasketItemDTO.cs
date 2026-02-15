using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Shared.DTOs.BasketDTOs
{
	public record BasketItemDTO(
		int Id,
		string ProductName,
		string PictureUrl,
		[Range(1,double.MaxValue)]
		decimal Price,
		[Range(1,100)]
		int Quantity
		);
}
