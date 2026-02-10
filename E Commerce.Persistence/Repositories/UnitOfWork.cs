using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _dbContext;
		private readonly Dictionary<Type, object> _repositories = [];

		public UnitOfWork(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			var EntityType = typeof(TEntity);
			if (_repositories.TryGetValue(EntityType, out object? repository))
			{
				return (IGenericRepository<TEntity, TKey>)repository;
			}
			var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
			_repositories.Add(EntityType, newRepo);
			return newRepo;
		}

		public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
	}
}
