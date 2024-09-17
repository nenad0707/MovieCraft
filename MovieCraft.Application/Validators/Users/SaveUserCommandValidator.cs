using FluentValidation;
using MovieCraft.Application.Features.Users.Commands;

namespace MovieCraft.Application.Validators.Users;

public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
{
    public SaveUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .MaximumLength(450).WithMessage("UserId cannot exceed 450 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
