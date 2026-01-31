using HIS.Application.DTOs.Encounter;
using MediatR;

namespace HIS.Application.Commands.Encounter;

public record UpdateEncounterCommand(UpdateEncounterDto Encounter) : IRequest<EncounterDto>;