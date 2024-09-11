using MediatR;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.FavoriteMovies.Commands;

public class AddFavoriteMovieCommandHandler : IRequestHandler<AddFavoriteMovieCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMovieRepository _movieRepository;

    public AddFavoriteMovieCommandHandler(IUserRepository userRepository, IMovieRepository movieRepository)
    {
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

        user.FavoriteMovies ??= new List<FavoriteMovie>();
        user.FavoriteMovies.Add(new FavoriteMovie { MovieId = movie.Id, UserId = user.Id });

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }

}

