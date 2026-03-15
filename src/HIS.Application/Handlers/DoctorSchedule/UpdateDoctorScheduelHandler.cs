using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Common;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class UpdateDoctorScheduelHandler : IRequestHandler<UpdateDoctorScheduleCommand, DoctorScheduleDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository doctorScheduelRepo;

        public UpdateDoctorScheduelHandler(IMapper mapper,IDoctorScheduleMasterRepository DoctorScheduelRepo)
        {
            this.mapper = mapper;
            doctorScheduelRepo = DoctorScheduelRepo;
        }
        public async Task<DoctorScheduleDto> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var master = await doctorScheduelRepo.GetByIdAsync(request.DoctorSchdeuel.Oid, cancellationToken);
            if (master == null)
            {
                throw new KeyNotFoundException($"Doctor with ID {request.DoctorSchdeuel.Oid} not found");
            }
            //Update Data Manualy (MasterData) 
           
            master.DoctorId = request.DoctorSchdeuel.DoctorId;
            master.BranchId = request.DoctorSchdeuel.BranchId;
            master.StatusId = request.DoctorSchdeuel.StatusId;
            master.IsActive = request.DoctorSchdeuel.IsActive;
            master.IsPriority = request.DoctorSchdeuel.IsPriority;
            master.SpecialtyId = request.DoctorSchdeuel.SpecialtyId;
            // (Detial Data)
            var exsistingDetail = master.Details.First();
            exsistingDetail.StartTime = request.DoctorSchdeuel.StartTime;
            exsistingDetail.EndTime = request.DoctorSchdeuel.EndTime;
            exsistingDetail.SlotDurationMinutes = request.DoctorSchdeuel.SlotDurationMinutes;
            exsistingDetail.DayOfWeekId = request.DoctorSchdeuel.DayOfWeekId;

            return  new DoctorScheduleDto()
            {
                DoctorId = master.DoctorId,
                DayOfWeekNameAr = exsistingDetail.DayOfweek.ValueNameAr,
                DayOfWeekNameEn = exsistingDetail.DayOfweek.ValueNameEn,
                StartTime = exsistingDetail.StartTime,
                EndTime = exsistingDetail.EndTime,
                Specialty = master.Specialty.NameEn,
                Branch = master.Branch.Name,
                SlotDurationMinutes = exsistingDetail.SlotDurationMinutes,
                Status = master.Status.ToString(),
                IsActive = master.IsActive,
                IsPriority = master.IsPriority,

            };
        }
    }
}
