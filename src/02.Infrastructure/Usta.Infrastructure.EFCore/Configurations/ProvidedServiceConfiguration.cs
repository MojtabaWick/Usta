using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.MinPrice)
                .HasPrecision(18, 0);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(1000);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.ProvidedService)
                .HasForeignKey(x => x.ProvidedServiceId);

            builder.HasMany(x => x.Experts)
                .WithMany(x => x.ProvidedServices);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.ProvidedServices)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Experts)
                .WithMany(x => x.ProvidedServices);

            builder.HasData(
                // موبایل و تبلت
                new ProvidedService { Id = 1, Title = "تعمیرات موبایل", CategoryId = 3, ImageUrl = "/Images/ProvidedServices/fix mobile.jpg" },
                new ProvidedService { Id = 2, Title = "گارانتی و فعال‌سازی", CategoryId = 3, ImageUrl = "/Images/ProvidedServices/mobile guarantee.jpg" },

                // لوازم خانگی
                new ProvidedService { Id = 3, Title = "تعمیرات یخچال", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/tamir yakhchal.jpg" },
                new ProvidedService { Id = 4, Title = "تعمیرات تلویزیون", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/fix tv.jpg" },
                new ProvidedService { Id = 5, Title = "تعمیرات لباس‌شویی", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/fix laundry.jpg" },
                new ProvidedService { Id = 7, Title = "تعمیرات لوازم برقی", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/tamir lavazem barghi.jpg" },
                new ProvidedService { Id = 8, Title = "تعمیرات اجاق گاز", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/tamir ojagh gas.jpg" },
                new ProvidedService { Id = 9, Title = "تعمیرات پکیج و آبگرمکن", CategoryId = 1, ImageUrl = "/Images/ProvidedServices/tamir package.jpg" },

                // ساختمان
                new ProvidedService { Id = 10, Title = "نقاشی ساختمان", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/painting building .jpg" },
                new ProvidedService { Id = 11, Title = "لوله‌کشی", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/lole keshi .jpg" },
                new ProvidedService { Id = 12, Title = "برق‌کاری", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/bargh kari .jpg" },
                new ProvidedService { Id = 13, Title = "آهنگری", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/ahangari.jpg" },
                new ProvidedService { Id = 14, Title = "گچ کاری", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/gach kari.jpg" },
                new ProvidedService { Id = 15, Title = "بنایی", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/banaei.jpg" },
                new ProvidedService { Id = 16, Title = "کاشی کاری", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/kashi kari.jpg" },
                new ProvidedService { Id = 17, Title = "کابینت سازی", CategoryId = 2, ImageUrl = "/Images/ProvidedServices/kabinet sazi.jpg" },

                // خودرو
                new ProvidedService { Id = 18, Title = "تعویض روغن", CategoryId = 4, ImageUrl = "/Images/ProvidedServices/taviz roghan.jpg" },
                new ProvidedService { Id = 19, Title = "باتری‌سازی", CategoryId = 4, ImageUrl = "/Images/ProvidedServices/battery sazi.jpg" },
                new ProvidedService { Id = 20, Title = "مکانیکی", CategoryId = 4, ImageUrl = "/Images/ProvidedServices/mechanic.jpg" },

                // کامپیوتر
                new ProvidedService { Id = 21, Title = "نصب ویندوز", CategoryId = 5, ImageUrl = "/Images/ProvidedServices/nasb windows.jpg" },
                new ProvidedService { Id = 22, Title = "تعمیرات لپ‌تاپ", CategoryId = 5, ImageUrl = "/Images/ProvidedServices/tamir laptop.jpg" }
            );
        }
    }
}