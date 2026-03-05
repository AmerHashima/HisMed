using HIS.Application.DTOs.Patient;
using MediatR;

namespace HIS.Application.Queries.Patient;

public record GetPatientAttachmentsQuery(Guid PatientId) : IRequest<IEnumerable<PatientAttachmentDto>>;
