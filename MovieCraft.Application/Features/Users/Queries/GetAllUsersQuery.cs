using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Users.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
