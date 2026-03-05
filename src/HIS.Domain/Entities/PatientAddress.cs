using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("PatientAddress")]
public class PatientAddress : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    public Guid? CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public virtual AppLookupDetail? Country { get; set; }

    public Guid? CityId { get; set; }
    [ForeignKey(nameof(CityId))]
    public virtual AppLookupDetail? City { get; set; }

    [MaxLength(200)]
    public string? District { get; set; }

    [MaxLength(200)]
    public string? Street { get; set; }

    [MaxLength(50)]
    public string? BuildingNumber { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    [MaxLength(20)]
    public string? AdditionalNumber { get; set; }
}
