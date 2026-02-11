using E_Commerce.Domain.Entities.ProductMudule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.Specifications
{
	internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
	{
		// Get Product By Id Including Product Type and Product Brand
		public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
		}


		// Get All Products Including Product Type and Product Brand
		public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) : base(ProductSpecificationsHelper.GetProductCriateria(queryParams))
				
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);


			switch(queryParams.Sort)
			{
				case ProductSortingOptions.NameAsc:
					AddOrderBy(p => p.Name);
					break;
				case ProductSortingOptions.NameDesc:
					AddOrderByDescending(p => p.Name);
					break;
				case ProductSortingOptions.PriceAsc:
					AddOrderBy(p => p.Price);
					break;
				case ProductSortingOptions.PriceDesc:
					AddOrderByDescending(p => p.Price);
					break;
				default:
					AddOrderBy(p => p.Id);
					break;
			}



			ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
		}
	}
}
