using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Common;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using MediatR;
namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class CreateDetailsHandler : IRequestHandler<CreateDetailsCommand, DoctorSchedulesListDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repo;

        public CreateDetailsHandler(IMapper mapper ,IDoctorScheduleMasterRepository repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        public  async Task<DoctorSchedulesListDto> Handle(CreateDetailsCommand request, CancellationToken cancellationToken)
        {
            
            var master = await repo.GetByIdAsync(request.details.MasterId, cancellationToken);
            if (master is null)
                throw new KeyNotFoundException($"Doctor Schedule With MasterId {request.details.MasterId} NotFound");

            
            var detail = mapper.Map<DoctorScheduleDetail>(request.details);

            
            var created = await repo.AddScheduleDetailAsync(detail, cancellationToken);

            return mapper.Map<DoctorSchedulesListDto>(created);
        }
    }
}
