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

        var existingTmdbIds = await _movieRepository.GetAllTmdbIdsAsync();

        var newMovies = popularMovies.Where(movieDto => !existingTmdbIds.Contains(movieDto.Id)).ToList();

        var movieEntities = _mapper.Map<IEnumerable<Movie>>(newMovies);

        await _movieRepository.AddMoviesAsync(movieEntities);

        var moviesInDb = await _movieRepository.GetAllAsync();

        var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(moviesInDb);

        return movieDtos;
    }
}
