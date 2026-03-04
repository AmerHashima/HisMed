using AutoMapper;
using HIS.Application.Commands.DoctorScheduelException;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class UpdateDoctorScheduleExceptionHandler : IRequestHandler<UpdateDoctorScheduleCommand, DoctorScheduleExceptionResponseDto>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;

        public UpdateDoctorScheduleExceptionHandler(IMapper mapper, IDoctorscheduelExceptionRepository repository) 
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<DoctorScheduleExceptionResponseDto> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var ExsistingScheduelException = await repository.GetByIdAsync(request.DoctorScheduleException.Id,cancellationToken);
            if(ExsistingScheduelException ==null)
                throw new KeyNotFoundException($"DoctorScheduleException with ID {request.DoctorScheduleException.Id} not found");
            await repository.UpdateAsync(ExsistingScheduelException,cancellationToken);
            return mapper.Map<DoctorScheduleExceptionResponseDto>(ExsistingScheduelException);
        }
    }
}
