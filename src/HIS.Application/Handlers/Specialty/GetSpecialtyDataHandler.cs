using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Specialty;
using HIS.Application.Queries.Specialty;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.Specialty;

public class GetSpecialtyDataHandler : IRequestHandler<GetSpecialtyDataQuery, PagedResult<SpecialtyDto>>
{
    private readonly ISpecialtyRepository _repository;
    private readonly IQueryBuilderService _queryBuilder;
    private readonly IMapper _mapper;

    public GetSpecialtyDataHandler(
        ISpecialtyRepository repository,
        IQueryBuilderService queryBuilder,
        IMapper mapper)
    {
        _repository = repository;
        _queryBuilder = queryBuilder;
        _mapper = mapper;
    }

    public async Task<PagedResult<SpecialtyDto>> Handle(GetSpecialtyDataQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.GetQueryable().Where(x => !x.IsDeleted);
        query = _queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);
        query = _queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);
        var pagedEntities = await _queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);
        var mappedData = _mapper.Map<IEnumerable<SpecialtyDto>>(pagedEntities.Data);

        return new PagedResult<SpecialtyDto>
        {
            Data = mappedData,
            TotalRecords = pagedEntities.TotalRecords,
            PageNumber = pagedEntities.PageNumber,
            PageSize = pagedEntities.PageSize,
            TotalPages = pagedEntities.TotalPages,
            HasNextPage = pagedEntities.HasNextPage,
            HasPreviousPage = pagedEntities.HasPreviousPage
        };
    }
}