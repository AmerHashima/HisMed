using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Commands.Patient;

public record CreatePatientInsuranceCommand(CreatePatientInsuranceDto Insurance) : IRequest<PatientInsuranceDto>;
