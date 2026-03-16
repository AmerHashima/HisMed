using FluentValidation;
using HIS.Application.DTOs.DoctorSchedule;

namespace HIS.Application.Validators.DoctorSchedule
{
    public  class  DoctorSchdeuleListValidator:AbstractValidator<DoctorSchedulesListDto>
    {
        public DoctorSchdeuleListValidator()
        {

            RuleFor(x => x.SlotDurationMinutes).NotEmpty().WithMessage("SlotDurationMinutes is Required");
            RuleFor(x => x.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId Cant be null");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is Required")
                .LessThan(x => x.EndTime).WithMessage("StartTime Must be Before EndTime");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is Required");


        }
        
    }
}
