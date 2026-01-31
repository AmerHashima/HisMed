using HIS.Application.DTOs.Prescription;
using HIS.Application.DTOs.Diagnosis;
namespace HIS.Application.DTOs.Encounter;

public class EncounterDto
{
    public Guid Oid { get; set; }
    
    public Guid? AppointmentId { get; set; }
    
    public Guid PatientId { get; set; }
    public string? PatientName { get; set; }
    public string? PatientMRN { get; set; }
    
    public Guid DoctorId { get; set; }
    public string? DoctorName { get; set; }
    
    public DateTime EncounterDate { get; set; }
    public string? EncounterType { get; set; }
    public string? Notes { get; set; }
    
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }
    
    public List<DiagnosisDto>? Diagnoses { get; set; }
    public List<PrescriptionDto>? Prescriptions { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}