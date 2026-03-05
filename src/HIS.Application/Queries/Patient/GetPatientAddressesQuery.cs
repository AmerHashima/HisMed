using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientAddressesQuery(Guid PatientId) : IRequest<IEnumerable<PatientAddressDto>>;
