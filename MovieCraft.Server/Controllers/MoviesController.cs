using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.Movies.Queries;

namespace MovieCraft.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies()
    {
        var movies = await _mediator.Send(new GetPopularMoviesQuery());
        return Ok(movies);
    }
}
