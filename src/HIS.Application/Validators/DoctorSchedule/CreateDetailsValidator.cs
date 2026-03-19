using FluentValidation;
using HIS.Application.Commands.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Validators.DoctorSchedule
{
    public class CreateDetailsValidator:AbstractValidator<CreateDetailsCommand>
    {
        public CreateDetailsValidator()
        {
                RuleFor(x => x.details.StartTime).NotEmpty()
                .WithMessage("StartTime is Required").LessThan(x => x.details.EndTime).WithMessage("StartTime must be before EndTime");
            RuleFor(x => x.details.EndTime).NotEmpty().WithMessage("EndTime is Required");
            RuleFor(x => x.details.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId is Required");
        }
    }
}
