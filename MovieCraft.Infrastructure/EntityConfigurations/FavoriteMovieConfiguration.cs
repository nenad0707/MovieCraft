using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Infrastructure.EntityConfigurations;

public class FavoriteMovieConfiguration : IEntityTypeConfiguration<FavoriteMovie>
{
    public void Configure(EntityTypeBuilder<FavoriteMovie> builder)
    {
        builder.ToTable("FavoriteMovies");

        builder.HasOne(fm => fm.User)
            .WithMany(u => u.FavoriteMovies)
            .HasForeignKey(fm => fm.UserId)
            .HasPrincipalKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fm => fm.Movie)
            .WithMany(m => m.FavoriteMovies)
            .HasForeignKey(fm => fm.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
