using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShoppingZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShoppingZone.Data
{
    public class EShoppingZoneDBContext : IdentityDbContext<UserProfile, IdentityRole<int>, int>
    {
        public EShoppingZoneDBContext(DbContextOptions<EShoppingZoneDBContext> options) : base(options) { }

        // public DbSet<Role> Roles {get; set;}
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>().ToTable("Profiles");


            modelBuilder.Entity<Address>()
                .HasOne(a => a.UserProfile)
                .WithMany(a => a.Addresses)
                .HasForeignKey(a => a.UserProfileId);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole<int> { Id = 2, Name = "Merchant", NormalizedName = "MERCHANT" },
                new IdentityRole<int> { Id = 3, Name = "Delivery Agent", NormalizedName = "DELIVERY AGENT" }
            );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}