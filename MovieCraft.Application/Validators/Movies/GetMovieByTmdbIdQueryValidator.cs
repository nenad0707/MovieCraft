using FluentValidation;
using MovieCraft.Application.Features.Movies.Queries;

namespace MovieCraft.Application.Validators.Movies;

public class GetMovieByTmdbIdQueryValidator : AbstractValidator<GetMovieByTmdbIdQuery>
{
    public GetMovieByTmdbIdQueryValidator()
    {
        RuleFor(x => x.TmdbId)
            .GreaterThan(0)
            .WithMessage("TmdbId must be greater than 0");
    }
}
