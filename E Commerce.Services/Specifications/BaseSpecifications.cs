using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Services.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{

		#region Criteria
		public Expression<Func<TEntity, bool>> Criteria { get; }
		protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
		{
			Criteria = criteria;
		}
		#endregion

		#region Includes
		public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
		protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
		{
			IncludeExpressions.Add(includeExp); 
		}
		#endregion

		#region Sorting
		public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
		public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
		protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
		{
			OrderByDescending = orderByDescendingExpression;
		}
		#endregion

		#region Pagination
		public int Skip { get; private set; }
		public int Take { get; private set; }
		public bool IsPaginated { get; private set; }

		protected void ApplyPagination(int pageIndex, int pageSize)
		{
			IsPaginated = true;
			Take = pageSize;
			Skip = (pageIndex - 1) * pageSize;
		}
		#endregion

	}
}
