using E_Commerce.Domain.Entities.ProductMudule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Data.Configurations
{
	public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
	{
		public void Configure(EntityTypeBuilder<ProductBrand> builder)
		{
			builder.Property(X => X.Name)
				.HasMaxLength(100);
		}
	}
}
