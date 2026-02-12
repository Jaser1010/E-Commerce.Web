using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
	public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
		Task<TEntity?> GetByIdAsync(TKey id);
		Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Remove(TEntity entity);
		Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
	}
}
