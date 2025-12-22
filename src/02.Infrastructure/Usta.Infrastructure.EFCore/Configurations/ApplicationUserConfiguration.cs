using Microsoft.AspNetCore.Identity;
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

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new Admin
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0)
            };

            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

            builder.HasData(admin);
        }
    }
}