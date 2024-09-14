using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.Movies.Queries;

namespace MovieCraft.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMediator mediator, ILogger<MoviesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies()
    {
        _logger.LogInformation("Fetching popular movies.");
        var movies = await _mediator.Send(new GetPopularMoviesQuery());
        return Ok(movies);
    }

    [HttpGet("{tmdbId}")]
    public async Task<IActionResult> GetMovieByTmdbId(int tmdbId)
    {
        _logger.LogInformation("Fetching movie with TmdbId: {tmdbId}", tmdbId);
        var movie = await _mediator.Send(new GetMovieByTmdbIdQuery(tmdbId));
        return Ok(movie);
    }
}
