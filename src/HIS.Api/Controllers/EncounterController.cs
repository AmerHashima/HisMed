using HIS.Api.Models;
using HIS.Application.Commands.Diagnosis;
using HIS.Application.Commands.Encounter;
using HIS.Application.Commands.Prescription;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.DTOs.Encounter;
using HIS.Application.DTOs.Prescription;
using HIS.Application.Queries.Encounter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIS.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class EncounterController : BaseApiController
{
    private readonly IMediator _mediator;

    public EncounterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get encounters with optional filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<EncounterDto>>>> GetEncounters(
        [FromQuery] Guid? patientId = null,
        [FromQuery] Guid? doctorId = null)
    {
        var query = new GetEncounterListQuery(patientId, doctorId);
        var encounters = await _mediator.Send(query);
        return SuccessResponse(encounters, "Encounters retrieved successfully");
    }

    /// <summary>
    /// Get encounter by ID with details
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EncounterDto>>> GetEncounter(Guid id)
    {
        var query = new GetEncounterByIdQuery(id);
        var encounter = await _mediator.Send(query);

        if (encounter == null)
            return ErrorResponse<EncounterDto>("Encounter not found", 404);

        return SuccessResponse(encounter, "Encounter retrieved successfully");
    }

    /// <summary>
    /// Create a new encounter
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<EncounterDto>>> CreateEncounter([FromBody] CreateEncounterDto createEncounterDto)
    {
        try
        {
            var command = new CreateEncounterCommand(createEncounterDto);
            var encounter = await _mediator.Send(command);
            return CreatedResponse(encounter, nameof(GetEncounter), new { id = encounter.Oid }, "Encounter created successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<EncounterDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Add diagnosis to encounter
    /// </summary>
    [HttpPost("{id}/diagnosis")]
    public async Task<ActionResult<ApiResponse<DiagnosisDto>>> AddDiagnosis(Guid id, [FromBody] CreateDiagnosisDto createDiagnosisDto)
    {
        try
        {
            createDiagnosisDto.EncounterId = id;
            var command = new CreateDiagnosisCommand(createDiagnosisDto);
            var diagnosis = await _mediator.Send(command);
            return CreatedResponse(diagnosis, nameof(GetEncounter), new { id }, "Diagnosis added successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<DiagnosisDto>(ex.Message, 400);
        }
    }

    /// <summary>
    /// Add prescription to encounter
    /// </summary>
    [HttpPost("{id}/prescription")]
    public async Task<ActionResult<ApiResponse<PrescriptionDto>>> AddPrescription(Guid id, [FromBody] CreatePrescriptionDto createPrescriptionDto)
    {
        try
        {
            createPrescriptionDto.EncounterId = id;
            var command = new CreatePrescriptionCommand(createPrescriptionDto);
            var prescription = await _mediator.Send(command);
            return CreatedResponse(prescription, nameof(GetEncounter), new { id }, "Prescription added successfully");
        }
        catch (InvalidOperationException ex)
        {
            return ErrorResponse<PrescriptionDto>(ex.Message, 400);
        }
    }
}