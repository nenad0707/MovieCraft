using Microsoft.Extensions.Options;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;
using TMDbLib.Client;

namespace MovieCraft.Infrastructure.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly TMDbClient _client;

        public TmdbService(IOptions<TmdbSettings> tmdbSettings)
        {
            _client = new TMDbClient(tmdbSettings.Value.ApiKey);
        }

        public async Task<IEnumerable<MovieDto>> GetPopularMoviesAsync()
        {
            var movies = await _client.GetMoviePopularListAsync();
            return movies.Results.Select(movie => new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                PosterPath = movie.PosterPath
            }).ToList();
        }
    }
}
