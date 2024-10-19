using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Movies.Queries;

public class SearchMoviesQuery : IRequest<IEnumerable<MovieDto>>
{
    public string Query { get; } = default!;

    public SearchMoviesQuery(string query)
    {
        Query = query;
    }
}
