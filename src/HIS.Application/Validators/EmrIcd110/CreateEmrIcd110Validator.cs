using FluentValidation;
using HIS.Application.Commands.EmrIcd110;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Validators.EmrIcd110
{
    public sealed  class CreateEmrIcd110Validator:AbstractValidator<CreateEmrIcd110Command>
    {
        public CreateEmrIcd110Validator()
        {
            RuleFor(x => x.Emr.Level)
            .NotEmpty()
            .WithMessage("Level is required");

        RuleFor(x => x.Emr.CodeId)
            .NotEmpty()
            .WithMessage("CodeId is required");

        RuleFor(x => x.Emr.Dagger)
            .NotEmpty()
            .WithMessage("Dagger is required");

        RuleFor(x => x.Emr.Asterisk)
            .NotEmpty()
            .WithMessage("Asterisk is required");

        RuleFor(x => x.Emr.Valid)
            .NotEmpty()
            .WithMessage("Valid is required");

        RuleFor(x => x.Emr.AustCode)
            .NotEmpty()
            .WithMessage("AustCode is required");

        RuleFor(x => x.Emr.AsciidDesc)
            .NotEmpty()
            .WithMessage("AsciidDesc is required");

        RuleFor(x => x.Emr.AsciiShortDesc)
            .NotEmpty()
            .WithMessage("AsciiShortDesc is required");

        RuleFor(x => x.Emr.Effectivefrom)
            .NotEmpty()
            .WithMessage("Effectivefrom is required");

        RuleFor(x => x.Emr.Rdiag)
            .NotEmpty()
            .WithMessage("Rdiag is required");

        RuleFor(x => x.Emr.MorphCode)
            .NotEmpty()
            .WithMessage("MorphCode is required");

        RuleFor(x => x.Emr.UnacceptPdx)
            .NotEmpty()
            .WithMessage("UnacceptPdx is required");

       
        RuleFor(x => x.Emr.Sex)
            .InclusiveBetween(1, 2)
            .WithMessage("Sex must be 1 (Male) or 2 (Female)")
            .When(x => x.Emr.Sex.HasValue);

        RuleFor(x => x.Emr.DiagnosisId)
            .NotEmpty()
            .WithMessage("DiagnosisId is required");

        }
    }
}
