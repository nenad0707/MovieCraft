using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Web.Resource;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Features.Movies.Commands;
using MovieCraft.Application.Features.Movies.Queries;
using MovieCraft.Shared.DTOs;

namespace MovieCraft.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;
    private readonly IMemoryCache _memoryCache;
    
    private const string PopularMoviesCacheKey = "popularMovies";

    public MoviesController(IMediator mediator, ILogger<MoviesController> logger,
        IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    [EnableRateLimiting("basic")]
    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies()
    {
        _logger.LogInformation("Handling request for popular movies.");

        if (_memoryCache.TryGetValue<IEnumerable<MovieDto>>(PopularMoviesCacheKey, out var cachedMovies))
        {
            _logger.LogInformation("Returning cached popular movies.");
            Response.Headers.Append("X-Cache", "HIT");
            return Ok(cachedMovies);
        }

        _logger.LogInformation("Fetching popular movies from TMDB API.");
        var movies = await _mediator.Send(new GetPopularMoviesQuery());

        _logger.LogInformation("Caching popular movies.");
        _memoryCache.Set(PopularMoviesCacheKey, movies, TimeSpan.FromMinutes(5));

        Response.Headers.Append("X-Cache", "MISS");

        return Ok(movies);
    }

    [HttpGet("{tmdbId}")]
    public async Task<IActionResult> GetMovieByTmdbId(int tmdbId)
    {
        _logger.LogInformation("Fetching movie data for a specific movie.");
        var movie = await _mediator.Send(new GetMovieByTmdbIdQuery(tmdbId));
        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddMovie([FromBody] AddMovieDto addMovieDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Adding a new movie.");

        var addMovieCommand = new AddMovieCommand
        {
            Title = addMovieDto.Title,
            Overview = addMovieDto.Overview,
            ReleaseDate = addMovieDto.ReleaseDate,
            PosterPath = addMovieDto.PosterPath,
            TmdbId = addMovieDto.TmdbId ?? throw new ArgumentNullException(nameof(addMovieDto.TmdbId))
        };

        await _mediator.Send(addMovieCommand);

        _logger.LogInformation("Invalidating popular movies cache.");
        _memoryCache.Remove(PopularMoviesCacheKey);

        return NoContent(); 
    }
}
