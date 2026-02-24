using E_Commerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Data.Configurations
{
	public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(X => X.Price)
							.HasPrecision(8, 2);
			builder.Property(X => X.ShortName).HasMaxLength(50);
			builder.Property(X => X.DeliveryTime).HasMaxLength(50);
			builder.Property(X => X.Description).HasMaxLength(100);
		}
	}
}
