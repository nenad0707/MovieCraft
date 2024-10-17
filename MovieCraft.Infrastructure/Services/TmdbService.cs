using Microsoft.Extensions.Options;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;
using MovieCraft.Infrastructure.Configuration;
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
            
            var movieDtos = new List<MovieDto>();

            foreach (var movie in movies.Results)
            {
                var videos = await _client.GetMovieVideosAsync(movie.Id);
               
                var trailer = videos.Results.FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube");

                var trailerUrl = trailer != null ? $"https://www.youtube.com/embed/{trailer.Key}" : null;

                movieDtos.Add(new MovieDto
                {
                    Id = movie.Id,
                    TmdbId = movie.Id, 
                    Title = movie.Title,
                    Overview = movie.Overview,
                    ReleaseDate = movie.ReleaseDate,
                    PosterPath = movie.PosterPath,
                    BackdropPath = movie.BackdropPath,
                    TrailerUrl = trailerUrl 
                });
            }
            return movieDtos;
        }
    }
}
