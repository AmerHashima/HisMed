namespace HIS.Application.DTOs.Doctor;

public class DoctorBranchDto
{
    public Guid Oid { get; set; }
    public Guid DoctorId { get; set; }
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }
    public bool IsDefault { get; set; }
    public DateTime? CreatedAt { get; set; }
}
