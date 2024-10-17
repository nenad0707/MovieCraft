using FluentValidation;
using MovieCraft.Application.Features.Movies.Commands;

namespace MovieCraft.Application.Validators.Movies;

public class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
{
    public AddMovieCommandValidator()
    {
        RuleFor(x => x.TmdbId)
            .GreaterThan(0)
            .WithMessage("TmdbId must be greater than 0");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title must not be empty")
            .MaximumLength(200)
            .WithMessage("Title cannont exceed 200 characters.");

        RuleFor(x => x.Overview)
            .NotEmpty()
            .WithMessage("Overview cannot exceed 1000 characters.")
            .MaximumLength(1000);

        RuleFor(x => x.PosterPath)
               .MaximumLength(500)
               .WithMessage("PosterPath cannot exceed 500 characters.");

        RuleFor(x => x.BackdropPath)
            .MaximumLength(500)
                .WithMessage("BackdropPath cannot exceed 500 characters.");

        RuleFor(x => x.TrailerUrl)
              .MaximumLength(500)
              .WithMessage("Trailer URL cannot exceed 500 characters.");
    }
}
