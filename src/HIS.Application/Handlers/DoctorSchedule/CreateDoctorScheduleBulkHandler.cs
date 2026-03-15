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
            
            var scheduleDetails = request.DoctorSechduel.DoctorSchedulesList
        .Select(s => new DoctorScheduleDetail()
        {
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            DayOfWeekId = s.DayOfWeekId,
            SlotDurationMinutes = s.SlotDurationMinutes,
        })
        .ToList();

            var scheduleMaster = new DoctorScheduleMaster()
            {
                DoctorId = request.DoctorSechduel.DoctorId,
                BranchId = request.DoctorSechduel.BranchId,
                StatusId = request.DoctorSechduel.StatusId,
                SpecialtyId = request.DoctorSechduel.SpecialtyId,
                IsActive = request.DoctorSechduel.IsActive,
                IsPriority = request.DoctorSechduel.IsPriority,
                StartDate = request.DoctorSechduel.StartDate,
                EndDate = request.DoctorSechduel.EndDate,
                Details = scheduleDetails   
            };



            var schedule = await repository.AddAsync(scheduleMaster, cancellationToken);
            var result = await repository.GetByIdAsync(schedule.Oid);


            return new List<DoctorScheduleDto>()
            {
                new DoctorScheduleDto(){

                        Branch = result.Branch.Name,
                        StartTime = result.Details.FirstOrDefault().StartTime,
                        EndTime= result.Details.FirstOrDefault().EndTime,
                        StartDate =result.StartDate,
                        EndDate = result.EndDate,
                        Status = result.Status.ToString(),
                        DayOfWeekNameAr = result.Details.FirstOrDefault().DayOfweek.ValueNameAr,
                        DayOfWeekNameEn = result.Details.FirstOrDefault().DayOfweek.ValueNameEn,
                        SlotDurationMinutes = result.Details.FirstOrDefault().SlotDurationMinutes,
                        Specialty = result.Specialty.NameEn,
                        DoctorId = result.DoctorId,
                        IsActive = result.IsActive,
                        IsPriority = result.IsPriority
                     }

            };
        }
    }
}
