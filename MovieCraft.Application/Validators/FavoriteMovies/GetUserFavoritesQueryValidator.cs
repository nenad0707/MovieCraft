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
            .MaximumLength(450)
            .WithMessage("UserId cannont exceed 450 characters.");
    }
}
