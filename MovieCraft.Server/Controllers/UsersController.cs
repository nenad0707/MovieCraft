using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.FavoriteMovies.Commands;
using MovieCraft.Application.Features.FavoriteMovies.Queries;

namespace MovieCraft.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{userId}/favorites")]
    public async Task<IActionResult> AddFavoriteMovie(string userId, [FromBody] int movieId)
    {
        await _mediator.Send(new AddFavoriteMovieCommand { UserId = userId, MovieId = movieId });
        return NoContent();
    }

    [HttpGet("{userId}/favorites")]
    public async Task<IActionResult> GetFavoriteMovies(string userId)
    {
        var user = await _mediator.Send(new GetUserFavoritesQuery { UserId = userId });
        return Ok(user.FavoriteMovies);
    }
}
