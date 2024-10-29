using MediatR;

namespace MovieCraft.Application.Features.Movies.Commands;

public class AddMovieCommand : IRequest<Unit>
{
    public int TmdbId { get; set; }
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
    public string BackdropPath { get; set; } = default!;
    public string? TrailerUrl { get; set; }
}
