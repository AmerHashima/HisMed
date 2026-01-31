namespace HIS.Application.DTOs.Appointment;

public class AppointmentDto
{
    public Guid Oid { get; set; }
    
    public Guid PatientId { get; set; }
    public string? PatientName { get; set; }
    public string? PatientMRN { get; set; }
    
    public Guid DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? SpecialtyName { get; set; }
    
    public DateTime AppointmentDate { get; set; }
    public string? AppointmentType { get; set; }
    public string? Status { get; set; }
    public string? Reason { get; set; }
    
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}