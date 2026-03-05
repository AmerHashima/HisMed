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
    /// Create a new patient with all related data (addresses, contacts, attachments, insurances) in a single request
    /// </summary>
    /// <param name="dto">Full patient creation data including sub-entities</param>
    /// <returns>Created patient with all related data</returns>
    [HttpPost("full")]
    public async Task<ActionResult<ApiResponse<FullPatientDto>>> CreateFullPatient([FromBody] CreateFullPatientDto dto)
    {
        try
        {
            var command = new CreateFullPatientCommand(dto);
            var patient = await _mediator.Send(command);
            return CreatedResponse(patient, nameof(GetPatient), new { id = patient.Oid }, "Patient with all related data created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<FullPatientDto>(ex.Message, 400);
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

    #region Patient Addresses

    /// <summary>
    /// Get all addresses for a patient
    /// </summary>
    [HttpGet("{patientId}/addresses")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientAddressDto>>>> GetPatientAddresses(Guid patientId)
    {
        var query = new GetPatientAddressesQuery(patientId);
        var addresses = await _mediator.Send(query);
        return SuccessResponse(addresses, "Patient addresses retrieved successfully");
    }

    /// <summary>
    /// Add an address to a patient
    /// </summary>
    [HttpPost("{patientId}/addresses")]
    public async Task<ActionResult<ApiResponse<PatientAddressDto>>> CreatePatientAddress(Guid patientId, [FromBody] CreatePatientAddressDto dto)
    {
        try
        {
            dto.PatientId = patientId;
            var command = new CreatePatientAddressCommand(dto);
            var address = await _mediator.Send(command);
            return SuccessResponse(address, "Patient address created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientAddressDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a patient address (soft delete)
    /// </summary>
    [HttpDelete("addresses/{id}")]
    public async Task<ActionResult<ApiResponse>> DeletePatientAddress(Guid id)
    {
        var command = new DeletePatientAddressCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Patient address not found", 404);

        return SuccessResponse("Patient address deleted successfully");
    }

    #endregion

    #region Patient Contacts

    /// <summary>
    /// Get all contacts for a patient
    /// </summary>
    [HttpGet("{patientId}/contacts")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientContactDto>>>> GetPatientContacts(Guid patientId)
    {
        var query = new GetPatientContactsQuery(patientId);
        var contacts = await _mediator.Send(query);
        return SuccessResponse(contacts, "Patient contacts retrieved successfully");
    }

    /// <summary>
    /// Add a contact to a patient
    /// </summary>
    [HttpPost("{patientId}/contacts")]
    public async Task<ActionResult<ApiResponse<PatientContactDto>>> CreatePatientContact(Guid patientId, [FromBody] CreatePatientContactDto dto)
    {
        try
        {
            dto.PatientId = patientId;
            var command = new CreatePatientContactCommand(dto);
            var contact = await _mediator.Send(command);
            return SuccessResponse(contact, "Patient contact created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientContactDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a patient contact (soft delete)
    /// </summary>
    [HttpDelete("contacts/{id}")]
    public async Task<ActionResult<ApiResponse>> DeletePatientContact(Guid id)
    {
        var command = new DeletePatientContactCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Patient contact not found", 404);

        return SuccessResponse("Patient contact deleted successfully");
    }

    #endregion

    #region Patient Attachments

    /// <summary>
    /// Get all attachments for a patient
    /// </summary>
    [HttpGet("{patientId}/attachments")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientAttachmentDto>>>> GetPatientAttachments(Guid patientId)
    {
        var query = new GetPatientAttachmentsQuery(patientId);
        var attachments = await _mediator.Send(query);
        return SuccessResponse(attachments, "Patient attachments retrieved successfully");
    }

    /// <summary>
    /// Add an attachment to a patient
    /// </summary>
    [HttpPost("{patientId}/attachments")]
    public async Task<ActionResult<ApiResponse<PatientAttachmentDto>>> CreatePatientAttachment(Guid patientId, [FromBody] CreatePatientAttachmentDto dto)
    {
        try
        {
            dto.PatientId = patientId;
            var command = new CreatePatientAttachmentCommand(dto);
            var attachment = await _mediator.Send(command);
            return SuccessResponse(attachment, "Patient attachment created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientAttachmentDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a patient attachment (soft delete)
    /// </summary>
    [HttpDelete("attachments/{id}")]
    public async Task<ActionResult<ApiResponse>> DeletePatientAttachment(Guid id)
    {
        var command = new DeletePatientAttachmentCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Patient attachment not found", 404);

        return SuccessResponse("Patient attachment deleted successfully");
    }

    #endregion

    #region Patient Insurances

    /// <summary>
    /// Get all insurances for a patient
    /// </summary>
    [HttpGet("{patientId}/insurances")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientInsuranceDto>>>> GetPatientInsurances(Guid patientId)
    {
        var query = new GetPatientInsurancesQuery(patientId);
        var insurances = await _mediator.Send(query);
        return SuccessResponse(insurances, "Patient insurances retrieved successfully");
    }

    /// <summary>
    /// Add an insurance to a patient
    /// </summary>
    [HttpPost("{patientId}/insurances")]
    public async Task<ActionResult<ApiResponse<PatientInsuranceDto>>> CreatePatientInsurance(Guid patientId, [FromBody] CreatePatientInsuranceDto dto)
    {
        try
        {
            dto.PatientId = patientId;
            var command = new CreatePatientInsuranceCommand(dto);
            var insurance = await _mediator.Send(command);
            return SuccessResponse(insurance, "Patient insurance created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PatientInsuranceDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Delete a patient insurance (soft delete)
    /// </summary>
    [HttpDelete("insurances/{id}")]
    public async Task<ActionResult<ApiResponse>> DeletePatientInsurance(Guid id)
    {
        var command = new DeletePatientInsuranceCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return ErrorResponse("Patient insurance not found", 404);

        return SuccessResponse("Patient insurance deleted successfully");
    }

    #endregion
}