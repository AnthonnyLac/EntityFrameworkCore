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
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.StartedIn).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.OrderStatus).HasConversion<string>();
            builder.Property(p => p.TypeFreigth).HasConversion<int>();
            builder.Property(p => p.observation).HasColumnType("VARCHAR(512)");

            builder.HasMany(p => p.items)
            .WithOne(p => p.Order)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
