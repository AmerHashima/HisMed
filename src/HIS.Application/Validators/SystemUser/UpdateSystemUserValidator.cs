using FluentValidation;
using HIS.Application.Commands.SystemUser;

namespace HIS.Application.Validators.SystemUser;

public class UpdateSystemUserValidator : AbstractValidator<UpdateSystemUserCommand>
{
    public UpdateSystemUserValidator()
    {
        RuleFor(x => x.UpdateSystemUserDto.Oid)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.UpdateSystemUserDto.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .Matches("^[a-zA-Z0-9._-]+$").WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens.");

        RuleFor(x => x.UpdateSystemUserDto.Email)
            .EmailAddress().WithMessage("Invalid email address format.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.UpdateSystemUserDto.Email));

        RuleFor(x => x.UpdateSystemUserDto.Mobile)
            .MaximumLength(20).WithMessage("Mobile must not exceed 20 characters.")
            .Matches(@"^[\+]?[0-9\-\s\(\)]+$").WithMessage("Invalid mobile number format.")
            .When(x => !string.IsNullOrEmpty(x.UpdateSystemUserDto.Mobile));

        RuleFor(x => x.UpdateSystemUserDto.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.UpdateSystemUserDto.MiddleName)
            .MaximumLength(50).WithMessage("Middle name must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.UpdateSystemUserDto.MiddleName));

        RuleFor(x => x.UpdateSystemUserDto.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.UpdateSystemUserDto.Gender)
            .Must(g => g == null || g == 'M' || g == 'F').WithMessage("Gender must be 'M' or 'F'.")
            .When(x => x.UpdateSystemUserDto.Gender.HasValue);

        RuleFor(x => x.UpdateSystemUserDto.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Birth date must be in the past.")
            .When(x => x.UpdateSystemUserDto.BirthDate.HasValue);

        RuleFor(x => x.UpdateSystemUserDto.RoleID)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
    }
}