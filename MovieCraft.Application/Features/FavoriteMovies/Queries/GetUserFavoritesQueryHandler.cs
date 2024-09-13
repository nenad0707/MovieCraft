using AutoMapper;
using MediatR;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.FavoriteMovies.Queries;

public class GetUserFavoritesQueryHandler : IRequestHandler<GetUserFavoritesQuery, IEnumerable<FavoriteMovieDto>>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;

    public GetUserFavoritesQueryHandler(IFavoriteMovieRepository favoriteMovieRepository)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
    }

    public async Task<IEnumerable<FavoriteMovieDto>> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
    {
        var favoriteMovies = await _favoriteMovieRepository.GetFavoriteMoviesByUserIdAsync(request.UserId);

        return favoriteMovies.Select(fm => new FavoriteMovieDto
        {
            MovieId = fm.Movie.TmdbId,
            Title = fm.Movie.Title,
            Overview = fm.Movie.Overview,
            ReleaseDate = fm.Movie.ReleaseDate,
            PosterPath = fm.Movie.PosterPath
        });
    }
}
