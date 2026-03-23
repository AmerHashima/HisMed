using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.DTOs.Emr_Icd110
{
    public class EmrResponseDto
    {
        public Guid Oid { get; set; }
        public int Level { get; set; }
        
        public string CodeId { get; set; } = default!;
        
        public int Dagger { get; set; }
       
        public int Asterisk { get; set; }
        
        public int Valid { get; set; }
        
        public int AustCode { get; set; }
       
        public string AsciidDesc { get; set; } = default!;
       
        public string AsciiShortDesc { get; set; } = default!;
       
        public DateOnly Effectivefrom { get; set; }
        public DateOnly? Inactive { get; set; }
        public DateOnly? reactivated { get; set; }
        
        public int Sex { get; set; }
        public int Stype { get; set; }
        public int AgeL { get; set; }
        public int AgeH { get; set; }
        public int Rdiag { get; set; }
        public int MorphCode { get; set; }
        public DateOnly? ConceptChange { get; set; }
        public int UnacceptPdx { get; set; }
        public int Atype { get; set; }
        public Guid DiagnosisId { get; set; }
    }
}
