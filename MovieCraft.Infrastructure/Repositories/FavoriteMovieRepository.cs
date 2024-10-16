﻿using Microsoft.EntityFrameworkCore;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;
using MovieCraft.Infrastructure.Persistence;

namespace MovieCraft.Infrastructure.Repositories;

public class FavoriteMovieRepository : IFavoriteMovieRepository
{
    private readonly MovieDbContext _dbContext;

    public FavoriteMovieRepository(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddFavoriteMovieAsync(FavoriteMovie favoriteMovie)
    {
        await _dbContext.FavoriteMovies.AddAsync(favoriteMovie);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<FavoriteMovie>> GetFavoriteMoviesByUserIdAsync(string userId)
    {
       return await _dbContext.FavoriteMovies
            .AsNoTracking()
            .Include(fm => fm.Movie)
            .Where(fm => fm.UserId == userId)
            .ToListAsync();
    }

    public async Task<FavoriteMovie?> GetFavoriteMovieAsync(string userId, int movieId)
    {
        return await _dbContext.FavoriteMovies
       .FirstOrDefaultAsync(fm => fm.UserId == userId && fm.MovieId == movieId);
    }

    public async Task RemoveFavoriteMovie(FavoriteMovie favoriteMovie)
    {
        _dbContext.FavoriteMovies.Remove(favoriteMovie);
        await _dbContext.SaveChangesAsync();
    }
}
