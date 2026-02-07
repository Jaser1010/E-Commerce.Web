using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.ProductMudule
{
	public class ProductBrand : BaseEntity<int>
	{
		public string Name { get; set; } = default!;
	}
}
