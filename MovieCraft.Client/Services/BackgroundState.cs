namespace MovieCraft.Client.Services;

public class BackgroundState
{
    private string _backgroundImageUrl = "/images/Batman.jpg";

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
