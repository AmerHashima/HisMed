using HIS.Domain.Common;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Interfaces
{
    public interface IDoctorScheduleRepository : IBaseRepository<DoctorSchedule>
    {

        public Task<DoctorSchedule?> GetScheduelByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        public  Task<List<DoctorSchedule>> AddDoctorScheduelList(IEnumerable<DoctorSchedule> doctorSchedules, CancellationToken cancellation = default);



    }
}
