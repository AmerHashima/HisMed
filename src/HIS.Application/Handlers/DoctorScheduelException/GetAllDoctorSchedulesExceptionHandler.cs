using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Application.Queries.DoctorSceduelException;
using HIS.Application.Services;
using HIS.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Handlers.DoctorScheduelException
{
    public sealed class GetAllDoctorSchedulesExceptionHandler : IRequestHandler<GetAllDoctorsScheduleExceptionQuery, PagedResult<DoctorScheduleExceptionResponseDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorscheduelExceptionRepository repository;
        private readonly IQueryBuilderService queryBuilder;

        public GetAllDoctorSchedulesExceptionHandler(IMapper mapper,IDoctorscheduelExceptionRepository repository, IQueryBuilderService queryBuilder)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.queryBuilder= queryBuilder;
        }
        public async Task<PagedResult<DoctorScheduleExceptionResponseDto>> Handle(GetAllDoctorsScheduleExceptionQuery request, CancellationToken cancellationToken)
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
            var mappedData = mapper.Map<IEnumerable<DoctorScheduleExceptionResponseDto>>(pagedEntities.Data);
            return new PagedResult<DoctorScheduleExceptionResponseDto>()
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
                      { "availableFilters", new List<string> { "DoctorId", "StartTime", "EndTime" , "ExceptionType", "ExceptionDate" } },
                      { "availableSortFields", new List<string> {  "StartTime", "EndTime", "ExceptionType", "ExceptionDate" } }
                }



            };
        }
    }
}
