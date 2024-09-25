namespace MovieCraft.Client.Services;

public class BackgroundState
{
    private string _backgroundImageUrl = "https://wallpapers.com/images/high/batman-ben-affleck-movie-hmg3hz6ajq6u5c1p.webp";

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
