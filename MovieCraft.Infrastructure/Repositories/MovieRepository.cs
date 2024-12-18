﻿using Microsoft.EntityFrameworkCore;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Infrastructure.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie?> GetByTmdbIdAsync(int tmdbId)
        {
            return await _dbContext.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
        }

        public async Task AddAsync(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMoviesAsync(IEnumerable<Movie> movies)
        {
            await _dbContext.Movies.AddRangeAsync(movies);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllTmdbIdsAsync()
        {
            return await _dbContext.Movies
                .AsNoTracking()
                .Select(m => m.TmdbId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByTmdbIdsAsync(IEnumerable<int> tmdbIds)
        {
            return await _dbContext.Movies
                .AsNoTracking()
                .Where(m => tmdbIds.Contains(m.TmdbId))
                .ToListAsync();
        }
    }
}
