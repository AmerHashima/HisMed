using MediatR;

namespace HIS.Application.Commands.Encounter;

public record DeleteEncounterCommand(Guid Id) : IRequest<bool>;