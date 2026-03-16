using FluentValidation;
using HIS.Application.Commands.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Validators.DoctorSchedule
{
    public sealed class UpdateDoctorScheduleDetailsValidator:AbstractValidator<UpdateDoctorScheduleDetailsCommand>
    {
        public UpdateDoctorScheduleDetailsValidator()
        {
            RuleFor(x => x.details.StartTime).NotEmpty()
            .Must((model,x) =>  x < model.details.EndTime)
            .WithMessage("Start Time must be Before EndTime");
            RuleFor(x => x.details.EndTime).NotEmpty()
            .Must((model,x) =>  x > model.details.StartTime)
            .WithMessage("End Time must be After StartTime");
            RuleFor(x => x.details.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId is Required");
            
        }
    }
}
