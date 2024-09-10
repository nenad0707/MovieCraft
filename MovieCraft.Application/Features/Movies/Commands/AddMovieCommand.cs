using MediatR;

namespace MovieCraft.Application.Features.Movies.Commands;

public class AddMovieCommand : IRequest
{
    public int TmdbId { get; set; }
    public string Title { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; } = default!;
}
