using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Shared.DTOs;

public class AddMovieDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title can have a maximum of 200 characters.")]
    public string Title { get; set; } = default!;

    [StringLength(1000, ErrorMessage = "Overview can have a maximum of 1000 characters.")]
    public string Overview { get; set; } = default!;

    [DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    [StringLength(500, ErrorMessage = "Poster path can have a maximum of 500 characters.")]
    public string PosterPath { get; set; } = default!;

    [StringLength(500, ErrorMessage = "Backdrop path can have a maximum of 500 characters.")] // Dodaj ovo
    public string BackdropPath { get; set; } = default!;

    [Required(ErrorMessage = "TmdbId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "TmdbId must be a positive number.")]
    public int? TmdbId { get; set; }
    
    [StringLength(500, ErrorMessage = "Trailer URL can have a maximum of 500 characters.")]
    public string? TrailerUrl { get; set; }
}
