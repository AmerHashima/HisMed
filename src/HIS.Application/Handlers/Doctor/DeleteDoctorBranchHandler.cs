using HIS.Application.Commands.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class DeleteDoctorBranchHandler : IRequestHandler<DeleteDoctorBranchCommand, bool>
{
    private readonly IDoctorBranchRepository _repository;

    public DeleteDoctorBranchHandler(IDoctorBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDoctorBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
