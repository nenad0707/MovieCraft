﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetPopularMoviesQueryHandler : IRequestHandler<GetPopularMoviesQuery, IEnumerable<MovieDto>>
{
    private readonly ITmdbService _tmdbService;
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPopularMoviesQueryHandler> _logger;
    private readonly IMemoryCache _memoryCache;

    private const string PopularMoviesCacheKey = "PopularMoviesCacheKey";

    public GetPopularMoviesQueryHandler(ITmdbService tmdbService, 
        IMapper mapper, IMovieRepository movieRepository, 
        ILogger<GetPopularMoviesQueryHandler> logger, IMemoryCache memoryCache)
    {
        _tmdbService = tmdbService;
        _mapper = mapper;
        _movieRepository = movieRepository;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<MovieDto>> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(PopularMoviesCacheKey, out IEnumerable<MovieDto>? cachedMovies))
        {
            _logger.LogInformation("Returning cached popular movies.");
            return cachedMovies!;
        }

        _logger.LogInformation("Fetching popular movies from TMDB API.");
        var popularMovies = await _tmdbService.GetPopularMoviesAsync();

        _logger.LogInformation("Checking if movies already exist in the database.");
        var existingTmdbIds = await _movieRepository.GetAllTmdbIdsAsync();

        _logger.LogInformation("Adding new movies to the database.");
        var newMovies = popularMovies.Where(movieDto => !existingTmdbIds.Contains(movieDto.Id)).ToList();

        _logger.LogInformation($"Adding {newMovies.Count} new movies to the database.");
        var movieEntities = _mapper.Map<IEnumerable<Movie>>(newMovies);

        _logger.LogInformation("Saving new movies to the database.");
        await _movieRepository.AddMoviesAsync(movieEntities);

        _logger.LogInformation("Fetching all movies from the database.");
        var moviesInDb = await _movieRepository.GetAllAsync();

        var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(moviesInDb);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
        _memoryCache.Set(PopularMoviesCacheKey, movieDtos, cacheEntryOptions);

        return movieDtos;
    }
}
