using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetMovieByTmdbIdQuery : IRequest<MovieDto>
{
    public int TmdbId { get; set; }

    public GetMovieByTmdbIdQuery(int tmdbId)
    {
        TmdbId = tmdbId;
    }
}
