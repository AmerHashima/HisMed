namespace HIS.Application.DTOs.Diagnosis;

public class DiagnosisDto
{
    public Guid Oid { get; set; }
    public Guid EncounterId { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime? CreatedAt { get; set; }
}