﻿using MediatR;

namespace MovieCraft.Application.Features.FavoriteMovies.Queries;

public class GetUserFavoritesQuery : IRequest<UserDto>
{
    public string UserId { get; set; } = default!;
}
