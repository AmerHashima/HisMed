using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record CreatePatientCommand(CreatePatientDto Patient) : IRequest<PatientDto>;