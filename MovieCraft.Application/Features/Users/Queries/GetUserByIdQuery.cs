using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public string UserId { get; set; } = default!;
}
