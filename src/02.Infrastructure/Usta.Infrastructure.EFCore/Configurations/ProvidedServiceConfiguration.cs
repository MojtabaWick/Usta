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

            builder.Property(x => x.MinPrice)
                .HasPrecision(18, 0);

            builder.HasOne(x => x.Image)
                .WithOne(x => x.ProvidedService)
                .HasForeignKey<ProvidedServiceImage>(x => x.ProvidedServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.ProvidedService)
                .HasForeignKey(x => x.ProvidedServiceId);

            builder.HasMany(x => x.Experts)
                .WithMany(x => x.ProvidedServices);
        }
    }
}