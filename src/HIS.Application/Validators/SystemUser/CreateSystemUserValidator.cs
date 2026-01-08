using FluentValidation;
using HIS.Application.Commands.SystemUser;

namespace HIS.Application.Validators.SystemUser;

public class CreateSystemUserValidator : AbstractValidator<CreateSystemUserCommand>
{
    public CreateSystemUserValidator()
    {
        RuleFor(x => x.CreateSystemUserDto.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9._-]+$").WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens.");

        RuleFor(x => x.CreateSystemUserDto.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");

        RuleFor(x => x.CreateSystemUserDto.Email)
            .EmailAddress().WithMessage("Invalid email address format.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.CreateSystemUserDto.Email));

        RuleFor(x => x.CreateSystemUserDto.Mobile)
            .MaximumLength(20).WithMessage("Mobile must not exceed 20 characters.")
            .Matches(@"^[\+]?[0-9\-\s\(\)]+$").WithMessage("Invalid mobile number format.")
            .When(x => !string.IsNullOrEmpty(x.CreateSystemUserDto.Mobile));

        RuleFor(x => x.CreateSystemUserDto.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.CreateSystemUserDto.MiddleName)
            .MaximumLength(50).WithMessage("Middle name must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.CreateSystemUserDto.MiddleName));

        RuleFor(x => x.CreateSystemUserDto.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.CreateSystemUserDto.Gender)
            .Must(g => g == null || g == 'M' || g == 'F').WithMessage("Gender must be 'M' or 'F'.")
            .When(x => x.CreateSystemUserDto.Gender.HasValue);

        RuleFor(x => x.CreateSystemUserDto.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Birth date must be in the past.")
            .When(x => x.CreateSystemUserDto.BirthDate.HasValue);

        RuleFor(x => x.CreateSystemUserDto.RoleID)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
    }
}