using MovieCraft.Shared.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace MovieCraft.Client.Services
{
    public class MovieService
    {
        private readonly HttpClient _anonymousClient;
        private readonly HttpClient _serverClient;
        private readonly UserState _userState;

        public MovieService(IHttpClientFactory httpClientFactory, UserState userState)
        {
            _anonymousClient = httpClientFactory.CreateClient("AnonymousServerAPI");
            _serverClient = httpClientFactory.CreateClient("MovieCraft.ServerAPI");
            _userState = userState;
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync()
        {
            var movies = await _anonymousClient.GetFromJsonAsync<IEnumerable<MovieDto>>("api/movies/popular").ConfigureAwait(false);
            return movies?.ToList() ?? new List<MovieDto>();
        }

        public async Task<string> AddToFavoritesAsync(int tmdbId)
        {
            var userId = GetUserId();

            var dto = new AddFavoriteMovieDto { MovieId = tmdbId };

            var response = await _serverClient.PostAsJsonAsync($"api/favorites/{userId}", dto).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return "This movie is already in your favorites.";
            }
            else if (!response.IsSuccessStatusCode)
            {
                return "Failed to add movie to favorites.";
            }

            return "Movie added to favorites successfully.";
        }


        public async Task<List<FavoriteMovieDto>> GetFavoriteMoviesAsync(string userId)
        {
            var favoriteMovies = await _serverClient.GetFromJsonAsync<IEnumerable<FavoriteMovieDto>>($"api/favorites/{userId}").ConfigureAwait(false);
            return favoriteMovies?.ToList() ?? new List<FavoriteMovieDto>();
        }

        public async Task RemoveFromFavoritesAsync(int tmdbId)
        {
            var userId = GetUserId();

            var response = await _serverClient.DeleteAsync($"api/favorites/{userId}/{tmdbId}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to remove movie from favorites.");
            }
        }

        public async Task<List<MovieDto>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));
            }

            var movies = await _anonymousClient.GetFromJsonAsync<IEnumerable<MovieDto>>($"api/movies/search?query={query}").ConfigureAwait(false);
            return movies?.ToList() ?? new List<MovieDto>();
        }

        private string GetUserId()
        {
            if (_userState.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            return _userState.CurrentUser.UserId;
        }
    }
}
