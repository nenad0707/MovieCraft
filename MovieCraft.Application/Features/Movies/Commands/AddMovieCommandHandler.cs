using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Movies.Commands;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Unit>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddMovieCommandHandler> _logger;


    public AddMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper, ILogger<AddMovieCommandHandler> logger)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding movie to the database.");
        var movie = _mapper.Map<Movie>(request);
      
        _logger.LogInformation("Saving movie to the database.");
        await _movieRepository.AddAsync(movie);
        
        return Unit.Value;
    }
}
