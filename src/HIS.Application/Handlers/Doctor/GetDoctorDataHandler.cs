using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Doctor;
using HIS.Application.Queries.Doctor;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Doctor;

public class GetDoctorDataHandler : IRequestHandler<GetDoctorDataQuery, PagedResult<DoctorDto>>
{
    private readonly IDoctorRepository _repository;
    private readonly IQueryBuilderService _queryBuilder;
    private readonly IMapper _mapper;

    public GetDoctorDataHandler(
        IDoctorRepository repository,
        IQueryBuilderService queryBuilder,
        IMapper mapper)
    {
        _repository = repository;
        _queryBuilder = queryBuilder;
        _mapper = mapper;
    }

    public async Task<PagedResult<DoctorDto>> Handle(GetDoctorDataQuery request, CancellationToken cancellationToken)
    {
        // Start with base query - all non-deleted doctors with includes
        var query = _repository.GetQueryable()
            .Where(x => !x.IsDeleted);

        // Apply filters
        query = _queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);

        // Apply sorting
        query = _queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);

        // Apply pagination and get results
        var pagedEntities = await _queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);

        // Map to DTOs
        var mappedData = _mapper.Map<IEnumerable<DoctorDto>>(pagedEntities.Data);

        // Create paged result with mapped data
        return new PagedResult<DoctorDto>
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
                { "availableFilters", new List<string> { "LicenseNumber", "SpecialtyId", "BranchId", "DepartmentLookupId", "IsActive", "IsNphiesEnabled" } },
                { "availableSortFields", new List<string> { "LicenseNumber", "IsActive", "CreatedAt" } }
            }
        };
    }
}