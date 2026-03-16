using HIS.Application.Commands.DoctorSchedule;
using MediatR;
using AutoMapper;
using HIS.Domain.Interfaces;
namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed record DeleteDoctorSecheduelHandler : IRequestHandler<DeleteDoctorScheduelCommand, bool>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleMasterRepository repository;

        public DeleteDoctorSecheduelHandler( IDoctorScheduleMasterRepository repository)
        {
            
            this.repository = repository;
        }
        public async Task<bool> Handle(DeleteDoctorScheduelCommand request, CancellationToken cancellationToken)
        {
            var existingScheduel = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (existingScheduel == null)
            {
                return false;
            }
            await repository.DeleteAsync(existingScheduel.Oid,cancellationToken);
             return true;
        }
    }
}
