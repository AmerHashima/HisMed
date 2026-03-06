using FluentValidation;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;

namespace HIS.Application.Validators.DoctorSchedule
{
    public  sealed class CreateDoctorSchdeuleListValidator:AbstractValidator<CreateDoctorScheduleBulkCommand>
    {
        public CreateDoctorSchdeuleListValidator()
        {
            
            RuleFor(x => x.DoctorSechduelList).NotEmpty().WithMessage("Schedule List cannot be Empty");
                
            RuleForEach(x => x.DoctorSechduelList).SetValidator(new DoctorSchdeuleListValidator());


        }
    }
}
