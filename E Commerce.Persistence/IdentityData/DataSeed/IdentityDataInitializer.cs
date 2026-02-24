using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
	public class IdentityDataInitializer : IDataInitializer
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<IdentityDataInitializer> _logger;

		public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
										RoleManager<IdentityRole> roleManager,
										ILogger<IdentityDataInitializer> logger)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}
		public async Task InitializeAsync()
		{
			try
			{
				if (!_roleManager.Roles.Any())
				{
					await _roleManager.CreateAsync(new IdentityRole("Admin"));
					await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
				}
				if (!_userManager.Users.Any())
				{
					var User01 = new ApplicationUser()
					{
						DisplayName = "Mohamed Tarek",
						UserName = "MohamedTarek",
						Email = "MohamedTarek@gmail.com",
						PhoneNumber = "01022738566"
					};
					var User02 = new ApplicationUser()
					{
						DisplayName = "Salma Tarek",
						UserName = "SalmaTarek",
						Email = "SalmaTarek@gmail.com",
						PhoneNumber = "01022738555"
					};

					await _userManager.CreateAsync(User01, "P@ssw0rd");
					await _userManager.CreateAsync(User02, "P@ssw0rd");

					await _userManager.AddToRoleAsync(User01, "Admin");
					await _userManager.AddToRoleAsync(User02, "SuperAdmin");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while seeding identity database : Message = {ex.Message}");
			}
		}
	}
}
