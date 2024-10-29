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

        public async Task<IEnumerable<MovieDto?>> GetPopularMoviesAsync()
        {
            var movies = await _client.GetMoviePopularListAsync();
            return await MapToMovieDtos(movies.Results);
        }

        public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string query)
        {
            var searchResult = await _client.SearchMovieAsync(query);
            return await MapToMovieDtos(searchResult.Results);
        }

        
        public async Task<MovieDto?> GetMovieDetailsAsync(int tmdbId)
        {
            var movie = await _client.GetMovieAsync(tmdbId);

            if (movie == null)
                return null;

            var trailerUrl = await GetVideoUrlAsync(movie.Id); 

            return new MovieDto
            {
                Id = movie.Id,
                TmdbId = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ReleaseDate = movie.ReleaseDate,
                PosterPath = movie.PosterPath,
                BackdropPath = movie.BackdropPath,
                TrailerUrl = trailerUrl
            };
        }

        private async Task<List<MovieDto>> MapToMovieDtos(IEnumerable<TMDbLib.Objects.Search.SearchMovie> movies)
        {
            var movieDtos = new List<MovieDto>();

            foreach (var movie in movies)
            {
                var trailerUrl = await GetVideoUrlAsync(movie.Id); 

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

        private async Task<string?> GetVideoUrlAsync(int movieId)
        {
            var videos = await _client.GetMovieVideosAsync(movieId);
            var video = videos.Results.FirstOrDefault(v =>
                (v.Type == "Trailer" || v.Type == "Teaser" || v.Type == "Clip") && v.Site == "YouTube");

            return video != null ? $"https://www.youtube.com/embed/{video.Key}" : null;
        }

    }
}
