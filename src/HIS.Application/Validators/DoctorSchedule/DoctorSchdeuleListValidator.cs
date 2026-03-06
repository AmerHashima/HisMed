using FluentValidation;
using HIS.Application.DTOs.DoctorSchedule;

namespace HIS.Application.Validators.DoctorSchedule
{
    public  class  DoctorSchdeuleListValidator:AbstractValidator<CreateDoctorScheduleBulkDto>
    {
        public DoctorSchdeuleListValidator()
        {
            
            RuleFor(x => x.DoctorSchedules).NotEmpty().WithMessage("DoctorSchdeules cannot be null");
            RuleForEach(x => x.DoctorSchedules)
                .ChildRules(DrScdeules => {
                    RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId is Required");
                    DrScdeules.RuleFor(x => x.DayOfWeekId).NotEmpty().WithMessage("DayOfWeekId Cant be null");
                    DrScdeules.RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is Required");
                    DrScdeules.RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is Required");
                    


                }); 
        }
    }
}
