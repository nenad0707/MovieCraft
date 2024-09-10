namespace MovieCraft.Domain.Entities;

public class FavoriteMovie
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
