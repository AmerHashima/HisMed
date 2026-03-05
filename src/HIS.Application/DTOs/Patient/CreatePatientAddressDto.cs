using System.ComponentModel.DataAnnotations;

namespace HIS.Application.DTOs.Patient;

public class CreatePatientAddressDto
{
    [Required(ErrorMessage = "Patient ID is required")]
    public Guid PatientId { get; set; }

    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }

    [StringLength(200, ErrorMessage = "District cannot exceed 200 characters")]
    public string? District { get; set; }

    [StringLength(200, ErrorMessage = "Street cannot exceed 200 characters")]
    public string? Street { get; set; }

    [StringLength(50, ErrorMessage = "Building number cannot exceed 50 characters")]
    public string? BuildingNumber { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string? PostalCode { get; set; }

    [StringLength(20, ErrorMessage = "Additional number cannot exceed 20 characters")]
    public string? AdditionalNumber { get; set; }
}
