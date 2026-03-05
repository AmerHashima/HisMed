namespace HIS.Application.DTOs.Patient;

public class PatientAddressDto
{
    public Guid Oid { get; set; }
    public Guid PatientId { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? BuildingNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? AdditionalNumber { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
