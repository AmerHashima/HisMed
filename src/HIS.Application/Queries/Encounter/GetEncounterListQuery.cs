using HIS.Application.DTOs.Encounter;
using MediatR;

namespace HIS.Application.Queries.Encounter;

public record GetEncounterListQuery(
    Guid? PatientId = null,
    Guid? DoctorId = null
) : IRequest<IEnumerable<EncounterDto>>;