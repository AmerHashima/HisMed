using AutoMapper;
using HIS.Api.Models;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Application.Queries.DoctorSchedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorScheduleController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public DoctorScheduleController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDto>>> CreateDoctorSchedule([FromBody] CreateDoctorScheduleDto request)
        {
            try
            {
                var Scheduel = await mediator.Send(new CreateDoctorScheduleCommand(request));

                //return CreatedResponse(Scheduel, nameof(GetDoctorById), new { id = Scheduel. }, "Doctor created successfully");
                return SuccessResponse(Scheduel, "DoctorSchedule Created successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<DoctorScheduleDto>(ex.Message,500);
            }

        }
        [HttpPost("AddDoctorScheduleBulk")]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleDto>>>>CreateDoctorScheduleBulk( [FromBody] CreateDoctorScheduleBulkDto request)
        {
            try        
            {
                var schdeuels = await mediator.Send(new CreateDoctorScheduleBulkCommand( request));
                return SuccessResponse(schdeuels,"Schedules Added successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<List<DoctorScheduleDto>>(ex.Message,500);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDto>>> UpdateDoctorScheduel([FromBody] UpdateDoctorSchdeuelDto request)
        {
            try
            {
                var Scheduel = await mediator.Send(new UpdateDoctorScheduleCommand(request));

                return SuccessResponse(Scheduel,"Doctor Schedule Created successfully");
            }
            catch(Exception ex)
            {
                return ErrorResponse<DoctorScheduleDto>(ex.Message,500);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteDoctorScheduel(Guid Id)
        {
            try
            {
                var Scheduel = await mediator.Send(new DeleteDoctorScheduelCommand(Id));
                
                if (!Scheduel)
                    return ErrorResponse("DoctorSchedule not found", 404);

                return SuccessResponse("DoctorSchedule deleted successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Error deleting doctorSchedule: {ex.Message}", 500);
            }

        }
        [HttpPost("query")]
        public async Task<ActionResult<ApiResponse<PagedResult<DoctorScheduleDto>>>> GetDoctorScheduelData([FromQuery] QueryRequest request)
        {
            try
            {
                var scheduel = await mediator.Send(new GetDoctorScheduleDataQuery(request));
                return SuccessResponse(scheduel, "Doctor data retrieved successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<PagedResult<DoctorScheduleDto>>($"Error retrieving doctorScheduel data: {ex.Message}", 500);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDto>>> GetDoctorById(Guid Id)
        {
            var Scheduel = await mediator.Send(new GetDoctorSchedualByIdQuery(Id));
            if (Scheduel == null)
                return ErrorResponse<DoctorScheduleDto>("DoctorSchedule NotFound", 404);
            return SuccessResponse(Scheduel, "DoctorScheduel data retrieved successfully");

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DoctorScheduleDto>>>> GetDoctorSchdeules(
            [FromQuery] Guid?DoctorId,
            [FromQuery] TimeOnly? StartTime)
        {
            try
            {
                var query = await mediator.Send(new GetDoctorSchdeuleListQuery(DoctorId, StartTime));
                return SuccessResponse(query, "Doctor Schdeduels retrieved successfully");
            }
            catch(Exception ex)
            {
                return ErrorResponse<IEnumerable<DoctorScheduleDto>>(ex.Message,500);
            }
        }
        

    }
}
