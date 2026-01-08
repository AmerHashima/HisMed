using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIS.Domain.Entities;
namespace HIS.Domain.Common
{
    

    public abstract class BaseEntity
    {
        [Key]
        public Guid Oid { get; set; } = Guid.NewGuid();

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public virtual SystemUser? CreatedByUser { get; set; } 

        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        public virtual SystemUser? UpdatedByUser { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        [ForeignKey(nameof(DeletedBy))]
        public virtual SystemUser? DeletedByUser { get; set; }
    }
}
