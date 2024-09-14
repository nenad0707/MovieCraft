using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieCraft.Application.Features.Users.Commands;
using MovieCraft.Application.Features.Users.Queries;

namespace MovieCraft.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        _logger.LogInformation("Fetching all users.");
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        _logger.LogInformation("Fetching user with UserId: {userId}",userId);
        var user = await _mediator.Send(new GetUserByIdQuery { UserId = userId });
        return Ok(user);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncUser([FromBody] SaveUserCommand command)
    {
        _logger.LogInformation("Synchronizing user with UserId: {command.UserId}", command.UserId);
        await _mediator.Send(command);
        return Ok("User synchronized.");
    }
}
