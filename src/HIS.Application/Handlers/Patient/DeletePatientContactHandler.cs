using HIS.Application.Commands.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class DeletePatientContactHandler : IRequestHandler<DeletePatientContactCommand, bool>
{
    private readonly IPatientContactRepository _repository;

    public DeletePatientContactHandler(IPatientContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePatientContactCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
