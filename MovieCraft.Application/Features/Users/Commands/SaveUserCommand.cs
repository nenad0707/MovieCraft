using MediatR;

namespace MovieCraft.Application.Features.Users.Commands;

public class SaveUserCommand : IRequest<Unit>
{
    public string UserId { get; set; } = default!;
    public string Name { get; set; } = default!;
}
