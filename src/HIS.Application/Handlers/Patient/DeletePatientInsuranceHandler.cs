using HIS.Application.Commands.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class DeletePatientInsuranceHandler : IRequestHandler<DeletePatientInsuranceCommand, bool>
{
    private readonly IPatientInsuranceRepository _repository;

    public DeletePatientInsuranceHandler(IPatientInsuranceRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePatientInsuranceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
