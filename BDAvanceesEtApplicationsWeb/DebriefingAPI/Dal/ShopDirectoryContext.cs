using System;
using DDDDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDDemo.Dal
{


    public class ShopDirectoryContext : DbContext
    {
        public virtual DbSet<Shop> Shops { get; set; }
        public ShopDirectoryContext(DbContextOptions<ShopDirectoryContext> options)
        : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shop>()
                .Property(c => c.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<OpeningPeriod>()
                .Property(c => c.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();
        }


    }
}