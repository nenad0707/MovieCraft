using MovieCraft.Shared.DTOs;

namespace MovieCraft.Client.Services;

public interface IMovieService
{
    Task<List<MovieDto>> GetPopularMoviesAsync();
    Task<MovieDto> GetMovieByIdAsync(int tmdbId);
    Task AddFavoriteMovieAsync(string userId, int movieId);
}
