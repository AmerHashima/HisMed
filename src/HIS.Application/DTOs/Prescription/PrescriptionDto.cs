namespace HIS.Application.DTOs.Prescription;

public class PrescriptionDto
{
    public Guid Oid { get; set; }
    public Guid EncounterId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public string? Duration { get; set; }
    public string? Instructions { get; set; }
    public DateTime? CreatedAt { get; set; }
}