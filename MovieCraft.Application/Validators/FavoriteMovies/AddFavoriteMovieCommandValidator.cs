using FluentValidation;
using MovieCraft.Application.Features.FavoriteMovies.Commands;

namespace MovieCraft.Application.Validators.FavoriteMovies;

public class AddFavoriteMovieCommandValidator : AbstractValidator<AddFavoriteMovieCommand>
{
    public AddFavoriteMovieCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .GreaterThan(0)
            .WithMessage("MovieId must be greater than 0");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId must not be empty")
            .MaximumLength(50)
            .WithMessage("UserId cannot exceed 50 characters.");
    }
}

