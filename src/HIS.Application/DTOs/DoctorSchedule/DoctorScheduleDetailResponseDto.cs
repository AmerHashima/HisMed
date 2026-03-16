namespace HIS.Application.DTOs.DoctorSchedule
{
    public class DoctorScheduleDetailResponseDto
    {
        public Guid Oid { get; set; }
        public Guid DayOfWeekId { get; set; }
        public string DayOfWeekNameEn { get; set; }
        public string DayOfWeekNameAr { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public float SlotDurationMinutes { get; set; }
    }
}
