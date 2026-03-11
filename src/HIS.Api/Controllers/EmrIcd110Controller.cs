using AutoMapper;
using HIS.Api.Models;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.Commands.EmrIcd110;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Application.Queries.DoctorSchedule;
using HIS.Application.Queries.EmrIcd110;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmrIcd110Controller : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public EmrIcd110Controller(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmrResponseDto>>> CreateEmr([FromBody] CreateEmr_icd110Dto request)
        {
            try
            {
                var emr = await mediator.Send(new CreateEmrIcd110Command(request));

                
                return SuccessResponse(emr, "Emr Created successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<EmrResponseDto>(ex.Message, 500);
            }

        }
      
        [HttpPut]
        public async Task<ActionResult<ApiResponse<EmrResponseDto>>> UpdateEmr([FromBody] UpdateEmrIcd110Dto request)
        {
            try
            {
                var emr = await mediator.Send(new UpdateEmrIcd110Command(request));

                return SuccessResponse(emr, "emr Updated successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<EmrResponseDto>(ex.Message, 500);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteEmr(Guid Id)
        {
            try
            {
                var emr = await mediator.Send(new DeleteEmrIcd110Command(Id));

                if (!emr)
                    return ErrorResponse("Emr not found", 404);

                return SuccessResponse("Emr deleted successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Error deleting Emr: {ex.Message}", 500);
            }

        }
        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResult<EmrResponseDto>>>> GetEmrData([FromQuery] QueryRequest request)
        {
            try
            {
                var emr = await mediator.Send(new GetEmrIcd110DataQuery(request));
                return SuccessResponse(emr, "emr data retrieved successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<PagedResult<EmrResponseDto>>($"Error retrieving emr data: {ex.Message}", 500);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse<EmrResponseDto>>> GetEmrById(Guid Id)
        {
            var Emr = await mediator.Send(new GetEmrIcd110ByIdQuery(Id));
            if (Emr == null)
                return ErrorResponse<EmrResponseDto>("emr  NotFound", 404);
            return SuccessResponse(Emr, "emr data retrieved successfully");

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmrResponseDto>>>> GetDoctorSchdeules(
            [FromQuery] int? Level,
            [FromQuery] string? CodeId,
               [FromQuery] int? AustCode,
               [FromQuery] int? Sex)   
        {
            try
            {
                var query = await mediator.Send(new GetEmrIcd110ListQuery(Level,CodeId,AustCode,Sex));
                return SuccessResponse(query, "emr retrieved successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<EmrResponseDto>>(ex.Message, 500);
            }
        }
    }
}
