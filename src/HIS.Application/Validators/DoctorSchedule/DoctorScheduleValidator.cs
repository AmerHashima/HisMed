using FluentValidation;
using HIS.Application.DTOs.DoctorSchedule;

namespace HIS.Application.Validators.DoctorSchedule
{
    public sealed class DoctorScheduleValidator:AbstractValidator<CreateDoctorScheduleDto>
    {
        public DoctorScheduleValidator()
        {
            
            

            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId  is Required");
            RuleFor(x => x.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId is Required");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is Required");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is Required");
            RuleFor(x => x.SlotDurationMinutes).NotEmpty().WithMessage("SlotDurationMinutes is Required");
            //RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId is Required");
            //RuleFor(x => x.SpecialtyId).NotEmpty().WithMessage("SpecialtyId is Required");


        }
        
    }
}
