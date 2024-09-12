using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.FavoriteMovies.Commands;
using MovieCraft.Application.Features.FavoriteMovies.Queries;

namespace MovieCraft.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FavoritesController(IMediator mediator)
    {
        _mediator = mediator;
    }

   
    [HttpPost("{userId}")]
    public async Task<IActionResult> AddFavoriteMovie(string userId, [FromBody] int movieId)
    {
        await _mediator.Send(new AddFavoriteMovieCommand { UserId = userId, MovieId = movieId });
        return NoContent();
    }

   
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavoriteMovies(string userId)
    {
        var favoriteMovies = await _mediator.Send(new GetUserFavoritesQuery { UserId = userId });
        return Ok(favoriteMovies);
    }
}
