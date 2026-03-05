namespace HIS.Application.DTOs.Patient;

public class PatientContactDto
{
    public Guid Oid { get; set; }
    public Guid PatientId { get; set; }
    public string? ContactName { get; set; }
    public Guid? RelationshipId { get; set; }
    public string? RelationshipName { get; set; }
    public string? Mobile { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
