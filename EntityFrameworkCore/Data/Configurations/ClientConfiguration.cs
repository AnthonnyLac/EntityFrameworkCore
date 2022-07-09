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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients"); 
            builder.HasKey(p => p.Id);  
            builder.Property(p => p.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.PhoneNumber).HasColumnType("CHAR(11)");
            builder.Property(p => p.Cep).HasColumnType("VARCHAR(8)").IsRequired();
            builder.Property(p => p.State).HasColumnType("CHAR(2))").IsRequired();
            builder.Property(p => p.City).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.PhoneNumber).HasName("idx_number_client");
        }
    }
}
