using Data.LohikaBackend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Configuration
{
    public class CategoryEntityConfiguration : BaseConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("tblCategories");

            builder.Property(e => e.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.UrlSlug)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(x => x.UrlSlug)
                .IsUnique(true);

            builder.Property(e => e.Priority)
                .IsRequired();

            builder.Property(e => e.Image)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(e => e.Description)
                .HasMaxLength(4000)
                .IsRequired(false);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false);

            CreateDefaultConfiguration(builder);

        }
    }
}
