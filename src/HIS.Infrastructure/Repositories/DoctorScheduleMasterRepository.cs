using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HIS.Infrastructure.Repositories
{
    public class DoctorScheduleMasterRepository: BaseRepository<DoctorScheduleMaster>,IDoctorScheduleMasterRepository
    {
        private readonly HISDbContext context;

        public DoctorScheduleMasterRepository(HISDbContext context):base(context)
        {
            this.context = context;
        }

        public  async Task<List<DoctorScheduleMaster>>AddDoctorScheduelList(IEnumerable<DoctorScheduleMaster> doctorSchedules,CancellationToken cancellation)
        {

            await context.DoctorSchedulesMaster.AddRangeAsync(doctorSchedules,cancellation);
            await context.SaveChangesAsync(cancellation);
            return doctorSchedules.ToList();

        }

      
        public async override Task<IEnumerable<DoctorScheduleMaster>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.DoctorSchedulesMaster.Where(x => !x.IsDeleted)
                .Include(x => x.Branch)
                .Include(x => x.Status)
                .Include(x => x.Details.Where(d => !d.IsDeleted))
                .ThenInclude(x => x.DayOfweek)
                .Include(x => x.Specialty)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DoctorScheduleMaster>> GetSchdeuleByDoctorIdAsync(Guid DoctorId, CancellationToken cancellation = default)
        {
            return await context.DoctorSchedulesMaster
                .Where(x => x.DoctorId == DoctorId&&  !x.IsDeleted)
                .Include(x => x.Branch)
                .Include(x => x.Status)
                .Include(x => x.Details.Where(d => !d.IsDeleted))
                    .ThenInclude(d => d.DayOfweek)
                .Include(x => x.Specialty)
                .ToListAsync(cancellation);
        }

        //public async Task<List<DoctorScheduleMaster>> GetSchdeulesByStartTime(TimeOnly? StartTime, CancellationToken cancellation = default)
        //{
        //    return await context.DoctorSchedules.Where(x => x.StartTime == StartTime).ToListAsync(); 
        //}


        public async override Task<DoctorScheduleMaster?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {


            return await context.DoctorSchedulesMaster
              .Where(x => !x.IsDeleted)
              .Include(x => x.Branch)
              .Include(x => x.Status)
              .Include(x => x.Details.Where(d => !d.IsDeleted))
              .ThenInclude(x => x.DayOfweek)
              .Include(x => x.Specialty)

              .FirstOrDefaultAsync(x => x.Oid == id, cancellationToken);
        }

        public async Task<DoctorScheduleDetail?> GetDoctorScheduleDetailByMasterId(Guid MasterId, CancellationToken cancellation)
        {
           return await context.DoctorScheduleDetail
                .Where(x => x.MasterId == MasterId && !x.IsDeleted)
                .Include(x => x.DayOfweek)
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<List<DoctorScheduleMaster>> GetDoctorSchedule()
        {
            return await context.DoctorSchedulesMaster.Where(x => !x.IsDeleted)
                .Include(x => x.Branch)
              .Include(x => x.Status)
              .Include(x => x.Doctor)
                .ThenInclude(x=>x.User)
              .Include(x => x.Specialty)
.ToListAsync();


        }

        //public  DoctorScheduleDetail UpdateScheduleDetails(DoctorScheduleDetail Details)
        //{
        //     return context.DoctorScheduleDetail.Update(Details).Entity;
            
        //}
        public async Task<DoctorScheduleDetail> UpdateScheduleDetails(DoctorScheduleDetail Details, CancellationToken cancellationToken=default)
        {
            context.DoctorScheduleDetail.Update(Details);
            await context.SaveChangesAsync(cancellationToken);
            return Details;
        }

        public async Task<DoctorScheduleDetail?> GetSchedulDetailsById(Guid id, CancellationToken cancellation)
        {
            return await context.DoctorScheduleDetail.Where(x => !x.IsDeleted)
                .Include(x => x.DayOfweek)
                .FirstOrDefaultAsync(x => x.Oid == id, cancellation);
        }

        public async Task DeleteScheduleDetailsById(Guid Id, CancellationToken cancellationToken)
        {
            var entity = await GetSchedulDetailsById(Id, cancellationToken);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                // UpdateScheduleDetails persists the change
                await UpdateScheduleDetails(entity, cancellationToken);
            }
        }

        public async  Task<DoctorScheduleDetail> AddScheduleDetailAsync(DoctorScheduleDetail detail, CancellationToken cancellationToken)
        {
            await context.DoctorScheduleDetail.AddAsync(detail, cancellationToken);
           await context.SaveChangesAsync(cancellationToken);
            return detail;
        }

        //public async Task<bool> HasOverLapAsync(DateTime NewStart, DateTime NewEnd, Guid DoctorId, Guid BranchId, Guid SpecialtyId,IEnumerable<DoctorScheduleDetail> details)
        //{
        //    foreach (var detail in details)
        //    {
        //        var HasConflict = await context.DoctorSchedulesMaster.AnyAsync(S =>

        //            NewEnd > S.StartDate.ToDateTime(detail.StartTime) &&
        //           NewStart < S.EndDate.ToDateTime(detail.EndTime) &&
        //           S.DoctorId == DoctorId && S.SpecialtyId == SpecialtyId &&
        //           S.BranchId == BranchId

        //        );
        //         if(HasConflict)
        //            return true;

        //    }
        //    return false;

        //}
        //public async Task<bool> HasOverLapAsync(DateTime NewStart, DateTime NewEnd, Guid DoctorId, Guid BranchId, Guid SpecialtyId)
        //{
        //    var Schedules = await context.DoctorSchedulesMaster.Include(x => x.Details)
        //       .Where(s => s.DoctorId == DoctorId && s.BranchId == BranchId && s.SpecialtyId == SpecialtyId).ToListAsync();
        //      var data = Schedules.SelectMany(s=> s.Details) 
        //    var result = Schedules.Any(S =>
        //         NewEnd > S.StartDate.ToDateTime(S.Details.First().StartTime) &&
        //           NewStart < S.EndDate.ToDateTime(S.Details.First().EndTime) &&
        //           S.DoctorId == DoctorId && S.SpecialtyId == SpecialtyId &&
        //           S.BranchId == BranchId
        //    );
        //    if (result)
        //        return true;
        //    return false;

        //}


    }
    }
