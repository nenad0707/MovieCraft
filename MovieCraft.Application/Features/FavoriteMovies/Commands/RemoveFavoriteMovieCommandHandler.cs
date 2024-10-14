using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.Interfaces;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class RemoveFavoriteMovieCommandHandler : IRequestHandler<RemoveFavoriteMovieCommand, Unit>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<RemoveFavoriteMovieCommandHandler> _logger;

    public RemoveFavoriteMovieCommandHandler(
        IFavoriteMovieRepository favoriteMovieRepository,
        IMovieRepository movieRepository,
        ILogger<RemoveFavoriteMovieCommandHandler> logger)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
        _movieRepository = movieRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(RemoveFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting to remove movie with TmdbId: {request.MovieId} for user: {request.UserId}");

        var movie = await _movieRepository.GetByTmdbIdAsync(request.MovieId); 
        if (movie == null)
        {
            _logger.LogWarning($"Movie with TmdbId {request.MovieId} not found in the Movies table.");
            throw new KeyNotFoundException($"Movie with TmdbId {request.MovieId} not found.");
        }

        var favoriteMovie = await _favoriteMovieRepository.GetFavoriteMovieAsync(request.UserId, movie.Id);
        if (favoriteMovie == null)
        {
            _logger.LogWarning($"Favorite movie with MovieId: {movie.Id} not found for user: {request.UserId}");
            throw new KeyNotFoundException($"Favorite movie with MovieId: {movie.Id} not found for user: {request.UserId}");
        }

        await _favoriteMovieRepository.RemoveFavoriteMovie(favoriteMovie);

        _logger.LogInformation($"Favorite movie with MovieId {movie.Id} removed for user with Id {request.UserId}.");

        return Unit.Value;
    }
}
