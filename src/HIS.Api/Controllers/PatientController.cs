using HIS.Api.Models;
using HIS.Application.Commands.Patient;
using HIS.Application.DTOs.Patient;
using HIS.Application.DTOs.Common;
using HIS.Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class PatientController : BaseApiController
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get patient data with advanced filtering, sorting, and pagination
    /// </summary>
    /// <param name="request">Query request with filters, sorting, and pagination</param>
    /// <returns>Paginated patient data</returns>
    [HttpPost("query")]
    public async Task<ActionResult<ApiResponse<PagedResult<PatientDto>>>> GetPatientData([FromBody] QueryRequest request)
    {
        try
        {
            var query = new GetPatientDataQuery(request);
            var result = await _mediator.Send(query);
            return SuccessResponse(result, "Patient data retrieved successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse<PagedResult<PatientDto>>($"Error retrieving patient data: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Get all patients with optional filtering
    /// </summary>
    /// <param name="searchTerm">Search term for patient name, MRN, identity number, or mobile</param>
    /// <param name="includeInactive">Include inactive patients</param>
    /// <param name="genderLookupId">Filter by gender lookup ID</param>
    /// <param name="bloodGroupLookupId">Filter by blood group lookup ID</param>
    /// <param name="nationalityLookupId">Filter by nationality lookup ID</param>
    /// <param name="branchId">Filter by branch ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>List of patients</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientDto>>>> GetPatients(
        [FromQuery] string? searchTerm = null,
        [FromQuery] bool includeInactive = false,
        [FromQuery] Guid? genderLookupId = null,
        [FromQuery] Guid? bloodGroupLookupId = null,
        [FromQuery] Guid? nationalityLookupId = null,
        [FromQuery] Guid? branchId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var query = new GetPatientListQuery(
            searchTerm,
            includeInactive,
            genderLookupId,
            bloodGroupLookupId,
            nationalityLookupId,
            branchId,
            page,
            pageSize);

        var patients = await _mediator.Send(query);
        return SuccessResponse(patients, "Patients retrieved successfully");
    }

    /// <summary>
    /// Get patient by ID
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <returns>Patient details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatient(Guid id)
    {
        var query = new GetPatientByIdQuery(id);
        var patient = await _mediator.Send(query);

        if (patient == null)
            return ErrorResponse<PatientDto>("Patient not found", 404);

        return SuccessResponse(patient, "Patient retrieved successfully");
    }

    /// <summary>
    /// Get patient by MRN (Medical Record Number)
    /// </summary>
    /// <param name="mrn">Medical Record Number</param>
    /// <returns>Patient details</returns>
    [HttpGet("by-mrn/{mrn}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatientByMRN(string mrn)
    {
        var query = new GetPatientByMRNQuery(mrn);
        var patient = await _mediator.Send(query);

        if (patient == null)
            return ErrorResponse<PatientDto>("Patient not found", 404);

        return SuccessResponse(patient, "Patient retrieved successfully");
    }

    /// <summary>
    /// Get patient by identity number
    /// </summary>
    /// <param name="identityNumber">Identity number (National ID, Passport, or Iqama)</param>
    /// <returns>Patient details</returns>
    //[HttpGet("by-identity/{identityNumber}")]
    //public async Task<ActionResult<ApiResponse<PatientDto>>> GetPatientByIdentityNumber(string identityNumber)
    //{
    //    var query = new GetPatientByIdentityNumberQuery(identityNumber);
    //    var patient = await _mediator.Send(query);

    //    if (patient == null)
    //        return ErrorResponse<PatientDto>("Patient not found", 404);

    //    return SuccessResponse(patient, "Patient retrieved successfully");
    //}

    /// <summary>
    /// Create a new patient
    /// </summary>
    /// <param name="createPatientDto">Patient creation data</param>
    /// <returns>Created patient</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<PatientDto>>> CreatePatient([FromBody] CreatePatientDto createPatientDto)
    {
        try
        {
            var command = new CreatePatientCommand(createPatientDto);
            var patient = await _mediator.Send(command);
            return CreatedResponse(patient, nameof(GetPatient), new { id = patient.Oid }, "Patient created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Update an existing patient
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <param name="updatePatientDto">Patient update data</param>
    /// <returns>Updated patient</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> UpdatePatient(Guid id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        try
        {
            if (id != updatePatientDto.Oid)
                return ErrorResponse<PatientDto>("Patient ID mismatch", 400);

            var command = new UpdatePatientCommand(updatePatientDto);
            var patient = await _mediator.Send(command);
            return SuccessResponse(patient, "Patient updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return ErrorResponse<PatientDto>("Patient not found", 404);
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a patient (soft delete)
    /// </summary>
    /// <param name="id">Patient ID</param>
    /// <returns>Success response</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeletePatient(Guid id)
    {
        try
        {
            var command = new DeletePatientCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
                return ErrorResponse("Patient not found", 404);

            return SuccessResponse("Patient deleted successfully");
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Error deleting patient: {ex.Message}", 500);
        }
    }
}