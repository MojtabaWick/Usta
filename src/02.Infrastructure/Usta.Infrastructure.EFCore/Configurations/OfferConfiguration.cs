using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.OfferAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.Property(x => x.Price)
                .HasPrecision(18, 0);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Offers)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Expert)
                .WithMany(x => x.Offers)
                .HasForeignKey(x => x.ExpertId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}