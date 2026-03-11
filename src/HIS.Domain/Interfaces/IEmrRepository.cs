using HIS.Domain.Common;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Interfaces
{
   public interface IEmrRepository:IBaseRepository<emr_icd110>
    {
        public Task<IEnumerable<emr_icd110>> GetEmrByAustCodeAsync(int AustCode,CancellationToken cancellationToken=default);
        public Task<IEnumerable<emr_icd110>> GetEmrByLevelAsync(int Level,CancellationToken cancellationToken=default);
        public Task<IEnumerable<emr_icd110>> GetEmrByCodeIdAsync(string? CodeId,CancellationToken cancellationToken=default);
        public Task<IEnumerable<emr_icd110>> GetEmrBySexAsync(int Sex, CancellationToken cancellationToken =default);
    }
}
