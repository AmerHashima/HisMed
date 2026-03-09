using AutoMapper;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Application.Queries.DoctorSceduelException;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class GetDoctorScheduleExceptionListHandler : IRequestHandler<GetDoctorSchdeuleExceptionListQuery, IEnumerable<DoctorScheduleExceptionResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;

        public GetDoctorScheduleExceptionListHandler(IMapper mapper,IDoctorscheduelExceptionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public  async Task<IEnumerable<DoctorScheduleExceptionResponseDto>> Handle(GetDoctorSchdeuleExceptionListQuery request, CancellationToken cancellationToken)
        {
            if (request.DoctorId.HasValue)
            {
                var result = await repository.GetSchdeulesExceptionByDoctorIdAsync(request.DoctorId.Value);
                return mapper.Map<IEnumerable<DoctorScheduleExceptionResponseDto>>(result);
            }
            else if (request.ExceptionDate.HasValue)
            {
                var result = await repository.GetSchdeuleExceptionByExceptionDateAsync(request.ExceptionDate.Value);
                return mapper.Map<IEnumerable<DoctorScheduleExceptionResponseDto>>(result);
            }
            else if (request.StartTime.HasValue)
            {
                var result = await repository.GetScheduleExceptionByStartTimeAsync(request.StartTime.Value);
                return mapper.Map<IEnumerable<DoctorScheduleExceptionResponseDto>>(result);
            }
            var scheduleException=await repository.GetAllAsync();
            return mapper.Map<IEnumerable<DoctorScheduleExceptionResponseDto>>(scheduleException);
        }
    }
}
