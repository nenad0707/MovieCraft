﻿using MediatR;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class AddFavoriteMovieCommand : IRequest<Unit>
{
    public int MovieId { get; set; }
    public string UserId { get; set; } = default!;
}
