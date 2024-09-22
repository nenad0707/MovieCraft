using MovieCraft.Shared.DTOs;

namespace MovieCraft.Client.Services;

public class PopularMoviesState
{
    private readonly MovieService _movieService;

    public IEnumerable<MovieDto>? PopularMovies { get; private set; }

    public event Action? OnChange;

    public PopularMoviesState(MovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task LoadPopularMoviesAsync(bool isLoggedIn)
    {
        if (PopularMovies == null || !PopularMovies.Any())
        {
            PopularMovies = await _movieService.GetPopularMoviesAsync(isLoggedIn);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
