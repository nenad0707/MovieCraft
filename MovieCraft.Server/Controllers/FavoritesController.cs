﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MovieCraft.Application.DTOs;
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
    public async Task<IActionResult> AddFavoriteMovie(string userId, [FromBody] AddFavoriteMovieDto dto)
    {
        try
        {
            _logger.LogInformation("Adding a movie to the user's favorite list.");
            await _mediator.Send(new AddFavoriteMovieCommand
            {
                UserId = userId,
                MovieId = dto.MovieId
            });
            return NoContent();
        }
        catch (InvalidOperationException ex)  
        {
            _logger.LogWarning(ex.Message); 
            return Conflict(new { Message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning("Validation failed: {Errors}", ex.Errors);
            return BadRequest(new { Errors = ex.Errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a favorite movie.");
            return StatusCode(500, "An error occurred while adding the favorite movie.");
        }
    }

    [HttpDelete("{userId}/{movieId}")]
    public async Task<IActionResult> RemoveFavoriteMovie(string userId, int movieId)
    {
        try
        {
            _logger.LogInformation("Removing a movie from the user's favorite list.");
            await _mediator.Send(new RemoveFavoriteMovieCommand
            {
                UserId = userId,
                MovieId = movieId
            });
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while removing a favorite movie.");
            return StatusCode(500, "An error occurred while removing the favorite movie.");
        }
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavoriteMovies(string userId)
    {
        _logger.LogInformation("Fetching the user's favorite movies.");
        var favoriteMovies = await _mediator.Send(new GetUserFavoritesQuery { UserId = userId });
        return Ok(favoriteMovies);
    }
}
