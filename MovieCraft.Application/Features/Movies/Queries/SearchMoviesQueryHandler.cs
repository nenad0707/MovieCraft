using MediatR;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.Movies.Queries;

public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, IEnumerable<MovieDto>>
{
    private readonly ITmdbService _tmdbService;

    public SearchMoviesQueryHandler(ITmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public async Task<IEnumerable<MovieDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _tmdbService.SearchMoviesAsync(request.Query);
    }
}
