using MovieCraft.Shared.DTOs;
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

        public async Task AddToFavorites(int tmdbId)
        {
            if (_userState.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var userId = _userState.CurrentUser.UserId;
            var dto = new AddFavoriteMovieDto { MovieId = tmdbId };

            var httpClient = _httpClientFactory.CreateClient("MovieCraft.ServerAPI");
            var response = await httpClient.PostAsJsonAsync($"api/favorites/{userId}", dto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add movie to favorites.");
            }
        }
    }
}
