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
                .HasMaxLength(1000);

            builder.HasMany(x => x.ProvidedServices)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(
                new Category { Id = 1, Title = "لوازم خانگی", ImagedUrl = "/Images\\Categories\\b22dbcc9-fbd1-4e79-8ac1-aa03b80ce90c.jpg" },
                new Category { Id = 2, Title = "ساختمان", ImagedUrl = "/Images\\Categories\\3c20c7c7-12e2-4645-8cda-2f73fd3e0fc0.jpg" },
                new Category { Id = 3, Title = "موبایل و تبلت", ImagedUrl = "/Images\\Categories\\d95c7625-89da-487a-9609-e19a632261e6.jpg" },
                new Category { Id = 4, Title = "خودرو", ImagedUrl = "/Images\\Categories\\7d1c2910-21bd-497d-877c-fde0801e2d2e.jpg" },
                new Category { Id = 5, Title = "کامپیوتر و لپ‌تاپ", ImagedUrl = "/Images\\Categories\\feb876d8-ee5d-420c-bab6-7fbb32c3f917.jpg" }
            );
        }
    }
}