using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;
using HIS.Domain.Entities;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class DoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, DoctorScheduleDto>
    {
        private readonly IDoctorScheduleMasterRepository masterRepo;
        private readonly IMapper mapper;
        

        public DoctorScheduleHandler(IDoctorScheduleMasterRepository masterRepo,IMapper mapper)
        {
            this.masterRepo = masterRepo;
            this.mapper = mapper;
            
        }
        public async Task<DoctorScheduleDto> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            //var DoctorSchedule = mapper.Map<Domain.Entities.DoctorScheduleMaster>(request.DoctorSechedule);
            //var Detail = new DoctorScheduleDetail()
            //{
            //    StartTime = request.DoctorSechedule.StartTime,
            //    EndTime = request.DoctorSechedule.EndTime,
            //    DayOfWeekId = request.DoctorSechedule.DayOfWeekId,
            //    SlotDurationMinutes = request.DoctorSechedule.SlotDurationMinutes,
                
            //};
            var Master = new DoctorScheduleMaster()
            {
                BranchId = request.DoctorSechedule.BranchId,
                DoctorId = request.DoctorSechedule.DoctorId,
                StatusId = request.DoctorSechedule.StatusId,
                SpecialtyId=request.DoctorSechedule.SpecialtyId,
                IsPriority = request.DoctorSechedule.IsPriority,
                Details = new List<DoctorScheduleDetail>()
                {
                   new DoctorScheduleDetail()
                   {
                            StartTime = request.DoctorSechedule.StartTime,
                            EndTime = request.DoctorSechedule.EndTime,
                           DayOfWeekId = request.DoctorSechedule.DayOfWeekId,
                          SlotDurationMinutes = request.DoctorSechedule.SlotDurationMinutes,
                   }

                }, 

            };
           var CreateMaster= await masterRepo.AddAsync(Master);
            //var CreateDetail = await detailRepo.AddAsync(Detail);
            //return mapper.Map<DoctorScheduleDto>(CreateDoctorSchedule);
            return new DoctorScheduleDto()
            {
                DoctorId = CreateMaster.DoctorId,
                DayOfWeekNameAr = CreateMaster.Details.First().DayOfweek.ValueNameAr,
                DayOfWeekNameEn = CreateMaster.Details.First().DayOfweek.ValueNameEn,
                StartTime = CreateMaster.Details.First().StartTime,
                EndTime = CreateMaster.Details.First().EndTime,
                Specialty = CreateMaster.Specialty.NameEn,
                Branch = CreateMaster.Branch.Name,
                SlotDurationMinutes = CreateMaster.Details.First().SlotDurationMinutes,
                Status = CreateMaster.Status.ToString(),
                IsActive = CreateMaster.IsActive,
                IsPriority = CreateMaster.IsPriority,

            };
        }
    }
}
