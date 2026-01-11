using HIS.Application.DTOs.AppLookup;
using MediatR;

namespace HIS.Application.Commands.AppLookup;

public record CreateAppLookupDetailCommand(CreateAppLookupDetailDto LookupDetail) : IRequest<AppLookupDetailDto>;