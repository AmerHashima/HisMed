using FluentValidation;
using HIS.Application.DTOs.Specialty;

namespace HIS.Application.Validators.Specialty;

public class CreateSpecialtyValidator : AbstractValidator<CreateSpecialtyDto>
{
    public CreateSpecialtyValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Specialty code is required")
            .MaximumLength(20).WithMessage("Code cannot exceed 20 characters")
            .Matches(@"^[A-Z0-9_-]+$").WithMessage("Code must contain only uppercase letters, numbers, underscores, and hyphens");

        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage("Arabic name is required")
            .MaximumLength(100).WithMessage("Arabic name cannot exceed 100 characters");

        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("English name is required")
            .MaximumLength(100).WithMessage("English name cannot exceed 100 characters");

        RuleFor(x => x.DefaultVisitDuration)
            .InclusiveBetween(5, 180).WithMessage("Visit duration must be between 5 and 180 minutes")
            .When(x => x.DefaultVisitDuration.HasValue);

        RuleFor(x => x.DefaultPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0")
            .When(x => x.DefaultPrice.HasValue);
    }
}