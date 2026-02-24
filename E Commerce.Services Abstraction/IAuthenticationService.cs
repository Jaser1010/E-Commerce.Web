using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface IAuthenticationService
	{
		// Login
		// Email , Password => Token , DisplayName , Email
		Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

		// Register
		// Email , Password , UserName , DisplayName , PhoneNumber => Token, DisplayName, Email
		Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
		Task<bool> CheckEmailAsync(string email);
		Task<Result<UserDTO>> GetUserByEmailAsync(string email);
	}
}
