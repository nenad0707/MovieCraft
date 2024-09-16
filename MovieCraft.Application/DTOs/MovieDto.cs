using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Application.DTOs;

public class MovieDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; } = default!;

    [MaxLength(1000, ErrorMessage = "Overview cannot exceed 1000 characters.")]
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }

    [MaxLength(500, ErrorMessage = "PosterPath cannot exceed 500 characters.")]
    public string PosterPath { get; set; } = default!;
}
