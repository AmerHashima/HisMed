using HIS.Api.Models;
using HIS.Application.Commands.DoctorScheduelException;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Application.Queries.DoctorSceduelException;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorScheduleExceptionController : BaseApiController
    {
        private readonly IMediator mediator;

        public DoctorScheduleExceptionController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DoctorScheduleExceptionResponseDto>>> CreateDoctorScheduleException([FromBody] CreateDoctorScheduleExceptionDto request)
        {
            try
            {
                var result = await mediator.Send(new CreateDoctorScheduelExceptionCommand(request));
                return CreatedResponse(result, nameof(GetDoctorScheduelExceptionById), new { id = result.Id }, "DoctorScheduelException created successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<DoctorScheduleExceptionResponseDto>(ex.Message,500);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<DoctorScheduleExceptionResponseDto>>> UpdateDoctorSchedule([FromBody] UpdateDoctorScheduleExceptionDto request)
        {
            try 
            {
               
                var command = await mediator.Send(new UpdateDoctorScheduleCommand(request));
                return SuccessResponse(command,"DoctorScheduelException is updated sucessfully!");
            }
            catch (Exception ex)
            {
                return ErrorResponse<DoctorScheduleExceptionResponseDto>(ex.Message,500);
            }
            
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteDoctorScheduleException(Guid Id)
        {
            try
            {
                var ScheduleException = await mediator.Send(new DeleteDoctorScheduleExceptionCommand(Id));
                if (ScheduleException)
                    return SuccessResponse("DoctorScheduleException deleted successfully");
                return ErrorResponse("DoctorScheduleException not found", 404);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message,500);
            }

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<DoctorScheduleExceptionResponseDto>>>> GetDoctorScheduelExceptions([FromBody] QueryRequest request)
        {
            try
            {
                var scheduelException = await mediator.Send(new GetAllDoctorsScheduleExceptionQuery(request));
                return SuccessResponse(scheduelException, "Doctor data retrieved successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse < PagedResult<DoctorScheduleExceptionResponseDto>>($"Error retrieving doctorScheduel data: {ex.Message}", 500);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse<DoctorScheduleExceptionResponseDto>>> GetDoctorScheduelExceptionById(Guid Id)
        {
            var ScheduelException = await mediator.Send(new GetDoctorScheduelExceptionByIdQuery(Id));
            if (ScheduelException == null)
                return ErrorResponse<DoctorScheduleExceptionResponseDto>("DoctorSchedule NotFound", 404);
            return SuccessResponse(ScheduelException, "DoctorScheduel data retrieved successfully");
        }

    }
}
