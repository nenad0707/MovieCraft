namespace MovieCraft.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public IEnumerable<FavoriteMovieDto> FavoriteMovies { get; set; } = new List<FavoriteMovieDto>();
}
