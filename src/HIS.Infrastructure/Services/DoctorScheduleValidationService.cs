using HIS.Application.Common.Exceptions;
using HIS.Application.Services;
using HIS.Domain.Common;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Infrastructure.Services
{
    public class DoctorScheduleValidationService : IDoctorScheduleValidationService
    {
       
        private readonly HISDbContext _context;
        public DoctorScheduleValidationService(HISDbContext context)
        {
            _context = context;
        }
        public async  Task<bool> HasOverLap(Guid BranchId, Guid SpecialityId, Guid DoctorId,IEnumerable<DoctorScheduleDetail> details, Guid? ExculdingSchedule, CancellationToken cancellation)
        {
            foreach (var detail in details)
            {

                var HasConflict = await _context.DoctorSchedulesMaster
                    .AnyAsync(x =>
                        x.Oid != ExculdingSchedule &&
                        x.DoctorId == DoctorId &&
                        x.SpecialtyId == SpecialityId &&
                        x.BranchId == BranchId &&
                        x.Details.Any(d =>
                            
                                d.StartTime < detail.EndTime &&
                                d.EndTime > detail.StartTime &&
                                d.DayOfWeekId == detail.DayOfWeekId
                        ),
                        
                        cancellation
                    );
                if (HasConflict) return true;
            }
            return false;
        }           
    }
}
