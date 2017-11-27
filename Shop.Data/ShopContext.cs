using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<User> User { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-N26VSL7\SQLEXPRESS;Database=AngularShop;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPurchase>(entity =>
            {
                entity.HasKey(m => new { m.ProductId, m.PurchaseId });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name);
                entity.Property(m => m.Price);
                entity.Property(m => m.Description);

                entity.HasOne(m => m.Category)
                    .WithMany(m => m.Products)
                    .HasForeignKey(m => m.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Product");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.DateTime);

                entity.HasOne(m => m.User)
                    .WithMany(m => m.Purchases)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Purchase");
                
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name);
            });
        }

    }
}
