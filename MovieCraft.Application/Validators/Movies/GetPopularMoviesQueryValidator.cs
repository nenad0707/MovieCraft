using FluentValidation;
using MovieCraft.Application.Features.Movies.Queries;

namespace MovieCraft.Application.Validators.Movies;

public class GetPopularMoviesQueryValidator : AbstractValidator<GetPopularMoviesQuery>
{
    public GetPopularMoviesQueryValidator()
    {
       
    }
}
