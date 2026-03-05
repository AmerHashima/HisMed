using MediatR;

namespace HIS.Application.Commands.Patient;

public record DeletePatientInsuranceCommand(Guid Id) : IRequest<bool>;
