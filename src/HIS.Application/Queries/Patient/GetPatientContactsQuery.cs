using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientContactsQuery(Guid PatientId) : IRequest<IEnumerable<PatientContactDto>>;
