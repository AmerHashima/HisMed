namespace HIS.Application.DTOs.Doctor;

public class DoctorDto
{
    public Guid Oid { get; set; }
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? DoctorFullName { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    
    public Guid SpecialtyId { get; set; }
    public string? SpecialtyNameEn { get; set; }
    public string? SpecialtyNameAr { get; set; }
    
    public Guid DepartmentLookupId { get; set; }
    public string? DepartmentName { get; set; }
    
    public Guid BranchId { get; set; }
    public string? BranchName { get; set; }
    
    public string? NphiesProviderId { get; set; }
    public bool IsNphiesEnabled { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}