using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.Diagnosis;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.Diagnosis
{
    public sealed class GetDiagnosisDataHandler : IRequestHandler<GetDiagnosisDataQuery, PagedResult<DiagnosisDto>>
    {
        private readonly IMapper mapper;
        private readonly IDiagonsisRepository repository;
        private readonly IQueryBuilderService queryBuilder;

        public GetDiagnosisDataHandler(IMapper mapper,IDiagonsisRepository repository,IQueryBuilderService queryBuilder )
        {
            this.mapper = mapper;
            this.repository = repository;
            this.queryBuilder = queryBuilder;
        }
        public async Task<PagedResult<DiagnosisDto>> Handle(GetDiagnosisDataQuery request, CancellationToken cancellationToken)
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
            var mappedData = mapper.Map<IEnumerable<DiagnosisDto>>(pagedEntities.Data);
            return new PagedResult<DiagnosisDto>()
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
                      { "availableFilters", new List<string> { "EncounterId" } },
                      { "availableSortFields", new List<string> { "CreatedAt" } }
                }



            };
        }
    }
}
