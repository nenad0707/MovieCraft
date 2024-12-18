﻿using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Interfaces;

public interface IFavoriteMovieRepository
{
    Task<FavoriteMovie?> GetFavoriteMovieAsync(string userId, int movieId);
    Task RemoveFavoriteMovie(FavoriteMovie favoriteMovie);
    Task AddFavoriteMovieAsync(FavoriteMovie favoriteMovie);
    Task<IEnumerable<FavoriteMovie>> GetFavoriteMoviesByUserIdAsync(string userId);
}
