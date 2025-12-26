using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProvidedService)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProvidedServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AcceptedOffer)
                .WithMany()
                .HasForeignKey(x => x.AcceptedOfferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}