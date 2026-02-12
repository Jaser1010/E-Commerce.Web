using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence
{
	internal static class SpecificationsEvaluator
	{
		// Create Query - Build Query 
		// _dbcontext.Product.Include(P => P.Brand).Include(P => P.Type)

		public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> EntryPoint,
			ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
		{
			var Query = EntryPoint;
			if (specifications is not null)
			{
				if (specifications.Criteria is not null)
				{
					Query = Query.Where(specifications.Criteria);
				}
				if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
				{
					Query = specifications.IncludeExpressions.Aggregate(Query,
						(CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
				}
				if (specifications.OrderBy is not null)
				{
					Query = Query.OrderBy(specifications.OrderBy);
				}
				if (specifications.OrderByDescending is not null)
				{
					Query = Query.OrderByDescending(specifications.OrderByDescending);
				}
				if (specifications.IsPaginated)
				{
					Query = Query.Skip(specifications.Skip).Take(specifications.Take);
				}
			}

			return Query;
		}
	}
}
