using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.OfferAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class OfferImageConfiguration : IEntityTypeConfiguration<OfferImage>
    {
        public void Configure(EntityTypeBuilder<OfferImage> builder)
        {
            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}