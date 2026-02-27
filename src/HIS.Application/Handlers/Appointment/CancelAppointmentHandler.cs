using HIS.Application.Commands.Appointment;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Appointment;

public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public CancelAppointmentHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (appointment == null)
        {
            throw new InvalidOperationException($"Appointment with ID '{request.Id}' not found");
        }

        // Update status to cancelled
        appointment.Status = "Cancelled";
        if (!string.IsNullOrEmpty(request.Reason))
        {
            appointment.Reason = request.Reason;
        }

        await _repository.UpdateAsync(appointment, cancellationToken);
        return true;
    }
}
