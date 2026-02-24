using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Services.Specifications
{
	internal static class ProductSpecificationsHelper
	{
		public static Expression<Func<Product, bool>> GetProductCriateria(ProductQueryParams queryParams)
		{
			return p => (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower())) &&
				(!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId.Value) &&
				(!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId.Value);
		}
	}
}
