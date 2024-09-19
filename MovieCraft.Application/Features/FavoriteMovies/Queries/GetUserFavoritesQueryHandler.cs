using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.FavoriteMovies.Queries;

public class GetUserFavoritesQueryHandler : IRequestHandler<GetUserFavoritesQuery, IEnumerable<FavoriteMovieDto>>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;
    private readonly ILogger<GetUserFavoritesQueryHandler> _logger;

    public GetUserFavoritesQueryHandler(IFavoriteMovieRepository favoriteMovieRepository, ILogger<GetUserFavoritesQueryHandler> logger)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<FavoriteMovieDto>> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching favorite movies for user with Id {request.UserId} from the database.");
        var favoriteMovies = await _favoriteMovieRepository.GetFavoriteMoviesByUserIdAsync(request.UserId);

        _logger.LogInformation($"Favorite movies for user with Id {request.UserId} found in the database.");
        return favoriteMovies.Select(fm => new FavoriteMovieDto
        {
            MovieId = fm.Movie.TmdbId,
            Title = fm.Movie.Title,
            Overview = fm.Movie.Overview,
            ReleaseDate = fm.Movie.ReleaseDate,
            PosterPath = fm.Movie.PosterPath,
            BackdropPath = fm.Movie.BackdropPath
        });
    }
}
