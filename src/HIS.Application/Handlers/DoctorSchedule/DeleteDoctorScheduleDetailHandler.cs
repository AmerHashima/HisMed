using HIS.Application.Commands.DoctorSchedule;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class DeleteDoctorScheduleDetailHandler : IRequestHandler<DeleteDoctorScheduleDetailCommand,bool>
    {
        private readonly IDoctorScheduleMasterRepository _repository;

        public DeleteDoctorScheduleDetailHandler(IDoctorScheduleMasterRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteDoctorScheduleDetailCommand request, CancellationToken cancellationToken)
        {
            var details = await _repository.GetDoctorScheduleDetailByMasterId(request.MasterId,cancellationToken);
            if (details == null)
            {
                return false;
            }
            await _repository.DeleteAsync(details.Oid);
            return true;
        }
    }
}
