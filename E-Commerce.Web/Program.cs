
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DataSeed;
using E_Commerce.Persistence.IdentityData.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfiles;
using E_Commerce.Services_Abstraction;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Commerce.Web
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Add services to the container
			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen();
			builder.Services.AddOpenApi();
			builder.Services.AddDbContext<StoreDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");
			builder.Services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			//builder.Services.AddAutoMapper(X => X.LicenseKey = "", typeof(ProductProfile).Assembly); // If you have a license key for AutoMapper, you can set it here. Otherwise, you can omit this line.
			builder.Services.AddAutoMapper(typeof(ServicesAssemblyReference).Assembly);
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddSingleton<IConnectionMultiplexer>(SP =>
			{
				return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
			});
			builder.Services.AddScoped<IBasketRepository, BasketRepository>();
			builder.Services.AddScoped<IBasketService, BasketService>();
			builder.Services.AddScoped<ICacheRepository, CacheRepository>();
			builder.Services.AddScoped<ICacheService, CacheService>();
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
			});

			builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddIdentityCore<ApplicationUser>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<StoreIdentityDbContext>();

			builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
			#endregion

			var app = builder.Build();

			#region Data seeding [Extension methods for database migration and seeding]
			await app.MigrateDatabaseAsync();
			await app.MigrateIdentityDatabaseAsync();
			await app.SeedDatabaseAsync();
			await app.SeedIdentityDatabaseAsync();
			#endregion

			#region Confiure the HTTP request pipeline
			app.UseMiddleware<ExceptionHandlerMiddleWare>();


			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.MapControllers();
			#endregion

			await app.RunAsync();
		}
	}
}
