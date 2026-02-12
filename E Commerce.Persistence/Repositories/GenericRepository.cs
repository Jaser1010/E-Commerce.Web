using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly StoreDbContext _dbContext;

		public GenericRepository(StoreDbContext dbContext) 
		{
			_dbContext = dbContext;
		}
		public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

		public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id).AsTask();

		public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

		public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
		public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

		public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
		{
			return await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
		}

		public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
		{
			return SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
		}

		public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
		{
			return await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
		}
	}
}
