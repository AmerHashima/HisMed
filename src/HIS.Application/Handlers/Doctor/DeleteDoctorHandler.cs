using HIS.Application.Commands.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class DeleteDoctorHandler : IRequestHandler<DeleteDoctorCommand, bool>
{
    private readonly IDoctorRepository _repository;

    public DeleteDoctorHandler(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (doctor == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}