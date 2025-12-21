using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .HasMaxLength(100);

            builder.HasOne(x => x.Image)
                .WithOne(x => x.User)
                .HasForeignKey<UserImage>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}