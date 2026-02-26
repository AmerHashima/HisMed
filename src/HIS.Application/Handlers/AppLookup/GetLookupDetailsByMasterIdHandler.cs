using AutoMapper;
using HIS.Application.DTOs.AppLookup;
using HIS.Application.Queries.AppLookup;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.AppLookup;

public class GetLookupDetailsByMasterIdHandler : IRequestHandler<GetLookupDetailsByMasterIdQuery, IEnumerable<AppLookupDetailDto>>
{
    private readonly IAppLookupDetailRepository _repository;
    private readonly IMapper _mapper;

    public GetLookupDetailsByMasterIdHandler(IAppLookupDetailRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppLookupDetailDto>> Handle(GetLookupDetailsByMasterIdQuery request, CancellationToken cancellationToken)
    {
        var details = await _repository.GetOrderedByMasterIdAsync(request.MasterID, cancellationToken);
        return _mapper.Map<IEnumerable<AppLookupDetailDto>>(details);
    }
}
