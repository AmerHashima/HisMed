using HIS.Domain.Common;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Interfaces
{
    public interface IDoctorScheduleMasterRepository : IBaseRepository<DoctorScheduleMaster>
    {

    
        public  Task<List<DoctorScheduleMaster>> AddDoctorScheduelList(IEnumerable<DoctorScheduleMaster> doctorSchedules, CancellationToken cancellation = default);
        public Task<List<DoctorScheduleMaster?>> GetSchdeuleByDoctorIdAsync(Guid DoctorId,CancellationToken cancellation=default);
        //public Task<List<DoctorScheduleMaster>> GetSchdeulesByStartTime(TimeOnly? StartTime,CancellationToken cancellation=default);
        public Task<DoctorScheduleDetail> GetDoctorScheduleDetailByMasterId(Guid MasterId, CancellationToken cancellation);
        public Task<List<DoctorScheduleMaster>> GetDoctorSchedule();

        public Task<bool> HasOverLapAsync(DateTime NewStart, DateTime NewEnd, Guid DoctorId, Guid BranchId, Guid SpecialtyId, IEnumerable<DoctorScheduleDetail> details);
        public Task<DoctorScheduleDetail> GetSchedulDetailsById(Guid Id,CancellationToken cancellation);
        public Task DeleteScheduleDetailsById(Guid Id, CancellationToken cancellationToken);

        Task<DoctorScheduleDetail> AddScheduleDetailAsync(DoctorScheduleDetail detail, CancellationToken cancellationToken);
        Task<DoctorScheduleDetail> UpdateScheduleDetails(DoctorScheduleDetail Details, CancellationToken cancellationToken= default);





    }
}
