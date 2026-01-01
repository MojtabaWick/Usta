using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);

            var cities = new List<City>
            {
                new City { Id = 1, Name = "تهران" },
                new City { Id = 2, Name = "مشهد" },
                new City { Id = 3, Name = "اصفهان" },
                new City { Id = 4, Name = "شیراز" },
                new City { Id = 5, Name = "تبریز" },
                new City { Id = 6, Name = "کرج" },
                new City { Id = 7, Name = "قم" },
                new City { Id = 8, Name = "اهواز" },
                new City { Id = 9, Name = "کرمانشاه" },
                new City { Id = 10, Name = "رشت" },
                new City { Id = 11, Name = "یزد" },
                new City { Id = 12, Name = "ارومیه" },
                new City { Id = 13, Name = "زاهدان" },
                new City { Id = 14, Name = "بندرعباس" },
                new City { Id = 15, Name = "همدان" },
                new City { Id = 16, Name = "سنندج" },
                new City { Id = 17, Name = "گرگان" },
                new City { Id = 18, Name = "ساری" },
                new City { Id = 19, Name = "قزوین" },
                new City { Id = 20, Name = "بوشهر" }
            };

            builder.HasData(cities);
        }
    }
}