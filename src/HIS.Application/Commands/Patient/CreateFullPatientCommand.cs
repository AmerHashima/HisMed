using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record CreateFullPatientCommand(CreateFullPatientDto Patient) : IRequest<FullPatientDto>;