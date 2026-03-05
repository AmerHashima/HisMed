using HIS.Application.Commands.Patient;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Patient;

public class DeletePatientAttachmentHandler : IRequestHandler<DeletePatientAttachmentCommand, bool>
{
    private readonly IPatientAttachmentRepository _repository;

    public DeletePatientAttachmentHandler(IPatientAttachmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePatientAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
