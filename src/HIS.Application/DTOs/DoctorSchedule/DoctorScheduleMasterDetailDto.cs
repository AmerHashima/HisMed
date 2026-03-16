namespace HIS.Application.DTOs.DoctorSchedule
{
    public class DoctorScheduleMasterDetailDto
    {
        public Guid Oid { get; set; }
        public Guid DoctorId { get; set; }
        public string Status { get; set; }
        public string Branch { get; set; }
        public string Specialty { get; set; }
        public bool IsActive { get; set; }
        public bool IsPriority { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<DoctorScheduleDetailResponseDto> Details { get; set; } = [];
    }
}
