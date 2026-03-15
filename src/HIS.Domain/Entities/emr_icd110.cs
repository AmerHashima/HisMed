using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIS.Domain.Entities
{
    [Table("emr_icd110")]
    public class emr_icd110:BaseEntity
    {
        [Required]
        public int Level { get; set; }
        [Required]
        public string CodeId { get; set; } = default!;
        [Required]
        public int Dagger { get; set; }
        [Required]
        public int Asterisk { get;  set; }
        [Required]
        public int Valid { get; set; }
        [Required]
        public int  AustCode { get; set; }
        [Required]
        public string AsciidDesc { get; set; }= default!;
        [Required]
        public string AsciiShortDesc { get; set; } = default!;
        [Required]
        public DateOnly Effectivefrom { get; set; }
        public DateOnly? Inactive { get; set; }
        public DateOnly? reactivated { get; set; } 
        [Range(1,2)]  //  // 1 = Male, 2 = Female 
        public int? Sex { get; set; }
        public int? Stype { get; set; }
        public  int? AgeL {get; set;}
        public int? AgeH {get; set;}
        public int Rdiag { get; set; }
        public int  MorphCode { get; set; }
        public DateOnly? ConceptChange { get; set; }  
        public int UnacceptPdx { get; set; }
        public int? Atype { get; set; }
        public Guid? DiagnosisId { get; set; }
        // Navigation Properties
        [ForeignKey("DiagnosisId")]
         public Diagnosis  Diagnosis { get; set; }

    }
}
