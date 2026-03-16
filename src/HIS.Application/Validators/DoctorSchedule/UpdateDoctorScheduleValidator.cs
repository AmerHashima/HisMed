using FluentValidation;
using HIS.Application.DTOs.DoctorSchedule;
using MediatR;
namespace HIS.Application.Validators.DoctorSchedule
{
    public sealed class UpdateDoctorScheduleValidator:AbstractValidator<UpdateDoctorSchdeuelDto>
    {
        public UpdateDoctorScheduleValidator() 
        {
            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId  is Required");
            RuleFor(x => x.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId is Required");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is Required")
           .LessThan(x => x.EndTime).WithMessage("StartTime must be Before EndTime"); 
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is Required"); 
        }
    }
}
