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

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movies");

            entity.Property(m => m.TmdbId)
                .IsRequired();

            entity.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200); 

            entity.Property(m => m.Overview)
                .HasMaxLength(1000); 

            entity.Property(m => m.PosterPath)
                .HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasIndex(u => u.UserId)
                .IsUnique();

            entity.Property(u => u.UserId)
                .IsRequired()
                .HasMaxLength(50); 

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100); 
        });

        modelBuilder.Entity<FavoriteMovie>(entity =>
        {
            entity.ToTable("FavoriteMovies");

            entity.HasOne(fm => fm.User)
                .WithMany(u => u.FavoriteMovies)
                .HasForeignKey(fm => fm.UserId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            entity.HasOne(fm => fm.Movie)
                .WithMany(m => m.FavoriteMovies)
                .HasForeignKey(fm => fm.MovieId)
                .OnDelete(DeleteBehavior.Cascade); 
        });
    }
}
