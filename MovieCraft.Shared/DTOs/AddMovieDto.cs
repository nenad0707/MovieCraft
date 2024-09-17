namespace MovieCraft.Shared.DTOs;

public class AddMovieDto
{
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
    public int? TmdbId { get; set; }
}
