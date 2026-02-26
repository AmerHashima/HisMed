using AutoMapper;
using HIS.Application.DTOs.AppLookup;
using HIS.Application.Queries.AppLookup;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.AppLookup;

public class GetLookupMasterByIdHandler : IRequestHandler<GetLookupMasterByIdQuery, AppLookupMasterDto?>
{
    private readonly IAppLookupMasterRepository _repository;
    private readonly IMapper _mapper;

    public GetLookupMasterByIdHandler(IAppLookupMasterRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AppLookupMasterDto?> Handle(GetLookupMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var master = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return master != null ? _mapper.Map<AppLookupMasterDto>(master) : null;
    }
}
