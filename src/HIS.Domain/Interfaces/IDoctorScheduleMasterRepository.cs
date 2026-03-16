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
        public  DoctorScheduleDetail UpdateScheduleDetails(DoctorScheduleDetail Details);
        


    }
}
