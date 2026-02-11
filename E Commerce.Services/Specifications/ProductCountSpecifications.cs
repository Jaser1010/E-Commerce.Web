using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.Specifications
{
	internal class ProductCountSpecifications : BaseSpecifications<Product, int>
	{
		public ProductCountSpecifications(ProductQueryParams queryParams) : base(ProductSpecificationsHelper.GetProductCriateria(queryParams))
		{
			
		}
	}
}
