using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CategoryAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.CategoryImage)
                .WithOne(x => x.Category)
                .HasForeignKey<CategoryImage>(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProvidedServices)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}