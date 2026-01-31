using HIS.Application.DTOs.Encounter;
using MediatR;

namespace HIS.Application.Commands.Encounter;

public record CreateEncounterCommand(CreateEncounterDto Encounter) : IRequest<EncounterDto>;