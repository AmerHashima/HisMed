using FluentValidation;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Services;

namespace HIS.Application.Validators.DoctorSchedule
{
    public  sealed class CreateDoctorSchdeuleListValidator:AbstractValidator<CreateDoctorScheduleBulkCommand>
    {
        private readonly IDoctorScheduleValidationService service;

      
        public CreateDoctorSchdeuleListValidator()
        {
            
            RuleFor(x => x.DoctorSechduel).NotEmpty().WithMessage("Schedule List cannot be Empty");

            RuleFor(x => x.DoctorSechduel.BranchId).NotEmpty().WithMessage("BranchId  is Required");
            RuleFor(x => x.DoctorSechduel.StatusId).NotEmpty().WithMessage("StatusId  is Required");
            RuleFor(x => x.DoctorSechduel.DoctorId).NotEmpty().WithMessage("DoctorId  is Required");
            RuleFor(x => x.DoctorSechduel.IsActive).NotEmpty().WithMessage("IsActive  is Required");
            RuleFor(x => x.DoctorSechduel.StartDate).NotEmpty().WithMessage("StartDate is Required")
                .Must((Model,x)=> x <= Model.DoctorSechduel.EndDate) .WithMessage("StartDate must be Before EndDate");
            RuleFor(x => x.DoctorSechduel.EndDate).NotEmpty().WithMessage("EndDate is Required");
            
                
            RuleForEach(x => x.DoctorSechduel.DoctorScheduleDetailList).SetValidator(new DoctorSchdeuleListValidator());



        }
    }
}
