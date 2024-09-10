namespace MovieCraft.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
}
