using AutoMapper;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Doctor;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using HIS.Application.Services;
using HIS.Domain.Common;
using HIS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;

namespace HIS.Application.Handlers.DoctorSchedule
{
    public sealed class GetDoctorScheduelDataHandler : IRequestHandler<GetDoctorScheduleDataQuery, PagedResult<DoctorScheduleDto>>
    {
        private readonly IMapper mapper;
        private readonly IDoctorScheduleRepository doctorScheduleRepo;
        private readonly IQueryBuilderService queryBuilder;

        public GetDoctorScheduelDataHandler(IMapper mapper,IDoctorScheduleRepository doctorScheduleRepo,IQueryBuilderService queryBuilder)
        {
            this.mapper = mapper;
            this.doctorScheduleRepo = doctorScheduleRepo;
            this.queryBuilder = queryBuilder;
        }
        public async Task<PagedResult<DoctorScheduleDto>> Handle(GetDoctorScheduleDataQuery request, CancellationToken cancellationToken)
        {
            // Start with base query - all non-deleted doctors with includes
            var query = doctorScheduleRepo.GetQueryable().Where(x => !x.IsDeleted);
           

            // Apply filters
            query = queryBuilder.ApplyFilters(query, request.QueryRequest.Request.Filters);

            // Apply sorting
            query = queryBuilder.ApplySorting(query, request.QueryRequest.Request.Sort);

            // Apply pagination and get results
            var pagedEntities = await queryBuilder.ApplyPaginationAsync(query, request.QueryRequest.Request.Pagination);

            // Map to DTOs
            var mappedData = mapper.Map<IEnumerable<DoctorScheduleDto>>(pagedEntities.Data);
            return new PagedResult<DoctorScheduleDto>()
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
                      { "availableFilters", new List<string> { "DoctorId", "StartTime", "EndTime" } },
                      { "availableSortFields", new List<string> {  "StartTime", "EndTime" } }
                }



            };
        }
    }
}
