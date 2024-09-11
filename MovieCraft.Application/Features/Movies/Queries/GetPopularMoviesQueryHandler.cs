using AutoMapper;
using MediatR;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetPopularMoviesQueryHandler : IRequestHandler<GetPopularMoviesQuery, IEnumerable<MovieDto>>
{
    private readonly ITmdbService _tmdbService;
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetPopularMoviesQueryHandler(ITmdbService tmdbService, IMapper mapper, IMovieRepository movieRepository)
    {
        _tmdbService = tmdbService;
        _mapper = mapper;
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDto>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var popularMovies = await _tmdbService.GetPopularMoviesAsync();


        foreach (var movieDto in popularMovies)
        {
            var existingMovie = await _movieRepository.GetByTmdbIdAsync(movieDto.Id);

            if (existingMovie == null) 
            {
                var movieEntity = _mapper.Map<Movie>(movieDto);
                await _movieRepository.AddAsync(movieEntity);
            }
        }

        return popularMovies;
    }
}
