using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Application.Queries.EmrIcd110;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;

namespace HIS.Application.Handlers.EmrIcd110
{
    public sealed class GetEmrIcd110DataHandler : IRequestHandler<GetEmrIcd110DataQuery, PagedResult<EmrResponseDto>>
    {
        private readonly IQueryBuilderService queryBuilder;
        private readonly IMapper mapper;
        private readonly IEmrRepository repository;

        public GetEmrIcd110DataHandler(IQueryBuilderService queryBuilder, IMapper mapper, IEmrRepository repository)
        {
            this.queryBuilder = queryBuilder;
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<PagedResult<EmrResponseDto>> Handle(GetEmrIcd110DataQuery request, CancellationToken cancellationToken)
        {
            // Start with base query - all non-deleted doctors with includes
            var query = repository.GetQueryable().Where(x => !x.IsDeleted);


            // Apply filters
            query = queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);

            // Apply sorting
            query = queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);

            // Apply pagination and get results
            var pagedEntities = await queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);

            // Map to DTOs
            var mappedData = mapper.Map<IEnumerable<EmrResponseDto>>(pagedEntities.Data);
            return new PagedResult<EmrResponseDto>()
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
                    { "availableFilters", new List<string> { "Level" } },
                    { "availableSortFields", new List<string> { "CodeId", "AustCode" } }
                }
            };
        }
    }
}
