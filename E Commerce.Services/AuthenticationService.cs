using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<bool> CheckEmailAsync(string email)
		{
			var User = await _userManager.FindByEmailAsync(email);
			return User is not null;
		}

		public async Task<Result<UserDTO>> GetUserByEmailAsync(string email)
		{
			var User = await _userManager.FindByEmailAsync(email);
			if(User is null)
				return Error.NotFound("User.NotFound", $"No User With Email {email} Was Found");
			return new UserDTO(User.Email!, User.DisplayName, await CreateTokenAsync(User));
		}

		public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginDTO.Email);
			if (user is null)
				return Error.InvalidCrendentials("User.InvalidCrendentials");

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
			if (!isPasswordValid)
				return Error.InvalidCrendentials("User.InvalidCrendentials");

			var Token = await CreateTokenAsync(user);
			return new UserDTO(	user.Email!,user.DisplayName, Token);
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
			{
				var Token = await CreateTokenAsync(user);
				return new UserDTO(user.Email!, user.DisplayName, Token);
			}
			return IdentityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();

		}

		private async Task<string> CreateTokenAsync(ApplicationUser user)
		{
			// Tocken [Issuer , audience , claims , expiration , signingCredentials]

			var Claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
			};


			var Roles = await _userManager.GetRolesAsync(user);
			foreach (var role in Roles)
			{
				 Claims.Add(new Claim(ClaimTypes.Role, role));
			}


			var SecretKey = _configuration["JWTOptions:SecretKey"];
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
			var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


			var Token = new JwtSecurityToken(
				issuer: _configuration["JWTOptions:Issuer"],
				audience: _configuration["JWTOptions:Audience"],
				claims: Claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: Credentials
			);

			
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
