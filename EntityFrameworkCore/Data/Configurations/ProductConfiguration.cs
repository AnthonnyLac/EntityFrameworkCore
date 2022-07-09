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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.BarCode).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR(60)");
            builder.Property(p => p.Value).HasColumnType("DECIMAL(10,2)").IsRequired();
            builder.Property(p => p.ProductType).HasConversion<string>();
        }
    }
}
