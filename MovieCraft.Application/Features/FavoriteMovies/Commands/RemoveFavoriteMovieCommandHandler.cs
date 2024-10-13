using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class RemoveFavoriteMovieCommandHandler : IRequestHandler<RemoveFavoriteMovieCommand, Unit>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;
    private readonly ILogger<RemoveFavoriteMovieCommandHandler> _logger;

    public RemoveFavoriteMovieCommandHandler(IFavoriteMovieRepository favoriteMovieRepository, ILogger<RemoveFavoriteMovieCommandHandler> logger)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(RemoveFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        var favoriteMovie = await _favoriteMovieRepository.GetFavoriteMovieAsync(request.UserId, request.MovieId);
        if (favoriteMovie == null)
        {
            throw new KeyNotFoundException("Favorite movie not found.");
        }

        await _favoriteMovieRepository.RemoveFavoriteMovie(favoriteMovie);

        _logger.LogInformation($"Favorite movie with Id {favoriteMovie.Id} removed for user with Id {request.UserId}.");

        return Unit.Value;
    }
}
