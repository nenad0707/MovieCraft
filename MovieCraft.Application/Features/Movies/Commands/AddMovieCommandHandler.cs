using AutoMapper;
using MediatR;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Movies.Commands;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Unit>
{
    private readonly IMovieRepository _movieRepository;
    
    private readonly IMapper _mapper;

    public AddMovieCommandHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = _mapper.Map<Movie>(request);
      
        await _movieRepository.AddAsync(movie);
        
        return Unit.Value;
    }
}
