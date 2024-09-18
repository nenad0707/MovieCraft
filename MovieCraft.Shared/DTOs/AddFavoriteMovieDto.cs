using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Shared.DTOs;

public class AddFavoriteMovieDto
{
    [Required(ErrorMessage = "MovieId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "MovieId must be greater than 0.")]
    public int MovieId { get; set; }
}
