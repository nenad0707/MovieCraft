using FluentValidation;
using MovieCraft.Application.Features.FavoriteMovies.Queries;

namespace MovieCraft.Application.Validators.FavoriteMovies;

public class GetUserFavoritesQueryValidator : AbstractValidator<GetUserFavoritesQuery>
{
    public GetUserFavoritesQueryValidator()
    {
        RuleFor(x => x.UserId)
          .NotEmpty()
          .WithMessage("UserId must not be empty")
          .MaximumLength(50)
          .WithMessage("UserId cannot exceed 50 characters.");
    }
}
