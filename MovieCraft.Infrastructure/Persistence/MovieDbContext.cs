using Microsoft.EntityFrameworkCore;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Infrastructure.Persistence;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
              .ToTable("Movies");

        modelBuilder.Entity<User>()
            .ToTable("Users");

        modelBuilder.Entity<FavoriteMovie>()
            .ToTable("FavoriteMovies");

        modelBuilder.Entity<FavoriteMovie>()
            .HasOne(fm => fm.User)
            .WithMany(u => u.FavoriteMovies)
            .HasForeignKey(fm => fm.UserId)
            .HasPrincipalKey(u => u.UserId);

        modelBuilder.Entity<FavoriteMovie>()
            .HasOne(fm => fm.Movie)
            .WithMany(m => m.FavoriteMovies)
            .HasForeignKey(fm => fm.MovieId);
    }
}
