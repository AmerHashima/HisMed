using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.Emr_Icd110
{
    public  class UpdateEmrIcd110Dto
    {
        public Guid Oid { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string CodeId { get; set; } = default!;
        [Required]
        public int Dagger { get; set; }
        [Required]
        public int Asterisk { get; set; }
        [Required]
        public int Valid { get; set; }
        [Required]
        public int AustCode { get; set; }
        [Required]
        public string AsciidDesc { get; set; } = default!;
        [Required]
        public string AsciiShortDesc { get; set; } = default!;
        [Required]
        public DateOnly Effectivefrom { get; set; }
        public DateOnly? Inactive { get; set; }
        public DateOnly? reactivated { get; set; }
        [Range(1, 2)]  //  // 1 = Male, 2 = Female 
        public int? Sex { get; set; }
        public int? Stype { get; set; }
        public int? AgeL { get; set; }
        public int? AgeH { get; set; }
        public int Rdiag { get; set; }
        public int MorphCode { get; set; }
        public DateOnly? ConceptChange { get; set; }
        public int UnacceptPdx { get; set; }
        public int? Atype { get; set; }
        public Guid? DiagnosisId { get; set; }
    }
}
