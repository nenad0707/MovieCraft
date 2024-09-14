using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetMovieByTmdbIdQueryHandler : IRequestHandler<GetMovieByTmdbIdQuery, MovieDto>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetMovieByTmdbIdQueryHandler> _logger;

    public GetMovieByTmdbIdQueryHandler(IMovieRepository movieRepository, IMapper mapper, ILogger<GetMovieByTmdbIdQueryHandler> logger)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MovieDto> Handle(GetMovieByTmdbIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching movie with TmdbId {request.TmdbId} from the database.");
        var movie = await _movieRepository.GetByTmdbIdAsync(request.TmdbId);

        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with TmdbId {request.TmdbId} not found.");
        }

        _logger.LogInformation($"Movie with TmdbId {request.TmdbId} found in the database.");
        return _mapper.Map<MovieDto>(movie);
    }
}
