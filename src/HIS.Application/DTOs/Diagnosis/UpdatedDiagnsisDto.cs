using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.Diagnosis
{
    public class UpdatedDiagnsisDto
    {
        [Required(ErrorMessage ="Oid is Required")]
        public Guid Oid { get; set; }
        [Required(ErrorMessage = "Oid is Required")]

        public Guid EncounterId { get; set; }

        public bool IsPrimary { get; set; } = false;
    }
}
