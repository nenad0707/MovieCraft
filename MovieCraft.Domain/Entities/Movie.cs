namespace MovieCraft.Domain.Entities;

public class Movie
{
    public int Id { get; set; } = default!;
    public int TmdbId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
    public string BackdropPath { get; set; } = default!;
    public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
}


