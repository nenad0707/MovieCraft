using MediatR;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class RemoveFavoriteMovieCommand : IRequest<Unit>
{
    public string UserId { get; set; } = default!;
    public int MovieId { get; set; }
}
