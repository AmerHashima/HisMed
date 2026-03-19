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
            RuleFor(x => x.StatusId).NotEmpty().WithMessage("StatusId is Required");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId is Required");
            RuleFor(x => x.SpecialtyId).NotEmpty().WithMessage("SpecialtyId is Required");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is Required")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("StartDate must be Before EndDate");

        }
    }
}
