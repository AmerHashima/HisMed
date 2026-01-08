using FluentValidation;
using HIS.Application.Commands.Auth;

namespace HIS.Application.Validators.Auth;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginDto)
            .NotNull()
            .WithMessage("Login data is required")
            .SetValidator(new LoginDtoValidator());
    }
}