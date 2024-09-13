using MediatR;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class AddFavoriteMovieCommandHandler : IRequestHandler<AddFavoriteMovieCommand, Unit>
{
    private readonly IFavoriteMovieRepository _favoriteMovieRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMovieRepository _movieRepository;

    public AddFavoriteMovieCommandHandler(IFavoriteMovieRepository favoriteMovieRepository, IUserRepository userRepository, IMovieRepository movieRepository)
    {
        _favoriteMovieRepository = favoriteMovieRepository;
        _userRepository = userRepository;
        _movieRepository = movieRepository;
    }

    public async Task<Unit> Handle(AddFavoriteMovieCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserIdAsync(request.UserId);
        var movie = await _movieRepository.GetByTmdbIdAsync(request.MovieId);

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User not found.");
        }

        if (movie == null)
        {
            throw new ArgumentNullException(nameof(movie), "Movie not found.");
        }

        var favoriteMovie = new FavoriteMovie
        {
            UserId = user.UserId, 
            MovieId = movie.Id
        };

        await _favoriteMovieRepository.AddFavoriteMovieAsync(favoriteMovie);

        return Unit.Value;
    }

}

