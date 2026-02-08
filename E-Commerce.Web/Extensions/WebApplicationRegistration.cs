using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extensions
{
	public static class WebApplicationRegistration
	{
		public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
			var pendingMigrations = await dbContextService.Database.GetPendingMigrationsAsync();
			if (pendingMigrations.Any())
			{
				await dbContextService.Database.MigrateAsync();
			}
			return app;
		}
		public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
		{
			await using var scope = app.Services.CreateAsyncScope();
			var DataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
			await DataInitializerService.InitializeAsync();
			return app;
		}
	}
}
