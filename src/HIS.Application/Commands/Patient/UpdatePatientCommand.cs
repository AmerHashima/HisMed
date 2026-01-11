using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record UpdatePatientCommand(UpdatePatientDto Patient) : IRequest<PatientDto>;