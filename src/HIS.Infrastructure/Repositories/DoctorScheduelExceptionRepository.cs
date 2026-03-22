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

namespace HIS.Infrastructure.Repositories
{
    public class DoctorScheduelExceptionRepository:BaseRepository<DoctorScheduleException>,IDoctorscheduelExceptionRepository
    {
        private readonly HISDbContext context;

        public DoctorScheduelExceptionRepository(HISDbContext context) : base(context)
        {
            this.context = context;
        }

       public async  Task<IEnumerable<DoctorScheduleException?>> GetSchdeuleExceptionByExceptionDateAsync(DateOnly ExceptionDate,CancellationToken cancellation=default)
        {
           return await context.DoctorScheduleExceptions.Where(x => x.ExceptionDate == ExceptionDate && !x.IsDeleted ).Include(x => x.Days).ToListAsync(cancellation);   
        }

        public async  Task<IEnumerable<DoctorScheduleException?>> GetSchdeulesExceptionByDoctorIdAsync(Guid DoctorId,CancellationToken cancellation =default)
        {
            return await context.DoctorScheduleExceptions.Where(x => x.DoctorId == DoctorId && !x.IsDeleted).Include(x => x.Days).ToListAsync(cancellation);
        }

        public async Task<IEnumerable<DoctorScheduleException?>> GetScheduleExceptionByStartTimeAsync(TimeOnly StartTime, CancellationToken cancellation = default)
        {
            return await context.DoctorScheduleExceptions.Where(x => x.StartTime == StartTime && !x.IsDeleted).Include(x => x.Days).ToListAsync(cancellation); 
        }
        public override async Task<DoctorScheduleException?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
             return await context.DoctorScheduleExceptions.Where(x => !x.IsDeleted).Include(x => x.Days).FirstOrDefaultAsync(cancellationToken);
        }
        public override async Task<IEnumerable<DoctorScheduleException>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.DoctorScheduleExceptions.Where(x => !x.IsDeleted).Include(x => x.Days).ToListAsync(cancellationToken);
        }
    }
}
