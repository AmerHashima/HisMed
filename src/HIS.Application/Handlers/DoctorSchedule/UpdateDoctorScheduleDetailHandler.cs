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
            var existingDetail = await repository.GetSchedulDetailsById(request.details.Oid,cancellationToken);
            if (existingDetail is null)
            {
                throw new KeyNotFoundException($"Schedule Details With MasterId {request.details.Oid} NotFound");
            }
            mapper.Map(request.details, existingDetail);
            var schedule = await repository.UpdateScheduleDetails(existingDetail, cancellationToken);

            return mapper.Map<DoctorSchedulesListDto>(schedule);
        }
    }
}
