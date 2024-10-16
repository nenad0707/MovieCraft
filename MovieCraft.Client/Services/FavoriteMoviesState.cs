using MovieCraft.Shared.DTOs;

namespace MovieCraft.Client.Services;

public class FavoriteMoviesState
{
    private readonly MovieService _movieService;
    private readonly UserState _userState;

    public IEnumerable<FavoriteMovieDto>? FavoriteMovies { get; private set; }

    public event Action? OnChange;

    public FavoriteMoviesState(MovieService movieService, UserState userState)
    {
        _movieService = movieService;
        _userState = userState;
    }

    public async Task LoadFavoriteMoviesAsync()
    {
        if (_userState.CurrentUser == null)
        {
            FavoriteMovies = new List<FavoriteMovieDto>();
            NotifyStateChanged();
            return;
        }

        FavoriteMovies = await _movieService.GetFavoriteMoviesAsync(_userState.CurrentUser.UserId);
        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}
