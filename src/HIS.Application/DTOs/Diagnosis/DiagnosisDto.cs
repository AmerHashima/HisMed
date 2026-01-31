namespace HIS.Application.DTOs.Diagnosis;

public class DiagnosisDto
{
    public Guid Oid { get; set; }
    public Guid EncounterId { get; set; }
    public string? DiagnosisCode { get; set; }
    public string DiagnosisName { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public DateTime? CreatedAt { get; set; }
}