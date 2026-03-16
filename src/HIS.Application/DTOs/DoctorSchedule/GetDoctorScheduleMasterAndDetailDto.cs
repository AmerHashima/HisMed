namespace HIS.Application.DTOs.DoctorSchedule
{
    public class GetDoctorScheduleMasterAndDetailDto
    {
        public Guid Oid { get; set; }
        public Guid DoctorId { get; set; }
        public Guid StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public Guid SpecialtyId { get; set; }
        public string Specialty { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPriority { get; set; }
        public List<DoctorScheduleDetailDto> Details { get; set; } = new();
    }
}
