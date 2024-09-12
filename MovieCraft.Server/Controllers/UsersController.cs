using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.FavoriteMovies.Commands;
using MovieCraft.Application.Features.FavoriteMovies.Queries;
using MovieCraft.Application.Features.Users.Commands;
using MovieCraft.Application.Features.Users.Queries;

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

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { UserId = userId });
        return Ok(user);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncUser([FromBody] SaveUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("User synchronized.");
    }
}
