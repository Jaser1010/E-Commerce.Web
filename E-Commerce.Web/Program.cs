
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfiles;
using E_Commerce.Services_Abstraction;
using E_Commerce.Web.Extensions;
using Microsoft.EntityFrameworkCore;

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
			builder.Services.AddScoped<IDataInitializer, DataInitializer>(); 
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			//builder.Services.AddAutoMapper(X => X.LicenseKey = "", typeof(ProductProfile).Assembly); // If you have a license key for AutoMapper, you can set it here. Otherwise, you can omit this line.
			builder.Services.AddAutoMapper(typeof(ServicesAssemblyReference).Assembly);
			builder.Services.AddScoped<IProductService, ProductService>();
			#endregion

			var app = builder.Build();

			#region Data seeding [Extension methods for database migration and seeding]
			await app.MigrateDatabaseAsync();
			await app.SeedDatabaseAsync();
			#endregion
			
			#region Confiure the HTTP request pipeline
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
