using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MovieCraft.Application.Features.FavoriteMovies.Commands;
using MovieCraft.Application.Features.FavoriteMovies.Queries;

namespace MovieCraft.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
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
        _logger.LogInformation("Adding a movie to the user's favorite list.");
        await _mediator.Send(new AddFavoriteMovieCommand { UserId = userId, MovieId = movieId });
        return NoContent();
    }

   
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavoriteMovies(string userId)
    {
        _logger.LogInformation("Fetching the user's favorite movies.");
        var favoriteMovies = await _mediator.Send(new GetUserFavoritesQuery { UserId = userId });
        return Ok(favoriteMovies);
    }
}
