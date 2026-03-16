namespace HIS.Application.DTOs.DoctorSchedule
{
    public class DoctorScheduleDetailDto
    {
        public Guid Oid { get; set; }
        public Guid DayOfWeekId { get; set; }
        public string DayOfWeekNameEn { get; set; } = string.Empty;
        public string DayOfWeekNameAr { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public float SlotDurationMinutes { get; set; }
    }
}
