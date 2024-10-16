using MediatR;
using Microsoft.Extensions.Logging;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class AddFavoriteMovieCommandHandler : IRequestHandler<AddFavoriteMovieCommand, Unit>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<AddFavoriteMovieCommandHandler> _logger;

    public AddFavoriteMovieCommandHandler(IFavoriteMovieRepository favoriteMovieRepository, IUserRepository userRepository, 
        IMovieRepository movieRepository, ILogger<AddFavoriteMovieCommandHandler> logger)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
        _userRepository = userRepository;
        _movieRepository = movieRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Adding movie with TmdbId {request.MovieId} to favorites for user with Id {request.UserId}.");

      
        var user = await _userRepository.GetByUserIdAsync(request.UserId);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User not found.");
        }

      
        var movie = await _movieRepository.GetByTmdbIdAsync(request.MovieId);
        if (movie == null)
        {
            throw new ArgumentNullException(nameof(movie), "Movie not found.");
        }

        
        var existingFavorite = await _favoriteMovieRepository.GetFavoriteMovieAsync(user.UserId, movie.Id);
        if (existingFavorite != null)
        {
            _logger.LogWarning($"Movie with Id {movie.Id} is already in favorites for user {user.UserId}.");
            throw new InvalidOperationException("This movie is already in your favorites.");
        }

        
        var favoriteMovie = new FavoriteMovie
        {
            UserId = user.UserId,
            MovieId = movie.Id
        };

        _logger.LogInformation("Saving favorite movie to the database.");
        await _favoriteMovieRepository.AddFavoriteMovieAsync(favoriteMovie);

        return Unit.Value;
    }

}

