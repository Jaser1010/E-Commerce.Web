using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Shared.DTOs.IdentityDTOs
{
	public record RegisterDTO(
		[EmailAddress] string Email,
		string Password,
		string UserName,
		string DisplayName,
		[Phone] string PhoneNumber
	);
}
