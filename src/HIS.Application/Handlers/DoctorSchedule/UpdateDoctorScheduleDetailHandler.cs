using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public class UpdateDoctorScheduleDetailHandler : IRequestHandler<UpdateDoctorScheduleDetailsCommand, DoctorSchedulesListDto>
    {
        private readonly IDoctorScheduleMasterRepository repository;
        private readonly IMapper mapper;

        public UpdateDoctorScheduleDetailHandler(IDoctorScheduleMasterRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<DoctorSchedulesListDto> Handle(UpdateDoctorScheduleDetailsCommand request, CancellationToken cancellationToken)
        {
            var ExsisitingDetail = await repository.GetDoctorScheduleDetailByMasterId(request.details.MasterId,cancellationToken);
            if (ExsisitingDetail is null)
            {
                throw new KeyNotFoundException($"Schedule Details With MasterId {request.details.MasterId} NotFound");
            }
             var schedule = repository.UpdateScheduleDetails(ExsisitingDetail);
            var result = await repository.GetByIdAsync(schedule.Oid,cancellationToken);
            return mapper.Map<DoctorSchedulesListDto>(result);
        }
    }
}
