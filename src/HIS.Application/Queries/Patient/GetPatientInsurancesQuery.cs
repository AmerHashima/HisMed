using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientInsurancesQuery(Guid PatientId) : IRequest<IEnumerable<PatientInsuranceDto>>;
