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
                Details = scheduleDetails   
            };



            var result = await repository.AddAsync(scheduleMaster, cancellationToken);


            return new List<DoctorScheduleDto>()
            {
                new DoctorScheduleDto(){

                        Branch = result.Branch.Name,
                        StartTime = result.Details.First().StartTime,
                        EndTime= result.Details.First().EndTime,
                        Status = result.Status.ToString(),
                        DayOfWeekNameAr = result.Details.First().DayOfweek.ValueNameAr,
                        DayOfWeekNameEn = result.Details.First().DayOfweek.ValueNameEn,
                        SlotDurationMinutes = result.Details.First().SlotDurationMinutes,
                        Specialty = result.Specialty.NameEn,
                        DoctorId = result.DoctorId,
                        IsActive = result.IsActive,
                        IsPriority = result.IsPriority
                     }

            };
        }
    }
}
