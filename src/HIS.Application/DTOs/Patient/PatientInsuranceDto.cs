namespace HIS.Application.DTOs.Patient;

public class PatientInsuranceDto
{
    public Guid Oid { get; set; }
    public Guid PatientId { get; set; }
    public Guid? InsuranceCompanyId { get; set; }
    public string? InsuranceCompanyName { get; set; }
    public string? PolicyNumber { get; set; }
    public string? MemberId { get; set; }
    public string? InsuranceClass { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
