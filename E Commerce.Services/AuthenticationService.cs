using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public AuthenticationService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginDTO.Email);
			if (user is null)
				return Error.InvalidCrendentials("User.InvalidCrendentials");

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
			if (!isPasswordValid)
				return Error.InvalidCrendentials("User.InvalidCrendentials");

			return new UserDTO(	user.Email!,user.DisplayName,	"Token");
		}
		public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
		{
			var user = new ApplicationUser()
			{
				Email = registerDTO.Email,
				DisplayName = registerDTO.DisplayName,
				PhoneNumber = registerDTO.PhoneNumber,
				UserName = registerDTO.UserName
			};
			var IdentityResult = await _userManager.CreateAsync(user, registerDTO.Password);
			if (IdentityResult.Succeeded)
				return new UserDTO(user.Email!, user.DisplayName, "Token");

			return IdentityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

		}
	}
}
