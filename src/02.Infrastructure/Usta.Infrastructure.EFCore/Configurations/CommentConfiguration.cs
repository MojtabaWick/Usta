using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CommentAgg.Entities;

namespace Usta.Infrastructure.EFCore.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Comment)
                .HasForeignKey<Comment>(x => x.OrderId);

            builder.HasOne(x => x.Expert)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ExpertId);
        }
    }
}