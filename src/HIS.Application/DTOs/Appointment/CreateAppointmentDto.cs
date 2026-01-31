using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Appointment;

public class CreateAppointmentDto
{
    [Required(ErrorMessage = "Patient is required")]
    public Guid PatientId { get; set; }

    [Required(ErrorMessage = "Doctor is required")]
    public Guid DoctorId { get; set; }

    [Required(ErrorMessage = "Appointment date is required")]
    public DateTime AppointmentDate { get; set; }

    [StringLength(50, ErrorMessage = "Appointment type cannot exceed 50 characters")]
    public string? AppointmentType { get; set; }

    [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
    public string? Status { get; set; } = "Scheduled";

    [StringLength(255, ErrorMessage = "Reason cannot exceed 255 characters")]
    public string? Reason { get; set; }

    public Guid? BranchId { get; set; }
}