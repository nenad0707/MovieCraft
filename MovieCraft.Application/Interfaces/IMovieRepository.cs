using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Interfaces;

public interface IMovieRepository
{
    Task<Movie> GetByTmdbIdAsync(int tmdbId);
    Task AddAsync(Movie movie);
}
