using MovieCraft.Shared.DTOs;
using System.Net.Http.Json;

namespace MovieCraft.Client.Services
{
    public class MovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync()
        {
            HttpClient httpClient;


            //if (isLoggedIn)
            //{
            //    httpClient = _httpClientFactory.CreateClient("MovieCraft.ServerAPI");  
            //}
            //else
            //{
            //    httpClient = _httpClientFactory.CreateClient("AnonymousServerAPI");  
            //}

            httpClient = _httpClientFactory.CreateClient("AnonymousServerAPI");

            var movies = await httpClient.GetFromJsonAsync<IEnumerable<MovieDto>>("api/movies/popular");

            return movies?.ToList() ?? new List<MovieDto>();
        }
    }
}
