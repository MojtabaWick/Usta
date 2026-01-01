using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class OrderImageConfiguration : IEntityTypeConfiguration<OrderImage>
    {
        public void Configure(EntityTypeBuilder<OrderImage> builder)
        {
            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}