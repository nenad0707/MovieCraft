using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public ICollection<FavoriteMovieDto> FavoriteMovies { get; set; } = new List<FavoriteMovieDto>();
}
