using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Interfaces;

public interface ITmdbService
{
    Task<IEnumerable<MovieDto?>> GetPopularMoviesAsync();
    Task<IEnumerable<MovieDto>> SearchMoviesAsync(string query);
    Task<MovieDto?> GetMovieDetailsAsync(int tmdbId);
}
