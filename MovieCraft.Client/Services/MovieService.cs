using MovieCraft.Client.Services;
using MovieCraft.Shared.DTOs;
using System.Net.Http.Json;

namespace MovieCraft.Server.Services;

public class MovieService : IMovieService
{

    private readonly HttpClient _httpClient;

    public MovieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MovieDto>> GetPopularMoviesAsync()
    {
        var popularMovies = await _httpClient.GetFromJsonAsync<List<MovieDto>>("api/movies/popular");
        return popularMovies ?? new List<MovieDto>();
    }

    public async Task<MovieDto> GetMovieByIdAsync(int tmdbId)
    {
        var movie = await _httpClient.GetFromJsonAsync<MovieDto>($"api/movies/{tmdbId}");
        return movie ?? throw new Exception("Movie not found.");
    }

    public async Task AddFavoriteMovieAsync(string userId, int movieId)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/favorites/{userId}", movieId);
        response.EnsureSuccessStatusCode();
    }
}
