using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Domain.Database.Builders;

internal static class UserEntityModelBuilder
{
    public static void ConfigureUser(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("User");
            entity.HasIndex(e => e.Id).IsUnique();
            entity.Property(e => e.FullName)
                  .HasColumnName("FullName")
                  .HasMaxLength(255)
                  .IsRequired();
            entity.Property(e => e.RegisteredAtUtc)
                  .HasColumnName("RegisteredAt")
                  .IsRequired();

        });
    }
}
