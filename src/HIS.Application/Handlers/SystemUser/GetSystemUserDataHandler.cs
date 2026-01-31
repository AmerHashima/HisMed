using AutoMapper;
using HIS.Application.DTOs.SystemUserSpace;
using HIS.Application.DTOs.Common;
using HIS.Application.Queries.SystemUserSpace;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.SystemUserSpace;

public class GetSystemUserDataHandler : IRequestHandler<GetSystemUserDataQuery, PagedResult<SystemUserDto>>
{
    private readonly ISystemUserRepository _repository;
    private readonly IQueryBuilderService _queryBuilder;
    private readonly IMapper _mapper;

    public GetSystemUserDataHandler(
        ISystemUserRepository repository, 
        IQueryBuilderService queryBuilder,
        IMapper mapper)
    {
        _repository = repository;
        _queryBuilder = queryBuilder;
        _mapper = mapper;
    }

    public async Task<PagedResult<SystemUserDto>> Handle(GetSystemUserDataQuery request, CancellationToken cancellationToken)
    {
        // Start with base query - all non-deleted users
        var query = _repository.GetQueryable()
            .Where(x => !x.IsDeleted);

        // Apply filters
        query = _queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);

        // Apply sorting
        query = _queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);

        // Apply pagination and get results
        var pagedEntities = await _queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);

        // Map to DTOs
        var mappedData = _mapper.Map<IEnumerable<SystemUserDto>>(pagedEntities.Data);

        // Create paged result with mapped data
        return new PagedResult<SystemUserDto>
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
                { "availableFilters", GetAvailableFilters() },
                { "availableSortFields", GetAvailableSortFields() }
            }
        };
    }

    private static List<string> GetAvailableFilters()
    {
        return new List<string>
        {
            "Username",
            "Email",
            "Mobile", 
            "FirstName",
            "MiddleName",
            "LastName",
            "FullName",
            "Gender",
            "BirthDate",
            "RoleID",
            "IsActive",
            "TwoFactorEnabled",
            "LastLogin",
            "FailedLoginCount",
            "CreatedAt",
            "UpdatedAt"
        };
    }

    private static List<string> GetAvailableSortFields()
    {
        return new List<string>
        {
            "Username",
            "FirstName",
            "LastName",
            "FullName",
            "Email",
            "RoleID",
            "IsActive",
            "LastLogin",
            "CreatedAt",
            "UpdatedAt"
        };
    }
}