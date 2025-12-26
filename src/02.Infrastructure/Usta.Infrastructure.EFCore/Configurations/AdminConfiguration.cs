using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
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
                PasswordHash = "AQAAAAIAAYagAAAAEOpgiIBwXBRZ8dPKfqULmgOi2hE7QuVOJD5BwUngSme7QU0D20U34GcfNYDcIUYNfA==", //Admin@123
                SecurityStamp = "STATIC-SECURITY-STAMP-ADMIN",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-ADMIN",
                CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0)
            };

            builder.HasData(admin);
        }
    }
}