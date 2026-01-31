using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Application.Queries.HospitalBranch;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.HospitalBranch;

public class GetHospitalBranchDataHandler : IRequestHandler<GetHospitalBranchDataQuery, PagedResult<HospitalBranchDto>>
{
    private readonly IHospitalBranchRepository _repository;
    private readonly IQueryBuilderService _queryBuilder;
    private readonly IMapper _mapper;

    public GetHospitalBranchDataHandler(
        IHospitalBranchRepository repository, 
        IQueryBuilderService queryBuilder,
        IMapper mapper)
    {
        _repository = repository;
        _queryBuilder = queryBuilder;
        _mapper = mapper;
    }

    public async Task<PagedResult<HospitalBranchDto>> Handle(GetHospitalBranchDataQuery request, CancellationToken cancellationToken)
    {
        // Start with base query - all non-deleted branches
        var query = _repository.GetQueryable()
            .Where(x => !x.IsDeleted);

        // Apply filters
        query = _queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);

        // Apply sorting
        query = _queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);

        // Apply pagination and get results
        var pagedEntities = await _queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);

        // Map to DTOs
        var mappedData = _mapper.Map<IEnumerable<HospitalBranchDto>>(pagedEntities.Data);

        // Create paged result with mapped data
        return new PagedResult<HospitalBranchDto>
        {
            Data = mappedData,
            TotalRecords = pagedEntities.TotalRecords,
            PageNumber = pagedEntities.PageNumber,
            PageSize = pagedEntities.PageSize,
            TotalPages = pagedEntities.TotalPages,
            HasNextPage = pagedEntities.HasNextPage,
            HasPreviousPage = pagedEntities.HasPreviousPage,
            Metadata = new Dictionary<string, object>
            {
                { "availableFilters", new List<string> { "Code", "Name", "City", "State", "Country", "IsActive" } },
                { "availableSortFields", new List<string> { "Code", "Name", "City", "IsActive", "CreatedAt" } }
            }
        };
    }
}