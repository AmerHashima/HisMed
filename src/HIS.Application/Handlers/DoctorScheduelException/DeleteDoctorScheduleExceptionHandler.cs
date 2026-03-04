using AutoMapper;
using HIS.Application.Commands.DoctorScheduelException;
using HIS.Domain.Common;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class DeleteDoctorScheduleExceptionHandler : IRequestHandler<DeleteDoctorScheduleExceptionCommand,bool>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;

        public DeleteDoctorScheduleExceptionHandler(IMapper mapper,IDoctorscheduelExceptionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteDoctorScheduleExceptionCommand request, CancellationToken cancellationToken)
        {
            var existingScheduelException = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingScheduelException   == null)
            {
                return false;
            }
            await repository.DeleteAsync(existingScheduelException.Oid, cancellationToken);
            return true;
        }
    }
}

