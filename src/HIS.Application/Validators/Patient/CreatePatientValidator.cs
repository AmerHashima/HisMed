using FluentValidation;
using HIS.Application.DTOs.Patient;

namespace HIS.Application.Validators.Patient;

public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientValidator()
    {
        // Identifier validation
        RuleFor(x => x.IdentifierType)
            .NotEmpty().WithMessage("Identifier type is required")
            .Must(BeValidIdentifierType).WithMessage("Identifier type must be NationalID, Passport, or Iqama");

        // Conditional identifier validation
        When(x => x.IdentifierType == "NationalID", () => {
            RuleFor(x => x.NationalID)
                .NotEmpty().WithMessage("National ID is required when identifier type is NationalID")
                .Length(10).WithMessage("National ID must be 10 digits")
                .Matches(@"^\d{10}$").WithMessage("National ID must contain only digits");
        });

        When(x => x.IdentifierType == "Passport", () => {
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage("Passport number is required when identifier type is Passport")
                .Length(5, 20).WithMessage("Passport number must be between 5 and 20 characters");
        });

        // Names validation
        RuleFor(x => x.FirstNameAr)
            .NotEmpty().WithMessage("Arabic first name is required")
            .MaximumLength(100).WithMessage("Arabic first name cannot exceed 100 characters");

        RuleFor(x => x.LastNameAr)
            .NotEmpty().WithMessage("Arabic last name is required")
            .MaximumLength(100).WithMessage("Arabic last name cannot exceed 100 characters");

        RuleFor(x => x.FirstNameEn)
            .NotEmpty().WithMessage("English first name is required")
            .MaximumLength(100).WithMessage("English first name cannot exceed 100 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("English first name must contain only letters");

        RuleFor(x => x.LastNameEn)
            .NotEmpty().WithMessage("English last name is required")
            .MaximumLength(100).WithMessage("English last name cannot exceed 100 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("English last name must contain only letters");

        // Demographics validation
        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required")
            .Must(BeValidGender).WithMessage("Gender must be M or F");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .Must(BeAValidAge).WithMessage("Invalid birth date");

        RuleFor(x => x.BloodGroup)
            .Must(BeValidBloodGroup).WithMessage("Invalid blood group")
            .When(x => !string.IsNullOrEmpty(x.BloodGroup));

        // Contact validation
        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile number is required")
            .Matches(@"^(\+966|0)?[5]\d{8}$").WithMessage("Mobile must be a valid Saudi mobile number");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.Email));

        // Address validation
        RuleFor(x => x.AddressLine1)
            .NotEmpty().WithMessage("Address line 1 is required")
            .MaximumLength(200).WithMessage("Address line 1 cannot exceed 200 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(100).WithMessage("Country cannot exceed 100 characters");
    }

    private static bool BeValidIdentifierType(string identifierType)
    {
        return identifierType is "NationalID" or "Passport" or "Iqama";
    }

    private static bool BeValidGender(char gender)
    {
        return gender == 'M' || gender == 'F';
    }

    private static bool BeAValidAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Year;
        
        return dateOfBirth <= today && age <= 150;
    }

    private static bool BeValidBloodGroup(string bloodGroup)
    {
        var validBloodGroups = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
        return validBloodGroups.Contains(bloodGroup);
    }
}