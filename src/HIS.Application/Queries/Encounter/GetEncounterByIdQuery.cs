using HIS.Application.DTOs.Encounter;
using MediatR;

namespace HIS.Application.Queries.Encounter;

public record GetEncounterByIdQuery(Guid Id) : IRequest<EncounterDto?>;