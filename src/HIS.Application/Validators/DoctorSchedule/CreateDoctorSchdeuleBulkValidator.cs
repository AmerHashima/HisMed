using FluentValidation;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;

namespace HIS.Application.Validators.DoctorSchedule
{
    public  sealed class CreateDoctorSchdeuleListValidator:AbstractValidator<CreateDoctorScheduleBulkCommand>
    {
        public CreateDoctorSchdeuleListValidator()
        {
            
            RuleFor(x => x.DoctorSechduel).NotEmpty().WithMessage("Schedule List cannot be Empty");

            RuleFor(x => x.DoctorSechduel.BranchId).NotEmpty().WithMessage("BranchId  is Required");
            RuleFor(x => x.DoctorSechduel.StatusId).NotEmpty().WithMessage("StatusId  is Required");
            RuleFor(x => x.DoctorSechduel.DoctorId).NotEmpty().WithMessage("DoctorId  is Required");
            RuleFor(x => x.DoctorSechduel.IsActive).NotEmpty().WithMessage("IsActive  is Required");
            RuleForEach(x => x.DoctorSechduel.DoctorSchedulesList).SetValidator(new DoctorSchdeuleListValidator());



        }
    }
}
