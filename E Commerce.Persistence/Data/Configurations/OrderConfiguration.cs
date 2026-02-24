using E_Commerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(X => X.SubTotal)
							.HasPrecision(8, 2);

			builder.OwnsOne(X => X.Address, OEntity =>
			{
				OEntity.Property(x => x.FirstName).HasMaxLength(50);
				OEntity.Property(x => x.LastName).HasMaxLength(50);
				OEntity.Property(x => x.City).HasMaxLength(50);
				OEntity.Property(x => x.Country).HasMaxLength(50);
				OEntity.Property(x => x.Street).HasMaxLength(50);
			});
		}
	}
}
