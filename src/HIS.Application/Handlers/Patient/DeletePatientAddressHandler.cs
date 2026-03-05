using HIS.Application.Commands.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class DeletePatientAddressHandler : IRequestHandler<DeletePatientAddressCommand, bool>
{
    private readonly IPatientAddressRepository _repository;

    public DeletePatientAddressHandler(IPatientAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePatientAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
