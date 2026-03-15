using HIS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace HIS.Domain.Entities;

[Table("DoctorSchedules")]
public class DoctorScheduleMaster : BaseEntity
{
    [Required]
    public Guid DoctorId { get; set; }  
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    [Required]

    public Guid StatusId { get; set; }
    [ForeignKey(nameof(StatusId))]
    public virtual AppLookupDetail Status { get; set; }
    public Guid BranchId { get; set; }
    [ForeignKey(nameof(BranchId))]
    public virtual HospitalBranch Branch { get; set; }
    public Guid SpecialtyId { get; set; }
    [ForeignKey(nameof(SpecialtyId))]
    public virtual Specialty Specialty { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsPriority { get; set; } =false;
    public DateOnly StartDate { get; set; } 
    public DateOnly EndDate { get; set; }
    //NAVIGTIONAL PROPERTY
    public ICollection<DoctorScheduleDetail> Details { get; set; } = new List<DoctorScheduleDetail>();
}