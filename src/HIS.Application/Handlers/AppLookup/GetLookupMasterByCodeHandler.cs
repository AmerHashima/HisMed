using AutoMapper;
using HIS.Application.DTOs.AppLookup;
using HIS.Application.Queries.AppLookup;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.AppLookup;

public class GetLookupMasterByCodeHandler : IRequestHandler<GetLookupMasterByCodeQuery, AppLookupMasterDto?>
{
    private readonly IAppLookupMasterRepository _repository;
    private readonly IMapper _mapper;

    public GetLookupMasterByCodeHandler(IAppLookupMasterRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AppLookupMasterDto?> Handle(GetLookupMasterByCodeQuery request, CancellationToken cancellationToken)
    {
        var lookupMaster = request.IncludeDetails
            ? await _repository.GetByCodeWithDetailsAsync(request.LookupCode, cancellationToken)
            : await _repository.GetByLookupCodeAsync(request.LookupCode, cancellationToken);

        return lookupMaster == null ? null : _mapper.Map<AppLookupMasterDto>(lookupMaster);
    }
}