using HIS.Api.Models;
using HIS.Application.Commands.Diagnosis;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.Common;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.Queries.Diagnosis;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosisController : BaseApiController
    {
        private readonly IMediator mediator;

        public DiagnosisController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("qurey")]
        public async Task<ActionResult<ApiResponse<PagedResult<DiagnosisDto>>>> GetDiagnosisData(QueryRequest request)
        {
            try
            {
                var diagnosis = await mediator.Send(new GetDiagnosisDataQuery(request));
                return SuccessResponse(diagnosis, "Diagnosis data retrieved successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<PagedResult<DiagnosisDto>>($"Error retrieving doctorScheduel data: {ex.Message}", 500);
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse<DiagnosisDto>>> GetDiagnosisById(Guid Id)
        {
            var diagnosis = await mediator.Send(new GetDiagnosisByIdQuery(Id));
            if (diagnosis == null)
                return ErrorResponse<DiagnosisDto>("DoctorSchedule NotFound", 404);
            return SuccessResponse(diagnosis, "Diagnosis data retrieved successfully");
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DiagnosisDto>>>> GetDiagnosisList(
            [FromQuery] Guid? EncounterId)
        {
            try
            {
                var diagnosis = await mediator.Send(new GetDiagnosisListQuery(EncounterId));
                return SuccessResponse(diagnosis, "Diagnosis Data retrived Sucessfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<DiagnosisDto>>(ex.Message, 500);

            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DiagnosisDto>>> CreateDiagnosis([FromBody] CreateDiagnosisDto request)
        {
            try
            {
                var diagnosis = await mediator.Send(new CreateDiagnosisCommand(request));
                return SuccessResponse(diagnosis, "Diagnosis Created Successfully");

            }
            catch (Exception ex)
            {
                return ErrorResponse<DiagnosisDto>(ex.Message, 500);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<DiagnosisDto>>> UpdateDiagnosis([FromBody] UpdatedDiagnsisDto request)
        {
            try
            {
                var diagnosis = await mediator.Send(new UpdateDiagnosisCommand(request));
                return SuccessResponse(diagnosis, "Diagnosis Updated sucessfully");

            }
            catch (Exception ex)
            {
                return ErrorResponse<DiagnosisDto>(ex.Message, 500);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteDiagnosis(Guid Id)
        {
            try
            {
                var diagnosis = await mediator.Send(new DeleteDiagnosisCommand(Id));

                if (!diagnosis)
                    return ErrorResponse("Diagnosis not found", 404);

                return SuccessResponse("Diagnosis deleted successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse($"Error deleting diagnosis: {ex.Message}", 500);

            }
        }
    }
}
