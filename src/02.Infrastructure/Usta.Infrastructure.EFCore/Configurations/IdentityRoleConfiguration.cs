using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
                new IdentityRole<int> { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }
    }
}