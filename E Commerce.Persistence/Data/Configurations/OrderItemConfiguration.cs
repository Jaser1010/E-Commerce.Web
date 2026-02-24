using E_Commerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Persistence.Data.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.Property(oi => oi.Price)
				.HasColumnType("decimal(18,2)");
			builder.OwnsOne(x => x.Product, OEntity =>
			{
				OEntity.Property(pi => pi.ProductName).HasMaxLength(100);
				OEntity.Property(pi => pi.PictureUrl).HasMaxLength(200);
			});
		}
	}
}
