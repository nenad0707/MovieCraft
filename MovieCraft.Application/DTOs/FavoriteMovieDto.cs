namespace MovieCraft.Application.DTOs;

public class FavoriteMovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
}
