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

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.ImagedUrl)
                .HasMaxLength(500);

            builder.HasMany(x => x.ProvidedServices)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.HasData(
                new Category { Id = 1, Title = "لوازم خانگی", ImagedUrl = "/Image/Categories/kitchen.png" },
                new Category { Id = 2, Title = "ساختمان", ImagedUrl = "/Image/Categories/building.png" },
                new Category { Id = 3, Title = "موبایل و تبلت", ImagedUrl = "/Image/Categories/mobile.png" },
                new Category { Id = 4, Title = "خودرو", ImagedUrl = "/Image/Categories/car.png" },
                new Category { Id = 5, Title = "کامپیوتر و لپ‌تاپ", ImagedUrl = "/Image/Categories/laptop.png" }
            );
        }
    }
}