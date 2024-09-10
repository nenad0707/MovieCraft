using AutoMapper;
using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetPopularMoviesQueryHandler : IRequestHandler<GetPopularMoviesQuery>
{
    private readonly ITmdbService _tmdbService;
    private readonly IMapper _mapper;

    public GetPopularMoviesQueryHandler(ITmdbService tmdbService, IMapper mapper)
    {
        _tmdbService = tmdbService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovieDto>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _tmdbService.GetPopularMoviesAsync();
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }
}
