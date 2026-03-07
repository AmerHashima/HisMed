using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Common;

namespace HIS.Domain.Entities;

[Table("DoctorBranch")]
public class DoctorBranch : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    public Guid? BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch? Branch { get; set; }

    public bool IsDefault { get; set; }
}
