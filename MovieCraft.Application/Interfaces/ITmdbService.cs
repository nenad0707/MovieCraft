using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Interfaces;

public interface ITmdbService
{
    Task<IEnumerable<MovieDto>> GetPopularMoviesAsync();
}
