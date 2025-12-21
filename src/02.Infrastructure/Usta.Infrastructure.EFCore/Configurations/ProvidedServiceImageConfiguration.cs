using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class ProvidedServiceImageConfiguration : IEntityTypeConfiguration<ProvidedServiceImage>
    {
        public void Configure(EntityTypeBuilder<ProvidedServiceImage> builder)
        {
            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}