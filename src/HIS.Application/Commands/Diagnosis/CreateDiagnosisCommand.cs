using HIS.Application.DTOs.Diagnosis;
using HIS.Application.DTOs.Encounter;
using MediatR;

namespace HIS.Application.Commands.Diagnosis;

public record CreateDiagnosisCommand(CreateDiagnosisDto Diagnosis) : IRequest<DiagnosisDto>;