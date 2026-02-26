using AutoMapper;
using HIS.Application.Commands.AppLookup;
using HIS.Application.DTOs.AppLookup;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.AppLookup;

public class CreateAppLookupDetailHandler : IRequestHandler<CreateAppLookupDetailCommand, AppLookupDetailDto>
{
    private readonly IAppLookupDetailRepository _repository;
    private readonly IAppLookupMasterRepository _masterRepository;
    private readonly IMapper _mapper;

    public CreateAppLookupDetailHandler(
        IAppLookupDetailRepository repository,
        IAppLookupMasterRepository masterRepository,
        IMapper mapper)
    {
        _repository = repository;
        _masterRepository = masterRepository;
        _mapper = mapper;
    }

    public async Task<AppLookupDetailDto> Handle(CreateAppLookupDetailCommand request, CancellationToken cancellationToken)
    {
        // Validate that the master lookup exists
        var master = await _masterRepository.GetByIdAsync(request.LookupDetail.LookupMasterID, cancellationToken);
        if (master == null)
        {
            throw new InvalidOperationException($"Lookup master with ID '{request.LookupDetail.LookupMasterID}' not found");
        }

        // Check if value code already exists for this master
        if (await _repository.ValueCodeExistsAsync(request.LookupDetail.LookupMasterID, request.LookupDetail.ValueCode, cancellationToken: cancellationToken))
        {
            throw new InvalidOperationException($"Value code '{request.LookupDetail.ValueCode}' already exists for this lookup master");
        }

        var lookupDetail = _mapper.Map<Domain.Entities.AppLookupDetail>(request.LookupDetail);
        var createdLookupDetail = await _repository.AddAsync(lookupDetail, cancellationToken);
        
        return _mapper.Map<AppLookupDetailDto>(createdLookupDetail);
    }
}
