using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Persistence.Data.DataSeed
{
	public class DataInitializer : IDataInitializer
	{
		private readonly StoreDbContext _dbContext;

		public DataInitializer(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task InitializeAsync()
		{
			try
			{
				var HasProducts = await _dbContext.Products.AnyAsync();
				var HasBrands = await _dbContext.Brands.AnyAsync();
				var HasTypes = await _dbContext.Types.AnyAsync();
				if (HasProducts && HasBrands && HasTypes) return;

				if (!HasBrands)
				{
					await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _dbContext.Brands);
				}
				if(!HasTypes)
				{
					await SeedDataFromJsonAsync<ProductType, int>("types.json", _dbContext.Types);
					await _dbContext.SaveChangesAsync();
				}
				if (!HasProducts)
				{
					await SeedDataFromJsonAsync<Product, int>("products.json", _dbContext.Products);
					await _dbContext.SaveChangesAsync();
				}
					
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
				throw;
			}
		}

		private async Task SeedDataFromJsonAsync<T, TKey>(string FileName, DbSet<T> dbset) where T : BaseEntity<TKey>
		{

			// var FilePath = @"..\E-Commerce.Persistence\Data\DataSeed\JSONFiles\" + FileName;
			// var FilePath = @$"{AppDomain.CurrentDomain.BaseDirectory}Data\\DataSeed\\JSONFiles\\{FileName}";
			var FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "DataSeed", "JSONFiles", FileName);
			
			if(!File.Exists(FilePath)) throw new FileNotFoundException($"File {FilePath} is not Exists");

			try
			{
				using var dataStram = File.OpenRead(FilePath);
				var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStram, new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true 
				});
				if (data != null && data.Count > 0)
				{
					await dbset.AddRangeAsync(data);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"An error occurred while seeding data from {FileName}: {ex.Message}");
			}


		}
	}
}
