namespace MovieCraft.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
}
