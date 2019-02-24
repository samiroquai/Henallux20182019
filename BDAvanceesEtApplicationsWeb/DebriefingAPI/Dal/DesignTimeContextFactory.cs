using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DDDDemo.Dal
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<ShopDirectoryContext>
    {
        private const string CONNECTION_STRING_CONFIG_KEY = "DesignTimeConnectionString";
        readonly string connectionString;
        public DesignTimeContextFactory()
        {
            var helper = new ConfigurationHelper(CONNECTION_STRING_CONFIG_KEY);
            connectionString = helper.GetConnectionString();
        }
        public ShopDirectoryContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ShopDirectoryContext> builder = new DbContextOptionsBuilder<ShopDirectoryContext>();
            builder.UseSqlServer(connectionString);
            return new ShopDirectoryContext(builder.Options);
        }
    }
}