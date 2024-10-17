using Microsoft.JSInterop;
using MovieCraft.Client.Helpers;

public class BackgroundState
{
    public string BackgroundImageUrl { get; private set; } = "images/Batman.jpg";
    public event Action? OnChange;

    private readonly IJSRuntime _js;

    public BackgroundState(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SetBackgroundAsync(string backdropPath)
    {
        string newImageUrl = ImageHelper.GetBackdropUrl(backdropPath);
        bool isLoaded = await _js.InvokeAsync<bool>("preloadImage", newImageUrl);
        if (isLoaded)
        {
            BackgroundImageUrl = newImageUrl;
            OnChange?.Invoke();
        }
    }
}
