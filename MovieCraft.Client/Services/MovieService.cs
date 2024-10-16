using MovieCraft.Shared.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace MovieCraft.Client.Services
{
    public class MovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly UserState _userState;

        public MovieService(IHttpClientFactory httpClientFactory, UserState userState)
        {
            _httpClientFactory = httpClientFactory;
            _userState = userState;
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("AnonymousServerAPI");

            var movies = await httpClient.GetFromJsonAsync<IEnumerable<MovieDto>>("api/movies/popular");

            return movies?.ToList() ?? new List<MovieDto>();
        }

        public async Task<string> AddToFavorites(int tmdbId)
        {
            if (_userState.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var userId = _userState.CurrentUser.UserId;
            var dto = new AddFavoriteMovieDto { MovieId = tmdbId };

            var httpClient = _httpClientFactory.CreateClient("MovieCraft.ServerAPI");
            var response = await httpClient.PostAsJsonAsync($"api/favorites/{userId}", dto);

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
            HttpClient httpClient = _httpClientFactory.CreateClient("MovieCraft.ServerAPI");
            var favoriteMovies = await httpClient.GetFromJsonAsync<IEnumerable<FavoriteMovieDto>>($"api/favorites/{userId}");
            return favoriteMovies?.ToList() ?? new List<FavoriteMovieDto>();
        }

        public async Task RemoveFromFavoritesAsync(int tmdbId)
        {
            if (_userState.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var userId = _userState.CurrentUser.UserId;
            var httpClient = _httpClientFactory.CreateClient("MovieCraft.ServerAPI");
            var response = await httpClient.DeleteAsync($"api/favorites/{userId}/{tmdbId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to remove movie from favorites.");
            }
        }
    }
}
