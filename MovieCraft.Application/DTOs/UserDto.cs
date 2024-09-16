using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "UserId is required.")]
    [MaxLength(450, ErrorMessage = "UserId cannot exceed 450 characters.")]
    public string UserId { get; set; } = default!;

    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = default!;

    public ICollection<FavoriteMovieDto> FavoriteMovies { get; set; } = new List<FavoriteMovieDto>();
}
