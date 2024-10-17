using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Infrastructure.EntityConfigurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.Property(m => m.TmdbId)
            .IsRequired();

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Overview)
            .HasMaxLength(1000);

        builder.Property(m => m.PosterPath)
            .HasMaxLength(500);

        builder.Property(m => m.BackdropPath)
            .HasMaxLength(500);

        builder.Property(m => m.TrailerUrl)
                .HasMaxLength(500);
    }
}
