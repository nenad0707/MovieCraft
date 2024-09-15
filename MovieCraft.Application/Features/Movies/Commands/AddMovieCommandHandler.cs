using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Movies.Commands;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Unit>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddMovieCommandHandler> _logger;
    private readonly IMemoryCache _memoryCache;

    private const string PopularMoviesCacheKey = "PopularMoviesCacheKey";


    public AddMovieCommandHandler(IMovieRepository movieRepository, 
        IMapper mapper, ILogger<AddMovieCommandHandler> logger, IMemoryCache memoryCache)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<Unit> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding movie to the database.");
        var movie = _mapper.Map<Movie>(request);
      
        _logger.LogInformation("Saving movie to the database.");
        await _movieRepository.AddAsync(movie);

        _logger.LogInformation("Invalidating the cache for popular movies.");
        _memoryCache.Remove(PopularMoviesCacheKey);

        return Unit.Value;
    }
}
