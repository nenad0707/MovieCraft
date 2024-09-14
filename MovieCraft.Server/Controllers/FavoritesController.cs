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
    private readonly ILogger<FavoritesController> _logger;

    public FavoritesController(IMediator mediator, ILogger<FavoritesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost("{userId}")]
    public async Task<IActionResult> AddFavoriteMovie(string userId, [FromBody] int movieId)
    {
        _logger.LogInformation("Adding movie with Id: {movieId} to favorites for user with UserId: .{userId}", movieId, userId);
        await _mediator.Send(new AddFavoriteMovieCommand { UserId = userId, MovieId = movieId });
        return NoContent();
    }

   
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavoriteMovies(string userId)
    {
        _logger.LogInformation("Fetching favorite movies for user with UserId: {userId}", userId);
        var favoriteMovies = await _mediator.Send(new GetUserFavoritesQuery { UserId = userId });
        return Ok(favoriteMovies);
    }
}
