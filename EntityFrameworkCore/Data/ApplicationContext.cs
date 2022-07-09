using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=(localdb)\\mssqllocaldb;initial catalog=EFCore;Integrated Security=true", 
                p => p.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null).MigrationsHistoryTable("EFCore_Migrations"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly); //Falando pra aplicação onde procurar as configurações das entidades (No assembly nesse caso)
            MapForgottenProperties(modelBuilder);
        }

        private void MapForgottenProperties(ModelBuilder modelBuilder) 
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes()) 
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties) 
                {
                    if (string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue) 
                    {
                        //property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
