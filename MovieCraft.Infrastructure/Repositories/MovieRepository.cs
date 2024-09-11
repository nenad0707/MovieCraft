using Microsoft.EntityFrameworkCore;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;
using MovieCraft.Infrastructure.Persistence;

namespace MovieCraft.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _dbContext;

    public MovieRepository(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Movie?> GetByTmdbIdAsync(int tmdbId)
    {
        return await _dbContext.Movies.FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
    }

    public async Task AddAsync(Movie movie)
    {
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
    }
}
