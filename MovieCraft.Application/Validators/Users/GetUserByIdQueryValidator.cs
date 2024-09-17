using FluentValidation;
using MovieCraft.Application.Features.Users.Queries;

namespace MovieCraft.Application.Validators.Users;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
         .NotEmpty()
         .WithMessage("UserId must not be empty")
         .MaximumLength(50)
         .WithMessage("UserId cannot exceed 50 characters.");

    }
}
