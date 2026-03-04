using FluentValidation;
using HIS.Application.DTOs.DoctorScheduleException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Validators.DoctorScheduleException
{
    public  sealed class UpdateDoctorSchdeulExceptionValidator:AbstractValidator<UpdateDoctorScheduleExceptionDto>
    {
        public UpdateDoctorSchdeulExceptionValidator()
        {

            RuleFor(x => x.ExceptionDate).NotEmpty().WithMessage("ExceptionDate is Required");
            RuleFor(x => x.ExceptionType).NotEmpty().WithMessage("ExceptionType is Required");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is Required");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage(" EndTime is Required");

            RuleFor(x => x.DoctorId).NotEmpty().WithMessage(" DoctorId is Required");
            RuleFor(x => x.DayOfWeekId).NotEmpty().WithMessage(" DayOfWeekId is Required");

        }
    }
}
