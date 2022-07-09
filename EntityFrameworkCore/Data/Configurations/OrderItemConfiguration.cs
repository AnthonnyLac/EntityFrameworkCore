using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Value).HasColumnType("DECIMAL(10,2)").HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Discount).HasColumnType("DECIMAL(10,2)").HasDefaultValue(0).IsRequired();
        }
    }
}
