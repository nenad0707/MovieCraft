namespace MovieCraft.Client.Services;

public class BackgroundState
{
    private string _backgroundImageUrl = "https://image.tmdb.org/t/p/original/lh5lbisD4oDbEKgUxoRaZU8HVrk.jpg";

    public string BackgroundImageUrl
    {
        get => _backgroundImageUrl;
        set
        {
            if (_backgroundImageUrl != value)
            {
                _backgroundImageUrl = value;
                NotifyStateChanged();
            }
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
