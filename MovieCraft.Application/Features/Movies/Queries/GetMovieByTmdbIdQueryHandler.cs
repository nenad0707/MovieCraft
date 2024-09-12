using AutoMapper;
using MediatR;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetMovieByTmdbIdQueryHandler : IRequestHandler<GetMovieByTmdbIdQuery, MovieDto>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieByTmdbIdQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieDto> Handle(GetMovieByTmdbIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByTmdbIdAsync(request.TmdbId);

        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with TmdbId {request.TmdbId} not found.");
        }

        return _mapper.Map<MovieDto>(movie);
    }
}
