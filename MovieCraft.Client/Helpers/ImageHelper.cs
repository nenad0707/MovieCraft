namespace MovieCraft.Client.Helpers;

public class ImageHelper
{
    private const string BaseUrl = "https://image.tmdb.org/t/p/";
    public const string PosterSize = "w342";
    public const string BackdropSize = "w1280";

    public static string GetPosterUrl(string posterPath) => $"{BaseUrl}{PosterSize}{posterPath}";
    public static string GetBackdropUrl(string backdropPath) => $"{BaseUrl}{BackdropSize}{backdropPath}";
}
