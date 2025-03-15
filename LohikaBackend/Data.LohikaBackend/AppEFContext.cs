using Data.LohikaBackend.Configuration;
using Data.LohikaBackend.Entities;
using Data.LohikaBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend
{
    public class AppEFContext : IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>,
        AppUserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options) :
            base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<BlogImageEntity> BlogImages { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<FilterEntity>(filter =>
            {
                filter.HasKey(ur => new { ur.ProductId, ur.FilterValueId });

                filter.HasOne(ur => ur.ProductOf)
                    .WithMany(r => r.Filters)
                    .HasForeignKey(ur => ur.ProductId)
                    .IsRequired();

                filter.HasOne(ur => ur.FilterValueOf)
                    .WithMany(r => r.Filtres)
                    .HasForeignKey(ur => ur.FilterValueId)
                    .IsRequired();
            });

            builder.ApplyConfiguration(new CategoryEntityConfiguration());
        }
    }
}
