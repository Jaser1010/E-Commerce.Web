
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
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
			app.MapControllers();
			#endregion

			await app.RunAsync();
		}
	}
}
