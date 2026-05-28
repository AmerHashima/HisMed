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

        public async Task<bool> HasOverLap(Guid BranchId, Guid SpecialityId, Guid DoctorId, DateOnly StartDate, DateOnly EndDate, IEnumerable<DoctorScheduleDetail> Newdetails, Guid? ExculdingSchedule = null, CancellationToken cancellation = default)
  {
            var data = await _context.DoctorSchedulesMaster.Where(master =>     // Load Needed Data To Memory 
            master.DoctorId == DoctorId &&
            master.SpecialtyId == SpecialityId &&
            master.BranchId == BranchId).Include(x => x.Details).ToListAsync(cancellation);
            var HasConflict = data.Any(ExsistingMaster =>
                                ExsistingMaster.Oid != ExculdingSchedule &&
                                ExsistingMaster.StartDate <= EndDate &&     //[To Do]  Fix OverNight 
                                ExsistingMaster.EndDate >= StartDate &&
                                ExsistingMaster.Details.Any(ExsistingDetails =>
                                Newdetails.Any(
                                              NewDetails => NewDetails.StartTime < ExsistingDetails.EndTime &&
                                                   NewDetails.EndTime > ExsistingDetails.StartTime &&
                                                   NewDetails.DayOfWeekId == ExsistingDetails.DayOfWeekId
                                )));


            if (HasConflict) return true;

            return false;



        }
    }
}
