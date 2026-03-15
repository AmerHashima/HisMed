using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDoctorScheduleBulkHandler : IRequestHandler<CreateDoctorScheduleBulkCommand, List<DoctorScheduleDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;

        public CreateDoctorScheduleBulkHandler(IMapper mapper,IDoctorScheduleMasterRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<List<DoctorScheduleDto>> Handle(CreateDoctorScheduleBulkCommand request, CancellationToken cancellationToken)
        {
            //var SchedulesList = mapper.Map<List<Domain.Entities.DoctorSchedule>>(request.DoctorSechduelList);
            var SchedulesList = request.DoctorSechduelList.SelectMany(x => x.DoctorSchedules
            .Select(innerlist => new DoctorScheduleMaster()
            {
                DoctorId = innerlist.DoctorId,
               BranchId = innerlist.BranchId,
               StatusId = innerlist.StatusId,
               SpecialtyId= innerlist.SpecialtyId,
               IsActive= innerlist.IsActive,
               IsPriority= innerlist.IsPriority,
               Details = new List<DoctorScheduleDetail>()
               {
                   new DoctorScheduleDetail()
                   {
                       StartTime = innerlist.StartTime,
                       EndTime = innerlist.EndTime,
                       DayOfWeekId = innerlist.DayOfWeekId,
                       SlotDurationMinutes = innerlist.SlotDurationMinutes,

                   }
               }
               
            }));
            
            var result = await repository.AddDoctorScheduelList(SchedulesList,cancellationToken);

           
            return   result.Select(schedule => new DoctorScheduleDto()
            {
                
                        Branch =schedule.Branch.Name,
                        StartTime = schedule.Details.First().StartTime,
                        EndTime= schedule.Details.First().EndTime,
                        Status = schedule.Status.ToString(),
                        DayOfWeekNameAr = schedule.Details.First().DayOfweek.ValueNameAr,
                        DayOfWeekNameEn = schedule.Details.First().DayOfweek.ValueNameEn,
                        SlotDurationMinutes =schedule.Details.First().SlotDurationMinutes,
                        Specialty = schedule.Specialty.NameEn,
                        DoctorId =schedule.DoctorId,
                        IsActive = schedule.IsActive,
                        IsPriority = schedule.IsPriority
                     

            }).ToList();
        }
    }
}
