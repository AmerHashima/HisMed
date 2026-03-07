using HIS.Application.Commands.Doctor;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class DeleteDoctorAttachmentHandler : IRequestHandler<DeleteDoctorAttachmentCommand, bool>
{
    private readonly IDoctorAttachmentRepository _repository;

    public DeleteDoctorAttachmentHandler(IDoctorAttachmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDoctorAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
