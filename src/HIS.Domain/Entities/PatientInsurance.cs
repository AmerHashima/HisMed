using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("PatientInsurance")]
public class PatientInsurance : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    public Guid? InsuranceCompanyId { get; set; }
    [ForeignKey(nameof(InsuranceCompanyId))]
    public virtual AppLookupDetail? InsuranceCompany { get; set; }

    [MaxLength(100)]
    public string? PolicyNumber { get; set; }

    [MaxLength(100)]
    public string? MemberId { get; set; }

    [MaxLength(50)]
    public string? InsuranceClass { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }
}
