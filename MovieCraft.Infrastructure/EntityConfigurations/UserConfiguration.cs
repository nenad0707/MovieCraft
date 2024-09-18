using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasIndex(u => u.UserId)
            .IsUnique();

        builder.Property(u => u.UserId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
