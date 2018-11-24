using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data
{
    public class ShopContext : IdentityDbContext<User>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ForumMessages> ForumMessages { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-N26VSL7\SQLEXPRESS;Database=AngularShop;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<ForumMessages>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Message);
                entity.Property(m => m.PublishedDateTime);

                entity.HasOne(m => m.User)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");

                entity.HasOne(m => m.Product)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(m => m.ProductId)
                    .HasConstraintName("FK_Message_Product")
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(m => m.ReplyTo)
                    .WithMany(m => m.Answers)
                    .HasForeignKey(m => m.ReplyToId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(m => m.Id);
            //    entity.Property(m => m.Name);
            //});
        }

    }
}
