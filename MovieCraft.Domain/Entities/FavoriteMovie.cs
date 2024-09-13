namespace MovieCraft.Domain.Entities;

public class FavoriteMovie
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
}
