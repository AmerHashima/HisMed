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

        public  async Task<List<DoctorScheduleMaster?>>AddDoctorScheduelList(IEnumerable<DoctorScheduleMaster> doctorSchedules,CancellationToken cancellation)
        {
           
            await context.DoctorSchedulesMaster.AddRangeAsync(doctorSchedules,cancellation);
            await context.SaveChangesAsync(cancellation);
            return doctorSchedules.ToList(); //return only the insterted records 
            
        }

      
        public async override Task<IEnumerable<DoctorScheduleMaster>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.DoctorSchedulesMaster.Where(x => !x.IsDeleted)
                .Include(x => x.Branch)
                .Include(x => x.Status)
                .Include(x => x.Details)
                .ThenInclude(x => x.DayOfweek)
                .Include(x => x.Specialty)
                .ToListAsync(cancellationToken);
        }

        public async  Task<List<DoctorScheduleMaster?>> GetSchdeuleByDoctorIdAsync(Guid DoctorId, CancellationToken cancellation = default)
        {
            return await context.DoctorSchedulesMaster
                .Where(x => x.DoctorId == DoctorId)
                 .Include(x => x.Branch)
                .Include(x => x.Status)
                .Include(x => x.Details)
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
              .Include(x => x.Details)
              .ThenInclude(x => x.DayOfweek)
              .Include(x => x.Specialty)
              .FirstOrDefaultAsync(x => x.Oid == id, cancellationToken);
        }

        public async Task<DoctorScheduleDetail> GetDoctorScheduleDetailByMasterId(Guid MasterId,CancellationToken cancellation)
        {
           return await context.DoctorScheduleDetail.Where(x => x.MasterId == MasterId).FirstOrDefaultAsync(cancellation);
        }

        public async Task<List<DoctorScheduleMaster>> GetDoctorSchedule()
        {
            return await context.DoctorSchedulesMaster.Where(x => !x.IsDeleted).ToListAsync();


        }

        public  DoctorScheduleDetail UpdateScheduleDetails(DoctorScheduleDetail Details)
        {
             return context.DoctorScheduleDetail.Update(Details).Entity;
        }
       
    }
    }
