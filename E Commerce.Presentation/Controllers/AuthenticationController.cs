using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	public class AuthenticationController : ApiBaseController
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}


		// Login endpoint
		// POST: BaseUrl/api/authentication/login
		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
		{
			var result = await _authenticationService.LoginAsync(loginDTO);
			return HandleReult(result);
		}

		// Register endpoint
		// POST: BaseUrl/api/authentication/register
		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
		{
			var result = await _authenticationService.RegisterAsync(registerDTO);
			return HandleReult(result);
		}


		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> CheckEmailExists(string email)
		{
			var result = await _authenticationService.CheckEmailAsync(email);
			return Ok(result);
		}

		[Authorize]
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDTO>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email)!;
			var result = await _authenticationService.GetUserByEmailAsync(email);
			return HandleReult(result);
		}

	}
}
