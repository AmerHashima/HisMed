using HIS.Domain.Common;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Interfaces
{
    public interface IDoctorscheduelExceptionRepository:IBaseRepository<DoctorScheduleException>
    {
        
        public Task<IEnumerable<DoctorScheduleException?>> GetSchdeulesExceptionByDoctorIdAsync(Guid DoctorId,CancellationToken cancellation=default);
        public Task<IEnumerable<DoctorScheduleException?>> GetScheduleExceptionByStartTimeAsync(TimeOnly StartTime, CancellationToken cancellation = default);
        public Task<IEnumerable<DoctorScheduleException?>> GetSchdeuleExceptionByExceptionDateAsync(DateOnly ExceptionDate, CancellationToken cancellation = default);
    }
}
