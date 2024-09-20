using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MovieCraft.Application.Features.Users.Commands;
using MovieCraft.Application.Features.Users.Queries;
using MovieCraft.Shared.DTOs;

namespace MovieCraft.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")]
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
        _logger.LogInformation("Fetching user data for a specific user.");
        var user = await _mediator.Send(new GetUserByIdQuery { UserId = userId });

        if (user == null)
        {
            return NotFound(); // Return 404 if user is not found
        }

        return Ok(user);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncUser([FromBody] UserDto userDto)
    {
        var command = new SaveUserCommand
        {
            UserId = userDto.UserId,
            Name = userDto.Name
        };

        try
        {

            _logger.LogInformation("Synchronizing user.");
            await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { userId = userDto.UserId }, userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while synchronizing user.");
            return StatusCode(500, "An error occurred while synchronizing the user.");
        }
    }
}
